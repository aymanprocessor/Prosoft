using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using ProSoft.EF.Models.Medical.HospitalPatData;
using ProSoft.EF.Models.Treasury;

namespace ProSoft.EF.Models.Stocks;

[Table("COST_CENTERS")]
public partial class CostCenter
{
    [Key]
    [Column("COST_CODE")]
    public int CostCode { get; set; }

    [Column("COST_DESC")]
    [StringLength(100)]
    [Unicode(false)]
    public string? CostDesc { get; set; }

    [Column("COST_FLAG")]
    public int? CostFlag { get; set; }

    [Column("DEPIT_VAL", TypeName = "decimal(11, 3)")]
    public decimal? DepitVal { get; set; }

    [Column("CREDIT_VAL", TypeName = "decimal(11, 3)")]
    public decimal? CreditVal { get; set; }

    [Column("F_YEAR")]
    public int? FYear { get; set; }

    [Column("COST_VISIBLE")]
    public int? CostVisible { get; set; }

    [InverseProperty("CostCodeNavigation")]
    public virtual ICollection<ServiceClinic> ServiceClinics { get; set; } = new List<ServiceClinic>();

    [InverseProperty("CostCodeNavigation")]
    public virtual ICollection<SubClinic> SubClinics { get; set; } = new List<SubClinic>();

    public ICollection<AccSafeCash>? AccSafeCashes { get; set; }

}
