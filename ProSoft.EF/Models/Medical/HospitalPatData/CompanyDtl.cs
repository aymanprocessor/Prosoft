using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProSoft.EF.Models.Medical.HospitalPatData;

[Table("COMPANY_DTL")]
public partial class CompanyDtl
{
    [Key]
    [Column("COMP_ID_DTL")]
    public int CompIdDtl { get; set; }

    [Column("COMP_ID")]
    public int CompId { get; set; }

    [Column("COMP_NAME_DTL")]
    [StringLength(255)]
    [Unicode(false)]
    public string? CompNameDtl { get; set; }

    [Column("SUB_CODE")]
    [StringLength(6)]
    [Unicode(false)]
    public string? SubCode { get; set; }

    [Column("MAIN_CODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string? MainCode { get; set; }

    [Column("BRANCH_ID")]
    public int? BranchId { get; set; }

    [Column("COMP_ID_DTL_ON_OFF")]
    public int? CompIdDtlOnOff { get; set; }

    [Column("TAX_NO")]
    [StringLength(20)]
    [Unicode(false)]
    public string? TaxNo { get; set; }

    [Column("ENTRY_DATE")]
    public DateOnly? EntryDate { get; set; }

    [Column("USER_CREATE")]
    public int? UserCreate { get; set; }

    [Column("USER_MODIFY")]
    public int? UserModify { get; set; }

    [Column("MODIFY_DATE")]
    public DateOnly? ModifyDate { get; set; }

    [Column("BR_REPLC")]
    [StringLength(20)]
    [Unicode(false)]
    public string? BrReplc { get; set; }

    [Column("EINV_TYPE")]
    public int? EinvType { get; set; }

    [Column("WINV_ITEMS_FLG")]
    [StringLength(1)]
    [Unicode(false)]
    public string? WinvItemsFlg { get; set; }

    [ForeignKey("CompId")]
    [InverseProperty("CompanyDtls")]
    public virtual Company Comp { get; set; } = null!;

    [InverseProperty("CompIdDtlNavigation")]
    public virtual ICollection<PatAdmission> PatAdmissions { get; set; } = new List<PatAdmission>();
}
