using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Stocks.Report.StockBalance
{
    public  class StockBalanceReportRequestDTO
    {
        public int BranchId { get; set; }
        public List<string> StockIds { get; set; } = new List<string>();

        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        
        public string? SearchByItemName { get; set; }

        public string? FromCode { get; set; }
        public string? ToCode { get; set; }

        public bool NagativeQty { get; set; } = false;
        public bool ZeroQty { get; set; } = false;
        public bool PositiveQty { get; set; } = true;

        public string ReportType { get; set; } = string.Empty;

    }
}
