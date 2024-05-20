using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using ProSoft.EF.Models.MedicalRecords;

namespace ProSoft.EF.Models.Medical.HospitalPatData;

[Table("PAT")]
public partial class Pat
{
    [Key]
    [Column("PAT_ID")]
    public int PatId { get; set; }

    [Column("BRANCH_ID")]
    public double? BranchId { get; set; }

    [Column("PAT_NAME")]
    [StringLength(100)]
    [Unicode(false)]
    public string? PatName { get; set; }

    [Column("PERSONAL_ID")]
    [StringLength(20)]
    [Unicode(false)]
    public string? PersonalId { get; set; }

    [Column("NEW_OLD")]
    public double? NewOld { get; set; }

    [Column("PAT_ADDRESS")]
    [StringLength(255)]
    [Unicode(false)]
    public string? PatAddress { get; set; }

    [Column("PAT_TEL")]
    [StringLength(14)]
    [Unicode(false)]
    public string? PatTel { get; set; }

    [Column("PAT_MOBILE")]
    [StringLength(14)]
    [Unicode(false)]
    public string? PatMobile { get; set; }

    [Column("BIRTH_DATE", TypeName = "datetime")]
    public DateTime? BirthDate { get; set; }

    [Column("PERSON_KIND")]
    public double? PersonKind { get; set; }

    [Column("PAT_DATE", TypeName = "datetime")]
    public DateTime? PatDate { get; set; }

    [Column("USER_CODE")]
    public double? UserCode { get; set; }

    [Column("PAT_REPEAT")]
    public double? PatRepeat { get; set; }

    [Column("REPLCATE")]
    public double? Replcate { get; set; }

    [Column("PAT_AGE_TYPE")]
    public double? PatAgeType { get; set; }

    [Column("PAT_ID_CARD")]
    public double? PatIdCard { get; set; }

    [Column("SHEET_NO")]
    public double? SheetNo { get; set; }

    [Column("YOUNG_DAY")]
    public double? YoungDay { get; set; }

    [Column("YOUNG_MONTH")]
    public double? YoungMonth { get; set; }

    [Column("BR_REPLC")]
    [StringLength(20)]
    [Unicode(false)]
    public string? BrReplc { get; set; }

    [Column("ENTRY_DATE", TypeName = "datetime")]
    public DateTime? EntryDate { get; set; }

    [Column("PAT_EMAIL")]
    [StringLength(60)]
    [Unicode(false)]
    public string? PatEmail { get; set; }

    [Column("NATIONALITY_ID")]
    public double? NationalityId { get; set; }

    [Column("RELIGION_ID")]
    public double? ReligionId { get; set; }

    [Column("PAT_ID_SER")]
    public long? PatIdSer { get; set; }

    [Column("ID_TYPE")]
    public double? IdType { get; set; }

    [Column("DOC_COMP_NR")]
    [StringLength(15)]
    [Unicode(false)]
    public string? DocCompNr { get; set; }

    [Column("DOC_PAT_NR")]
    [StringLength(6)]
    [Unicode(false)]
    public string? DocPatNr { get; set; }

    [Column("P_DEP")]
    [StringLength(15)]
    [Unicode(false)]
    public string? PDep { get; set; }

    [Column("P_CRT")]
    [StringLength(15)]
    [Unicode(false)]
    public string? PCrt { get; set; }

    [Column("P_GROUP")]
    [StringLength(15)]
    [Unicode(false)]
    public string? PGroup { get; set; }

    [Column("FINANCE_ID")]
    [StringLength(15)]
    [Unicode(false)]
    public string? FinanceId { get; set; }

    [Column("PAT_PASSWORD")]
    [StringLength(15)]
    [Unicode(false)]
    public string? PatPassword { get; set; }

    [Column("MARITAL_STATUS")]
    public int? MaritalStatus { get; set; }

    [Column("PAT_JOB")]
    [StringLength(50)]
    public string? PatJob { get; set; }

    [Column("ENTRY_NO")]
    public double? EntryNo { get; set; }

    [Column("ENTRY_TIME", TypeName = "datetime")]
    public DateTime? EntryTime { get; set; }

    [Column("PAT_HOSPITAL")]
    [StringLength(50)]
    [Unicode(false)]
    public string? PatHospital { get; set; }

    [Column("PAT_SECTOR")]
    [StringLength(50)]
    [Unicode(false)]
    public string? PatSector { get; set; }

    [InverseProperty("Pat")]
    public ICollection<PatAdmission> PatAdmissions { get; set; } = new List<PatAdmission>();

    // Medical Records
    public ICollection<CoronaryAngiographyReport>? CoronaryAngiographyReports {  get; set; }
    public ICollection<DailyFollowUpCcuChant>? DailyFollowUpCcuChants {  get; set; }
    public ICollection<PastHistory>? PastHistories {  get; set; }
    public ICollection<EcgAndEcho>? EcgAndEchoes {  get; set; }
    public ICollection<Echo>? Echoes {  get; set; }
    public ICollection<DischargeSummery>? DischargeSummeries {  get; set; }
    public ICollection<HistoryExamination>? HistoryExaminations { get; set; }
    public ICollection<LabReport>? LabReports { get; set; }
    public ICollection<MedicationAtCcu>? medicationAtCcus { get; set; }
    public ICollection<PciReport>? PciReports { get; set; }
}
