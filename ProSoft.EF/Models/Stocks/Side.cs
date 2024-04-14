using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProSoft.EF.Models.Stocks;

[Table("SIDES")]
public partial class Side
{
    [Key]
    [Column("SIDE_ID")]
    public int SideId { get; set; }

    [Column("SIDE_DESC")]
    [StringLength(100)]
    [Unicode(false)]
    public string? SideDesc { get; set; }
}
