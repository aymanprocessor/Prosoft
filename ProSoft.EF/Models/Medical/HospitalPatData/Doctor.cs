using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProSoft.EF.Models.Medical.HospitalPatData;

[Table("DOCTORS")]
public partial class Doctor
{
    [Column("BRANCH_ID")]
    public int BranchId { get; set; }

    [Key]
    [Column("DR_ID")]
    public int DrId { get; set; }

    [Column("DR_DESC")]
    [StringLength(100)]
    [Unicode(false)]
    public string? DrDesc { get; set; }

    [Column("DR_DEGREE")]
    public int? DrDegree { get; set; }

    [Column("CLINIC_ID")]
    public int? ClinicId { get; set; }

    [Column("REPLCATE")]
    public int? Replcate { get; set; }

    [Column("VISITOR_PER_DAY")]
    public int? VisitorPerDay { get; set; }

    [Column("CONTRACT_TYP")]
    public int? ContractTyp { get; set; }

    [Column("S_CLINIC_ID")]
    public int? SClinicId { get; set; }

    [Column("JOP_TYP")]
    public int? JopTyp { get; set; }

    [Column("TAXABLE")]
    public int? Taxable { get; set; }

    [Column("SUB_CODE")]
    [StringLength(6)]
    [Unicode(false)]
    public string? SubCode { get; set; }

    [Column("MAIN_CODE")]
    [StringLength(10)]
    [Unicode(false)]
    public string? MainCode { get; set; }

    [Column("SHAREHOLDER")]
    public int? Shareholder { get; set; }

    [Column("TEL_NO1")]
    [StringLength(15)]
    [Unicode(false)]
    public string? TelNo1 { get; set; }

    [Column("TEL_NO2")]
    [StringLength(15)]
    [Unicode(false)]
    public string? TelNo2 { get; set; }

    [Column("DR_ADDRESS")]
    [StringLength(100)]
    [Unicode(false)]
    public string? DrAddress { get; set; }

    [Column("OTHER_DESC")]
    [StringLength(150)]
    [Unicode(false)]
    public string? OtherDesc { get; set; }

    [Column("VISIT_VAL", TypeName = "decimal(11, 2)")]
    public decimal? VisitVal { get; set; }

    [Column("BR_REPLC")]
    [StringLength(20)]
    [Unicode(false)]
    public string? BrReplc { get; set; }

    [Column("DR_ON_OFF")]
    public int? DrOnOff { get; set; }

    [Column("DR_TYPE")]
    public int? DrType { get; set; }

    [InverseProperty("DrSendNavigation")]
    public virtual ICollection<ClinicTran> ClinicTrans { get; set; } = new List<ClinicTran>();

    [InverseProperty("DrCodeNavigation")]
    public virtual ICollection<PatAdmission> PatAdmissions { get; set; } = new List<PatAdmission>();
}
