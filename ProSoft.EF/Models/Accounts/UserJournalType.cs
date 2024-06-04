using Microsoft.EntityFrameworkCore;
using ProSoft.EF.Models.Treasury;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.Models.Accounts
{
    [Table("USER_JOURNAL_TYPE")]
    public class UserJournalType
    {
        [Key]
        public int UserJournalId { get; set; }

        [Column("USER_CODE")]
        public int? UserCode { get; set; }

<<<<<<< HEAD
||||||| 8386149
        [Column("JOURNAL_CODE")]
        [StringLength(5)]
        [Unicode(false)]
        public string? JournalCode { get; set; }

=======
        [Column("JOURNAL_CODE")]
        [StringLength(5)]
        [Unicode(false)]
        public int? JournalCode { get; set; }

>>>>>>> abe1c2d5b9bec770d681c7ae4595029f1a34450d
        [Column("BRANCH_ID")]
        public int? BranchId { get; set; }

        [Column("JOURNAL_CODE")]
        public int? JournalCode { get; set; }

        [ForeignKey("JournalCode")]
        public JournalType? JournalType { get; set; }
    }
}
