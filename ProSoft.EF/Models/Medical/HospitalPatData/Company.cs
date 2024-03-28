using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProSoft.EF.Models.Medical.HospitalPatData;

[Table("COMPANY")]
public partial class Company
{
    [Key]
    [Column("COMP_ID")]
    public int CompId { get; set; }

    [Column("GROUP_ID", TypeName = "decimal(38, 0)")]
    public decimal? GroupId { get; set; }

    [Column("COMP_NAME")]
    [StringLength(255)]
    [Unicode(false)]
    public string? CompName { get; set; }

    [Column("COM_ADD")]
    [StringLength(255)]
    [Unicode(false)]
    public string? ComAdd { get; set; }

    [Column("COM_TYPE")]
    [StringLength(255)]
    [Unicode(false)]
    public string? ComType { get; set; }

    [Column("COMP_TELEPHONE")]
    [StringLength(20)]
    [Unicode(false)]
    public string? CompTelephone { get; set; }

    [Column("COMP_FAX")]
    [StringLength(15)]
    [Unicode(false)]
    public string? CompFax { get; set; }

    [Column("COMP_E_MAIL")]
    [StringLength(255)]
    [Unicode(false)]
    public string? CompEMail { get; set; }

    [Column("MEDICAL_MANAGER")]
    [StringLength(255)]
    [Unicode(false)]
    public string? MedicalManager { get; set; }

    [Column("INSURANCE_PERIOD")]
    [StringLength(255)]
    [Unicode(false)]
    public string? InsurancePeriod { get; set; }

    [Column("RESPONSABLE_MANAGER")]
    [StringLength(255)]
    [Unicode(false)]
    public string? ResponsableManager { get; set; }

    [Column("P_L_ID", TypeName = "decimal(38, 0)")]
    public decimal? PLId { get; set; }

    [Column("P_L_R_CODE", TypeName = "decimal(38, 0)")]
    public decimal? PLRCode { get; set; }

    [Column("OP_PL", TypeName = "decimal(38, 0)")]
    public decimal? OpPl { get; set; }

    [Column("BRANCH_ID", TypeName = "decimal(38, 0)")]
    public decimal? BranchId { get; set; }

    [Column("REPLCATE", TypeName = "decimal(38, 0)")]
    public decimal? Replcate { get; set; }

    [Column("STAMP", TypeName = "decimal(4, 2)")]
    public decimal? Stamp { get; set; }

    [Column("COMP_PER", TypeName = "decimal(5, 2)")]
    public decimal? CompPer { get; set; }

    [Column("COMPANY_TYPE", TypeName = "decimal(38, 0)")]
    public decimal? CompanyType { get; set; }

    [Column("SUB_CODE")]
    [StringLength(6)]
    [Unicode(false)]
    public string? SubCode { get; set; }

    [Column("MAIN_CODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string? MainCode { get; set; }

    [Column("STAMP_PER", TypeName = "decimal(4, 2)")]
    public decimal? StampPer { get; set; }

    [Column("TAX_PER", TypeName = "decimal(4, 2)")]
    public decimal? TaxPer { get; set; }

    [Column("KIND_STORE")]
    public int? KindStore { get; set; }

    [Column("BR_REPLC")]
    [StringLength(20)]
    [Unicode(false)]
    public string? BrReplc { get; set; }

    [Column("COMPANY_ON_OFF")]
    public int? CompanyOnOff { get; set; }

    [Column("TAX_NO")]
    [StringLength(20)]
    [Unicode(false)]
    public string? TaxNo { get; set; }

    [Column("ENTRY_DATE", TypeName = "datetime")]
    public DateTime? EntryDate { get; set; }

    [Column("USER_CREATE")]
    public int? UserCreate { get; set; }

    [Column("USER_MODIFY")]
    public int? UserModify { get; set; }

    [Column("MODIFY_DATE", TypeName = "datetime")]
    public DateTime? ModifyDate { get; set; }

    [Column("E_INV_MAIN_FLG")]
    [StringLength(1)]
    [Unicode(false)]
    public string? EInvMainFlg { get; set; }

    [InverseProperty("Comp")]
    public virtual ICollection<CompanyDtl> CompanyDtls { get; set; } = new List<CompanyDtl>();

    [InverseProperty("Comp")]
    public virtual ICollection<PatAdmission> PatAdmissions { get; set; } = new List<PatAdmission>();
}
