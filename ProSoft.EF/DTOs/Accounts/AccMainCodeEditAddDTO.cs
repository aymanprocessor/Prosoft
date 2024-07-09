using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Accounts
{
    public class AccMainCodeEditAddDTO
    {
        [DisplayName("Main Account Code")]
        [Required(ErrorMessage = "The field is required")]
        public string MainCode { get; set; }

        [DisplayName("Account Name")]
        [Required(ErrorMessage = "The field is required")]
        public string MainName { get; set; }

        [DisplayName("Debit")]
        [Required(ErrorMessage = "The field is required")]
        public int? LastLevel { get; set; }

        [DisplayName("Nature of Account")]
        [Required(ErrorMessage = "The field is required")]
        public string? BalanceFlag { get; set; }

        [DisplayName("Nature of Account")]
        [Required(ErrorMessage = "The field is required")]
        public string? MainSubType { get; set; }

        [DisplayName("General Teacher")]
        [Required(ErrorMessage = "The field is required")]
        public string? OstazType { get; set; }

    }
}
