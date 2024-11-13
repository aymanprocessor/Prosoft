using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Stocks.Report.Customer_Transaction
{
    public class CustomerTransactionReportRequestDTO
    {

        public string? CustomerId { get; set; } // Optional
        public int StockId { get; set; } 
        public int TransType { get; set; }
        public DateTime FromDate { get; set; } 
        public DateTime ToDate { get; set; }
    }
}
