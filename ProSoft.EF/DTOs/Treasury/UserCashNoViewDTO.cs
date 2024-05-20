using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Treasury
{
    public class UserCashNoViewDTO
    {
        public int? UserCashID { get; set; }
        public string? SafeNames { get; set; }
        public int? ActdeactFlag { get; set; }
        public string? CshUsr { get; set; }
        public string? SafeSub { get; set; }
        public string? SafeMain { get; set; }
    }
}
