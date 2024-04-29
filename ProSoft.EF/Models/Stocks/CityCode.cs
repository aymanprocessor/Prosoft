using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProSoft.EF.Models.Stocks;

[Keyless]
[Table("CITY_CODE")]
public partial class CityCode
{
    [Column("CITY_CODE")]
    [StringLength(3)]
    [Unicode(false)]
    public string? CityCode1 { get; set; }

    [Column("CITY_NAME")]
    [StringLength(60)]
    [Unicode(false)]
    public string? CityName { get; set; }

    [Column("BRANCH")]
    public int? Branch { get; set; }
}
