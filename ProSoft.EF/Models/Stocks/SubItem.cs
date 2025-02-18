using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using ProSoft.EF.Models.Medical.HospitalPatData;

namespace ProSoft.EF.Models.Stocks;

[Table("SUB_ITEM")]
public partial class SubItem
{
    [Key]
    [Column("SUB_ID")]
    public int SubId { get; set; }

    [Column("MAIN_ID")]
    public int? MainId { get; set; }

    [Column("SUB_CODE")]
    [StringLength(6)]
    [Unicode(false)]
    public string? SubCode { get; set; }

    [Column("MAIN_CODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string? MainCode { get; set; }

    [Column("SUB_NAME")]
    [StringLength(150)]
    [Unicode(false)]
    public string? SubName { get; set; }

    [Column("ITEM_CODE")]
    [StringLength(30)]
    [Unicode(false)]
    public string? ItemCode { get; set; }

    [Column("FLAG1")]
    public int? Flag1 { get; set; }

    [Column("MAIN_NAME2")]
    [StringLength(150)]
    [Unicode(false)]
    public string? MainName2 { get; set; }

    [Column("ITEM_PIC")]
    [StringLength(150)]
    [Unicode(false)]
    public string? ItemPic { get; set; }

    [Column("MAIN_SORT")]
    public int? MainSort { get; set; }

    [Column("BRANCH_ID")]
    public int? BranchId { get; set; }

    [Column("OLD_ITEM_CODE")]
    [StringLength(15)]
    [Unicode(false)]
    public string? OldItemCode { get; set; }

    [Column("USER_SORT")]
    public int? UserSort { get; set; }

    [Column("ITEM_COUNTER")]
    public int? ItemCounter { get; set; }

    [Column("SUB")]
    public int? Sub { get; set; }

    [Column("ITEM_PRICE")]
    public decimal? ItemPrice { get; set; }

    [Column("ITEM_TAX")]
    public decimal? ItemTax { get; set; }

    [Column("LEMIT_CALL")]
    public decimal? LemitCall { get; set; }

    [Column("REPLCATE")]
    public int? Replcate { get; set; }

    [Column("BR_REPLC")]
    [StringLength(20)]
    [Unicode(false)]
    public string? BrReplc { get; set; }

    [Column("BATCH_COUNTER")]
    public int? BatchCounter { get; set; }

    [Column("ITEM_GROUP")]
    public int? ItemGroup { get; set; }

    [Column("STENT_ID")]
    public int? StentId { get; set; }

    [Column("PROD_COM")]
    public int? ProdCom { get; set; }

    [Column("SUB_NAME_E")]
    [StringLength(150)]
    [Unicode(false)]
    public string? SubNameE { get; set; }

    [Column("ITEM_ORIGIN")]
    public int? ItemOrigin { get; set; }

    [Column("ITEM_NATURE")]
    public int? ItemNature { get; set; }

    [Column("SCIENTIFIC_GRP")]
    public int? ScientificGrp { get; set; }

    [Column("EFFECTIVE_MATERIAL")]
    public int? EffectiveMaterial { get; set; }

    [Column("ITEM_USE")]
    [StringLength(150)]
    [Unicode(false)]
    public string? ItemUse { get; set; }

    [Column("MEDICINE")]
    public int? Medicine { get; set; }

    [Column("ITEM_FREEZER")]
    public int? ItemFreezer { get; set; }

    [Column("AUTHORITY_DT")]
    public int? AuthorityDt { get; set; }

    [Column("PROTECT_COLUMUN")]
    public int? ProtectColumun { get; set; }

    [Column("ROW_ON_OFF")]
    public int? RowOnOff { get; set; }

    [Column("E_INV_CODE")]
    [StringLength(30)]
    [Unicode(false)]
    public string? EInvCode { get; set; }

    [Column("E_INV_TYPE")]
    [StringLength(5)]
    [Unicode(false)]
    public string? EInvType { get; set; }

    [InverseProperty("Sub")]
    public virtual ICollection<ClinicTran> ClinicTrans { get; set; } = new List<ClinicTran>();

    [ForeignKey("MainId")]
    [InverseProperty("SubItems")]
    public virtual MainItem? Main { get; set; }


}
