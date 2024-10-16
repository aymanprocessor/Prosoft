using ProSoft.EF.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProSoft.EF.DTOs.Stocks.Report.TransferAndReceiptTransactionReport
{
    public class TransferAndReceiptTransactionReportRequestDTO
    {
        public int BranchId { get; set; }
        [Required]
        public int UniqueType { get; set; }
        [Required]
        
        public DateTime FromDate { get; set; }
        [Required]
        public DateTime ToDate { get; set; }
        [Required]
        public int FromStock { get; set; }
        [Required]
        public int ToStock { get; set; }
        public string StockType { get; set; } = string.Empty;

        public string? SearchSubName { get; set; }
       
        public string? SearchItemMaster { get; set; }
       
        [ReqiureTo(nameof(SearchToItemMaster),ErrorMessage ="مطلوب")]
        public string? SearchFromItemMaster { get; set; }
        
        [ReqiureTo(nameof(SearchFromItemMaster), ErrorMessage = "مطلوب")]
        public string? SearchToItemMaster { get; set; }
    }
}
