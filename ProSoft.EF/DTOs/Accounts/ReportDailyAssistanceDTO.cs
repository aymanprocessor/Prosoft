using ProSoft.EF.DTOs.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Accounts
{
    public class ReportDailyAssistanceDTO
    {
        public List<BranchDTO>? branchs { get; set; }
        public List<JournalTypeDTO>? JournalTypes { get; set; }
    }
}
