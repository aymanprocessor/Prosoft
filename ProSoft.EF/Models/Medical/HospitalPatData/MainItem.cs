using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProSoft.EF.Models.Medical.HospitalPatData;

[Table("MAIN_ITEM")]
public partial class MainItem
{
    [Key]
    [Column("MAIN_ID")]
    public int MainId { get; set; }

    [Column("PARENT_CODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string? ParentCode { get; set; }

    [Column("MAIN_CODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string? MainCode { get; set; }

    [Column("MAIN_NAME")]
    [StringLength(100)]
    [Unicode(false)]
    public string? MainName { get; set; }

    [Column("CURRENT_LEVEL")]
    public int? CurrentLevel { get; set; }

    [Column("FLAG1")]
    public int? Flag1 { get; set; }

    [Column("MAIN_NAME2")]
    [StringLength(100)]
    [Unicode(false)]
    public string? MainName2 { get; set; }

    [Column("MAIN_SORT")]
    public int? MainSort { get; set; }

    [Column("BRANCH_ID")]
    public int? BranchId { get; set; }

    [Column("MAIN_NAME_ALL")]
    [StringLength(150)]
    [Unicode(false)]
    public string? MainNameAll { get; set; }

    [Column("LAST_SUB")]
    public int? LastSub { get; set; }

    [Column("USER_CODE")]
    public int? UserCode { get; set; }

    [Column("REPLCATE")]
    public int? Replcate { get; set; }

    [Column("POLICY_PRICE")]
    public int? PolicyPrice { get; set; }

    [Column("SERVICE_PRC", TypeName = "decimal(5, 2)")]
    public decimal? ServicePrc { get; set; }

    [Column("BR_REPLC")]
    [StringLength(20)]
    [Unicode(false)]
    public string? BrReplc { get; set; }

    [Column("BATCH_COUNTER")]
    public int? BatchCounter { get; set; }

    [Column("ROW_ON_OFF")]
    public int? RowOnOff { get; set; }

    [Column("DW_NAME")]
    [StringLength(100)]
    [Unicode(false)]
    public string? DwName { get; set; }

    [Column("E_INV_CODE")]
    [StringLength(30)]
    [Unicode(false)]
    public string? EInvCode { get; set; }

    [Column("E_INV_TYPE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? EInvType { get; set; }

    [InverseProperty("Main")]
    public virtual ICollection<ClinicTran> ClinicTrans { get; set; } = new List<ClinicTran>();

    [InverseProperty("Main")]
    public virtual ICollection<SubItem> SubItems { get; set; } = new List<SubItem>();
}
