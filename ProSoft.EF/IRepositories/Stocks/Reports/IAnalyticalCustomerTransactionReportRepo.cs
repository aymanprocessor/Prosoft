using ProSoft.EF.DTOs.Stocks.Report.Analytical_Customer_Transaction_Report;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.IRepositories.Stocks.Reports
{
    public interface IAnalyticalCustomerTransactionReportRepo
    {
        Task<List<AnalyticalCustomerTransactionReportDTO>> GetAnalyticalCustomerTransactionReport(DateTime FromDate, DateTime ToDate, Filter filter);
    }
}
