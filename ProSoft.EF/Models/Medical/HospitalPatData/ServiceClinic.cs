using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using ProSoft.EF.Models.Stocks;

namespace ProSoft.EF.Models.Medical.HospitalPatData;

[Table("SERVICE_CLINIC")]
public partial class ServiceClinic
{
    [Key]
    [Column("SERV_ID")]
    public int ServId { get; set; }

    [Column("CLINIC_ID")]
    public int? ClinicId { get; set; }

    [Column("S_CLINIC_ID")]
    public int? SClinicId { get; set; }

    [Column("BRANCH_ID")]
    public int? BranchId { get; set; }

    [Column("SERV_DESC")]
    [StringLength(100)]
    [Unicode(false)]
    public string? ServDesc { get; set; }

    [Column("SEC_ID")]
    public int? SecId { get; set; }

    [Column("TYP_ID")]
    public int? TypId { get; set; }

    [Column("REPLCATE")]
    public int? Replcate { get; set; }

    [Column("DR_PERC", TypeName = "decimal(5, 2)")]
    public decimal? DrPerc { get; set; }

    [Column("PL_VALUE", TypeName = "decimal(9, 2)")]
    public decimal? PlValue { get; set; }

    [Column("SERV_TYPE")]
    public int? ServType { get; set; }

    [Column("DR_VAL", TypeName = "decimal(11, 2)")]
    public decimal? DrVal { get; set; }

    [Column("SERV_VAL", TypeName = "decimal(11, 2)")]
    public decimal? ServVal { get; set; }

    [Column("PROTECT_ID")]
    public int? ProtectId { get; set; }

    [Column("SERV_ON_OFF")]
    public int? ServOnOff { get; set; }

    [Column("ENTRY_DATE", TypeName = "datetime")]
    public DateTime? EntryDate { get; set; }

    [Column("MODFY_DATE", TypeName = "datetime")]
    public DateTime? ModfyDate { get; set; }

    [Column("USER_ENTRY")]
    public int? UserEntry { get; set; }

    [Column("USER_MODIFY")]
    public int? UserModify { get; set; }

    [Column("SERV_COST_VAL", TypeName = "decimal(11, 2)")]
    public decimal? ServCostVal { get; set; }

    [Column("CLINIC_ID_NEW")]
    public int? ClinicIdNew { get; set; }

    [Column("S_CLINIC_ID_NEW")]
    public int? SClinicIdNew { get; set; }

    [Column("SERV_ID_NEW")]
    public int? ServIdNew { get; set; }

    [Column("FLAG")]
    public int? Flag { get; set; }

    [Column("MEDICAL_FLAG")]
    public int? MedicalFlag { get; set; }

    [Column("BR_REPLC")]
    [StringLength(20)]
    [Unicode(false)]
    public string? BrReplc { get; set; }

    [Column("COST_CODE")]
    public int? CostCode { get; set; }

    [InverseProperty("Serv")]
    public virtual ICollection<ClinicTran> ClinicTrans { get; set; } = new List<ClinicTran>();

    [ForeignKey("CostCode")]
    [InverseProperty("ServiceClinics")]
    public virtual CostCenter? CostCodeNavigation { get; set; }

    [InverseProperty("SubDetailCodeL1Navigation")]
    public virtual ICollection<DoctorsPercent> DoctorsPercents { get; set; } = new List<DoctorsPercent>();

    [InverseProperty("Serv")]
    public virtual ICollection<PriceListDetail> PriceListDetails { get; set; } = new List<PriceListDetail>();

    [ForeignKey("SClinicId")]
    [InverseProperty("ServiceClinics")]
    public virtual SubClinic? SClinic { get; set; }
}
