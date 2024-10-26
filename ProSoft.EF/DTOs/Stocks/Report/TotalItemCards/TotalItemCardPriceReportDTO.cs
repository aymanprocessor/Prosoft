using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Stocks.Report.TotalItemCards
{
    public class TotalItemCardPriceReportDTO
    {
        public string ItemCode { get; set; } = string.Empty;
        public string ItemName { get; set; } = string.Empty;

        // ---- Balance Before ---- //
        public decimal TotalBalanceBeforeQty { get; set; }
        public decimal TotalBalanceBeforeValue { get; set; }
        public decimal TotalBalanceBeforeAvgPrice { get; set; }
        // ---- Total Of In ---- //
        public decimal TotalInQty { get; set; }
        public decimal TotalInValue { get; set; }
        public decimal TotalInAvgPrice { get; set; }
        // ---- Total Of Out ---- //
        public decimal TotalOutQty { get; set; }
        public decimal TotalOutValue { get; set; }
        public decimal TotalOutAvgPrice { get; set; }
        // ---- Current Balance ---- //
        public decimal TotalCurrentBalanceQty { get; set; }
        public decimal TotalCurrentBalanceValue { get; set; }
        public decimal TotalCurrentBalanceAvgPrice { get; set; }


    }
}
