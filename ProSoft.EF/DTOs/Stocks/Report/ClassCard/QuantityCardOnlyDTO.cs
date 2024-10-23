using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Stocks.Report.ClassCard
{
    public class QuantityCardOnlyDTO
    {
        public DateTime? TransDate { get; set; }
        public int? TransNo { get; set; }
        public string? TransType { get; set; }
        public string? CustName { get; set; }
        public int? DesItem { get; set; }
        public int? RefNo { get; set; }
        public decimal? TranPrice { get; set; }
        public decimal? TranCount { get; set; }
        public decimal? RasidCount { get; set; }
        public decimal? BalanceCount { get; set; }
    }
}
