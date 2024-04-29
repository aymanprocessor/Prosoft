using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProSoft.EF.Models.Stocks;

[Keyless]
[Table("PLACE_CODE")]
public partial class PlaceCode
{
    [Column("PLACE_CODE")]
    [StringLength(3)]
    [Unicode(false)]
    public string? PlaceCode1 { get; set; }

    [Column("PLACE_NAME")]
    [StringLength(60)]
    [Unicode(false)]
    public string? PlaceName { get; set; }

    [Column("CITY_CODE")]
    [StringLength(3)]
    [Unicode(false)]
    public string? CityCode { get; set; }

    [Column("BRANCH")]
    public int? Branch { get; set; }
}
