using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Stocks.Report.Customer_Transaction
{
    public class CustomerTransactionQuantityReportDTO
    {
        public string? CustomerName { get; set; } = string.Empty;
        public string? TransDate { get; set; } = string.Empty;
        public decimal? TransNo { get; set; } = 0;
        public string? ItemCode { get; set; } = string.Empty;
        public string? ItemName { get; set; } = string.Empty;
        private decimal _quantity =0;

        public decimal Quantity
        {
            get => Math.Floor(_quantity);
            set => _quantity = value;
        }
    }
}
