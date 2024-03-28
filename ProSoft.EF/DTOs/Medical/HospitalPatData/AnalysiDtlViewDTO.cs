using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Medical.HospitalPatData
{
    public class AnalysiDtlViewDTO
    {
        public int Id { get; set; }
        public int Itemanalcode { get; set; }
        public string Itemanalname { get; set; }
        public string Itemrate { get; set; }

    }
}
