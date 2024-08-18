using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Accounts.Report
{
    public class ReviewJournalVouchersDTO
    {
        public DateTime? TransDate { get; set; }
        public int? TransNo { get; set; }
        public decimal? ValDep { get; set; }
        public decimal? ValCredit { get; set; }
        public string? MainCode { get; set; }
        public string? SubCode { get; set; }
        public string? LineDesc { get; set; }
    }
}
