using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Shared
{
    public class EisPostingViewDTO
    {
        public int PostId { get; set; }
        public string ModuleN { get; set; }
        public string MainCode { get; set; }
        public string SubCode { get; set; }
        public string CodeDesc { get; set; }
    }
}
