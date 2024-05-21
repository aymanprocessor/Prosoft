using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using ProSoft.EF.Models.Accounts;
using ProSoft.EF.Models.MedicalRecords;
using ProSoft.EF.Models.Shared;
using ProSoft.EF.Models.Stocks;

namespace ProSoft.EF.Models.Treasury;

[Table("ACC_SAFE_CASH")]
public partial class AccSafeCash
{
    [Key]
    [Column("SAFE_CASH_ID")]
    public int SafeCashId { get; set; }

    [Column("CO_CODE")]
    public int? CoCode { get; set; }

    [Column("F_YEAR")]
    public int? FYear { get; set; }

    [Column("DOC_TYPE")]
    [StringLength(5)]
    [Unicode(false)]
    public string DocType { get; set; }

    [Column("DOC_NO")]
    public int? DocNo { get; set; }

    [Column("DOC_DATE")]
    [Precision(6)]
    public DateTime? DocDate { get; set; }

    [Column("CUR_CODE")]
    public int? CurCode { get; set; }

    [Column("PERSON_NAME")]
    [StringLength(150)]
    [Unicode(false)]
    public string? PersonName { get; set; }

    [Column("VALUE_PAY", TypeName = "decimal(12, 3)")]
    public decimal? ValuePay { get; set; }

    [Column("COMMENTT")]
    [StringLength(200)]
    [Unicode(false)]
    public string? Commentt { get; set; }

    [Column("DISCOUNT_VAL", TypeName = "decimal(12, 3)")]
    public decimal? DiscountVal { get; set; }

    [Column("MAIN_CODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string? MainCode { get; set; }

    [Column("SUB_CODE")]
    [StringLength(6)]
    [Unicode(false)]
    public string? SubCode { get; set; }

    [Column("DELETE_FLAG")]
    [StringLength(3)]
    [Unicode(false)]
    public string? DeleteFlag { get; set; }

    [Column("APROVED_FLAG")]
    [StringLength(3)]
    [Unicode(false)]
    public string? AprovedFlag { get; set; }

    [Column("FLAG_PAY")]
    [StringLength(6)]
    [Unicode(false)]
    public string? FlagPay { get; set; }

    [Column("PROFIT_TAX", TypeName = "decimal(9, 3)")]
    public decimal? ProfitTax { get; set; }

    [Column("VAL_PAY_AFTER", TypeName = "decimal(9, 3)")]
    public decimal? ValPayAfter { get; set; }

    [Column("ACC_NAME")]
    [StringLength(150)]
    [Unicode(false)]
    public string? AccName { get; set; }

    [Column("CUR_SER")]
    public int? CurSer { get; set; }

    [Column("FLAG")]
    public int? Flag { get; set; }

    [Column("RATE1", TypeName = "decimal(7, 4)")]
    public decimal? Rate1 { get; set; }

    [Column("F_MONTH")]
    public double? FMonth { get; set; }

    [Column("ACC_TRANS_NO")]
    public long? AccTransNo { get; set; }

    [Column("ACC_TRANS_TYPE")]
    public int? AccTransType { get; set; }

    [Column("SAFE_CODE")]
    public int? SafeCode { get; set; }

    [Column("BRANCH_ID")]
    public int? BranchId { get; set; }

    [Column("SM_NO")]
    public int? SmNo { get; set; }

    [Column("MASTER_ID")]
    public int? MasterId { get; set; }

    [Column("REPLCATE")]
    public int? Replcate { get; set; }

    [Column("USER_CODE")]
    public int? UserCode { get; set; }

    [Column("ENTRY_TYPE")]
    public int? EntryType { get; set; }

    [Column("PAT_RET_FLAG")]
    public int? PatRetFlag { get; set; }

    [Column("ENTRY_DATE", TypeName = "datetime")]
    public DateTime? EntryDate { get; set; }

    [Column("BR_REPLC")]
    [StringLength(20)]
    [Unicode(false)]
    public string? BrReplc { get; set; }

    [Column("M_CODE_DTL")]
    public int? MCodeDtl { get; set; }

    [Column("POST_RECIPT")]
    public int? PostRecipt { get; set; }

    [Column("COST_CENTER_CODE")]
    public int? CostCenterCode { get; set; }

    [Column("G_VALUE_PAY", TypeName = "decimal(12, 3)")]
    public decimal? GValuePay { get; set; }

    [Column("SAFE_CODE2")]
    public int? SafeCode2 { get; set; }

    [Column("DOC_NO_FR")]
    public int? DocNoFr { get; set; }

    [Column("CSH_ORD_NUM")]
    public int? CshOrdNum { get; set; }

    [Column("SER_ID")]
    public int? SerId { get; set; }

    public ICollection<CustCollectionsDiscount>? custCollectionsDiscounts { get; set; }


    // [Column("G_CODE")]
    //  public int? GCode { get; set; }

    [ForeignKey("EntryType")]
    [InverseProperty("AccSafeCashes")]
    public GTable? GTable { get; set; }

    //[Column("JOURNAL_CODE")]
    // public int? JournalCode { get; set; }

    [ForeignKey("AccTransType")]
    [InverseProperty("AccSafeCashes")]
    public JournalType? JournalType { get; set; }

    // [Column("COST_CODE")]
    // public int? CostCode { get; set; }

    [ForeignKey("CostCenterCode")]
    [InverseProperty("AccSafeCashes")]
    public CostCenter? CostCenter { get; set; }

    //[Column("SAFE_CODE")]
    //  public int? SAFE_CODE { get; set; }

    [ForeignKey("SafeCode")]
    [InverseProperty("AccSafeCashes")]
    public SafeName? SafeName { get; set; }

    //[Column("CODE_NO")]
    // public int? CodeNo { get; set; }

    [ForeignKey("CurCode")]
    [InverseProperty("AccSafeCashes")]
    public AccGlobalDef? AccGlobalDef { get; set; }

    //[ForeignKey("AccMainCode")]
    //[Column("ACC_CO_CODE_MAIN")]
    //public int? AccCoCodeMain { get; set; }
    //public AccMainCode? AccMainCode { get; set; }

    //[ForeignKey("AccSubCode")]
    //[Column("ACC_CO_CODE_SUB")]
    //public int? AccCoCodeSub { get; set; }
    //public AccSubCode? AccSubCode { get; set; }
}
