using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProSoft.EF.Models.Medical.Analysis;

[PrimaryKey("Codeanalcode", "Itemanalcode", "Subanalcode")]
[Table("ITEMANALYSES")]
public partial class Itemanalysis
{
    [Key]
    [Column("CODEANALCODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string Codeanalcode { get; set; } = null!;

    [Key]
    [Column("ITEMANALCODE")]
    public int Itemanalcode { get; set; }

    [Column("ITEMANALNAME")]
    [StringLength(100)]
    public string? Itemanalname { get; set; }

    [Column("ITEMANALUNITCODE")]
    public short? Itemanalunitcode { get; set; }

    [Column("ITEMANALNORMALRATE")]
    [Unicode(false)]
    public string? Itemanalnormalrate { get; set; }

    [Column("ITEMANALMALENORMAL")]
    [Unicode(false)]
    public string? Itemanalmalenormal { get; set; }

    [Column("ITEMANALFEMALENORMAL")]
    [Unicode(false)]
    public string? Itemanalfemalenormal { get; set; }

    [Column("ITEMANALRATE")]
    [Unicode(false)]
    public string? Itemanalrate { get; set; }

    [Column("MAINANALCODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string? Mainanalcode { get; set; }

    [Key]
    [Column("SUBANALCODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string Subanalcode { get; set; } = null!;
}
