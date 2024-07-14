using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace ProSoft.EF.DTOs.Accounts
{
    public class AccSubCodeDTO
    {
        [DisplayName("Sub Account Code")]
        [Required(ErrorMessage = "The field is required")]
        public string SubCode { get; set; }
        public string MainCode { get; set; }

        [DisplayName("Account Name")]
        [Required(ErrorMessage = "The field is required")]
        public string SubName { get; set; }
        public string? ActionName { get; set; }
    }
}
