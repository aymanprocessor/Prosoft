using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ProSoft.EF.DbContext;
using ProSoft.EF.DTOs.Stocks;
using ProSoft.EF.DTOs.Stocks.Report;
using ProSoft.EF.IRepositories.Stocks;
using ProSoft.EF.Models.Stocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
       
        public async Task<QuantityCardOnlyDTO> GetQuantityCardOnly(int id, int subItem, DateTime? fromPeriod, DateTime? toPeriod, int branch)
        {
            QuantityCardOnlyDTO quantityCardOnlyDTO = new QuantityCardOnlyDTO();
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
            // Define the parameters for the stored procedure
            var branchID = new SqlParameter("@br", branch);
            var stockID = new SqlParameter("@stk", id);
            var fromPeriodParam = new SqlParameter("@fr_date", fromDate);
            var subItemID = new SqlParameter("@fr_itm", subItem);
            // Call the stored procedure and get the result
            decimal result = await _Context.Database.ExecuteSqlRawAsync(
                "EXEC [YourStoredProcedureName] @br, @stk, @fr_date, @fr_itm",
                branchID, stockID, fromPeriodParam, subItemID);
            decimal ll_Count = Math.Round(result, 2);
            var ld_Rasid_c = ll_Count;
            


            return quantityCardOnlyDTO;
        }
    }
}
