using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProSoft.EF.Models.Treasury;

namespace ProSoft.EF.Models.Accounts
{

    [Table("ACC_TRANS_MASTER")]
    public class AccTransMaster
    {
        [Key]
        [Column("TRANS_ID")]
        public int TransId { get; set; }

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

        [Column("TRANS_DESC")]
        [StringLength(250)]
        [Unicode(false)]
        public string? TransDesc { get; set; }

        [Column("TOTAL_TRANS")]
        public decimal? TotalTrans { get; set; }

        [Column("OK_POST")]
        [StringLength(1)]
        [Unicode(false)]
        public string? OkPost { get; set; }

        [Column("CUR_CODE")]
        [StringLength(5)]
        [Unicode(false)]
        public string? CurCode { get; set; }

        [Column("APR_DATE")]
        [Precision(6)]
        public DateTime? AprDate { get; set; }

        [Column("CUR_RATE")]
        public decimal? CurRate { get; set; }

        [Column("YEAR_TRANS_NO")]
        [StringLength(10)]
        [Unicode(false)]
        public string? YearTransNo { get; set; }

        [Column("F_MONTH")]
        public int? FMonth { get; set; }

        [Column("MASTER_ID")]
        public int? MasterId { get; set; }

        [Column("M_CODE")]
        public int? MCode { get; set; }

        [Column("DOC_STATUS")]
        public int? DocStatus { get; set; }

        [Column("BR_REPLC")]
        [StringLength(20)]
        [Unicode(false)]
        public string? BrReplc { get; set; }

        [Column("ENTRY_DATE")]
        public DateTime? EntryDate { get; set; }

        [Column("M_CODE_DTL")]
        public int? MCodeDtl { get; set; }

        [Column("DOC_NO")]
        [StringLength(10)]
        [Unicode(false)]
        public string? DocNo { get; set; }

        [Column("DOC_DATE")]
        [Precision(6)]
        public DateTime? DocDate { get; set; }

        [Column("POST_RECIPT")]
        public int? PostRecipt { get; set; }

        [Column("USER_CODE_MODIFY")]
        public int? UserCodeModify { get; set; }

        [Column("USER_DATE_MODIFY")]
        public DateTime? UserDateModify { get; set; }
        public ICollection<AccTransDetail>? AccTransDetails { get; set; }

    }
}
