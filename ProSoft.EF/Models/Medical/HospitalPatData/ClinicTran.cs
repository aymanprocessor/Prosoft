using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using ProSoft.EF.Models.Stocks;

namespace ProSoft.EF.Models.Medical.HospitalPatData;

[Table("CLINIC_TRANS")]
public partial class ClinicTran
{
    [Key]
    [Column("CHECK_ID")]
    public int CheckId { get; set; }

    [Column("MASTER_ID")]
    public int? MasterId { get; set; }

    [Column("COUNTER")]
    public int? Counter { get; set; }

    [Column("MAIN_ID")]
    public int? MainId { get; set; }

    [Column("SUB_ID")]
    public int? SubId { get; set; }

    [Column("EX_YEAR")]
    public int? ExYear { get; set; }

    [Column("CLINIC_ID")]
    public int? ClinicId { get; set; }

    [Column("EX_DATE", TypeName = "datetime")]
    public DateTime? ExDate { get; set; }

    [Column("EX_DAY")]
    [StringLength(20)]
    [Unicode(false)]
    public string? ExDay { get; set; }

    [Column("S_CLINIC_ID")]
    public int? SClinicId { get; set; }

    [Column("SERV_ID")]
    public int? ServId { get; set; }

    [Column("EX_PERIOD")]
    public int? ExPeriod { get; set; }

    [Column("ENTRY_STATUS")]
    public int? EntryStatus { get; set; }

    [Column("VALUE_SERVICE", TypeName = "decimal(9, 2)")]
    public decimal? ValueService { get; set; }

    [Column("PAT_EXTERNAL")]
    public int? PatExternal { get; set; }

    [Column("DR_CODE")]
    public int? DrCode { get; set; }

    [Column("SPECIFIC_CODE")]
    public int? SpecificCode { get; set; }

    [Column("RECEPT_EMP_CODE")]
    public int? ReceptEmpCode { get; set; }

    [Column("DR_CODE_CONERTER")]
    public int? DrCodeConerter { get; set; }

    [Column("COMP_ID")]
    public int? CompId { get; set; }

    [Column("PATIENT_VALUE", TypeName = "decimal(9, 2)")]
    public decimal? PatientValue { get; set; }

    [Column("CHECK_ID_CANCEL")]
    public int? CheckIdCancel { get; set; }

    [Column("EX_INVOICE_NO")]
    public int? ExInvoiceNo { get; set; }

    [Column("PAT_STATUS")]
    public int? PatStatus { get; set; }

    [Column("EX_MONTH")]
    public int? ExMonth { get; set; }

    [Column("PAT_EMERG")]
    public int? PatEmerg { get; set; }

    [Column("QTY")]
    public int? Qty { get; set; }

    [Column("PAT_TIME", TypeName = "datetime")]
    public DateTime? PatTime { get; set; }

    [Column("FLAG")]
    public int? Flag { get; set; }

    [Column("PAT_SER")]
    public int? PatSer { get; set; }

    [Column("PAT_PHAR_NAME")]
    [StringLength(75)]
    [Unicode(false)]
    public string? PatPharName { get; set; }

    [Column("CREATE_DATE", TypeName = "datetime")]
    public DateTime? CreateDate { get; set; }

    [Column("PAT_PERC", TypeName = "decimal(5, 2)")]
    public decimal? PatPerc { get; set; }

    [Column("MODIFY_DATE_TIME", TypeName = "datetime")]
    public DateTime? ModifyDateTime { get; set; }

    [Column("DR_VAL", TypeName = "decimal(9, 2)")]
    public decimal? DrVal { get; set; }

    [Column("HO_VAL", TypeName = "decimal(9, 2)")]
    public decimal? HoVal { get; set; }

    [Column("DR_VAL_PAT", TypeName = "decimal(9, 2)")]
    public decimal? DrValPat { get; set; }

    [Column("HO_VAL_PAT", TypeName = "decimal(9, 2)")]
    public decimal? HoValPat { get; set; }

    [Column("BRANCH_ID")]
    public int? BranchId { get; set; }

    [Column("COMP_VALUE", TypeName = "decimal(9, 2)")]
    public decimal? CompValue { get; set; }

    [Column("WAIT_STATUS")]
    public int? WaitStatus { get; set; }

    [Column("WAIT_RESON")]
    public int? WaitReson { get; set; }

    [Column("VIST_CONFIRM")]
    public int? VistConfirm { get; set; }

    [Column("WAIT_DATE", TypeName = "datetime")]
    public DateTime? WaitDate { get; set; }

    [Column("WAIT_CONV")]
    public int? WaitConv { get; set; }

    [Column("WAIT_OTHER")]
    public int? WaitOther { get; set; }

    [Column("PAT_ID")]
    public int? PatId { get; set; }

    [Column("MAIN_INV_NO")]
    public long? MainInvNo { get; set; }

    [Column("USER_CODE_CREATE")]
    public int? UserCodeCreate { get; set; }

    [Column("USER_CODE_MODIFY")]
    public int? UserCodeModify { get; set; }

    [Column("CONV_DATE", TypeName = "datetime")]
    public DateTime? ConvDate { get; set; }

    [Column("STOCK_CODE")]
    public int? StockCode { get; set; }

    [Column("SUB_CODE")]
    [StringLength(6)]
    [Unicode(false)]
    public string? SubCode { get; set; }

    [Column("MAIN_CODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string? MainCode { get; set; }

    [Column("ITEM_MASTER")]
    [StringLength(30)]
    [Unicode(false)]
    public string? ItemMaster { get; set; }

    [Column("TRANS_TYPE")]
    public int? TransType { get; set; }

    [Column("SERIAL")]
    public int? Serial { get; set; }

    [Column("SER_SYS")]
    public int? SerSys { get; set; }

    [Column("REPLCATE")]
    public int? Replcate { get; set; }

    [Column("RESERVE_FLAG")]
    public int? ReserveFlag { get; set; }

    [Column("UNIT_PRICE", TypeName = "decimal(9, 2)")]
    public decimal? UnitPrice { get; set; }

    [Column("PAT_AD_DATE", TypeName = "datetime")]
    public DateTime? PatAdDate { get; set; }

    [Column("DR_SEND")]
    public int? DrSend { get; set; }

    [Column("EXCHANGE_TYPE")]
    public int? ExchangeType { get; set; }

    [Column("DOC_NO")]
    public int? DocNo { get; set; }

    [Column("MACHINE")]
    public int? Machine { get; set; }

    [Column("MCH_HOURS", TypeName = "decimal(6, 2)")]
    public decimal? MchHours { get; set; }

    [Column("ITM_SERV_FLAG")]
    public int? ItmServFlag { get; set; }

    [Column("BENEFICIARY_NAME")]
    [StringLength(100)]
    [Unicode(false)]
    public string? BeneficiaryName { get; set; }

    [Column("KINSHIP")]
    public int? Kinship { get; set; }

    [Column("DR_ALALYSIS")]
    public int? DrAlalysis { get; set; }

    [Column("NERSE1")]
    public int? Nerse1 { get; set; }

    [Column("NERSE2")]
    public int? Nerse2 { get; set; }

    [Column("DATE_ANALYSIS", TypeName = "datetime")]
    public DateTime? DateAnalysis { get; set; }

    [Column("FLAG1")]
    public int? Flag1 { get; set; }

    [Column("SERIVICE_WITH_ITEMS")]
    public int? SeriviceWithItems { get; set; }

    [Column("DR_SEND_VAL", TypeName = "decimal(11, 2)")]
    public decimal? DrSendVal { get; set; }

    [Column("FLAG_DR_DISC")]
    public int? FlagDrDisc { get; set; }

    [Column("COST_PRICE", TypeName = "decimal(12, 2)")]
    public decimal? CostPrice { get; set; }

    [Column("DISCOUNT_VAL", TypeName = "decimal(11, 2)")]
    public decimal? DiscountVal { get; set; }

    [Column("PIPE_FLAG")]
    public int? PipeFlag { get; set; }

    [Column("POST_ID")]
    public int? PostId { get; set; }

    [Column("ITM_BARCODE")]
    [StringLength(50)]
    [Unicode(false)]
    public string? ItmBarcode { get; set; }

    [Column("ENTRY_DATE", TypeName = "datetime")]
    public DateTime? EntryDate { get; set; }

    [Column("DEAL")]
    public int? Deal { get; set; }

    [Column("APPROVAL_PERIOD")]
    public int? ApprovalPeriod { get; set; }

    [Column("APPROVAL_DATE", TypeName = "datetime")]
    public DateTime? ApprovalDate { get; set; }

    [Column("USER_APPROVAL")]
    public int? UserApproval { get; set; }

    [Column("ATTENDANCE_SER")]
    public int? AttendanceSer { get; set; }

    [Column("PROCEDURE_ID")]
    public int? ProcedureId { get; set; }

    [Column("BR_REPLC")]
    [StringLength(20)]
    [Unicode(false)]
    public string? BrReplc { get; set; }

    [Column("SEND_FR")]
    public int? SendFr { get; set; }

    [Column("SEND_TO")]
    public int? SendTo { get; set; }

    [Column("ITEM_VISIBLE")]
    public int? ItemVisible { get; set; }

    [Column("INCOMING_FR")]
    public int? IncomingFr { get; set; }

    [Column("EXTRA_VAL", TypeName = "decimal(9, 2)")]
    public decimal? ExtraVal { get; set; }

    [Column("COMP_ID_DTL")]
    public int? CompIdDtl { get; set; }

    [Column("MAIN_INV_NO_ALL")]
    public int? MainInvNoAll { get; set; }

    [Column("MAIN_INV_NO_TAX")]
    public int? MainInvNoTax { get; set; }

    [Column("MAIN_INV_TOT")]
    public int? MainInvTot { get; set; }

    [Column("SALSE_PRICE", TypeName = "decimal(12, 3)")]
    public decimal? SalsePrice { get; set; }

    [Column("DR_DUE_VAL", TypeName = "decimal(9, 2)")]
    public decimal? DrDueVal { get; set; }

    [Column("PAT_ID_SECTION")]
    public int? PatIdSection { get; set; }

    [Column("USER_SAFE")]
    public int? UserSafe { get; set; }

    [Column("CSH_ORD_NUM")]
    public int? CshOrdNum { get; set; }

    [Column("SESSION_NO")]
    public int? SessionNo { get; set; }

    [Column("KNOW_US_FR")]
    public short? KnowUsFr { get; set; }

    [Column("EXTRA_VAL2", TypeName = "decimal(9, 2)")]
    public decimal? ExtraVal2 { get; set; }

    [Column("SUP_CODE")]
    [StringLength(6)]
    [Unicode(false)]
    public string? SupCode { get; set; }

    [ForeignKey("ClinicId")]
    [InverseProperty("ClinicTrans")]
    public virtual MainClinic? Clinic { get; set; }

    [ForeignKey("DrSend")]
    [InverseProperty("ClinicTrans")]
    public virtual Doctor? DrSendNavigation { get; set; }

    [ForeignKey("MainId")]
    [InverseProperty("ClinicTrans")]
    public virtual MainItem? Main { get; set; }

    [ForeignKey("MasterId")]
    [InverseProperty("ClinicTrans")]
    public virtual PatAdmission? Master { get; set; }

    [ForeignKey("SClinicId")]
    [InverseProperty("ClinicTrans")]
    public virtual SubClinic? SClinic { get; set; }

    [ForeignKey("ServId")]
    [InverseProperty("ClinicTrans")]
    public virtual ServiceClinic? Serv { get; set; }

    [ForeignKey("SubId")]
    [InverseProperty("ClinicTrans")]
    public virtual SubItem? Sub { get; set; }
}
