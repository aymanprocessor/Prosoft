using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using ProSoft.EF.Models.Stocks;

namespace ProSoft.EF.Models.Medical.HospitalPatData;

[Table("SUB_CLINIC")]
public partial class SubClinic
{
    [Key]
    [Column("S_CLINIC_ID")]
    public int SClinicId { get; set; }

    [Column("S_CLINIC_DESC")]
    [StringLength(100)]
    [Unicode(false)]
    public string? SClinicDesc { get; set; }

    [Column("BRANCH_ID")]
    public int? BranchId { get; set; }

    [Column("CLINIC_ID")]
    public int? ClinicId { get; set; }

    [Column("TYP_ID")]
    public int? TypId { get; set; }

    [Column("REPLCATE")]
    public int? Replcate { get; set; }

    [Column("TAX_PREC", TypeName = "decimal(4, 2)")]
    public decimal? TaxPrec { get; set; }

    [Column("S_ON_OFF")]
    public int? SOnOff { get; set; }

    [Column("ENTRY_DATE", TypeName = "datetime")]
    public DateTime? EntryDate { get; set; }

    [Column("MODFY_DATE", TypeName = "datetime")]
    public DateTime? ModfyDate { get; set; }

    [Column("USER_ENTRY")]
    public int? UserEntry { get; set; }

    [Column("USER_MODIFY")]
    public int? UserModify { get; set; }

    [Column("FLAG_N")]
    public int? FlagN { get; set; }

    [Column("MEDICAL_FLAG")]
    public int? MedicalFlag { get; set; }

    [Column("STOCK_CD")]
    public int? StockCd { get; set; }

    [Column("BR_REPLC")]
    [StringLength(20)]
    [Unicode(false)]
    public string? BrReplc { get; set; }

    [Column("EINV_ITEM")]
    [StringLength(30)]
    [Unicode(false)]
    public string? EinvItem { get; set; }

    [Column("COST_CODE")]
    public int? CostCode { get; set; }

    [Column("SRV_INV_SHOW_FLG")]
    [StringLength(1)]
    [Unicode(false)]
    public string? SrvInvShowFlg { get; set; }

    [ForeignKey("ClinicId")]
    [InverseProperty("SubClinics")]
    public virtual MainClinic? Clinic { get; set; }

    [InverseProperty("SClinic")]
    public virtual ICollection<ClinicTran> ClinicTrans { get; set; } = new List<ClinicTran>();

    [ForeignKey("CostCode")]
    [InverseProperty("SubClinics")]
    public virtual CostCenter? CostCodeNavigation { get; set; }

    [InverseProperty("SClinic")]
    public virtual ICollection<DocSubDtl> DocSubDtls { get; set; } = new List<DocSubDtl>();

    [InverseProperty("SubCodeNavigation")]
    public virtual ICollection<DoctorsPercent> DoctorsPercents { get; set; } = new List<DoctorsPercent>();

    [InverseProperty("SClinic")]
    public virtual ICollection<ServiceClinic> ServiceClinics { get; set; } = new List<ServiceClinic>();

    [ForeignKey("StockCd")]
    [InverseProperty("SubClinics")]
    public virtual Stock? StockCdNavigation { get; set; }

    [ForeignKey("TypId")]
    [InverseProperty("SubClinics")]
    public virtual ServiceType? Typ { get; set; }
}
