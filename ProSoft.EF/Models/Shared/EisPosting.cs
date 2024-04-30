using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProSoft.EF.Models.Shared;

[Table("EIS_POSTING")]
public partial class EisPosting
{
    [Key]
    [Column("POST_ID")]
    public int PostId { get; set; }

    [Column("MODULE_N")]
    [StringLength(50)]
    [Unicode(false)]
    public string? ModuleN { get; set; }

    [Column("MAIN_CODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string? MainCode { get; set; }

    [Column("SUB_CODE")]
    [StringLength(6)]
    [Unicode(false)]
    public string? SubCode { get; set; }

    [Column("BRANCH_ID")]
    public int? BranchId { get; set; }
}
