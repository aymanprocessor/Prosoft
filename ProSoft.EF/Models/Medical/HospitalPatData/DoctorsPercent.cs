using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProSoft.EF.Models.Medical.HospitalPatData;

[Table("DOCTORS_PERCENT")]
public partial class DoctorsPercent
{
    [Key]
    [Column("DR_PERCENT")]
    public int DrPercent { get; set; }

    [Column("BRANCH_ID")]
    public int? BranchId { get; set; }

    [Column("DR_CODE")]
    public int? DrCode { get; set; }

    [Column("MAIN_CODE")]
    public int? MainCode { get; set; }

    [Column("SUB_CODE")]
    public int? SubCode { get; set; }

    [Column("SUB_DETAIL_CODE_L1")]
    public int? SubDetailCodeL1 { get; set; }

    [Column("DR_PERC")]
    public int? DrPerc { get; set; }

    [Column("SRV_FLAG")]
    public int? SrvFlag { get; set; }

    [Column("DR_VAL")]
    public decimal? DrVal { get; set; }

    [Column("VALUE_SERVICE")]
    public decimal? ValueService { get; set; }

    [Column("DR_VAL_CONTRACT")]
    public decimal? DrValContract { get; set; }

    [Column("DR_PERC_CONTRACT")]
    public decimal? DrPercContract { get; set; }

    [Column("DR_VAL_FLG")]
    public int? DrValFlg { get; set; }

    [ForeignKey("DrCode")]
    [InverseProperty("DoctorsPercents")]
    public virtual Doctor? DrCodeNavigation { get; set; }

    [ForeignKey("SubCode")]
    [InverseProperty("DoctorsPercents")]
    public virtual SubClinic? SubCodeNavigation { get; set; }

    [ForeignKey("SubDetailCodeL1")]
    [InverseProperty("DoctorsPercents")]
    public virtual ServiceClinic? SubDetailCodeL1Navigation { get; set; }
}
