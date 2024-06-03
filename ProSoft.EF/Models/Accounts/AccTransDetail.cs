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
    [Table("ACC_TRANS_DETAIL")]
    public class AccTransDetail
    {
        [Key]
        [Column("TRANS_DTL_ID")]
        public int TransDtlId { get; set; }

        [Column("CO_CODE")]
        public int? CoCode { get; set; }

        [Column("F_YEAR")]
        public int? FYear { get; set; }

        [Column("TRANS_TYPE")]
        [StringLength(5)]
        [Unicode(false)]
        public string? TransType { get; set; }

        [Column("TRANS_NO")]
        public int? TransNo { get; set; }

        [Column("TRANS_DATE")]
        [Precision(6)]
        public DateTime? TransDate { get; set; }

        [Column("TRANS_SERIAL")]
        public long? TransSerial { get; set; }

        [Column("MAIN_CODE")]
        [StringLength(10)]
        [Unicode(false)]
        public string? MainCode { get; set; }

        [Column("SUB_CODE")]
        [StringLength(6)]
        [Unicode(false)]
        public string? SubCode { get; set; }

        [Column("VAL_DEP", TypeName = "decimal(12, 2)")]
        public decimal? ValDep { get; set; }

        [Column("VAL_CREDIT", TypeName = "decimal(12, 2)")]
        public decimal? ValCredit { get; set; }

        [Column("VAL_DEP_CUR", TypeName = "decimal(12, 2)")]
        public decimal? ValDepCur { get; set; }

        [Column("VAL_CREDIT_CUR", TypeName = "decimal(12, 2)")]
        public decimal? ValCreditCur { get; set; }

        [Column("DOC_NO")]
        [StringLength(10)]
        [Unicode(false)]
        public string? DocNo { get; set; }

        [Column("DOC_STATUS")]
        public int? DocStatus { get; set; }

        [Column("DOC_DATE")]
        [Precision(6)]
        public DateTime? DocDate { get; set; }

        [Column("COST_CENTER_CODE")]
        [StringLength(6)]
        [Unicode(false)]
        public string? CostCenterCode { get; set; }

        [Column("ACC_NAME")]
        [StringLength(100)]
        [Unicode(false)]
        public string? AccName { get; set; }

        [Column("LINE_DESC")]
        [StringLength(200)]
        [Unicode(false)]
        public string? LineDesc { get; set; }

        [Column("OK_POST")]
        [StringLength(1)]
        [Unicode(false)]
        public string? OkPost { get; set; }

        [Column("CUR_CODE")]
        [StringLength(5)]
        [Unicode(false)]
        public string? CurCode { get; set; }

        [Column("DOC_CODE")]
        [StringLength(10)]
        [Unicode(false)]
        public string? DocCode { get; set; }

        [Column("YEAR_TRANS_NO")]
        [StringLength(10)]
        [Unicode(false)]
        public string? YearTransNo { get; set; }

        [Column("F_MONTH")]
        public int? FMonth { get; set; }

        [Column("USER_CODE")]
        public int? UserCode { get; set; }

        [Column("ENTRY_TYPE")]
        public int? EntryType { get; set; }

        [Column("BR_REPLC")]
        [StringLength(20)]
        [Unicode(false)]
        public string? BrReplc { get; set; }

        [Column("ENTRY_DATE", TypeName = "datetime")]
        public DateTime? EntryDate { get; set; }

        [Column("M_CODE_DTL")]
        public int? MCodeDtl { get; set; }

        [Column("POST_RECIPT")]
        public int? PostRecipt { get; set; }

        [Column("USER_CODE_MODIFY")]
        public int? UserCodeModify { get; set; }

        [Column("USER_DATE_MODIFY", TypeName = "datetime")]
        public DateTime? UserDateModify { get; set; }

    }
}
