using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Accounts
{
    public class AccMainCodeDtlDTO
    {
        public string? MainCode { get; set; }
        public string? MainName { get; set; }

        [DisplayName("Main Account Code")]
        [Required(ErrorMessage = "The field is required")]
        public string? SecCode { get; set; }
        public int? CoCode { get; set; }
        public string? ActionName { get; set; }
    }
}
