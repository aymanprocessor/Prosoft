using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Stocks.Report.TransferAndReceiptTransactionReport
{
    public class TransferAndReceiptTransactionReportDTO
    {
        public int SeqCode { get; set; }
        public int DocNo { get; set; }
        public string? DocDate { get; set; }
        public string? ItemMaster { get; set; } = string.Empty;
        public string? SubName { get; set; } = string.Empty;
        public string? UnitName { get; set; } = string.Empty;
        public int? UnitQty { get; set; } 
        public decimal? Price { get; set; } 
        public decimal? ItemVal { get; set; } 
        public decimal? SalePrice { get; set; } 
    }
}
