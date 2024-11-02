using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Stocks.Report.Transactions
{
    public class SupplierPermitsTransactionReportDTO
    {
        public long? DocNo { get; set; }
        public string? DocDate { get; set; }
        public string? SupplierName { get; set; }
        public string? ItemCode { get; set; }
        public string? ItemName { get; set; }
        public string? UnitName { get; set; }
        
        public decimal? ItemQty { get; set; }
        public decimal? ItemPrice { get; set; }
        public decimal? ItemValue { get; set; }
    }
}
