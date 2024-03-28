using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProSoft.EF.Models.Medical.HospitalPatData;

[Table("CLASSIFICATION_CUST")]
public partial class ClassificationCust
{
    [Key]
    [Column("CLASSIFICATION_CUST")]
    public int ClassificationCust1 { get; set; }

    [Column("CLASSIFICATION_DESC")]
    [StringLength(40)]
    [Unicode(false)]
    public string? ClassificationDesc { get; set; }

    [InverseProperty("BrnachInitialNavigation")]
    public virtual ICollection<PatAdmission> PatAdmissions { get; set; } = new List<PatAdmission>();

    [InverseProperty("ClassificationCustNavigation")]
    public virtual ICollection<Region> Regions { get; set; } = new List<Region>();
}
