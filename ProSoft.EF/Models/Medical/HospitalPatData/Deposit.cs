using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProSoft.EF.Models.Medical.HospitalPatData;

[Table("DEPOSIT")]
public partial class Deposit
{
    [Key]
    [Column("DPS_SER")]
    public int DpsSer { get; set; }

    [Column("DPS_DATE", TypeName = "datetime")]
    public DateTime? DpsDate { get; set; }

    [Column("DPS_TYPE")]
    public int? DpsType { get; set; }

    [Column("DPS_VAL", TypeName = "decimal(9, 2)")]
    public decimal? DpsVal { get; set; }

    [Column("MOD_ID")]
    public int? ModId { get; set; }

    [Column("MASTER_ID")]
    public int? MasterId { get; set; }

    [Column("F_YEAR")]
    public int? FYear { get; set; }

    [Column("CHECK_NO")]
    [StringLength(20)]
    [Unicode(false)]
    public string? CheckNo { get; set; }

    [Column("CHECK_DATE", TypeName = "datetime")]
    public DateTime? CheckDate { get; set; }

    [Column("BANK_ID")]
    [StringLength(6)]
    [Unicode(false)]
    public string? BankId { get; set; }

    [Column("CASH_NO")]
    public int? CashNo { get; set; }

    [Column("USER_CODE")]
    public int? UserCode { get; set; }

    [Column("BRANCH_ID")]
    public int? BranchId { get; set; }

    [Column("SAFE_DOC_NO")]
    public int? SafeDocNo { get; set; }

    [Column("JOR_KIED_NO")]
    public int? JorKiedNo { get; set; }

    [Column("EXCHANGE_TYPE")]
    public int? ExchangeType { get; set; }

    [Column("POST_RECIPT")]
    public int? PostRecipt { get; set; }

    [Column("DEPOSIT_DESC")]
    [StringLength(200)]
    [Unicode(false)]
    public string? DepositDesc { get; set; }

    [Column("ENTRY_DATE", TypeName = "datetime")]
    public DateTime? EntryDate { get; set; }

    [Column("MODFY_DATE", TypeName = "datetime")]
    public DateTime? ModfyDate { get; set; }

    [Column("USER_MODIFY")]
    public int? UserModify { get; set; }

    [Column("PAT_ID")]
    public int? PatId { get; set; }

    [Column("POST_USER")]
    public int? PostUser { get; set; }
}
