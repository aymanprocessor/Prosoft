using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProSoft.EF.Models.Shared;

[Table("NATIONALITY_EIS")]
public partial class NationalityEi
{
    [Key]
    [Column("NATIONALITY_ID")]
    public int NationalityId { get; set; }

    [Column("NATIONALITY_DESC")]
    [StringLength(60)]
    [Unicode(false)]
    public string? NationalityDesc { get; set; }

    [Column("NATIONALITY_DESC_E")]
    [StringLength(100)]
    [Unicode(false)]
    public string? NationalityDescE { get; set; }

    [Column("PUBLIC_CD")]
    [StringLength(5)]
    [Unicode(false)]
    public string? PublicCd { get; set; }
}
