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
    [Table("TRANS_MASTER")]
    public partial class TransMaster
    {
        [Key]
        public int TransMAsterID {  get; set; }

        [Column("STOCK_CODE")]
        public short? StockCode { get; set; }

        [Column("TRANS_TYPE")]
        public int? TransType { get; set; }

        [Column("DOC_NO")]
        public long? DocNo { get; set; }

        [Column("DOC_NO2")]
        [StringLength(15)]
        [Unicode(false)]
        public string? DocNo2 { get; set; }

        [Column("DOC_DATE")]
        [Precision(6)]
        public DateTime? DocDate { get; set; }

        [Column("CUST_NO")]
        [StringLength(6)]
        [Unicode(false)]
        public string? CustNo { get; set; }

        [Column("SUP_NO")]
        [StringLength(6)]
        [Unicode(false)]
        public string? SupNo { get; set; }

        [Column("MAIN_CODE")]
        [StringLength(10)]
        [Unicode(false)]
        public string? MainCode { get; set; }

        [Column("REF_DOC_NO")]
        [StringLength(20)]
        [Unicode(false)]
        public string? RefDocNo { get; set; }

        [Column("REMARK")]
        [StringLength(50)]
        [Unicode(false)]
        public string? Remark { get; set; }

        [Column("TOT_TRANS_VAL", TypeName = "decimal(10, 2)")]
        public decimal? TotTransVal { get; set; }

        [Column("DESCOUNT", TypeName = "decimal(10, 4)")]
        public decimal? Descount { get; set; }

        [Column("STATUS_BAL")]
        [StringLength(1)]
        [Unicode(false)]
        public string? StatusBal { get; set; }

        [Column("FLAG")]
        [StringLength(2)]
        [Unicode(false)]
        public string? Flag { get; set; }

        [Column("TOT_WEIGHT", TypeName = "decimal(10, 2)")]
        public decimal? TotWeight { get; set; }

        [Column("PAY")]
        [StringLength(2)]
        [Unicode(false)]
        public string? Pay { get; set; }

        [Column("F_YEAR")]
        public int? FYear { get; set; }

        [Column("FLAG2")]
        [StringLength(1)]
        [Unicode(false)]
        public string? Flag2 { get; set; }

        [Column("TOT_TRANS_VAL2", TypeName = "decimal(10, 2)")]
        public decimal? TotTransVal2 { get; set; }

        [Column("AMOUNT_VISA", TypeName = "decimal(10, 2)")]
        public decimal? AmountVisa { get; set; }

        [Column("CASH_AMOUNT", TypeName = "decimal(10, 2)")]
        public decimal? CashAmount { get; set; }

        [Column("SALE_STATUS")]
        [StringLength(2)]
        [Unicode(false)]
        public string? SaleStatus { get; set; }

        [Column("ADD_PERS", TypeName = "decimal(4, 2)")]
        public decimal? AddPers { get; set; }

        [Column("USER_CODE")]
        public int? UserCode { get; set; }

        [Column("DISC_PERS", TypeName = "decimal(4, 2)")]
        public decimal? DiscPers { get; set; }

        [Column("DUE_DATE")]
        [Precision(6)]
        public DateTime? DueDate { get; set; }

        [Column("CUST_DISC1", TypeName = "decimal(10, 2)")]
        public decimal? CustDisc1 { get; set; }

        [Column("CUST_DISC2", TypeName = "decimal(10, 2)")]
        public decimal? CustDisc2 { get; set; }

        [Column("CUST_DISC3", TypeName = "decimal(10, 2)")]
        public decimal? CustDisc3 { get; set; }

        [Column("CUST_DISC4", TypeName = "decimal(10, 2)")]
        public decimal? CustDisc4 { get; set; }

        [Column("CUST_DISC5", TypeName = "decimal(10, 2)")]
        public decimal? CustDisc5 { get; set; }

        [Column("TAX_PRC", TypeName = "decimal(4, 2)")]
        public decimal? TaxPrc { get; set; }

        [Column("TAX_VALUE", TypeName = "decimal(10, 2)")]
        public decimal? TaxValue { get; set; }

        [Column("DUE_VALUE", TypeName = "decimal(10, 2)")]
        public decimal? DueValue { get; set; }

        [Column("INV_NO")]
        public long? InvNo { get; set; }

        [Column("SM_NO")]
        public int? SmNo { get; set; }

        [Column("STOCK_CODE2")]
        public short? StockCode2 { get; set; }

        [Column("DUE_MONTH")]
        public int? DueMonth { get; set; }

        [Column("DUE_YEAR")]
        public int? DueYear { get; set; }

        [Column("FLAG3")]
        [StringLength(1)]
        [Unicode(false)]
        public string? Flag3 { get; set; }

        [Column("REF_DOC_DATE")]
        [Precision(6)]
        public DateTime? RefDocDate { get; set; }

        [Column("SUP_INV_NO")]
        [StringLength(20)]
        [Unicode(false)]
        public string? SupInvNo { get; set; }

        [Column("SUP_INV_DATE")]
        [Precision(6)]
        public DateTime? SupInvDate { get; set; }

        [Column("INV_TYPE")]
        [StringLength(1)]
        [Unicode(false)]
        public string? InvType { get; set; }

        [Column("ACC_TRANS_NO")]
        public long? AccTransNo { get; set; }

        [Column("ACC_TRANS_TYPE")]
        [StringLength(5)]
        [Unicode(false)]
        public string? AccTransType { get; set; }

        [Column("F_MONTH")]
        public int? FMonth { get; set; }

        [Column("FLAG1")]
        public int? Flag1 { get; set; }

        [Column("SIDE_ID")]
        public int? SideId { get; set; }

        [Column("BRANCH_ID")]
        public int? BranchId { get; set; }

        [Column("REC_CHECKS_NO")]
        [StringLength(30)]
        [Unicode(false)]
        public string? RecChecksNo { get; set; }

        [Column("SUPPLY_ORDER_NO")]
        [StringLength(30)]
        [Unicode(false)]
        public string? SupplyOrderNo { get; set; }

        [Column("LC_NO")]
        [StringLength(30)]
        [Unicode(false)]
        public string? LcNo { get; set; }

        [Column("MSG_CODE")]
        public int? MsgCode { get; set; }

        [Column("USER_CONFIRM")]
        public int? UserConfirm { get; set; }

        [Column("SER_SYS")]
        public long? SerSys { get; set; }

        [Column("CONFIRM_DATE")]
        [Precision(6)]
        public DateTime? ConfirmDate { get; set; }

        [Column("TRANS_CONFIRM")]
        public int? TransConfirm { get; set; }

        [Column("SHOW_ROW")]
        public int? ShowRow { get; set; }

        [Column("END_DATE")]
        [Precision(6)]
        public DateTime? EndDate { get; set; }

        [Column("OPERATION_TYPE")]
        public int? OperationType { get; set; }

        [Column("ATTACH_FILE")]
        [StringLength(150)]
        [Unicode(false)]
        public string? AttachFile { get; set; }

        [Column("SM_NO2")]
        public int? SmNo2 { get; set; }

        [Column("PRC_SM1")]
        public int? PrcSm1 { get; set; }

        [Column("PRC_SM2")]
        public int? PrcSm2 { get; set; }

        [Column("QUOTE")]
        public int? Quote { get; set; }

        [Column("CONTACT_PERSON")]
        [StringLength(50)]
        [Unicode(false)]
        public string? ContactPerson { get; set; }

        [Column("REPLCATE")]
        public int? Replcate { get; set; }

        [Column("BR_REPLC")]
        [StringLength(20)]
        [Unicode(false)]
        public string? BrReplc { get; set; }

        [Column("DOC_NO_FR")]
        public long? DocNoFr { get; set; }

        [Column("PRICE_INC_TAX")]
        public short? PriceIncTax { get; set; }

        [Column("TAX_BASE")]
        [StringLength(1)]
        [Unicode(false)]
        public string? TaxBase { get; set; }

        [Column("DISCOUNT_BASE")]
        [StringLength(1)]
        [Unicode(false)]
        public string? DiscountBase { get; set; }

        [Column("LOT_NO")]
        [StringLength(20)]
        [Unicode(false)]
        public string? LotNo { get; set; }

        [Column("ENTRY_DATE", TypeName = "datetime")]
        public DateTime? EntryDate { get; set; }

        [Column("MODIFY_DATE", TypeName = "datetime")]
        public DateTime? ModifyDate { get; set; }

        [Column("SEND_FR")]
        public int? SendFr { get; set; }
    }
}
