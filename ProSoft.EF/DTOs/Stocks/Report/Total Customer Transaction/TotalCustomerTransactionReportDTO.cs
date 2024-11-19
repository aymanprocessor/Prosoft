using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Stocks.Report.Total_Customer_Transaction
{
    public class TotalCustomerTransactionReportDTO
    {
        public string CustomerName { get; set; } = string.Empty;
        public int InvoiceCount { get; set; } = 0;
        public decimal NetSales { get; set; } = 0;
        public decimal PaymentAmount { get; set; } = 0;
        public decimal DueAmount { get; set; } = 0;
    }
}
