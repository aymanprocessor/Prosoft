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

        [Column("JOURNAL_CODE")]
        [StringLength(5)]
        [Unicode(false)]
        public int? JournalCode { get; set; }

        [Column("BRANCH_ID")]
        public int? BranchId { get; set; }

        [ForeignKey("JournalCode")]
        public JournalType? JournalType { get; set; }
    }
}
