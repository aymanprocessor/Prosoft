using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Stocks.Report.Customer_Transaction
{
    public class CustomerTransactionValueReportDTO
    {
        public string? CustomerName { get; set; } = string.Empty;
        public string? TransDate { get; set; } = string.Empty;
        public decimal? TransNo { get; set; } = 0;
        public string? ItemCode { get; set; } = string.Empty;
        public string? ItemName { get; set; } = string.Empty;

        private decimal _quantity = 0;
        public decimal Quantity
        {
            get => Math.Floor(_quantity);
            set => _quantity = value;
        }

        private decimal _price = 0;
        public decimal Price
        {
            get => Math.Round(_price, 2);
            set => _price = value;
        }

        private decimal _value = 0;
        public decimal Value
        {
            get => Math.Round(_value, 2);
            set => _value = value;
        }
    }
}
