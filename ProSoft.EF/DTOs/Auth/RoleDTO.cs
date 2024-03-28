using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Auth
{
    public class RoleDTO
    {
        public string? Id {  get; set; }
        [Display(Name = "RoleName")]
        [Required(ErrorMessage = "The field is required")]
        public string Name { get; set; }
    }
}
