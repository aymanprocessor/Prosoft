using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Accounts.Report
{
    public class GeneralProfessorFacilityDTO
    {
        public string MainCode { get; set; }
        public string SubCode { get; set; }
        public string SubName { get; set; }
        public decimal? FDepCur { get; set; }
        public decimal? FCreditOr { get; set; }
        public decimal? ValDep { get; set; }
        public decimal? ValCredit { get; set; }
        public string MainName { get; set; }
    }
}
