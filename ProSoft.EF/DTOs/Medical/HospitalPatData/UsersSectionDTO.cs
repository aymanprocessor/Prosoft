using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Medical.HospitalPatData
{
    public class UsersSectionDTO
    {
        public int USecId { get; set; }
        public int? UserCode { get; set; }

        [DisplayName("The Sections")]
        [Required(ErrorMessage = "The field is required")]
        public int? ClinicId { get; set; }
        public string? ClinicDes { get; set; }

        [DisplayName("Default")]
        [Required(ErrorMessage = "The field is required")]
        public int? DefaultId { get; set; }
        public int? BranchId { get; set; }

        //List
        public List<MainClinicViewDTO>? MainClinics { get; set; }

    }
}
