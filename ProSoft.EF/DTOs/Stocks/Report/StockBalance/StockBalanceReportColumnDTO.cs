using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Stocks.Report.StockBalance
{
    public class StockBalanceReportColumnDTO
    {
        // generate properties fields
        public int? BranchId { get; set; }
        public string? ItemCode { get; set; }
        public string? ItemName { get; set; }
        public string? StockName { get; set; }
        public decimal? ItemQty { get; set; } = 0;
        public decimal? ItemPrice { get; set; } = 0;
        public decimal? ItemValue { get; set; } = 0;
        public decimal? SumItemQty { get; set; } = 0;
        public decimal? SumItemValue { get; set; } = 0;

    }
}
