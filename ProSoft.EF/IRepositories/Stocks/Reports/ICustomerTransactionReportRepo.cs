using ProSoft.EF.DTOs.Stocks.Report.Customer_Transaction;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.IRepositories.Stocks.Reports
{
    public interface ICustomerTransactionReportRepo
    {
        List<CustomerTransactionValueReportDTO> GetCustomerTransactionValueReport(CustomerTransactionReportRequestDTO request, Filter filter );
        List<CustomerTransactionQuantityReportDTO> GetCustomerTransactionQuantityReport(CustomerTransactionReportRequestDTO request, Filter filter );

    }
}
