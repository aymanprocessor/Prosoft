using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Medical.HospitalPatData
{
    public class DocSubDtlEditAddDTO
    {
        [Required(ErrorMessage = "The field is required")]
        public int DocSubId { get; set; }

        [Required(ErrorMessage = "The field is required")]
        public int DrId { get; set; }
        public string? DrDesc { get; set; }

        [Required(ErrorMessage = "The field is required")]
        public int ClinicId { get; set; }

        [Required(ErrorMessage = "The field is required")]
        public int SClinicId { get; set; }

        [Required(ErrorMessage = "The field is required")]
        public int DrOnOff { get; set; }

        [Required(ErrorMessage = "The field is required")]
        public int DocSubDef { get; set; }
        ////////////
        //Lists
        public List<MainClinicViewDTO>? MainClinics { get; set; }
        public List<SubClinicViewDTO>? SubClinics { get; set; }

    }
}
