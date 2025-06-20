﻿using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProSoft.EF.Models.Medical.HospitalPatData;

[Table("PAT_ADMISSION")]
public partial class PatAdmission
{
    [Key]
    [Column("MASTER_ID")]
    public int MasterId { get; set; }

    [Column("BRANCH_ID")]
    public decimal? BranchId { get; set; }

    [Column("PAT_ID")]
    public int? PatId { get; set; }

    [Column("DEAL")]
    public decimal? Deal { get; set; }

    [Column("PAT_AD_DATE")]
    public DateTime? PatAdDate { get; set; }

    [Column("COMP_ID")]
    public int? CompId { get; set; }

    [Column("PAT_INTERNAL")]
    public decimal? PatInternal { get; set; }

    [Column("PAT_EXTERNAL")]
    public decimal? PatExternal { get; set; }

    [Column("PAT_EMERGENCY")]
    public decimal? PatEmergency { get; set; }

    [Column("PAT_EXIT")]
    public decimal? PatExit { get; set; }

    [Column("PAT_DATE_EXIT")]
    public DateTime? PatDateExit { get; set; }

    [Column("DECISION_SHEET")]
    [StringLength(255)]
    [Unicode(false)]
    public string? DecisionSheet { get; set; }

    [Column("PAT_CARD_ID")]
    public decimal? PatCardId { get; set; }

    [Column("REPLCATE")]
    public decimal? Replcate { get; set; }

    [Column("EX_YEAR")]
    public decimal? ExYear { get; set; }

    [Column("PAT_AD_TIME")]
    public DateTime? PatAdTime { get; set; }

    [Column("MAIN_INV_NO")]
    public int? MainInvNo { get; set; }

    [Column("MAIN_INV_TOT")]
    public decimal? MainInvTot { get; set; }

    [Column("MAIN_INV_DESC")]
    [StringLength(200)]
    [Unicode(false)]
    public string? MainInvDesc { get; set; }

    [Column("PATIENT_VALUE")]
    public decimal? PatientValue { get; set; }

    [Column("COMP_VALUE")]
    public decimal? CompValue { get; set; }

    [Column("DR_CODE")]
    public int? DrCode { get; set; }

    [Column("DR_VAL")]
    public decimal? DrVal { get; set; }

    [Column("DR_TAX")]
    public decimal? DrTax { get; set; }

    [Column("EX_MONTH")]
    public decimal? ExMonth { get; set; }

    [Column("DR_OBTAIN")]
    public decimal? DrObtain { get; set; }

    [Column("DISCOUNT_VAL")]
    public decimal? DiscountVal { get; set; }

    [Column("ACC_TRANS_NO")]
    public long? AccTransNo { get; set; }

    [Column("ACC_TRANS_NO2")]
    public long? AccTransNo2 { get; set; }

    [Column("SAFE_DOC_NO")]
    public long? SafeDocNo { get; set; }

    [Column("SAFE_DOC_NO2")]
    public long? SafeDocNo2 { get; set; }

    [Column("ARCH")]
    [StringLength(15)]
    [Unicode(false)]
    public string? Arch { get; set; }

    [Column("ROOM_NO")]
    [StringLength(20)]
    [Unicode(false)]
    public string? RoomNo { get; set; }

    [Column("DAYS_NO")]
    public decimal? DaysNo { get; set; }

    [Column("DAYS_VALUE")]
    public decimal? DaysValue { get; set; }

    [Column("FLAG")]
    public decimal? Flag { get; set; }

    [Column("DUE_VAL")]
    public decimal? DueVal { get; set; }

    [Column("EXCHANGE_TYPE")]
    public decimal? ExchangeType { get; set; }

    [Column("NOTE")]
    [StringLength(100)]
    [Unicode(false)]
    public string? Note { get; set; }

    [Column("AMANAT")]
    public decimal? Amanat { get; set; }

    [Column("AMANAT_RET")]
    public decimal? AmanatRet { get; set; }

    [Column("SAFE_IN_NO1")]
    public int? SafeInNo1 { get; set; }

    [Column("SAFE_IN_NO2")]
    public int? SafeInNo2 { get; set; }

    [Column("AMANAT_RET_PAT")]
    public decimal? AmanatRetPat { get; set; }

    [Column("KASTARA_TOT")]
    public decimal? KastaraTot { get; set; }

    [Column("ENAYA_TOT")]
    public decimal? EnayaTot { get; set; }

    [Column("JOR_KIED_NO1")]
    public int? JorKiedNo1 { get; set; }

    [Column("JOR_KIED_NO2")]
    public int? JorKiedNo2 { get; set; }

    [Column("CASH_NO")]
    public int? CashNo { get; set; }

    [Column("STAMP_VAL")]
    public decimal? StampVal { get; set; }

    [Column("BR_REPLC")]
    [StringLength(20)]
    [Unicode(false)]
    public string? BrReplc { get; set; }

    [Column("PAT_CLOSE")]
    public int? PatClose { get; set; }

    [Column("PAY_FLAG")]
    public int? PayFlag { get; set; }

    [Column("JOR_KIED_NO3")]
    public int? JorKiedNo3 { get; set; }

    [Column("SAFE_DOC_NO3")]
    public int? SafeDocNo3 { get; set; }

    [Column("EX_YEAR_TO")]
    public int? ExYearTo { get; set; }

    [Column("EX_MONTH_TO")]
    public int? ExMonthTo { get; set; }

    [Column("MAIN_CODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string? MainCode { get; set; }

    [Column("SUB_CODE")]
    [StringLength(6)]
    [Unicode(false)]
    public string? SubCode { get; set; }

    [Column("BENEFICIARY_NAME")]
    [StringLength(100)]
    [Unicode(false)]
    public string? BeneficiaryName { get; set; }

    [Column("KINSHIP")]
    public int? Kinship { get; set; }

    [Column("INV_TYPE")]
    public int? InvType { get; set; }

    [Column("ENTRY_DATE")]
    public DateTime? EntryDate { get; set; }

    [Column("TRANSF_VAL")]
    public decimal? TransfVal { get; set; }

    [Column("POST_ID")]
    public decimal? PostId { get; set; }

    [Column("PAT_RELATIONSHIP")]
    public decimal? PatRelationship { get; set; }

    [Column("ACC_TRANS_TYPE")]
    public decimal? AccTransType { get; set; }

    [Column("BRNACH_INITIAL")]
    public int? BrnachInitial { get; set; }

    [Column("DR_SEND")]
    public int? DrSend { get; set; }

    [Column("PAY_PAT_TOT")]
    public decimal? PayPatTot { get; set; }

    [Column("PAT_ID_SECTION")]
    public int? PatIdSection { get; set; }

    [Column("WINDOW_FLAG")]
    public decimal? WindowFlag { get; set; }

    [Column("CO_NAME_2")]
    [StringLength(150)]
    [Unicode(false)]
    public string? CoName2 { get; set; }

    [Column("SEND_TO")]
    public int? SendTo { get; set; }

    [Column("MAIN_INV_NO_ALL")]
    public int? MainInvNoAll { get; set; }

    [Column("PAT_DATE_OUT")]
    public DateTime? PatDateOut { get; set; }

    [Column("PROCEDURE_ID")]
    public int? ProcedureId { get; set; }

    [Column("PAT_FR")]
    public int? PatFr { get; set; }

    [Column("USER_CODE_CREATE")]
    public int? UserCodeCreate { get; set; }

    [Column("USER_CODE_MODIFY")]
    public int? UserCodeModify { get; set; }

    [Column("P_DEP")]
    [StringLength(15)]
    [Unicode(false)]
    public string? PDep { get; set; }

    [Column("P_CRT")]
    [StringLength(15)]
    [Unicode(false)]
    public string? PCrt { get; set; }

    [Column("P_GROUP")]
    [StringLength(15)]
    [Unicode(false)]
    public string? PGroup { get; set; }

    [Column("FINANCE_ID")]
    [StringLength(15)]
    [Unicode(false)]
    public string? FinanceId { get; set; }

    [Column("SEND_FR")]
    public int? SendFr { get; set; }

    [Column("BOOK_ROOM")]
    public int? BookRoom { get; set; }

    [Column("MAIN_INV_NO_TAX")]
    public int? MainInvNoTax { get; set; }

    [Column("INV_TYPE_TAX")]
    public int? InvTypeTax { get; set; }

    [Column("SEND_TRANSFIR")]
    public int? SendTransfir { get; set; }

    [Column("M_CODE")]
    public int? MCode { get; set; }

    [Column("M_CODE_DTL")]
    public int? MCodeDtl { get; set; }

    [Column("OPERATION_CODE")]
    public int? OperationCode { get; set; }

    [Column("OPERATION_TYPE")]
    public int? OperationType { get; set; }

    [Column("EXTRA_VAL")]
    public decimal? ExtraVal { get; set; }

    [Column("COMP_ID_DTL")]
    public int? CompIdDtl { get; set; }

    [Column("UUID")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Uuid { get; set; }

    [Column("SUBMITID")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Submitid { get; set; }

    [Column("E_INV_STS")]
    [StringLength(1)]
    [Unicode(false)]
    public string? EInvSts { get; set; }

    [Column("EINV_DATE")]
    public DateTime? EinvDate { get; set; }

    [Column("E_INV_ID")]
    [StringLength(50)]
    [Unicode(false)]
    public string? EInvId { get; set; }

    [Column("EINV_ERR")]
    [StringLength(200)]
    [Unicode(false)]
    public string? EinvErr { get; set; }

    [Column("TAX_FLAG")]
    public int? TaxFlag { get; set; }

    [Column("TAX_DATE")]
    public DateTime? TaxDate { get; set; }

    [Column("PREPAID")]
    public decimal? Prepaid { get; set; }

    [Column("SESSION_NO")]
    public int? SessionNo { get; set; }

    [Column("KNOW_US_FR")]
    public short? KnowUsFr { get; set; }

    [Column("EXTRA_VAL2")]
    public decimal? ExtraVal2 { get; set; }

    [ForeignKey("BrnachInitial")]
    [InverseProperty("PatAdmissions")]
    public virtual ClassificationCust? BrnachInitialNavigation { get; set; }

    [InverseProperty("Master")]
    public virtual ICollection<ClinicTran> ClinicTrans { get; set; } = new List<ClinicTran>();

    [ForeignKey("CompId")]
    [InverseProperty("PatAdmissions")]
    public virtual Company? Comp { get; set; }

    [ForeignKey("CompIdDtl")]
    [InverseProperty("PatAdmissions")]
    public virtual CompanyDtl? CompIdDtlNavigation { get; set; }

    [ForeignKey("DrCode")]
    [InverseProperty("PatAdmissions")]
    public virtual Doctor? DrCodeNavigation { get; set; }

    [ForeignKey("PatId")]
    [InverseProperty("PatAdmissions")]
    public virtual Pat? Pat { get; set; }

    [ForeignKey("SendFr")]
    [InverseProperty("PatAdmissions")]
    public virtual Region? SendFrNavigation { get; set; }
}
