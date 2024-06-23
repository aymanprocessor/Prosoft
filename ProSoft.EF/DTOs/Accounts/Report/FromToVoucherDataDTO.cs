using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Accounts.Report
{
    public class FromToVoucherDataDTO
    {
        public int? TransNo { get; set; }
        public DateTime? TransDate { get; set; }
        public string? AccountName { get; set; }
        public string? LineDesc { get; set; }
        public decimal? ValDep { get; set; }
        public decimal? ValCredit { get; set; }


    }
}
