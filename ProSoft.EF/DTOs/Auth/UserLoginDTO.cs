using Microsoft.AspNetCore.Mvc;
using System;
using System.Web;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using RemoteAttribute = Microsoft.AspNetCore.Mvc.RemoteAttribute;

namespace ProSoft.EF.DTOs.Auth
{
    public class UserLoginDTO
    {
        [Display(Name = "User ID")]
        [Required(ErrorMessage = "The field is required")]
        public int UserCode { get; set; }

        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "The field is required")]
        [Remote(action: "VerifyUser", controller: "Account", AdditionalFields = "UserCode", ErrorMessage = "Password is not correct")]
        public string PassWord { get; set; }

        [Display(Name = "Remember Me")]
        public bool rememberMe { get; set; }
    }
}
