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
        public decimal? ItemQty { get; set; }
        public decimal? ItemPrice { get; set; }
        public decimal? ItemValue { get; set; }
        public decimal? SumItemQty { get; set; }
        public decimal? SumItemValue { get; set; }






    }
}
