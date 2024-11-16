using ProSoft.EF.DTOs.Stocks.Report.Total_Customer_Transaction;
using ProSoft.EF.IRepositories.Stocks.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.Core.Repositories.Stocks.Reports
{
    public class TotalCustomerTransactionReportRepo : ITotalCustomerTransactionReportRepo
    {
        public async Task<IEnumerable<TotalCustomerTransactionReportDTO>> GetTotalCustomerTransactionQuantityReport(TotalCustomerTransactionReportRequestDTO request)
        {
            // Add data-fetching logic here
            return new List<TotalCustomerTransactionReportDTO>();
        }
    }
}
