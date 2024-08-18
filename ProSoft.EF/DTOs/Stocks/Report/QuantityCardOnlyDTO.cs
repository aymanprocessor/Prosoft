using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Stocks.Report
{
    public class QuantityCardOnlyDTO
    {
        public DateTime? TransDate { get; set; }
        public int? TransNo { get; set; }
        public string? TransType { get; set; }
        public string? CustName { get; set; }
        public int? DesItem { get; set; }
        public int? RefNo { get; set; }
        public int? TranPrice { get; set; }
        public int? TranCount { get; set; }
        public int? RasidCount { get; set; }
        public decimal? BalanceCount { get; set; }
    }
}
