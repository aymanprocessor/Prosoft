using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Accounts
{
    public class UserJournalTypeDTO
    {
        public int UserJournalId { get; set; }
        public int? UserCode { get; set; }

        [DisplayName("Journal Type")]
        [Required(ErrorMessage = "The field is required")]
        public int? JournalCode { get; set; }
        public string? JournalName { get; set; }
        public int? BranchId { get; set; }

        //Lists
        public List<JournalTypeDTO>? JournalTypes { get; set; }

    }
}
