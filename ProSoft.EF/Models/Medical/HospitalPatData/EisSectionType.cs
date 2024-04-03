using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProSoft.EF.Models.Medical.HospitalPatData;

[Table("EIS_SECTION_TYPES")]
public partial class EisSectionType
{
    [Key]
    [Column("SEC_CODE")]
    public int SecCode { get; set; }

    [Column("SEC_NAME")]
    [StringLength(100)]
    [Unicode(false)]
    public string? SecName { get; set; }

    [Column("SEC_ON_OFF")]
    public int? SecOnOff { get; set; }

    [Column("FLG")]
    public int? Flg { get; set; }

    [Column("OBJ_NAME")]
    [StringLength(100)]
    [Unicode(false)]
    public string? ObjName { get; set; }

    [Column("OBJ_PHARM1")]
    [StringLength(100)]
    [Unicode(false)]
    public string? ObjPharm1 { get; set; }

    [Column("OBJ_PHARM2")]
    [StringLength(100)]
    [Unicode(false)]
    public string? ObjPharm2 { get; set; }

    [Column("CSH_ORD_FLAG")]
    public int? CshOrdFlag { get; set; }

    [InverseProperty("SysSectionNavigation")]
    public virtual ICollection<MainClinic> MainClinics { get; set; } = new List<MainClinic>();
}
