using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace ProSoft.EF.DTOs.Medical.HospitalPatData
{
    public class CheckupClinicDTO
    {
        [DisplayName("Code")]
        [Required(ErrorMessage = "The field is required")]
        public int CheckupId { get; set; }
        [DisplayName("Examination Name")]
        [Required(ErrorMessage = "The field is required")]
        public string CheckupDesc { get; set; } = null!;

        [DisplayName("Note")]
        [Required(ErrorMessage = "The field is required")]
        public string? CheckupDesc2 { get; set; }

       
        public int BranchId { get; set; }

       
        public int Flag1 { get; set; }
    }
}
