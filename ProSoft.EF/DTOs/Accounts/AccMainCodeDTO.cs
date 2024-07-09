using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Accounts
{
    public class AccMainCodeDTO
    {
        public string MainCode { get; set; }
        public string MainName { get; set; }
        public int? LastLevel { get; set; }
        public string? BalanceFlag { get; set; }
        public string? MainSubType { get; set; }
        public string? OstazType { get; set; }

    }
}
