using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.Models.Accounts
{
    [PrimaryKey("MainCode", "SubCodeFr", "SubCodeTo")]

    [Table("ACC_SUB_CODE_EDIT")]
    public class AccSubCodeEdit
    {
        [Key]
        [Column("MAIN_CODE")]
        [StringLength(10)]
        [Unicode(false)]
        public string? MainCode { get; set; }

        [Key]
        [Column("SUB_CODE_FR")]
        [StringLength(6)]
        [Unicode(false)]
        public string? SubCodeFr { get; set; }

        [Key]
        [Column("SUB_CODE_TO")]
        [StringLength(6)]
        [Unicode(false)]
        public string? SubCodeTo { get; set; }
    }
}
