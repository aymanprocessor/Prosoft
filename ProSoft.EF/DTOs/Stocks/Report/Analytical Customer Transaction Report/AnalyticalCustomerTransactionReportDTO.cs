using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Stocks.Report.Analytical_Customer_Transaction_Report
{
    public class AnalyticalCustomerTransactionReportDTO
    {
     
        public string? CustomerName { get; set; } = string.Empty;
        public decimal? InvoiceNo { get; set; } = 0;
        public DateTime? InvoiceDate { get; set; }
        public DateTime? DueDate { get; set; }

        private decimal _salesValue = 0;
        public decimal SalesValue
        {
            get => _salesValue;
            set => _salesValue = Math.Round(value, 2);
        }

        private decimal _cashAmount = 0;
        public decimal CashAmount
        {
            get => _cashAmount;
            set => _cashAmount = Math.Round(value, 2);
        }

        private decimal _dueValue = 0;
        public decimal DueValue
        {
            get => _dueValue;
            set => _dueValue = Math.Round(value, 2);
        }


    }
}
