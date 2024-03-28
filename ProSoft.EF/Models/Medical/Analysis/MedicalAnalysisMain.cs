using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProSoft.EF.Models.Medical.Analysis;

[PrimaryKey("CoCode", "MainCode")]
[Table("MEDICAL_ANALYSIS_MAIN")]
public partial class MedicalAnalysisMain
{
    [Key]
    [Column("CO_CODE")]
    public double CoCode { get; set; }

    [Column("PARENT_CODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string? ParentCode { get; set; }

    [Key]
    [Column("MAIN_CODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string MainCode { get; set; } = null!;

    [Column("MAIN_NAME")]
    [StringLength(100)]
    [Unicode(false)]
    public string? MainName { get; set; }

    [Column("CURRENT_LEVEL")]
    public double? CurrentLevel { get; set; }

    [Column("BR_REPLC")]
    [StringLength(20)]
    [Unicode(false)]
    public string? BrReplc { get; set; }

    [Column("MAIN_SUB_TYPE")]
    [StringLength(3)]
    [Unicode(false)]
    public string? MainSubType { get; set; }

    [Column("MEDICAL_FLAG")]
    public double? MedicalFlag { get; set; }
}
