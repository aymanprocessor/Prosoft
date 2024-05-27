using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.Models.Treasury
{
    [Table("BANK_DATA")]
    public class BankData
    {
        [Key]
        [Column("BANK_NO")]
        public int BankNo { get; set; }

        [Column("BANK_DESC")]
        public string? BankDesc { get; set; }
    }
}
