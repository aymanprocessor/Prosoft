using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Treasury
{
    public class BankDataDTO
    {
        [DisplayName("Code")]
        [Required(ErrorMessage = "The field is required")]
        public int BankNo { get; set; }

        [DisplayName("Bank Name")]
        [Required(ErrorMessage = "The field is required")]
        public string? BankDesc { get; set; }
    }
}
