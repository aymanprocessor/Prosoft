using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using ProSoft.EF.Models.Shared;

namespace ProSoft.EF.Models.Medical.HospitalPatData;

[Table("PRICE_LIST")]
public partial class PriceList
{
    [Key]
    [Column("P_L_ID")]
    public int PLId { get; set; }

    [Column("PL_DESC")]
    [StringLength(100)]
    [Unicode(false)]
    public string? PlDesc { get; set; }

    [Column("P_L_DATE", TypeName = "datetime")]
    public DateTime? PLDate { get; set; }

    [Column("BRANCH_ID")]
    public int? BranchId { get; set; }

    [Column("YEAR")]
    public int? Year { get; set; }

    [Column("ACTIVE_LIST")]
    public int? ActiveList { get; set; }

    [Column("REPLCATE")]
    public int? Replcate { get; set; }

    [Column("FLAG1")]
    public int? Flag1 { get; set; }

    [Column("BR_REPLC")]
    [StringLength(20)]
    [Unicode(false)]
    public string? BrReplc { get; set; }

    [Column("SERV_ON_OFF")]
    public int? ServOnOff { get; set; }

    [Column("ENTRY_DATE", TypeName = "datetime")]
    public DateTime? EntryDate { get; set; }

    [Column("MODFY_DATE", TypeName = "datetime")]
    public DateTime? ModfyDate { get; set; }

    [Column("USER_MODIFY")]
    public int? UserModify { get; set; }

    [Column("P_TYPE")]
    public int? PType { get; set; }

    [Column("CHECK_ADD_TO_COMPANY")]
    public int? CheckAddToCompany { get; set; }

    [ForeignKey("BranchId")]
    [InverseProperty("PriceLists")]
    public virtual Branch? Branch { get; set; }

    [InverseProperty("PL")]
    public virtual ICollection<Company> Companies { get; set; } = new List<Company>();

    [InverseProperty("PL")]
    public virtual ICollection<PriceListDetail> PriceListDetails { get; set; } = new List<PriceListDetail>();
}
