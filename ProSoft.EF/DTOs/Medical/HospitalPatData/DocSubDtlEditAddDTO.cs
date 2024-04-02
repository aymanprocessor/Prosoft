using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Medical.HospitalPatData
{
    public class DocSubDtlEditAddDTO
    {
        public int DocSubId { get; set; }
        public int DrId { get; set; }
        public string? DrDesc { get; set; }
        public int ClinicId { get; set; }
        public int SClinicId { get; set; }
        public int DrOnOff { get; set; }
        public int DocSubDef { get; set; }
        ////////////
        //Lists
        public List<MainClinicViewDTO>? MainClinics { get; set; }
        public List<SubClinicViewDTO>? SubClinics { get; set; }

    }
}
