using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProSoft.EF.Models.Shared;

[Table("SECTIONS2")]
public partial class Sections2
{
    [Key]
    [Column("SEC_CD")]
    public int SecCd { get; set; }

    [Column("SEC_DESC")]
    [StringLength(50)]
    [Unicode(false)]
    public string? SecDesc { get; set; }

    [Column("BRANCH_ID")]
    public int? BranchId { get; set; }

    [Column("ON_OFF")]
    public int? OnOff { get; set; }

    [Column("SEC_COST")]
    public int? SecCost { get; set; }

    [ForeignKey("BranchId")]
    [InverseProperty("Sections2s")]
    public virtual Branch? Branch { get; set; }
}
