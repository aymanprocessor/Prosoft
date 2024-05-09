using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Treasury
{
    public class TreasuryNameViewDTO
    {
        public int SafeCode { get; set; }
        public string? SafeNames { get; set; }
        public int? Flag1 { get; set; }
        public int? PostAccount { get; set; }
        public string? JournalName { get; set; }

    }
}
