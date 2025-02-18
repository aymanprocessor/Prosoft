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
    [Table("TRANS_DTL")]
    public partial class TransDtl
    {
        [Key]
        public int TransDtlId { get; set; }
        public int TransMAsterID { get; set; }

        [Column("STOCK_CODE")]
        public short? StockCode { get; set; }

        [Column("TRANS_TYPE")]
        public int? TransType { get; set; }

        [Column("DOC_NO")]
        public long? DocNo { get; set; }

        [Column("DOC_DATE")]
        [Precision(6)]
        public DateTime? DocDate { get; set; }

        [Column("SERIAL")]
        public long? Serial { get; set; }

        [Column("MAIN_CODE")]
        [StringLength(10)]
        [Unicode(false)]
        public string? MainCode { get; set; }

        [Column("SUB_CODE")]
        public int? SubCode { get; set; }

        [Column("ITEM_MASTER")]
        [StringLength(30)]
        [Unicode(false)]
        public string? ItemMaster { get; set; }

        [Column("ITEM_COUNT")]
        public decimal? ItemCount { get; set; }

        [Column("ITEM_QTY")]
        public decimal? ItemQty { get; set; }

        [Column("PRICE")]
        public decimal? Price { get; set; }

        [Column("ITEM_VAL")]
        public decimal? ItemVal { get; set; }

        [Column("FLAG_INV")]
        [StringLength(6)]
        [Unicode(false)]
        public string? FlagInv { get; set; }

        [Column("DES_ITEM")]
        [StringLength(20)]
        [Unicode(false)]
        public string? DesItem { get; set; }

        [Column("F_YEAR")]
        public int? FYear { get; set; }

        [Column("ITEM_UNIT_QTY")]
        public decimal? ItemUnitQty { get; set; }

        [Column("PRICE_PURCHASE")]
        public decimal? PricePurchase { get; set; }

        [Column("PRICE_WITHOUT_TAX")]
        public decimal? PriceWithoutTax { get; set; }

        [Column("PRICE2")]
        public decimal? Price2 { get; set; }

        [Column("ITEM_VAL2")]
        public decimal? ItemVal2 { get; set; }

        [Column("F_MONTH")]
        public int? FMonth { get; set; }

        [Column("REF_DOC_NO")]
        [StringLength(20)]
        [Unicode(false)]
        public string? RefDocNo { get; set; }

        [Column("FLAG1")]
        public int? Flag1 { get; set; }

        [Column("ITEM_MASTER_ORE")]
        [StringLength(3)]
        [Unicode(false)]
        public string? ItemMasterOre { get; set; }

        [Column("UNIT_QTY")]
        public decimal? UnitQty { get; set; }

        [Column("BRANCH_ID")]
        public int? BranchId { get; set; }

        [Column("PRICE_WITH_TAX")]
        public decimal? PriceWithTax { get; set; }

        [Column("CURRANCY_TYPE")]
        public short? CurrancyType { get; set; }

        [Column("CURRANCY_VAL")]
        public decimal? CurrancyVal { get; set; }

        [Column("CURRANCY_RATE")]
        public decimal? CurrancyRate { get; set; }

        [Column("SER_SYS")]
        public long? SerSys { get; set; }

        [Column("STORE_TYPE")]
        public int? StoreType { get; set; }

        [Column("EXPIRE_DATE")]
        [Precision(6)]
        public DateTime? ExpireDate { get; set; }

        [Column("TAX_PRC")]
        public decimal? TaxPrc { get; set; }

        [Column("SUP_NO")]
        public int? SupNo { get; set; }

        [Column("TAX_VAL")]
        public decimal? TaxVal { get; set; }

        [Column("CURRANCY_TYPE2")]
        public short? CurrancyType2 { get; set; }

        [Column("CURRANCY_VAL2")]
        public decimal? CurrancyVal2 { get; set; }

        [Column("CURRANCY_RATE2")]
        public decimal? CurrancyRate2 { get; set; }

        [Column("EXPEN_VAL")]
        public decimal? ExpenVal { get; set; }

        [Column("EXPEN_QTY")]
        public decimal? ExpenQty { get; set; }

        [Column("SHOW_ROW")]
        public int? ShowRow { get; set; }

        [Column("ITEM_DEFINE")]
        [StringLength(100)]
        [Unicode(false)]
        public string? ItemDefine { get; set; }

        [Column("USER_CODE")]
        public int? UserCode { get; set; }

        [Column("ITEM_MASTER2")]
        public int? ItemMaster2 { get; set; }

        [Column("PRICE3")]
        public decimal? Price3 { get; set; }

        [Column("PRICE_TYPE")]
        public int? PriceType { get; set; }

        [Column("AVAILABILITY")]
        [StringLength(30)]
        [Unicode(false)]
        public string? Availability { get; set; }

        [Column("TOT_SALES_PRICE")]
        public decimal? TotSalesPrice { get; set; }

        [Column("TAX_TYPE")]
        public int? TaxType { get; set; }

        [Column("REPLCATE")]
        public int? Replcate { get; set; }

        [Column("UNIT_CODE")]
        public int? UnitCode { get; set; }

        [Column("REF_DOC_NO2")]
        [StringLength(20)]
        [Unicode(false)]
        public string? RefDocNo2 { get; set; }

        [Column("BR_REPLC")]
        [StringLength(20)]
        [Unicode(false)]
        public string? BrReplc { get; set; }

        [Column("DOC_NO_FR")]
        public long? DocNoFr { get; set; }

        [Column("INV_TYPE")]
        [StringLength(1)]
        [Unicode(false)]
        public string? InvType { get; set; }

        [Column("ITM_BATCH")]
        [StringLength(15)]
        [Unicode(false)]
        public string? ItmBatch { get; set; }

        [Column("ITM_BARCODE")]
        [StringLength(50)]
        [Unicode(false)]
        public string? ItmBarcode { get; set; }

        [Column("POST_POS")]
        public int? PostPos { get; set; }

        [Column("ITM_HAVE_TAX")]
        public short? ItmHaveTax { get; set; }

        [Column("TAX_BASE")]
        [StringLength(1)]
        [Unicode(false)]
        public string? TaxBase { get; set; }

        [Column("ITEM_DISCOUNT")]
        public decimal? ItemDiscount { get; set; }

        [Column("BOUNS_QTY")]
        public decimal? BounsQty { get; set; }

        [Column("PUR_QTY")]
        public decimal? PurQty { get; set; }

        [Column("ITEM_DISCOUNT_PRC")]
        public decimal? ItemDiscountPrc { get; set; }

        [Column("DISC_BASE")]
        [StringLength(1)]
        [Unicode(false)]
        public string? DiscBase { get; set; }

        [Column("LOT_NO")]
        [StringLength(20)]
        [Unicode(false)]
        public string? LotNo { get; set; }

        [Column("ENTRY_DATE")]
        public DateTime? EntryDate { get; set; }

        [Column("MODIFY_DATE")]
        public DateTime? ModifyDate { get; set; }

        [Column("G_ID")]
        public int? GId { get; set; }

        [Column("S_ID")]
        public int? SId { get; set; }

        [Column("SEND_FR")]
        public int? SendFr { get; set; }

        [ForeignKey("TransMAsterID")]
        public TransMaster? PermissionForm { get; set; }
    }
}
