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
    [PrimaryKey("CoCode", "SecCode", "MainCode")]
    [Table("ACC_MAIN_CODE_DTL")]
    public class AccMainCodeDtl
    {
        [Key]
        [Column("MAIN_CODE")]
        [StringLength(9)]
        [Unicode(false)]
        public string? MainCode { get; set; }

        [Key]
        [Column("SEC_CODE")]
        [StringLength(9)]
        [Unicode(false)]
        public string? SecCode { get; set; }

        [Key]
        [Column("CO_CODE")]
        public int? CoCode { get; set; }

        [Column("MAIN_NAME")]
        [StringLength(100)]
        [Unicode(false)]
        public string? MainName { get; set; }
    }
}
