using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Accounts
{
    public class AccTransDetailViewDTO
    {
        public int? TransDtlId { get; set; }
        public string? AccountName { get; set; }
        public decimal? ValDep { get; set; }
        public decimal? ValCredit { get; set; }
        public string? DocNo { get; set; }
        public DateTime? DocDate { get; set; }
        public string? LineDesc { get; set; }

    }
}
