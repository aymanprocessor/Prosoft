using AutoMapper;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ProSoft.EF.DbContext;
using ProSoft.EF.DTOs.Stocks;
using ProSoft.EF.DTOs.Stocks.Report;
using ProSoft.EF.IRepositories.Stocks;
using ProSoft.EF.Migrations;
using ProSoft.EF.Models;
using ProSoft.EF.Models.Shared;
using ProSoft.EF.Models.Stocks;
using ProSoft.EF.Models.Stocks.StoredProcedure;
using System.Data;

namespace ProSoft.Core.Repositories.Stocks
{
    public class ReportQuantityClassCardRepo : IReportQuantityClassCardRepo
    {
        private readonly AppDbContext _Context;
        private readonly IMapper _mapper;
        public ReportQuantityClassCardRepo(AppDbContext Context, IMapper mapper)
        {
            _Context = Context;
            _mapper = mapper;
        }
        public async Task<ReportQuantityClassCardDTO> GetAllDataAsync(int userCode)
        {
            ReportQuantityClassCardDTO reportQuantityClassCardDTO = new ReportQuantityClassCardDTO();
            List<StockViewDTO> stockViewDTOs = await GetActiveStocksForUserAsync(userCode);
            List<SubItem> subItems = await _Context.SubItems.ToListAsync();
            reportQuantityClassCardDTO.stocks = _mapper.Map<List<StockViewDTO>>(stockViewDTOs);
            reportQuantityClassCardDTO.subItems = subItems.Select(obj => new SubItemViewDTO
            {
                SubId = obj.SubId,
                SubName = obj.SubName,
                ItemCode = obj.ItemCode,
                CodeAndName = $"{obj.SubName} \\ {obj.ItemCode}",
            }).ToList(); return reportQuantityClassCardDTO;
        }
        private async Task<List<StockViewDTO>> GetActiveStocksForUserAsync(int userCode)
        {
            List<StockEmp> stockTransList = await _Context.StockEmps
                .Where(obj => obj.UserId == userCode && obj.StockDef == 1).ToListAsync();
            List<Stock> stocksList = await _Context.Stocks.ToListAsync();

            var stocksDTO = new List<StockViewDTO>();
            var isExisted = false;
            foreach (var stock in stocksList)
            {
                foreach (var stockTrans in stockTransList)
                {
                    if (stock.Stkcod == stockTrans.Stkcod)
                    {
                        isExisted = true;
                        break;
                    }
                    else
                        isExisted = false;
                }
                if (isExisted)
                {
                    var stockDTO = new StockViewDTO();
                    stockDTO.Stkcod = stock.Stkcod;
                    stockDTO.Stknam = (await _Context.Stocks
                        .FindAsync(stock.Stkcod)).Stknam;
                    stocksDTO.Add(stockDTO);
                }
            }
            return stocksDTO;
        }

        public async Task<decimal?> GetItemBalance(int branchId, int stockId, DateTime beforeFromDateOneDay, string itemCode)
        {

            var stock = await _Context.Stocks.FirstOrDefaultAsync(s => s.Stkcod == stockId);
            var item = await _Context.SubItems.FirstOrDefaultAsync(s => s.ItemCode == itemCode);
            var transDtlList = await _Context.TransDtls.Where(t => t.ItemMaster == itemCode).ToListAsync();
            var stkbalance = await _Context.Stkbalances.Where(st => st.FYear >= beforeFromDateOneDay.Year && st.Flag1 == stock.Flag1).ToListAsync();
            var stkBalOpenQty = stkbalance.Sum(s => s.QtyStart);
            var stlBalTotalPrice = stkbalance.Sum(s => s.ItemPrice2);
            var avgPrice = Math.Round((stlBalTotalPrice / stkBalOpenQty).Value, 2);


            decimal? qty =0 ;
            decimal? price = 0;
            decimal? total = 0;
            foreach (var transDtl in transDtlList)
            {
                var generalCode = await _Context.GeneralCodes.FirstOrDefaultAsync(g => g.UniqueType == transDtl.TransType);

                if (transDtl.DocDate.Value.Date <= beforeFromDateOneDay.Date && transDtl.DocDate.Value.Date >= new DateTime(DateTime.Now.Year, 1, 1))
                {
                    if (generalCode != null && generalCode.AddSub == 1) // وارد
                    {


                        stkBalOpenQty += transDtl.UnitQty;
                        stlBalTotalPrice += transDtl.ItemVal;
                        avgPrice = Math.Round((stlBalTotalPrice / stkBalOpenQty).Value, 2);
                    }
                    else if (generalCode != null && generalCode.AddSub == 2)
                    {
                        stkBalOpenQty -= transDtl.UnitQty;
                        stlBalTotalPrice -= transDtl.ItemVal;
                        avgPrice = Math.Round((stlBalTotalPrice / stkBalOpenQty).Value, 2);
                    }
                }

                if (transDtl.DocDate.Value.Date > beforeFromDateOneDay.Date)
                {
                    if (generalCode != null && generalCode.AddSub == 1) // وارد
                    {


                        stkBalOpenQty = stkBalOpenQty + transDtl.UnitQty;
                        stlBalTotalPrice = stlBalTotalPrice + transDtl.ItemVal;
                        avgPrice = Math.Round((stlBalTotalPrice / stkBalOpenQty).Value, 2);
                        Console.WriteLine($"Balance Qty : {stkBalOpenQty}");
                        Console.WriteLine($"Balance Total : {stlBalTotalPrice}");
                        Console.WriteLine($"Balance AVG Price : {avgPrice}");
                    }
                    else if (generalCode != null && generalCode.AddSub == 2)
                    {

                        stkBalOpenQty = stkBalOpenQty- transDtl.UnitQty;
                        stlBalTotalPrice = stlBalTotalPrice - transDtl.ItemVal;
                        avgPrice = Math.Round((stlBalTotalPrice / stkBalOpenQty).Value, 2);
                        Console.WriteLine($"Balance Qty : {stkBalOpenQty}");
                        Console.WriteLine($"Balance Total : {stlBalTotalPrice}");
                        Console.WriteLine($"Balance AVG Price : {avgPrice}");
                    }

                }
            }
            try
            {

            }
            catch (Exception ex)
            {
            }

            return stkBalOpenQty;
        }


        public async Task<List<QuantityCardOnlyDTO>> GetQuantityCardOnly(int id, string subItem, DateTime? fromPeriod, DateTime? toPeriod, int branch)
        {
            List<QuantityCardOnlyDTO> quantityCardOnlyDTOs = new List<QuantityCardOnlyDTO>();
            // UnitCode unitCode = await _Context.UnitCodes.FirstOrDefaultAsync(obj=>obj.Code == 0 && obj.Flag1 ==1 );

            DateTime fromDate;

            if (fromPeriod.Value.Day == 1 && fromPeriod.Value.Month == 1)
            {
                // fromPeriod is January 1st, so set fromDate to fromPeriod
                fromDate = fromPeriod.Value.AddDays(-1);
            }
            else
            {
                // fromPeriod is not January 1st, so subtract one day
                fromDate = fromPeriod.Value;
            }

            var brParam = new SqlParameter("@br", branch);
            var stkParam = new SqlParameter("@stk", id);
            var frDateParam = new SqlParameter("@fr_date", fromDate);
            var frItmParam = new SqlParameter("@fr_itm", subItem);

            // Define the output parameter
            var outputParam = new SqlParameter("@itm_balance", SqlDbType.Decimal)
            {
                Direction = ParameterDirection.Output
            };

            var bbalance = await GetItemBalance(branch, id, fromDate, subItem);
            // Execute the stored procedure
            await _Context.Database.ExecuteSqlRawAsync(
                "EXEC @itm_balance = [dbo].[item_balance2] @br, @stk, @fr_date, @fr_itm",
                outputParam, brParam, stkParam, frDateParam, frItmParam
            );

            // Return the output value
            var balance = (decimal)outputParam.Value;
            var ld_Rasid_c = balance;
            CompanyProfile companyProfile = await _Context.CompanyProfiles.FirstOrDefaultAsync(obj => obj.CoCode == 1);

            List<TransDtl> transDtls = await _Context.TransDtls.Where(obj => obj.StockCode == id && obj.ItemMaster == subItem.ToString() && (obj.DocDate >= fromPeriod && obj.DocDate <= toPeriod)).ToListAsync();
            foreach (var item in transDtls)
            {
                QuantityCardOnlyDTO quantityCardOnlyDTO = new QuantityCardOnlyDTO();
                GeneralCode generalCode = await _Context.GeneralCodes.FirstOrDefaultAsync(obj => (obj.GType == "4" || obj.GType == "4") && obj.UniqueType == item.TransType);
                TransMaster transMaster = await _Context.TransMasters.FirstOrDefaultAsync(obj => obj.TransMAsterID == item.TransMAsterID);

                if (item.UnitQty > 0 && generalCode.AddSub > 0)
                {
                    decimal ld_Count_Rasid = 0;
                    var ls_Cust_SubName = "";
                    if (generalCode.AddSub == 1)
                    {
                        ld_Count_Rasid = (decimal)ld_Rasid_c + (decimal)item.UnitQty;
                        var res = await _Context.SupCodes.FirstOrDefaultAsync(obj => obj.SupCode1 == transMaster.SupNo && obj.BranchId == branch);
                        if (res != null) ls_Cust_SubName = res.SupName;
                    }
                    else if (generalCode.AddSub == 2)
                    {
                        ld_Count_Rasid = (decimal)ld_Rasid_c - (decimal)item.UnitQty;
                        var res = await _Context.CustCodes.FirstOrDefaultAsync(obj => obj.CustCode1 == transMaster.CustNo && obj.BranchId == branch);
                        if (res != null) ls_Cust_SubName = res.CustName;
                    }
                    quantityCardOnlyDTO.TranPrice = item.Price;
                    quantityCardOnlyDTO.TransDate = item.DocDate;
                    quantityCardOnlyDTO.RefNo = transMaster.SupInvNo != null ? int.Parse(transMaster.SupInvNo) : null;
                    quantityCardOnlyDTO.TransNo = (int)item.SerSys;
                    quantityCardOnlyDTO.TransType = generalCode.GDesc;
                    quantityCardOnlyDTO.TranCount = item.UnitQty;
                    quantityCardOnlyDTO.RasidCount = ld_Count_Rasid;
                    quantityCardOnlyDTO.DesItem = item.Flag1;
                    quantityCardOnlyDTO.CustName = ls_Cust_SubName;
                    quantityCardOnlyDTO.BalanceCount = ld_Rasid_c;
                    quantityCardOnlyDTOs.Add(quantityCardOnlyDTO);
                }
            }
            return quantityCardOnlyDTOs;
        }
        // ------------------------------- Ayman Saad ------------------------------- //
        public async Task<List<QuantityAndValueCardDTO>> GetQuantityAndValueCard(int id, string subItem, DateTime? fromPeriod, DateTime? toPeriod, int branch)
        {
            List<QuantityAndValueCardDTO> quantityAndValueCardDTOs = new List<QuantityAndValueCardDTO>();
            // UnitCode unitCode = await _Context.UnitCodes.FirstOrDefaultAsync(obj=>obj.Code == 0 && obj.Flag1 ==1 );

            DateTime fromDate;

            if (fromPeriod.Value.Day == 1 && fromPeriod.Value.Month == 1)
            {
                // fromPeriod is January 1st, so set fromDate to fromPeriod
                fromDate = fromPeriod.Value;
            }
            else
            {
                // fromPeriod is not January 1st, so subtract one day
                fromDate = fromPeriod.Value.AddDays(-1);
            }

            var brParam = new SqlParameter("@br", branch);
            var stkParam = new SqlParameter("@stk", id);
            var frDateParam = new SqlParameter("@fr_date", fromDate);
            var frItmParam = new SqlParameter("@fr_itm", subItem);

            // Define the output parameter
            var outputParam = new SqlParameter("@itm_balance", SqlDbType.Decimal)
            {
                Direction = ParameterDirection.Output
            };

            // Execute the stored procedure
            await _Context.Database.ExecuteSqlRawAsync(
                "EXEC @itm_balance = [dbo].[item_balance2] @br, @stk, @fr_date, @fr_itm",
                outputParam, brParam, stkParam, frDateParam, frItmParam
            );

            // Return the output value
            var balance = (decimal)outputParam.Value;
            var BalanceQty = balance;
            decimal TotalValue = 0;
            decimal TotalQty = 0;
            CompanyProfile companyProfile = await _Context.CompanyProfiles.FirstOrDefaultAsync(obj => obj.CoCode == 1);

            List<TransDtl> transDtls = await _Context.TransDtls.Where(obj => obj.StockCode == id && obj.ItemMaster == subItem.ToString() && (obj.DocDate >= fromPeriod && obj.DocDate <= toPeriod)).ToListAsync();
            foreach (var item in transDtls)
            {
                QuantityAndValueCardDTO quantityAndValueCardDTO = new QuantityAndValueCardDTO();
                GeneralCode generalCode = await _Context.GeneralCodes.FirstOrDefaultAsync(obj => (obj.GType == "4" || obj.GType == "4") && obj.UniqueType == item.TransType);
                TransMaster transMaster = await _Context.TransMasters.FirstOrDefaultAsync(obj => obj.TransMAsterID == item.TransMAsterID);

                if (item.UnitQty > 0 && generalCode.AddSub > 0)
                {

                    var ls_Cust_SubName = "";
                    if (generalCode.AddSub == 1)
                    {
                        BalanceQty = (decimal)BalanceQty + (decimal)item.UnitQty;
                        var res = await _Context.SupCodes.FirstOrDefaultAsync(obj => obj.SupCode1 == transMaster.SupNo && obj.BranchId == branch);
                        if (res != null) ls_Cust_SubName = res.SupName;

                        quantityAndValueCardDTO.InPrice = item.Price;
                        quantityAndValueCardDTO.InQty = item.UnitQty;
                        quantityAndValueCardDTO.InValue = item.UnitQty * item.Price;


                    }
                    else if (generalCode.AddSub == 2)
                    {
                        BalanceQty = (decimal)BalanceQty - (decimal)item.UnitQty;
                        var res = await _Context.CustCodes.FirstOrDefaultAsync(obj => obj.CustCode1 == transMaster.CustNo && obj.BranchId == branch);
                        if (res != null) ls_Cust_SubName = res.CustName;

                        quantityAndValueCardDTO.OutPrice = item.Price;
                        quantityAndValueCardDTO.OutQty = item.UnitQty;
                        quantityAndValueCardDTO.OutValue = item.UnitQty * item.Price;



                    }
                    //quantityAndValueCardDTO.TranPrice = item.Price;
                    //quantityAndValueCardDTO.TransDate = item.DocDate;
                    //quantityAndValueCardDTO.RefNo = transMaster.SupInvNo != null ? int.Parse(transMaster.SupInvNo) : null;
                    //quantityAndValueCardDTO.TransNo = (int)item.SerSys;
                    //quantityAndValueCardDTO.TransType = generalCode.GDesc;
                    //quantityAndValueCardDTO.TranCount = item.UnitQty;
                    //quantityAndValueCardDTO.RasidCount = ld_Count_Rasid;
                    //quantityAndValueCardDTO.DesItem = item.Flag1;
                    //quantityAndValueCardDTO.CustName = ls_Cust_SubName;
                    //quantityAndValueCardDTO.BalanceCount = ld_Rasid_c;
                    //quantityAndValueCardDTOs.Add(quantityAndValueCardDTO);
                }
            }
            return quantityAndValueCardDTOs;
        }
    }
}
