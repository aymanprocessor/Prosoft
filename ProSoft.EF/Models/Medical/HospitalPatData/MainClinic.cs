using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProSoft.EF.Models.Medical.HospitalPatData;

[Table("MAIN_CLINIC")]
public partial class MainClinic
{
    [Key]
    [Column("CLINIC_ID")]
    public int ClinicId { get; set; }

    [Column("CLINIC_DESC")]
    [StringLength(100)]
    [Unicode(false)]
    public string? ClinicDesc { get; set; }

    [Column("BRANCH_ID")]
    public int? BranchId { get; set; }

    [Column("REPLCATE")]
    public int? Replcate { get; set; }

    [Column("FLAG")]
    public int? Flag { get; set; }

    [Column("M_ON_OFF")]
    public double? MOnOff { get; set; }

    [Column("ENTRY_DATE", TypeName = "datetime")]
    public DateTime? EntryDate { get; set; }

    [Column("MODFY_DATE", TypeName = "datetime")]
    public DateTime? ModfyDate { get; set; }

    [Column("USER_ENTRY")]
    public double? UserEntry { get; set; }

    [Column("USER_MODIFY")]
    public double? UserModify { get; set; }

    [Column("FLAG_N")]
    public double? FlagN { get; set; }

    [Column("SYS_SECTION")]
    public double? SysSection { get; set; }

    [Column("BR_REPLC")]
    [StringLength(20)]
    [Unicode(false)]
    public string? BrReplc { get; set; }

    [InverseProperty("Clinic")]
    public virtual ICollection<ClinicTran> ClinicTrans { get; set; } = new List<ClinicTran>();

    [InverseProperty("Clinic")]
    public virtual ICollection<SubClinic> SubClinics { get; set; } = new List<SubClinic>();
}
