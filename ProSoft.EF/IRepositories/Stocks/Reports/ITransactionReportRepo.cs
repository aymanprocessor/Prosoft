using ProSoft.EF.DTOs.Stocks.Report.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.IRepositories.Stocks.Reports
{
    public interface ITransactionReportRepo
    {
        Task<List<TransactionReportDTO>> GetTransactionReport(DateTime FromDate, DateTime ToDate, int StockId, string? MainCode = null, int? FirstRows = null, string OrderType="ASC");
    }
}
