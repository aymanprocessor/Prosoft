using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using ProSoft.EF.Models.Medical.HospitalPatData;
using ProSoft.EF.Models.Shared;

namespace ProSoft.EF.Models.Stocks;

[Table("STOCK")]
public partial class Stock
{
    [Key]
    [Column("STKCOD")]
    public int Stkcod { get; set; }

    [Column("STKNAM")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Stknam { get; set; }

    [Column("FLAG1")]
    public int? Flag1 { get; set; }

    [Column("BRANCH_ID")]
    public int? BranchId { get; set; }

    [Column("STOCK_TYPE")]
    public int? StockType { get; set; }

    [Column("STK_DEFULT")]
    public int? StkDefult { get; set; }

    [Column("STOCK_PURCH_ONSHELF")]
    public int? StockPurchOnshelf { get; set; }

    [Column("JORNAL_CODE")]
    public int? JornalCode { get; set; }

    [Column("STK_ON_OFF")]
    public int? StkOnOff { get; set; }

    [ForeignKey("BranchId")]
    [InverseProperty("Stocks")]
    public virtual Branch? Branch { get; set; }

    [ForeignKey("Flag1")]
    [InverseProperty("Stocks")]
    public virtual KindStore? Flag1Navigation { get; set; }

    [ForeignKey("JornalCode")]
    [InverseProperty("Stocks")]
    public virtual JournalType? JornalCodeNavigation { get; set; }

    [InverseProperty("StockCdNavigation")]
    public virtual ICollection<SubClinic> SubClinics { get; set; } = new List<SubClinic>();
}
