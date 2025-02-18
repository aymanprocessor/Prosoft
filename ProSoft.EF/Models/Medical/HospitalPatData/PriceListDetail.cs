using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using ProSoft.EF.Models.Shared;

namespace ProSoft.EF.Models.Medical.HospitalPatData;

[Table("PRICE_LIST_DETAIL")]
public partial class PriceListDetail
{
    [Key]
    [Column("P_L_DTL_ID")]
    public int PLDtlId { get; set; }

    [Column("P_L_ID")]
    public int? PLId { get; set; }

    [Column("COMP_COV_PERCENTAGE")]
    public decimal? CompCovPercentage { get; set; }

    [Column("COMP_VALUE")]
    public decimal? CompValue { get; set; }

    [Column("COVERED")]
    public int? Covered { get; set; }

    [Column("CLINIC_ID")]
    public int? ClinicId { get; set; }

    [Column("S_CLINIC_ID")]
    public int? SClinicId { get; set; }

    [Column("SERV_ID")]
    public int? ServId { get; set; }

    [Column("PL_VALUE")]
    public decimal? PlValue { get; set; }

    [Column("P_L_DATE")]
    public DateTime? PLDate { get; set; }

    [Column("P_L_DETAIL_CODE")]
    public int? PLDetailCode { get; set; }

    [Column("BRANCH_ID")]
    public int? BranchId { get; set; }

    [Column("REPLCATE")]
    public int? Replcate { get; set; }

    [Column("PL_VALUE2")]
    public decimal? PlValue2 { get; set; }

    [Column("DISCOUT_COMP")]
    public decimal? DiscoutComp { get; set; }

    [Column("SERV_DESC")]
    [StringLength(100)]
    [Unicode(false)]
    public string? ServDesc { get; set; }

    [Column("SERV_BEF_DESC")]
    public decimal? ServBefDesc { get; set; }

    [Column("DR_PERC")]
    public decimal? DrPerc { get; set; }

    [Column("USER_CODE")]
    public int? UserCode { get; set; }

    [Column("PL_VALUE3")]
    public decimal? PlValue3 { get; set; }

    [Column("COMP_COV_PERCENTAGE2")]
    public decimal? CompCovPercentage2 { get; set; }

    [Column("COMP_VALUE2")]
    public decimal? CompValue2 { get; set; }

    [Column("COST_CARE")]
    public decimal? CostCare { get; set; }

    [Column("BR_REPLC")]
    [StringLength(20)]
    [Unicode(false)]
    public string? BrReplc { get; set; }

    [Column("SERV_ON_OFF")]
    public int? ServOnOff { get; set; }

    [Column("ENTRY_DATE")]
    public DateTime? EntryDate { get; set; }

    [Column("MODFY_DATE")]
    public DateTime? ModfyDate { get; set; }

    [Column("USER_MODIFY")]
    public double? UserModify { get; set; }

    [Column("EXTRA_VAL")]
    public decimal? ExtraVal { get; set; }

    [Column("ITEM_SRV_FLG")]
    public int? ItemSrvFlg { get; set; }

    [Column("MAIN_CODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string? MainCode { get; set; }

    [Column("ITEM_CODE")]
    [StringLength(30)]
    [Unicode(false)]
    public string? ItemCode { get; set; }

    [Column("EXTRA_VAL2")]
    public decimal? ExtraVal2 { get; set; }

    [ForeignKey("BranchId")]
    [InverseProperty("PriceListDetails")]
    public virtual Branch? Branch { get; set; }

    [ForeignKey("ClinicId")]
    [InverseProperty("PriceListDetails")]
    public virtual MainClinic? Clinic { get; set; }

    [ForeignKey("PLId")]
    [InverseProperty("PriceListDetails")]
    public virtual PriceList? PL { get; set; }

    [ForeignKey("SClinicId")]
    [InverseProperty("PriceListDetails")]
    public virtual SubClinic? SClinic { get; set; }

    [ForeignKey("ServId")]
    [InverseProperty("PriceListDetails")]
    public virtual ServiceClinic? Serv { get; set; }
}
