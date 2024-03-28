using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProSoft.EF.Models.Medical.Analysis;

[PrimaryKey("CoCode", "MainCode", "SubCode")]
[Table("MEDICAL_ANALYSIS_SUB")]
public partial class MedicalAnalysisSub
{
    [Key]
    [Column("CO_CODE")]
    public double CoCode { get; set; }

    [Key]
    [Column("SUB_CODE")]
    [StringLength(6)]
    [Unicode(false)]
    public string SubCode { get; set; } = null!;

    [Key]
    [Column("MAIN_CODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string MainCode { get; set; } = null!;

    [Column("SUB_NAME")]
    [StringLength(80)]
    [Unicode(false)]
    public string? SubName { get; set; }

    [Column("BR_REPLC")]
    [StringLength(20)]
    [Unicode(false)]
    public string? BrReplc { get; set; }

    [Column("MEDICAL_FLAG")]
    public double? MedicalFlag { get; set; }

    [Column("ANALYSIS_VAL", TypeName = "decimal(11, 2)")]
    public decimal? AnalysisVal { get; set; }

    [Column("SUB_NAME2")]
    [StringLength(150)]
    [Unicode(false)]
    public string? SubName2 { get; set; }

    [Column("ANALYSIS_COST", TypeName = "decimal(11, 2)")]
    public decimal? AnalysisCost { get; set; }
}
