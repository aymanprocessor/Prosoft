using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.Models.Stocks
{
    [Table("SUB_ITEM_DTL")]
    public class SubItemDtl
    {
        [Key]
        [Column("SubItemDtlId")]
        public int SubItemDtlId { get; set; }

        [Column("ITEM_ID")]
        public int? ItemId { get; set; }
        [Column("ITEM_NAME")]
        [StringLength(60)]
        [Unicode(false)]
        public string? ItemName { get; set; }

        [Column("ITEM_CODE")]
        [StringLength(30)]
        [Unicode(false)]
        public string? ItemCode { get; set; }

        [Column("MAIN_CODE")]
        [StringLength(10)]
        [Unicode(false)]
        public string? MainCode { get; set; }

        [Column("FLAG1")]
        public int? Flag1 { get; set; }

        [Column("BRANCH_ID")]
        public int? BranchId { get; set; }

        [Column("UNIT_CODE")]
        public int? UnitCode { get; set; }

        [Column("SALE_PRICE", TypeName = "decimal(7, 2)")]
        public decimal? SalePrice { get; set; }

        [Column("DISCOUT_S1", TypeName = "decimal(5, 2)")]
        public decimal? DiscoutS1 { get; set; }

        [Column("DISC_VAL_S1", TypeName = "decimal(7, 2)")]
        public decimal? DiscValS1 { get; set; }

        [Column("PURCH_PRICE", TypeName = "decimal(7, 2)")]
        public decimal? PurchPrice { get; set; }

        [Column("DISCOUT_P1", TypeName = "decimal(5, 2)")]
        public decimal? DiscoutP1 { get; set; }

        [Column("DISC_VAL_P1", TypeName = "decimal(7, 2)")]
        public decimal? DiscValP1 { get; set; }

        [Column("PIECE_COUNT")]
        public int? PieceCount { get; set; }

        [Column("PURCH_AVERAGE", TypeName = "decimal(7, 2)")]
        public decimal? PurchAverage { get; set; }

        [Column("PROTECT_COLUMUN")]
        public int? ProtectColumun { get; set; }

        [Column("ROW_ON_OFF")]
        public int? RowOnOff { get; set; }

        [Column("USER_MODIFY_CODE")]
        public int? UserModifyCode { get; set; }

        [Column("USER_MODIFY_DATE", TypeName = "datetime")]
        public DateTime? UserModifyDate { get; set; }

        [Column("SMALL_U_FLAG")]
        public int? SmallUFlag { get; set; }
    }
}
