using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProSoft.EF.Models.Medical.HospitalPatData;

[Table("REGIONS")]
public partial class Region
{
    [Key]
    [Column("REGION_CODE")]
    public int RegionCode { get; set; }

    [Column("REGION_DESC")]
    [StringLength(50)]
    [Unicode(false)]
    public string? RegionDesc { get; set; }

    [Column("CLASSIFICATION_CUST")]
    public int? ClassificationCust { get; set; }

    [Column("FLAG")]
    public int? Flag { get; set; }

    [Column("QTY_ORE")]
    public decimal? QtyOre { get; set; }

    [Column("QTY_IN")]
    public decimal? QtyIn { get; set; }

    [Column("QTY_OUT")]
    public decimal? QtyOut { get; set; }

    [Column("REG_DATE")]
    [Precision(6)]
    public DateTime? RegDate { get; set; }

    [Column("REG_YEAR")]
    public int? RegYear { get; set; }

    [Column("STOCK_CODE")]
    public int? StockCode { get; set; }

    [Column("BRANCH_ID")]
    public int? BranchId { get; set; }

    [Column("ON_OFF")]
    public int? OnOff { get; set; }

    [Column("SECTION_CODE")]
    public int SectionCode { get; set; }

    [ForeignKey("ClassificationCust")]
    [InverseProperty("Regions")]
    public virtual ClassificationCust? ClassificationCustNavigation { get; set; }

    [InverseProperty("SendFrNavigation")]
    public virtual ICollection<PatAdmission> PatAdmissions { get; set; } = new List<PatAdmission>();
}
