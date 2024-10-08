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
        public int TransType { get; set; }
        [Required]
        
        public DateTime FromDate { get; set; }
        [Required]
        public DateTime ToDate { get; set; }
        [Required]
        public int FromStock { get; set; }
        [Required]
        public int ToStock { get; set; }
    }
}
