using ProSoft.EF.DTOs.Stocks.Report.TransferAndReceiptTransactionReport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.IRepositories.Stocks.Reports
{
    public interface IReportTransferAndReceiptTransactionRepo
    {
        public IEnumerable<TransferAndReceiptTransactionReportDTO> GetReport(TransferAndReceiptTransactionReportRequestDTO model);
    }
}
