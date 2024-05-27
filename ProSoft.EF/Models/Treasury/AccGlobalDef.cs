using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProSoft.EF.Models.Treasury;

namespace ProSoft.EF.Models.Shared
{
    [Table("ACC_GLOBAL_DEF")]
    public class AccGlobalDef
    {
        [Key]
        [Column("CODE_NO")]
        public int CodeNo { get; set; }

        [Column("CO_CODE")]
        public int? CoCode { get; set; }

        [Column("TABLE_CODE")]
        [StringLength(3)]
        [Unicode(false)]
        public string? TableCode { get; set; }


        [Column("CODE_DESC")]
        [StringLength(40)]
        [Unicode(false)]
        public string? CodeDesc { get; set; }

        [Column("CUR_RATE", TypeName = "decimal(9, 6)")]
        public decimal? CurRate { get; set; }

        [Column("CODE_KEY")]
        [StringLength(5)]
        [Unicode(false)]
        public string? CodeKey { get; set; }

        [Column("MAIN_CODE")]
        [StringLength(10)]
        [Unicode(false)]
        public string? MainCode { get; set; }

        [Column("SUB_CODE")]
        [StringLength(6)]
        [Unicode(false)]
        public string? SubCode { get; set; }

        [Column("JOURNAL_CODE")]
        [StringLength(5)]
        [Unicode(false)]
        public string? JournalCode { get; set; }

        [Column("JOURNAL_CODE2")]
        [StringLength(5)]
        [Unicode(false)]
        public string? JournalCode2 { get; set; }

        [Column("SYMBOL")]
        [StringLength(10)]
        [Unicode(false)]
        public string? Symbol { get; set; }
        public ICollection<AccSafeCash>? AccSafeCashes { get; set; }
        public ICollection<AccSafeCheck>? AccSafeChecks { get; set; }


    }

}
