using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Stocks.TransferSuppliesToStocks
{
    public class TransferSuppliesToStocksMasterDTO
    {
        public DateTime? Date { get; set; }
        public string? CompanyName { get; set; } = string.Empty;
        public string? PatientName { get; set; } = string.Empty;
        public string? DoctorName { get; set; } = string.Empty;
        public int? ExitStatus { get; set; }
        public DateTime? ExitDate { get; set; }
        public decimal? InvoiceTotal { get; set; }
        public  int? InvoiceNo { get; set; }
        public bool? Post { get; set; }





    }
}
