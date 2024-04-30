using ProSoft.EF.DTOs.Calculus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Shared
{
    public class EisPostingEditAddDTO
    {
        public int PostId { get; set; }
        public string ModuleN { get; set; }
        public string MainCode { get; set; }
        public string SubCode { get; set; }
        public int BranchId { get; set; }

        public List<AccMainCodeDTO>? MainCodes { get; set; }
        public List<AccSubCodeDTO>? SubCodes { get; set; }
    }
}
