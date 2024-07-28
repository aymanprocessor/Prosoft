using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Accounts.Report
{
    public class DetailsDailyAssistanceDTO
    {
        public string? MainName { get; set; }
        public string? SubName { get; set; }
        public decimal? ValDep { get; set; }
        public decimal? ValCredit { get; set; }
        public decimal? ValDepCur { get; set; }
        public decimal? ValCreditCur { get; set; }
        public int? DocStatus { get; set; }
        public string? DocNo { get; set; }
        public DateTime? DocDate { get; set; }
        public string? LineDesc { get; set; }

    }
}
