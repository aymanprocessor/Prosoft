using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Medical.HospitalPatData
{
    public class DegreeRequestDTO
    {
        public int Id { get; set; }

        public int Branch { get; set; }

        public string DegreeName { get; set; } = string.Empty;

        public int DegreeType { get; set; }

        public int DegreeOnOff { get; set; }
    }
}
