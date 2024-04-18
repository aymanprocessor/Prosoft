using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProSoft.EF.Models.Stocks;

[PrimaryKey("UsrId", "GId")]
[Table("USER_TRANSS")]
public partial class UserTranss
{
    [Key]
    [Column("USR_ID")]
    public int UsrId { get; set; }

    [Key]
    [Column("G_ID")]
    public int GId { get; set; }

    [Column("D_TYPE")]
    [StringLength(2)]
    [Unicode(false)]
    public string? DType { get; set; }

    [Column("TRANS_FLAG", TypeName = "decimal(38, 0)")]
    public decimal? TransFlag { get; set; }

    [Column("UE_INS", TypeName = "decimal(38, 0)")]
    public decimal? UeIns { get; set; }

    [Column("UE_SAV", TypeName = "decimal(38, 0)")]
    public decimal? UeSav { get; set; }

    [Column("UE_DEL", TypeName = "decimal(38, 0)")]
    public decimal? UeDel { get; set; }
}
