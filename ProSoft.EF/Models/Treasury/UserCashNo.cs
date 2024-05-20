using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.Models.Treasury
{
    [Table("USER_CASH_NO")]
    public class UserCashNo
    {
        [Key]
        [Column("USER_CASH_ID")]
        public int UserCashID { get; set; }

        [Column("USER_CODE")]
        public int? UserCode { get; set; }

        [Column("SAFE_CODE")]
        public int? SafeCode { get; set; }

        [Column("BRANCH_ID")]
        public int? BranchId { get; set; }

        [Column("ACTDEACT_FLAG")]
        public int? ActdeactFlag { get; set; }

        [Column("M_CODE")]
        public int? MCode { get; set; }

        [Column("MAIN_CODE")]
        [StringLength(10)]
        [Unicode(false)]
        public string? MainCode { get; set; }

        [Column("SUB_CODE")]
        [StringLength(6)]
        [Unicode(false)]
        public string? SubCode { get; set; }

        [Column("MAIN_CODE2")]
        [StringLength(10)]
        [Unicode(false)]
        public string? MainCode2 { get; set; }

        [Column("SUB_CODE2")]
        [StringLength(6)]
        [Unicode(false)]
        public string? SubCode2 { get; set; }

        [Column("CSH_USR")]
        [StringLength(1)]
        [Unicode(false)]
        public string? CshUsr { get; set; }
    }
}
