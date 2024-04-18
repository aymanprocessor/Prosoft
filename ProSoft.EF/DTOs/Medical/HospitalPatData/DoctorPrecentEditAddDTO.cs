using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Medical.HospitalPatData
{
    public class DoctorPrecentEditAddDTO
    {
        public int DrPercent { get; set; }
        public int DrCode { get; set; }
        public int MainCode { get; set; }

        [DisplayName("SubClinic Name")]
        [Required(ErrorMessage = "The field is required")]
        public int SubCode { get; set; }

        [DisplayName("ServClinc Name")]
        [Required(ErrorMessage = "The field is required")]
        public int SubDetailCodeL1 { get; set; }

        [DisplayName("Value OR Ratio")]
        public int? DrValFlg { get; set; }

        [DisplayName("Service Value")]
        [Required(ErrorMessage = "The field is required")]
        public decimal ValueService { get; set; }

        [DisplayName("Dr Value")]
        [Required(ErrorMessage = "The field is required")]
        public decimal DrVal { get; set; }

        [DisplayName("Dr Ratio")]
        [Required(ErrorMessage = "The field is required")]
        public int DrPerc { get; set; }

        [DisplayName("Dr Value")]
        [Required(ErrorMessage = "The field is required")]
        public decimal DrValContract { get; set; }

        [DisplayName("Dr Ratio")]
        public decimal? DrPercContract { get; set; }
        //list
        public List<DoctorViewDTO>? Doctors { get; set; }
        public List<SubClinicViewDTO>? subClinics { get; set; }
        public List<ServiceClinicViewDTO>? services { get; set; }
    }
}
