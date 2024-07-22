using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.Models.Accounts
{
    [Table("ACC_BAL_ALL")]
    public class AccBalAll
    {
        [Key]
        [Column("BAL_ID")]
        public int? BalId { get; set; }

        [Column("CO_CODE")]
        public int? CoCode { get; set; }

        [Column("DOC_TYPE")]
        [StringLength(3)]
        [Unicode(false)]
        public string? DocType { get; set; }

        [Column("LINE_SRL")]
        public int? LineSrl { get; set; }

        [Column("LINE_TYPE")]
        [StringLength(3)]
        [Unicode(false)]
        public string? LineType { get; set; }

        [Column("MAIN_CODE")]
        [StringLength(10)]
        [Unicode(false)]
        public string? MainCode { get; set; }

        [Column("SUB_CODE")]
        [StringLength(6)]
        [Unicode(false)]
        public string? SubCode { get; set; }

        [Column("SRL_SORT")]
        public long? SrlSort { get; set; }

        [Column("NAME1")]
        [StringLength(150)]
        [Unicode(false)]
        public string? Name1 { get; set; }
    }

}
