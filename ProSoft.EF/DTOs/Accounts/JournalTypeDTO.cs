using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Accounts
{
    public class JournalTypeDTO
    {
        [DisplayName("Code")]
        [Required(ErrorMessage = "The field is required")]
        public int JournalCode { get; set; }

        [DisplayName("Journal Name")]
        [Required(ErrorMessage = "The field is required")]
        public string JournalName { get; set; }

        [DisplayName("Serial Type")]
        [Required(ErrorMessage = "The field is required")]
        public string? CounterFlag { get; set; }

        [DisplayName("Journal Type")]
        [Required(ErrorMessage = "The field is required")]
        public string? JournalType1 { get; set; }
        public string? JournalInOut { get; set; }
    }
}
