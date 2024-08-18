using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Accounts
{
    public class AccSubCodeEditDTO
    {
        public string? MainCode { get; set; }

        [DisplayName("From")]
        [Required(ErrorMessage = "The field is required")]
        [StringLength(6)]
        public string? SubCodeFr { get; set; }

        [DisplayName("To")]
        [Required(ErrorMessage = "The field is required")]
        [StringLength(6)]
        public string? SubCodeTo { get; set; }
        public string? SubName { get; set; }
    }
}
