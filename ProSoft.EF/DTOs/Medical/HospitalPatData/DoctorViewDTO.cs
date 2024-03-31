using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Medical.HospitalPatData
{
    public class DoctorViewDTO
    {
        [DisplayName("Code")]
        public int DrId { get; set; }
        [DisplayName("Doctor Name")]
        public string DrDesc { get; set; }

        [DisplayName("Doctor Type")]
        public int? DrType { get; set; }

        [DisplayName("Doctor Degree")]
        public string? DegreeDesc { get; set; }

        [DisplayName("Tax")]
        public int? Taxable { get; set; }

        [DisplayName("Contributor")]
        public int? Shareholder { get; set; }

        public int? DrOnOff { get; set; }
    }
}
