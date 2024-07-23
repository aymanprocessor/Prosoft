using ProSoft.EF.DTOs.Shared;
using ProSoft.EF.Models.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Accounts
{
    public class CancelJournalVoucherDTO
    {
        [DisplayName("Year")]
        [Required(ErrorMessage = "The field is required")]
        public int? Year { get; set; }
        public List<JournalTypeDTO>? JournalTypes { get; set; }
        public List<AccMonth>? AccMonths { get; set; }
    }
}
