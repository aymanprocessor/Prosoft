using ProSoft.EF.DTOs.Stocks.Report.Customer_Transaction;
using ProSoft.EF.IRepositories.Stocks.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.Core.Repositories.Stocks.Reports
{
    public class CustomerTransactionReportRepo : ICustomerTransactionReportRepo
    {
        List<CustomerTransactionQuantityReportDTO> ICustomerTransactionReportRepo.GetCustomerTransactionQuantityReport(CustomerTransactionReportRequestDTO request)
        {
            throw new NotImplementedException();
        }

        List<CustomerTransactionValueReportDTO> ICustomerTransactionReportRepo.GetCustomerTransactionValueReport(CustomerTransactionReportRequestDTO request)
        {
            throw new NotImplementedException();
        }
    }
}
