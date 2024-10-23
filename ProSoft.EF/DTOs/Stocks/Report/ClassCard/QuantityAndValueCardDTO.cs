using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Stocks.Report.ClassCard
{
    public class QuantityAndValueCardDTO
    {
        public int SeqNo { get; set; }
        public string? TransDate { get; set; }
        public int? TransNo { get; set; }
        public string? TransName { get; set; }
        public decimal? InQty { get; set; }
        public decimal? InPrice { get; set; }
        public decimal? InValue { get; set; }
        public decimal? OutQty { get; set; }
        public decimal? OutPrice { get; set; }
        public decimal? OutValue { get; set; }
        public decimal? BalanceQty { get; set; }
        public decimal? BalancePrice { get; set; }
        public decimal? BalanceValue { get; set; }
    }
}
