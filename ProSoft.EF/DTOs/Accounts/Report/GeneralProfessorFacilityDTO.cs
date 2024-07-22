using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Accounts.Report
{
    public class GeneralProfessorFacilityDTO
    {
        public string? SubCode { get; set; }
        public string? AccName { get; set; }
        public decimal? ValDep { get; set; }
        public decimal? ValCredit { get; set; }
        public decimal? TransValDep { get; set; }
        public decimal? TransValCredit { get; set; }
        public decimal? LcGapValDep { get; set; }
        public decimal? LcGapValCredit { get; set; }
    }
}
