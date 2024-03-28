using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProSoft.EF.Models.Medical.Analysis;

[Table("ANALDETAIL")]
public partial class Analdetail
{
    [Key]
    public int Id { get; set; }

    [Column("HEADDATE")]
    [Precision(3)]
    public DateTime? Headdate { get; set; }

    [Column("PAT_ID")]
    public double? PatId { get; set; }

    [Column("CODEANALCODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Codeanalcode { get; set; }

    [Column("ITEMANALCODE")]
    public int? Itemanalcode { get; set; }

    [Column("ITEMANALNAME")]
    [StringLength(100)]
    public string? Itemanalname { get; set; }

    [Column("ITEMRATE")]
    [StringLength(150)]
    public string? Itemrate { get; set; }

    [Column("MASTER_ID")]
    public long? MasterId { get; set; }

    [Column("BRANCH_ID")]
    public double? BranchId { get; set; }

    [Column("MAIN_CODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string? MainCode { get; set; }

    [Column("F_YEAR")]
    public double? FYear { get; set; }

    [Column("SUB_CODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string? SubCode { get; set; }
}
