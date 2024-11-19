using ProSoft.EF.DTOs.Stocks.Report.Total_Customer_Transaction;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.IRepositories.Stocks.Reports
{
    public interface ITotalCustomerTransactionReportRepo
    {
        Task<IEnumerable<TotalCustomerTransactionReportDTO>> GetTotalCustomerTransactionReport(TotalCustomerTransactionReportRequestDTO request,Filter filter);
    }
}
