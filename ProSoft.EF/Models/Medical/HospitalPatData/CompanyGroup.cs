using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProSoft.EF.Models.Medical.HospitalPatData;

[Table("COMPANY_GROUP")]
public partial class CompanyGroup
{
    [Key]
    [Column("GROUP_ID")]
    public int GroupId { get; set; }

    [Column("GROUP_NAME")]
    [StringLength(100)]
    [Unicode(false)]
    public string? GroupName { get; set; }

    [Column("BRANCH_ID")]
    public int? BranchId { get; set; }

    [Column("REPLCATE")]
    public int? Replcate { get; set; }

    [Column("COMP_GROUP_ON_OFF")]
    public int? CompGroupOnOff { get; set; }
}
