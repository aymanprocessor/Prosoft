using System;
using System.Collections.Generic;
<<<<<<< HEAD
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
=======
>>>>>>> 9a290d6aeb3fa25da2e55fee92952c7cc6fc0d67
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Accounts
{
    public class JournalTypeDTO
    {
<<<<<<< HEAD
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
=======
        public int JournalCode { get; set; }

        public string JournalName { get; set; }

        public string? CounterFlag { get; set; }

>>>>>>> 9a290d6aeb3fa25da2e55fee92952c7cc6fc0d67
        public string? JournalType1 { get; set; }

        public string? JournalInOut { get; set; }
    }
}
