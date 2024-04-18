using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Medical.HospitalPatData
{
    public class DoctorEditAddDTO
    {
        [DisplayName("Code")]
        [Required(ErrorMessage = "The field is required")]
        public int DrId { get; set; }

        [DisplayName("Doctor Name")]
        [Required(ErrorMessage = "The field is required")]
        public string DrDesc { get; set; }

        [DisplayName("Doctor Type")]
        [Required(ErrorMessage = "The field is required")]
        public int DrType { get; set; }

        [DisplayName("Doctor Degree")]
        [Required(ErrorMessage = "The field is required")]
        public int DrDegree { get; set; }

        [DisplayName("Tax")]
        [Required(ErrorMessage = "The field is required")]
        public int Taxable { get; set; }

        [DisplayName("Contributor")]
        [Required(ErrorMessage = "The field is required")]
        public int Shareholder { get; set; }

        [Required(ErrorMessage = "The field is required")]
        public int DrOnOff { get; set; }

        //list
        public List<DrDegreeDTO>? drDegrees { get; set; }

    }

}
