using ProSoft.EF.DTOs.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Auth
{
    public class UserEditDTO
    {
        public int UserCode { get; set; }
        [Display(Name = "User Name")]
        [Required(ErrorMessage = "The field is required")]
        public string UserName { get; set; }

        [Display(Name = "Mobile number")]
        [Required(ErrorMessage = "The field is required")]
        [MinLength(11)]
        [RegularExpression(@"^(011|010|015|012)\d{8}$", ErrorMessage = "Please enter a valid phone number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Branch")]
        [Required(ErrorMessage = "The field is required")]
        public int BranchId { get; set; }

        [Required(ErrorMessage = "Please, choose at least one role")]
        public List<RoleDTO> Roles { get; set; }
        public List<BranchDTO>? Branches { get; set; }
    }
}
