using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Accounts.Report
{
    public class AmericanDailyDTO
    {
        public string? MainName { get; set; }
        public decimal? ValDep { get; set; }
        public decimal? ValCredit { get; set; }
        public int? TransNo { get; set; }
        public DateTime? TransDate { get; set; }
    }
}
