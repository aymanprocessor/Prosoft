using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProSoft.EF.Models.Stocks;

[Table("ADJECTIVE_CUST")]
public partial class AdjectiveCust
{
    [Key]
    [Column("ADJECTIVE_CODE")]
    public int AdjectiveCode { get; set; }

    [Column("ADJECTIVE_DESC")]
    [StringLength(30)]
    [Unicode(false)]
    public string? AdjectiveDesc { get; set; }
}
