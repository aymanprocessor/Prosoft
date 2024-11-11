using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Stocks.Report.Request_Limit_Items_Report
{
    public  class RequestLimitItemsReportRequestDTO
    {
        public int BranchId { get; set; }
        public int StockId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        public string? FromCode { get; set; }
        public string? ToCode { get; set; }

        public string? SearchByItemName { get; set; }
    }
}
