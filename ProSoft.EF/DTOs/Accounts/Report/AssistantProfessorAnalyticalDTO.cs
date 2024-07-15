using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Accounts.Report
{
    public class AssistantProfessorAnalyticalDTO
    {
        public decimal? Balance { get; set; }
        public decimal? ValDep { get; set; }
        public decimal? ValCredit { get; set; }
        public string? TransDesc { get; set; }
        public string? CostDesc { get; set; }
        public int? TransNo { get; set; }
        public DateTime? TransDate { get; set; }
    }
}
