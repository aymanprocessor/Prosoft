using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProSoft.EF.Models.Stocks;

[Table("KIND_STORE")]
public partial class KindStore
{
    [Key]
    [Column("K_ID")]
    public int KId { get; set; }

    [Column("K_NAME")]
    [StringLength(50)]
    [Unicode(false)]
    public string? KName { get; set; }

    [Column("K_TYPE")]
    public int? KType { get; set; }

    [Column("STOCK_TYPE")]
    public int? StockType { get; set; }

    [Column("K_STK_ON_OFF")]
    public int? KStkOnOff { get; set; }
}
