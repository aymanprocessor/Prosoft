using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Medical.Analysis
{
    public class SubEditAddDTO
    {
        public string SubCode { get; set; }
        public string MainCode { get; set; }
        public string SubName { get; set; }
        public double MedicalFlag { get; set; }
    }
}
