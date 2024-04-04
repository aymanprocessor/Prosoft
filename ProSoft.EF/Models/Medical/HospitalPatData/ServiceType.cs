using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProSoft.EF.Models.Medical.HospitalPatData;

[Table("SERVICE_TYPE")]
public partial class ServiceType
{
    [Key]
    [Column("TYP_ID")]
    public int TypId { get; set; }

    [Column("TYP_DESC")]
    [StringLength(100)]
    [Unicode(false)]
    public string? TypDesc { get; set; }

    [Column("BRANCH_ID")]
    public int? BranchId { get; set; }

    [Column("REPLCATE")]
    public int? Replcate { get; set; }

    [Column("STOCK_CODE")]
    public int? StockCode { get; set; }

    [InverseProperty("Typ")]
    public virtual ICollection<SubClinic> SubClinics { get; set; } = new List<SubClinic>();
}
