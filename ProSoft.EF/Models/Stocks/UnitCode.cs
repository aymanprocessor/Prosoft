using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProSoft.EF.Models.Stocks;

[Table("UNIT_CODE")]
public partial class UnitCode
{
    [Key]
    [Column("CODE")]
    public int Code { get; set; }

    [Column("NAMES")]
    [StringLength(30)]
    [Unicode(false)]
    public string? Names { get; set; }

    [Column("ITEM_QTY", TypeName = "decimal(15, 3)")]
    public decimal? ItemQty { get; set; }

    [Column("FLAG1")]
    public int? Flag1 { get; set; }
}
