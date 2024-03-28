using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Auth
{
    public class UserRegisterDTO
    {
        public int UserCode { get; set; }

        [Required(ErrorMessage = "The field is required")]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Display(Name = "Mobile number")]
        [Required(ErrorMessage = "The field is required")]
        [MinLength(11)]
        [RegularExpression(@"^(011|010|015|012)\d{8}$", ErrorMessage = "Please enter a valid phone number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "The field is required")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{7,}$", ErrorMessage = "password must contain (capitals + smalls + numbers + symbols) characters.")]
        public string PassWord { get; set; }

        [Display(Name = "Confirm Password")]
        [Required(ErrorMessage = "The field is required")]
        [DataType(DataType.Password)]
        [Compare("PassWord", ErrorMessage = "Please enter the same password correctly")]
        public string PassConfirm { get; set; }

        [Display(Name = "Role Name")]
        [Required(ErrorMessage = "The field is required")]
        public string Name { get; set; }
    }
}
