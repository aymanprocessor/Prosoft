using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Shared
{
    public class SectionEditAddDTO
    {
        [DisplayName("Section Code")]
        public int SecCd { get; set; }

        [DisplayName("Section Name")]
        [Required(ErrorMessage = "The field is required")]
        public string SecDesc { get; set; }

        [DisplayName("Branch")]
        [Required(ErrorMessage = "The field is required")]
        public int BranchId { get; set; }

        [DisplayName("Is Active")]
        [Required(ErrorMessage = "The field is required")]
        public int OnOff { get; set; }

        [DisplayName("Pricing Type")]
        [Required(ErrorMessage = "The field is required")]
        public int SecCost { get; set; }

        public List<BranchDTO>? Branches { get; set; }
    }
}
