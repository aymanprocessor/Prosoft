using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Treasury.Report
{
    public class IncomingChecksDataDTO
    {
        public int? DocNo { get; set; }
        public DateTime? DocDate { get; set; }
        public string? ChekNo { get; set; }
        public DateTime? SattlDate { get; set; }
        public int? CurrencyCode { get; set; }
        public decimal? CheckValue { get; set; }
        public string? CheckStatus { get; set; }
        public DateTime? ChekDate { get; set; }
        public string? PersonName { get; set; }
        public string? AccName { get; set; }
    }
}
