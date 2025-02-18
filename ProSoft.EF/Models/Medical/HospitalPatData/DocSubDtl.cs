using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProSoft.EF.Models.Medical.HospitalPatData;

[Table("DOC_SUB_DTL")]
public partial class DocSubDtl
{
    [Key]
    [Column("DOC_SUB_ID")]
    public int DocSubId { get; set; }

    [Column("DR_ID")]
    public int? DrId { get; set; }

    [Column("CLINIC_ID")]
    public int? ClinicId { get; set; }

    [Column("S_CLINIC_ID")]
    public int? SClinicId { get; set; }

    [Column("FLAG")]
    public int? Flag { get; set; }

    [Column("BRANCH_ID")]
    public int? BranchId { get; set; }

    [Column("DR_ON_OFF")]
    public int? DrOnOff { get; set; }

    [Column("DOC_SUB_DEF")]
    public int? DocSubDef { get; set; }

    [Column("ENTRY_DATE")]
    public DateTime? EntryDate { get; set; }

    [Column("USER_CREATE")]
    public int? UserCreate { get; set; }

    [Column("USER_MODIFY")]
    public int? UserModify { get; set; }

    [Column("BR_REPLC")]
    [StringLength(20)]
    [Unicode(false)]
    public string? BrReplc { get; set; }

    [ForeignKey("ClinicId")]
    [InverseProperty("DocSubDtls")]
    public virtual MainClinic? Clinic { get; set; }

    [ForeignKey("DrId")]
    [InverseProperty("DocSubDtls")]
    public virtual Doctor? Dr { get; set; }

    [ForeignKey("SClinicId")]
    [InverseProperty("DocSubDtls")]
    public virtual SubClinic? SClinic { get; set; }
}
