using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Medical.HospitalPatData
{
    public class DocSubDtlViewDTO
    {
        public int DocSubId { get; set; }
        public int DrId { get; set; }
        public string ClinicDesc { get; set; }
        public string SClinicDesc { get; set; }
        public int DrOnOff { get; set; }
        public int DocSubDef { get; set; }

        public int? ClinicId { get; set; }
        public int? SClinicId { get; set; }


    }
}
