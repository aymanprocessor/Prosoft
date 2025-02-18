using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProSoft.EF.Models.Medical.HospitalPatData;

[Table("DR_DEGREE")]
public partial class DrDegree
{
    [Key]
    [Column("DEGREE_ID")]
    public int DegreeId { get; set; }

    [Column("DEGREE_DESC")]
    [StringLength(100)]
    [Unicode(false)]
    public string? DegreeDesc { get; set; }

    [Column("BRANCH_ID")]
    public int? BranchId { get; set; }

    [Column("DR_PERC")]
    public decimal? DrPerc { get; set; }

    [Column("REPLCATE")]
    public int? Replcate { get; set; }

    [InverseProperty("DrDegreeNavigation")]
    public virtual ICollection<Doctor> Doctors { get; set; } = new List<Doctor>();
}
