using AutoMapper;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ProSoft.EF.DbContext;
using ProSoft.EF.DTOs.Stocks;
using ProSoft.EF.DTOs.Stocks.Report;
using ProSoft.EF.IRepositories.Stocks;
using ProSoft.EF.Migrations;
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

        public async Task<List<QuantityCardOnlyDTO>> GetQuantityCardOnly(int id, string subItem, DateTime? fromPeriod, DateTime? toPeriod, int branch)
        {
            List<QuantityCardOnlyDTO> quantityCardOnlyDTOs = new List<QuantityCardOnlyDTO>();
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
            //// Define the parameters for the stored procedure
            //var branchID = new SqlParameter("@br", branch);
            //var stockID = new SqlParameter("@stk", id);
            //var fromPeriodParam = new SqlParameter("@fr_date", fromDate);
            //var subItemID = new SqlParameter("@fr_itm", subItem);
            //// Call the stored procedure and get the result
            //decimal result = await _Context.Database.(
            //    "EXEC [item_balance2] @br, @stk, @fr_date, @fr_itm",
            //    branchID, stockID, fromPeriodParam, subItemID);
            //decimal ll_Count = Math.Round(result, 2);
            // Create parameters for the stored procedure
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
            var balance =  (decimal)outputParam.Value;
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
                     var ls_Cust_SubName ="";
                    if (generalCode.AddSub == 1)
                    {
                        ld_Count_Rasid = (decimal)ld_Rasid_c + (decimal)item.UnitQty;
                        var res = await _Context.SupCodes.FirstOrDefaultAsync(obj =>  obj.SupCode1 == transMaster.SupNo && obj.BranchId == branch);
                        if (res != null) ls_Cust_SubName = res.SupName;
                    }
                    else if (generalCode.AddSub == 2)
                    {
                        ld_Count_Rasid = (decimal)ld_Rasid_c - (decimal)item.UnitQty;
                        ls_Cust_SubName = (await _Context.CustCodes.FirstOrDefaultAsync(obj => obj.CustCode1 == transMaster.CustNo && obj.BranchId == branch)).CustName;
                    }
                    quantityCardOnlyDTO.TranPrice = item.Price;
                    quantityCardOnlyDTO.TransDate = item.DocDate;
                    quantityCardOnlyDTO.RefNo = transMaster.SupInvNo !=null? int.Parse(transMaster.SupInvNo):null;
                    quantityCardOnlyDTO.TransNo = (int)item.SerSys;
                    quantityCardOnlyDTO.TransType = item.TransType.ToString();
                    quantityCardOnlyDTO.TranCount = item.UnitQty;
                    quantityCardOnlyDTO.RasidCount = 400;//ld_Count_Rasid;
                    quantityCardOnlyDTO.DesItem = item.Flag1;
                    quantityCardOnlyDTO.CustName = ls_Cust_SubName;
                    quantityCardOnlyDTOs.Add(quantityCardOnlyDTO);
                }
            }
            return quantityCardOnlyDTOs;
        }
    }
}
