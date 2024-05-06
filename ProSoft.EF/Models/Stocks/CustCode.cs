using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProSoft.EF.Models.Stocks;

[Table("CUST_CODE")]
public partial class CustCode
{
    [Key]
    [Column("CUST")]
    public int Cust { get; set; }

    [Column("CUST_CODE")]
    [StringLength(6)]
    [Unicode(false)]
    public string? CustCode1 { get; set; }

    [Column("CUST_NAME")]
    [StringLength(70)]
    [Unicode(false)]
    public string? CustName { get; set; }

    [Column("CUST_ADD")]
    [StringLength(100)]
    [Unicode(false)]
    public string? CustAdd { get; set; }

    [Column("CUST_PHONE1")]
    [StringLength(15)]
    [Unicode(false)]
    public string? CustPhone1 { get; set; }

    [Column("CUST_PHONE2")]
    [StringLength(15)]
    [Unicode(false)]
    public string? CustPhone2 { get; set; }

    [Column("CUST_FAX")]
    [StringLength(15)]
    [Unicode(false)]
    public string? CustFax { get; set; }

    [Column("REMARKS")]
    [StringLength(80)]
    [Unicode(false)]
    public string? Remarks { get; set; }

    [Column("VAL_DEPT", TypeName = "decimal(17, 3)")]
    public decimal? ValDept { get; set; }

    [Column("VAL_CREDIT", TypeName = "decimal(17, 3)")]
    public decimal? ValCredit { get; set; }

    [Column("CUST_LIMIT", TypeName = "decimal(15, 2)")]
    public decimal? CustLimit { get; set; }

    [Column("DEU_PERIOD")]
    public int? DeuPeriod { get; set; }

    [Column("CUST_DISC1", TypeName = "decimal(4, 2)")]
    public decimal? CustDisc1 { get; set; }

    [Column("CUST_DISC2", TypeName = "decimal(4, 2)")]
    public decimal? CustDisc2 { get; set; }

    [Column("CUST_DISC3", TypeName = "decimal(4, 2)")]
    public decimal? CustDisc3 { get; set; }

    [Column("CUST_DISC4")]
    public int? CustDisc4 { get; set; }

    [Column("CUST_DISC5", TypeName = "decimal(4, 2)")]
    public decimal? CustDisc5 { get; set; }

    [Column("TAX_TYPE")]
    [StringLength(2)]
    [Unicode(false)]
    public string? TaxType { get; set; }

    [Column("SM")]
    public int? Sm { get; set; }

    [Column("CLASSIFICATION_CUST")]
    public int? ClassificationCust { get; set; }

    [Column("REGION_CODE")]
    [StringLength(2)]
    [Unicode(false)]
    public string? RegionCode { get; set; }

    [Column("ADJECTIVE_CODE")]
    public int? AdjectiveCode { get; set; }

    [Column("SUB_CODE")]
    [StringLength(6)]
    [Unicode(false)]
    public string? SubCode { get; set; }

    [Column("MAIN_CODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string? MainCode { get; set; }

    [Column("FLAG")]
    public int? Flag { get; set; }

    [Column("CONTACT_PERSON")]
    [StringLength(50)]
    [Unicode(false)]
    public string? ContactPerson { get; set; }

    [Column("EMAIL")]
    [StringLength(40)]
    [Unicode(false)]
    public string? Email { get; set; }

    [Column("CITY_CODE")]
    public int? CityCode { get; set; }

    [Column("AREA_CODE")]
    public int? AreaCode { get; set; }

    [Column("CUST_NAME_E")]
    [StringLength(100)]
    [Unicode(false)]
    public string? CustNameE { get; set; }

    [Column("BRANCH_ID")]
    public int? BranchId { get; set; }

    [Column("CATALOG_ID")]
    public int? CatalogId { get; set; }

    [Column("REPLCATE")]
    public int? Replcate { get; set; }

    [Column("CUST_TAX_TYP")]
    [StringLength(1)]
    [Unicode(false)]
    public string? CustTaxTyp { get; set; }

    [Column("TAX_FILE_NO")]
    [StringLength(30)]
    [Unicode(false)]
    public string? TaxFileNo { get; set; }

    [Column("COUNTRY_CODE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? CountryCode { get; set; }
}
