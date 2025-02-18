using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.Models.Stocks
{
    [Table("STKBALANCE")]
    public partial class Stkbalance
    {
        [Key]
        public int StkbalanceId { get; set; }

        [Column("STKCOD")]
        public short? Stkcod { get; set; }

        [Column("ITEM_CODE")]
        [StringLength(30)]
        [Unicode(false)]
        [ForeignKey(nameof(SubItem))]

        public string? ItemCode { get; set; }

        [Column("QTY_START"), DefaultValue(0)]
        public decimal? QtyStart { get; set; }

        [Column("QTY_START_DT")]
        [Precision(6)]
        public DateTime? QtyStartDt { get; set; }

        [Column("QTY_CURR")]
        public decimal? QtyCurr { get; set; }

        [Column("QTY_CURR_DT")]
        [Precision(6)]
        public DateTime? QtyCurrDt { get; set; }

        [Column("ITEM_FLAG")]
        [StringLength(3)]
        [Unicode(false)]
        public string? ItemFlag { get; set; }

        [Column("ITEM_PRICE"), DefaultValue(0)]
        public decimal? ItemPrice { get; set; }

        [Column("F_YEAR")]
        public int? FYear { get; set; }

        [Column("ITEM_PRICE2"), DefaultValue(0)]
        public decimal? ItemPrice2 { get; set; }

        [Column("MAIN_CODE")]
        [StringLength(10)]
        [Unicode(false)]
        public string? MainCode { get; set; }

        [Column("SER")]
        public int? Ser { get; set; }

        [Column("FLAG1")]
        public int? Flag1 { get; set; }

        [Column("BRANCH_ID")]
        public int? BranchId { get; set; }

        [Column("DEL_ITEM")]
        public int? DelItem { get; set; }

        [Column("ITEM_ID")]
        public int? ItemId { get; set; }

        [Column("BAR_CODE")]
        [StringLength(30)]
        [Unicode(false)]
        public string? BarCode { get; set; }

        [Column("ITEM_COUNTER")]
        public long? ItemCounter { get; set; }

        [Column("REPLCATE")]
        public int? Replcate { get; set; }

        [Column("ITEM_CODE_ERROR")]
        [StringLength(20)]
        [Unicode(false)]
        public string? ItemCodeError { get; set; }

        [Column("BR_REPLC")]
        [StringLength(20)]
        [Unicode(false)]
        public string? BrReplc { get; set; }

        [Column("UNIT_CODE")]
        [ForeignKey("UnitCodee")]
        public int? UnitCode { get; set; }

        [Column("EXP_DATE")]
        public DateTime? ExpDate { get; set; }

        [Column("POST_POS")]
        public int? PostPos { get; set; }

        [Column("QTY_LAST")]
        public decimal? QtyLast { get; set; }

        [Column("COST_LAST_VAL")]
        public decimal? CostLastVal { get; set; }

        [Column("EXP_M")]
        public int? ExpM { get; set; }

        [Column("EXP_YR")]
        public int? ExpYr { get; set; }

        [Column("PLACE_STK")]
        public int? PlaceStk { get; set; }

        [Column("G_ID")]
        public int? GId { get; set; }

        [Column("S_ID")]
        public int? SId { get; set; }
        [Column("MAIN_ID")]
        [ForeignKey(nameof(MainItem))]
        public int MainId { get; set; }


        public MainItem MainItem { get; set; } 
        public SubItem SubItem { get; set; }

        public UnitCode UnitCodee { get; set; }
    }
}
