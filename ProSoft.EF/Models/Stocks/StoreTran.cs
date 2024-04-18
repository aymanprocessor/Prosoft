using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProSoft.EF.Models.Stocks;

[Table("STORE_TRANS")]
public partial class StoreTran
{
    [Key]
    [Column("TRANS_ID")]
    public int TransId { get; set; }

    [Column("TRANS_DESC")]
    [StringLength(20)]
    [Unicode(false)]
    public string? TransDesc { get; set; }
}
