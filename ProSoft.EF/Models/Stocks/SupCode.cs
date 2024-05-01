using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProSoft.EF.Models.Stocks;

[Table("SUP_CODE")]
public partial class SupCode
{
    [Key]
    [Column("SUP")]
    public int Sup { get; set; }

    [Column("SUP_CODE")]
    [StringLength(6)]
    [Unicode(false)]
    public string? SupCode1 { get; set; }

    [Column("SUP_NAME")]
    [StringLength(70)]
    [Unicode(false)]
    public string? SupName { get; set; }

    [Column("SUP_ADD")]
    [StringLength(100)]
    [Unicode(false)]
    public string? SupAdd { get; set; }

    [Column("SUP_PHONE1")]
    [StringLength(15)]
    [Unicode(false)]
    public string? SupPhone1 { get; set; }

    [Column("SUP_PHONE2")]
    [StringLength(15)]
    [Unicode(false)]
    public string? SupPhone2 { get; set; }

    [Column("SUP_FAX")]
    [StringLength(15)]
    [Unicode(false)]
    public string? SupFax { get; set; }

    [Column("REMARKS")]
    [StringLength(80)]
    [Unicode(false)]
    public string? Remarks { get; set; }

    [Column("VAL_DEPT", TypeName = "decimal(17, 3)")]
    public decimal? ValDept { get; set; }

    [Column("VAL_CREDIT", TypeName = "decimal(17, 3)")]
    public decimal? ValCredit { get; set; }

    [Column("SUB_CODE")]
    [StringLength(6)]
    [Unicode(false)]
    public string? SubCode { get; set; }

    [Column("MAIN_CODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string? MainCode { get; set; }

    [Column("EMAIL")]
    [StringLength(40)]
    [Unicode(false)]
    public string? Email { get; set; }

    [Column("CITY_CODE")]
    public int? CityCode { get; set; }

    [Column("AREA_CODE")]
    public int? AreaCode { get; set; }

    [Column("BRANCH_ID")]
    public int? BranchId { get; set; }

    [Column("PERSON_NAME")]
    [StringLength(30)]
    [Unicode(false)]
    public string? PersonName { get; set; }

    [Column("SUP_TYPE")]
    public short? SupType { get; set; }

    [Column("REPLCATE")]
    public int? Replcate { get; set; }

    [Column("BR_REPLC")]
    [StringLength(20)]
    [Unicode(false)]
    public string? BrReplc { get; set; }
}
