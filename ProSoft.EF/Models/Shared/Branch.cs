using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using ProSoft.EF.Models.Medical.HospitalPatData;
using ProSoft.EF.Models.Stocks;

namespace ProSoft.EF.Models.Shared;

[Table("BRANCHS")]
public partial class Branch
{
    [Key]
    [Column("BRANCH_ID")]
    public int BranchId { get; set; }

    [Column("BRANCH_DESC")]
    [StringLength(50)]
    [Unicode(false)]
    public string? BranchDesc { get; set; }

    [Column("E_INV_DEAL1")]
    public double? EInvDeal1 { get; set; }

    [Column("E_INV_DEAL2")]
    public double? EInvDeal2 { get; set; }

    [Column("BRANCH_ID_DEFAULT")]
    public double? BranchIdDefault { get; set; }

    [InverseProperty("Branch")]
    public virtual ICollection<PriceListDetail> PriceListDetails { get; set; } = new List<PriceListDetail>();

    [InverseProperty("Branch")]
    public virtual ICollection<PriceList> PriceLists { get; set; } = new List<PriceList>();

    [InverseProperty("Branch")]
    public virtual ICollection<Sections2> Sections2s { get; set; } = new List<Sections2>();

    [InverseProperty("Branch")]
    public virtual ICollection<Stock> Stocks { get; set; } = new List<Stock>();
}
