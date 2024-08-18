using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Accounts.Report
{
    public class AmericanTotalJournalDTO
    {
        public string? MainCode { get; set; }
        public string? MainName { get; set; }
        public decimal? ValDep { get; set; }
        public decimal? ValCredit { get; set; }
    }
}
