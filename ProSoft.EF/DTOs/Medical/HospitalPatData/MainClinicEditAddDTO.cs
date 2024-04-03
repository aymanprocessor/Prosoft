using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Medical.HospitalPatData
{
    public class MainClinicEditAddDTO
    {
        [DisplayName("Code")]
        public int ClinicId { get; set; }

        [DisplayName("Clinic Name")]
        public string ClinicDesc { get; set; }

        [DisplayName("Activation")]
        public int MOnOff { get; set; }

        [DisplayName("System Section")]
        public int SecCode { get; set; }

        //list
        public List<EisSectionTypeDTO>? eisSectionTypes { get; set; }
    }
}
