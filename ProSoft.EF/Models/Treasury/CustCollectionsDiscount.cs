using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.Models.Treasury
{
    [Table("CUST_COLLECTIONS_DISCOUNT")]
    public class CustCollectionsDiscount
    {
        [Key]
        [Column("DISCOUNT_CODE")]
        public int? DiscountCode { get; set; }

        [Column("RECEIPT_NO")]
        public int? ReceiptNo { get; set; }

        [Column("RECEIPT_DATE")]
        [Precision(6)]
        public DateTime? ReceiptDate { get; set; }

        [Column("DISC_PRC", TypeName = "decimal(4, 2)")]
        public decimal? DiscPrc { get; set; }

        [Column("DISC_VALUE", TypeName = "decimal(15, 2)")]
        public decimal? DiscValue { get; set; }

        [Column("DOC_TYPE")]
        [StringLength(5)]
        [Unicode(false)]
        public string? DocType { get; set; }

        [Column("F_YEAR")]
        public int? FYear { get; set; }

        [Column("BRANCH_ID")]
        public int? BranchId { get; set; }

        [Column("MAIN_CODE")]
        [StringLength(10)]
        [Unicode(false)]
        public string? MainCode { get; set; }

        [Column("SUB_CODE")]
        [StringLength(6)]
        [Unicode(false)]
        public string? SubCode { get; set; }

        [Column("SAFE_CODE")]
        public int? SafeCode { get; set; }
    }
}
