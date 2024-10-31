using ProSoft.EF.DTOs.Stocks.Report.Transactions;

namespace ProSoft.EF.IRepositories.Stocks.Reports
{
    public interface ITransactionReportRepo
    {
        Task<List<TransactionReportDTO>> GetTransactionReport(DateTime FromDate, DateTime ToDate, int StockId, string? MainCode = null, int? FirstRows = null, string OrderType="ASC");
        Task<List<TransactionReportDTO>> GetZeroTransactionReport(DateTime FromDate, DateTime ToDate, int StockId, int FYear, int? FirstRows = null);
        Task<List<TotalPermitsTransactionReportDTO>> GetTotalPermitsTransactionReport(DateTime FromDate, DateTime ToDate, int BranchId, int transType);
    }
}
