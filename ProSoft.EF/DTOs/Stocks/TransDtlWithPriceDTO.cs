﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Stocks
{
    public class TransDtlWithPriceDTO
    {
        public int TransDtlId { get; set; }
        public int TransMasterID { get; set; }
        public long? Serial { get; set; }
        public int TransType { get; set; }

        [DisplayName("The Item")]
        public string? ItemMaster { get; set; }
        public string? ShowItemMaster { get; set; }
        public string? ItemMasterName { get; set; }

        [DisplayName("The Barcode")]
        public string? ItmBarcode { get; set; }

        [DisplayName("The Quantity")]
        [Required(ErrorMessage = "The field is required")]
        public decimal UnitQty { get; set; }

        [DisplayName("The Unit")]
        [Required(ErrorMessage = "The field is required")]
        public int UnitCode { get; set; }

        public string? UnitCodeName { get; set; }

        [DisplayName("Unit Price")]
        [Required(ErrorMessage = "The field is required")]
        public decimal Price { get; set; }

        [DisplayName("Under Tax")]
        [Required(ErrorMessage = "The field is required")]
        public short ItmHaveTax { get; set; }

        [DisplayName("Tax")]
        [Required(ErrorMessage = "The field is required")]
        public decimal TaxVal { get; set; }

        [DisplayName("The Total")]
        [Required(ErrorMessage = "The field is required")]
        public decimal ItemVal { get; set; }

        [DisplayName("Expire Date")]
        public DateTime? ExpireDate { get; set; }


        public int? UserCode { get; set; }
        public int? BranchId { get; set; }
        public int? ShowTransPrice { get; set; }
        public string? PermissionName { get; set; }
        public int? Flag1 { get; set; }

        public List<SubItemViewDTO>? SubItems { get; set; }
        public List<UnitCodeDTO>? UnitCodes { get; set; }

        //[Column("STOCK_CODE")]
        //public short? StockCode { get; set; }

        //[Column("TRANS_TYPE")]

        //[Column("DOC_NO")]
        //public long? DocNo { get; set; }

        //[Column("DOC_DATE")]
        //[Precision(6)]
        //public DateTime? DocDate { get; set; }


        //[Column("MAIN_CODE")]
        //[StringLength(10)]
        //[Unicode(false)]
        //public string? MainCode { get; set; }

        //[Column("SUB_CODE")]
        //public int? SubCode { get; set; }


        //[Column("ITEM_COUNT")]
        //public decimal? ItemCount { get; set; }

        //[Column("ITEM_QTY")]
        //public decimal? ItemQty { get; set; }



        //[Column("FLAG_INV")]
        //[StringLength(6)]
        //[Unicode(false)]
        //public string? FlagInv { get; set; }

        //[Column("DES_ITEM")]
        //[StringLength(20)]
        //[Unicode(false)]
        //public string? DesItem { get; set; }

        //[Column("F_YEAR")]
        //public int? FYear { get; set; }

        //[Column("ITEM_UNIT_QTY")]
        //public decimal? ItemUnitQty { get; set; }

        //[Column("PRICE_PURCHASE")]
        //public decimal? PricePurchase { get; set; }

        //[Column("PRICE_WITHOUT_TAX")]
        //public decimal? PriceWithoutTax { get; set; }

        //[Column("PRICE2")]
        //public decimal? Price2 { get; set; }

        //[Column("ITEM_VAL2")]
        //public decimal? ItemVal2 { get; set; }

        //[Column("F_MONTH")]
        //public int? FMonth { get; set; }

        //[Column("REF_DOC_NO")]
        //[StringLength(20)]
        //[Unicode(false)]
        //public string? RefDocNo { get; set; }

        //[Column("FLAG1")]

        //[Column("ITEM_MASTER_ORE")]
        //[StringLength(3)]
        //[Unicode(false)]
        //public string? ItemMasterOre { get; set; }



        //[Column("PRICE_WITH_TAX")]
        //public decimal? PriceWithTax { get; set; }

        //[Column("CURRANCY_TYPE")]
        //public short? CurrancyType { get; set; }

        //[Column("CURRANCY_VAL")]
        //public decimal? CurrancyVal { get; set; }

        //[Column("CURRANCY_RATE")]
        //public decimal? CurrancyRate { get; set; }

        //[Column("SER_SYS")]
        //public long? SerSys { get; set; }

        //[Column("STORE_TYPE")]
        //public int? StoreType { get; set; }


        //[Column("TAX_PRC")]
        //public decimal? TaxPrc { get; set; }

        //[Column("SUP_NO")]
        //public int? SupNo { get; set; }


        //[Column("CURRANCY_TYPE2")]
        //public short? CurrancyType2 { get; set; }

        //[Column("CURRANCY_VAL2")]
        //public decimal? CurrancyVal2 { get; set; }

        //[Column("CURRANCY_RATE2")]
        //public decimal? CurrancyRate2 { get; set; }

        //[Column("EXPEN_VAL")]
        //public decimal? ExpenVal { get; set; }

        //[Column("EXPEN_QTY")]
        //public decimal? ExpenQty { get; set; }

        //[Column("SHOW_ROW")]
        //public int? ShowRow { get; set; }

        //[Column("ITEM_DEFINE")]
        //[StringLength(100)]
        //[Unicode(false)]
        //public string? ItemDefine { get; set; }


        //[Column("ITEM_MASTER2")]
        //public int? ItemMaster2 { get; set; }

        //[Column("PRICE3")]
        //public decimal? Price3 { get; set; }

        //[Column("PRICE_TYPE")]
        //public int? PriceType { get; set; }

        //[Column("AVAILABILITY")]
        //[StringLength(30)]
        //[Unicode(false)]
        //public string? Availability { get; set; }

        //[Column("TOT_SALES_PRICE")]
        //public decimal? TotSalesPrice { get; set; }

        //[Column("TAX_TYPE")]
        //public int? TaxType { get; set; }

        //[Column("REPLCATE")]
        //public int? Replcate { get; set; }


        //[Column("REF_DOC_NO2")]
        //[StringLength(20)]
        //[Unicode(false)]
        //public string? RefDocNo2 { get; set; }

        //[Column("BR_REPLC")]
        //[StringLength(20)]
        //[Unicode(false)]
        //public string? BrReplc { get; set; }

        //[Column("DOC_NO_FR")]
        //public long? DocNoFr { get; set; }

        //[Column("INV_TYPE")]
        //[StringLength(1)]
        //[Unicode(false)]
        //public string? InvType { get; set; }

        //[Column("ITM_BATCH")]
        //[StringLength(15)]
        //[Unicode(false)]
        //public string? ItmBatch { get; set; }


        //[Column("POST_POS")]
        //public int? PostPos { get; set; }


        //[Column("TAX_BASE")]
        //[StringLength(1)]
        //[Unicode(false)]
        //public string? TaxBase { get; set; }

        //[Column("ITEM_DISCOUNT")]
        //public decimal? ItemDiscount { get; set; }

        //[Column("BOUNS_QTY")]
        //public decimal? BounsQty { get; set; }

        //[Column("PUR_QTY")]
        //public decimal? PurQty { get; set; }

        //[Column("ITEM_DISCOUNT_PRC")]
        //public decimal? ItemDiscountPrc { get; set; }

        //[Column("DISC_BASE")]
        //[StringLength(1)]
        //[Unicode(false)]
        //public string? DiscBase { get; set; }

        //[Column("LOT_NO")]
        //[StringLength(20)]
        //[Unicode(false)]
        //public string? LotNo { get; set; }

        //[Column("ENTRY_DATE")]
        //public DateTime? EntryDate { get; set; }

        //[Column("MODIFY_DATE")]
        //public DateTime? ModifyDate { get; set; }

        //[Column("G_ID")]
        //public int? GId { get; set; }

        //[Column("S_ID")]
        //public int? SId { get; set; }

        //[Column("SEND_FR")]
        //public int? SendFr { get; set; }
    }
}
