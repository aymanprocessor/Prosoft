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
    [Table("ACC_START_BAL")]
    public class AccStartBal
    {
        [Key]
        [Column("START_BAL_ID")]
        public int StartBalId { get; set; }

        [Column("CO_CODE")]
        public int? CoCode { get; set; }

        [Column("F_YEAR")]
        public int? FYear { get; set; }

        [Column("MAIN_CODE")]
        [StringLength(10)]
        [Unicode(false)]
        public string? MainCode { get; set; }

        [Column("SUB_CODE")]
        [StringLength(6)]
        [Unicode(false)]
        public string? SubCode { get; set; }

        [Column("F_DEP_OR", TypeName = "decimal(12, 2)")]
        public decimal? FDepOr { get; set; }

        [Column("F_CREDIT_OR", TypeName = "decimal(12, 2)")]
        public decimal? FCreditOr { get; set; }

        [Column("F_DEP_CUR", TypeName = "decimal(12, 2)")]
        public decimal? FDepCur { get; set; }

        [Column("F_CREDIT_CUR", TypeName = "decimal(12, 2)")]
        public decimal? FCreditCur { get; set; }

        [Column("CUR_CODE")]
        [StringLength(5)]
        [Unicode(false)]
        public string? CurCode { get; set; }

        [Column("CUR_RATE", TypeName = "decimal(6, 5)")]
        public decimal? CurRate { get; set; }

        [Column("ACC_NAME")]
        [StringLength(150)]
        [Unicode(false)]
        public string? AccName { get; set; }

        [Column("COMMENTT")]
        [StringLength(60)]
        [Unicode(false)]
        public string? Commentt { get; set; }

        [Column("BR_REPLC")]
        [StringLength(20)]
        [Unicode(false)]
        public string? BrReplc { get; set; }

        [Column("TRANS_TYPE")]
        [StringLength(3)]
        [Unicode(false)]
        public string? TransType { get; set; }

        [Column("COST_CENTER_CODE")]
        [StringLength(6)]
        [Unicode(false)]
        public string? CostCenterCode { get; set; }
    }
}
