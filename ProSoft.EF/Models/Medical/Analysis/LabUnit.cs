using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProSoft.EF.Models.Medical.Analysis;

[Table("LAB_UNITS")]
public partial class LabUnit
{
    [Key]
    [Column("LAB_UNIT_CODE")]
    public short LabUnitCode { get; set; }

    [Column("LAB_UNIT_NAME")]
    [StringLength(50)]
    [Unicode(false)]
    public string? LabUnitName { get; set; }
}
