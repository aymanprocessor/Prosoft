using AutoMapper;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ProSoft.EF.DbContext;
using ProSoft.EF.DTOs.Stocks;
using ProSoft.EF.DTOs.Stocks.Report.ClassCard;
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
                // رصيد اول مدة
                if (transDtl.DocDate.Value.Date <= beforeFromDateOneDay.Date && transDtl.DocDate.Value.Date >= new DateTime(DateTime.Now.Year, 1, 1)) 
                {
                    if (generalCode != null && generalCode.AddSub == 1) // وارد
                    {


                        stkBalOpenQty += transDtl.UnitQty;
                        stlBalTotalPrice += transDtl.ItemVal;
                        avgPrice = Math.Round((stlBalTotalPrice / stkBalOpenQty).Value, 2);
                    }
                    else if (generalCode != null && generalCode.AddSub == 2) // منصرف
                    {
                        stkBalOpenQty -= transDtl.UnitQty;
                        stlBalTotalPrice -= transDtl.ItemVal;
                        avgPrice = Math.Round((stlBalTotalPrice / stkBalOpenQty).Value, 2);
                    }

                }
                // حركات
                else if (transDtl.DocDate.Value.Date > beforeFromDateOneDay.Date)
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
                    else if (generalCode != null && generalCode.AddSub == 2) // منصرف
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
        public async Task<List<QuantityAndValueCardDTO>> GetQuantityAndValueCard(int id, string itemCode, DateTime? fromPeriod, DateTime? toPeriod, int branch)
        {
            List<QuantityAndValueCardDTO> quantityAndValueCardDTOs = new List<QuantityAndValueCardDTO>();
            // UnitCode unitCode = await _Context.UnitCodes.FirstOrDefaultAsync(obj=>obj.Code == 0 && obj.Flag1 ==1 );
            QuantityAndValueCardDTO quantityAndValueCardDTO = new QuantityAndValueCardDTO();

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


            var stock = await _Context.Stocks.FirstOrDefaultAsync(s => s.Stkcod == id);
            var item = await _Context.SubItems.FirstOrDefaultAsync(s => s.ItemCode == itemCode);
            var transDtlList = await _Context.TransDtls.Where(t => t.ItemMaster == itemCode).ToListAsync();
            var stkbalance = await _Context.Stkbalances.Where(st => st.FYear >= fromPeriod.Value.Year && st.Flag1 == stock.Flag1).ToListAsync();
            var stkBalOpenQty = stkbalance.Sum(s => s.QtyStart);
            var stlBalTotalPrice = stkbalance.Sum(s => s.ItemPrice2);
            var avgPrice = Math.Round((stlBalTotalPrice / stkBalOpenQty).Value, 2);

            // رصيد اول المدة
            var transDtlBetweenStartDateAndBeforeFromDateList = transDtlList.Where(t =>  t.DocDate.Value.Date >= new DateTime(DateTime.Now.Year,1,1) && t.DocDate.Value.Date <= fromPeriod.Value.Date).ToList();

            if(transDtlBetweenStartDateAndBeforeFromDateList.Count > 0)
            {
                foreach (var tranDtl in transDtlBetweenStartDateAndBeforeFromDateList)
                {

                    stkBalOpenQty += tranDtl.UnitQty;
                    stlBalTotalPrice += tranDtl.ItemVal;
                    avgPrice = Math.Round((stlBalTotalPrice / stkBalOpenQty).Value, 2);
                }


            }
            quantityAndValueCardDTO.TransName = "رصيد اول المدة";
            quantityAndValueCardDTO.TransDate = fromPeriod.Value.AddDays(-1).ToString("yyyy-MM-dd");

            quantityAndValueCardDTO.BalanceQty = stkBalOpenQty;
            quantityAndValueCardDTO.BalancePrice = avgPrice;
            quantityAndValueCardDTO.BalanceValue = stlBalTotalPrice;

            quantityAndValueCardDTOs.Add(quantityAndValueCardDTO);


            // الحركات
            var transDtlAfterFromDateList = transDtlList.Where(t =>  t.DocDate.Value.Date > fromPeriod.Value.Date && t.DocDate.Value.Date<= toPeriod.Value.Date).ToList();

            foreach (var transDtl in transDtlAfterFromDateList)
            {
                quantityAndValueCardDTO = new();
                var generalCode = await _Context.GeneralCodes.FirstOrDefaultAsync(g => g.UniqueType == transDtl.TransType);
               
            
                    if (generalCode != null && generalCode.AddSub == 1) // وارد
                    {


                        stkBalOpenQty = stkBalOpenQty + transDtl.UnitQty;
                        stlBalTotalPrice = stlBalTotalPrice + transDtl.ItemVal;
                        avgPrice = Math.Round((stlBalTotalPrice / stkBalOpenQty).Value, 2);
                        Console.WriteLine($"Balance Qty : {stkBalOpenQty}");
                        Console.WriteLine($"Balance Total : {stlBalTotalPrice}");
                        Console.WriteLine($"Balance AVG Price : {avgPrice}");

                        quantityAndValueCardDTO.InQty = transDtl.UnitQty;
                        quantityAndValueCardDTO.InValue = transDtl.ItemVal;
                        quantityAndValueCardDTO.InPrice = transDtl.Price ;
                    }
                    else if (generalCode != null && generalCode.AddSub == 2) // منصرف
                    {

                        stkBalOpenQty = stkBalOpenQty - transDtl.UnitQty;
                        stlBalTotalPrice = stlBalTotalPrice - transDtl.ItemVal;
                        avgPrice = Math.Round((stlBalTotalPrice / stkBalOpenQty).Value, 2);
                        Console.WriteLine($"Balance Qty : {stkBalOpenQty}");
                        Console.WriteLine($"Balance Total : {stlBalTotalPrice}");
                        Console.WriteLine($"Balance AVG Price : {avgPrice}");

                        quantityAndValueCardDTO.OutQty = transDtl.UnitQty;
                        quantityAndValueCardDTO.OutValue = transDtl.ItemVal;
                        quantityAndValueCardDTO.OutPrice = transDtl.Price;
                    }

                    quantityAndValueCardDTO.TransName = generalCode.GDesc;
                    quantityAndValueCardDTO.TransNo = (int)transDtl.SerSys;
                    quantityAndValueCardDTO.TransDate = transDtl.DocDate.Value.ToString("yyyy-MM-dd");

                quantityAndValueCardDTO.BalanceQty = stkBalOpenQty;
                quantityAndValueCardDTO.BalancePrice = avgPrice;
                quantityAndValueCardDTO.BalanceValue = stlBalTotalPrice;

                quantityAndValueCardDTOs.Add(quantityAndValueCardDTO);

            }


            quantityAndValueCardDTOs = quantityAndValueCardDTOs.OrderBy(q => DateTime.Parse( q.TransDate)).ToList();
            return quantityAndValueCardDTOs;
        }
        public async Task<List<AtTransactionLevelCardDTO>> GetAtLevelTransactionCard(int id, string itemCode,int unitCode, DateTime? fromPeriod, DateTime? toPeriod, int branch)
        {
            var stock = await _Context.Stocks.FirstOrDefaultAsync(s => s.Stkcod == id);
            var item = await _Context.SubItems.FirstOrDefaultAsync(s => s.ItemCode == itemCode);
            var transDtlList = await _Context.TransDtls.Where(t => t.ItemMaster == itemCode&&t.UnitCode== unitCode && t.DocDate >= fromPeriod && t.DocDate <= toPeriod).ToListAsync();
            List< AtTransactionLevelCardDTO > atTransactionLevelCardDTOs = new();
            foreach (var transDtl in transDtlList)
            {
                AtTransactionLevelCardDTO AtTransactionLevelCardDTO = new();
                AtTransactionLevelCardDTO.TransDate = transDtl.DocDate.Value.ToString("yyyy-MM-dd");
                AtTransactionLevelCardDTO.TransNo = (int)transDtl.DocNo;
                AtTransactionLevelCardDTO.RefNo = transDtl.RefDocNo;
                AtTransactionLevelCardDTO.itemType = 1;


                switch (transDtl.TransType)
                {
                    case 2:
                        AtTransactionLevelCardDTO.ItemIn = transDtl.UnitQty;
                        break;
                    case 4:
                        AtTransactionLevelCardDTO.ItemOut = transDtl.UnitQty;
                        break;
                    case 12:
                        AtTransactionLevelCardDTO.ItemOut = transDtl.UnitQty;
                        break;
                    case 16:
                        AtTransactionLevelCardDTO.ReItemPlus = transDtl.UnitQty;
                        break;
                    case 24:
                        AtTransactionLevelCardDTO.LevelingOut = transDtl.UnitQty;
                        break;
                    case 17:
                        AtTransactionLevelCardDTO.ReItemMinus = transDtl.UnitQty;
                        break;
                   
                }
                atTransactionLevelCardDTOs.Add(AtTransactionLevelCardDTO);

            }
            return atTransactionLevelCardDTOs;
        }
        // ------------------------------- Ayman Saad ------------------------------- //

    }
}
