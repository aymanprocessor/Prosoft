using ProSoft.EF.DTOs.Stocks.Report.StockBalance;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.IRepositories.Stocks.Reports
{
    public interface IStockBalanceReportRepo
    {
        Task<List<StockBalanceReportColumnDTO>> GetStockBalanceReportColumns(int BranchId, List<string> StockIds, DateTime FromDate, DateTime ToDate,int FYear, Filter? filter = null);
        Task<List<StockBalanceReportRowByStockDTO>> GetStockBalanceReportRowByStocks(int BranchId, List<string> StockIds, DateTime FromDate, DateTime ToDate, int FYear, Filter? filter = null);
        Task<List<StockBalanceReportRowByItemDTO>> GetStockBalanceReportRowByItems(int BranchId, List<string> StockIds, DateTime FromDate, DateTime ToDate, int FYear, Filter? filter = null);

    }
}
