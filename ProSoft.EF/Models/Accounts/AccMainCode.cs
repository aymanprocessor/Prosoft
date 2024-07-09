using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProSoft.EF.Models.Accounts;

[PrimaryKey("CoCode", "MainCode")]
[Table("ACC_MAIN_CODE")]
public partial class AccMainCode
{
    [Key]
    [Column("CO_CODE")]
    public int CoCode { get; set; }

    [Key]
    [Column("MAIN_CODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string MainCode { get; set; } = null!;

    [Column("PARENT_CODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string? ParentCode { get; set; }

    [Column("MAIN_NAME")]
    [StringLength(100)]
    [Unicode(false)]
    public string? MainName { get; set; }

    [Column("BALANCE_FLAG")]
    [StringLength(1)]
    [Unicode(false)]
    public string? BalanceFlag { get; set; }

    [Column("CURRENT_LEVEL")]
    public int? CurrentLevel { get; set; }

    [Column("BALANCE_REP")]
    [StringLength(1)]
    [Unicode(false)]
    public string? BalanceRep { get; set; }

    [Column("OSTAZ_TYPE")]
    [StringLength(3)]
    [Unicode(false)]
    public string? OstazType { get; set; }

    [Column("BR_REPLC")]
    [StringLength(20)]
    [Unicode(false)]
    public string? BrReplc { get; set; }

    [Column("MAIN_SUB_TYPE")]
    [StringLength(3)]
    [Unicode(false)]
    public string? MainSubType { get; set; }

    [Column("LAST_LEVEL")]
    public int? LastLevel { get; set; }
    // public ICollection<AccSafeCash>? AccSafeCashes { get; set; }

}
