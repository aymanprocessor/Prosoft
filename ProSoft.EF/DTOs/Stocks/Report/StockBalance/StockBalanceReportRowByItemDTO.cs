using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Stocks.Report.StockBalance
{
     public class StockBalanceReportRowByItemDTO
    {
        public string? ItemCode { get; set; }
        public string? ItemName { get; set; }

        public decimal? CarriedBalanceItemQty { get; set; }
        public decimal? CarriedBalanceItemPrice { get; set; }
        public decimal? CarriedBalanceItemValue { get; set; }

        public decimal? InItemQty { get; set; }
        public decimal? InItemPrice { get; set; }
        public decimal? InItemValue { get; set; }

        public decimal? OutItemQty { get; set; }
        public decimal? OutItemPrice { get; set; }
        public decimal? OutItemValue { get; set; }

        public decimal? CurrentBalanceItemQty { get; set; }
        public decimal? CurrentBalanceItemPrice { get; set; }
        public decimal? CurrentBalanceItemValue { get; set; }
    }
}
