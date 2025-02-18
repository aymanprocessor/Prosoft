using Microsoft.EntityFrameworkCore;
using ProSoft.EF.Models.Medical.HospitalPatData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.Models.MedicalRecords
{
    [Table("HISTORY_EXAMINATION")]
    public partial class HistoryExamination
    {
        [Key]
        [Column("SERIAL_HISTORY")]
        public int SerialHistory { get; set; }

        [Column("PAT_SSN")]
        [StringLength(14)]
        [Unicode(false)]
        public string? PatSsn { get; set; }

        [Column("ENTRY_DATE")]
        public DateTime? EntryDate { get; set; }

        [Column("BELONGING_TO_SUB_SPECIALITY")]
        [StringLength(500)]
        [Unicode(false)]
        public string? BelongingToSubSpeciality { get; set; }

        [Column("COMPLAINT_AND_PRESENT_HISTORY")]
        [StringLength(500)]
        [Unicode(false)]
        public string? ComplaintAndPresentHistory { get; set; }

        [Column("PAST_HISTORY")]
        [StringLength(500)]
        [Unicode(false)]
        public string? PastHistory { get; set; }

        [Column("CARDIAC_EXAMINATION")]
        [StringLength(500)]
        [Unicode(false)]
        public string? CardiacExamination { get; set; }

        [Column("GENERAL_EXAMINATION")]
        [StringLength(500)]
        [Unicode(false)]
        public string? GeneralExamination { get; set; }

        [Column("WEIGHT")]
        public decimal? Weight { get; set; }

        [Column("HEIGHT")]
        public decimal? Height { get; set; }

        [Column("SYTOLIC_BLOOD_PRESSURE")]
        public decimal? SytolicBloodPressure { get; set; }

        [Column("DIASTOLIC_BLOOD_PRESSURE")]
        public decimal? DiastolicBloodPressure { get; set; }

        [Column("TEMP")]
        public decimal? Temp { get; set; }

        [Column("RESP_RATE")]
        public decimal? RespRate { get; set; }

        [Column("OXYGEN_SAT")]
        public decimal? OxygenSat { get; set; }

        [Column("ECHO_ADMISSION")]
        [StringLength(500)]
        [Unicode(false)]
        public string? EchoAdmission { get; set; }

        [Column("ECG_EXAMINATION")]
        [StringLength(500)]
        [Unicode(false)]
        public string? EcgExamination { get; set; }

        [Column("PROVISIONAL_DIAGNOSIS")]
        [StringLength(500)]
        [Unicode(false)]
        public string? ProvisionalDiagnosis { get; set; }

        [Column("DOOR_TO_BALLOON_H")]
        public decimal? DoorToBalloonH { get; set; }

        [Column("SYMP_TO_DOOR_H")]
        public decimal? SympToDoorH { get; set; }

        [Column("KILLIP_CLASS")]
        public double? KillipClass { get; set; }

        [Column("CARDIAC_ARREST_IN_PRESENTATION")]
        public double? CardiacArrestInPresentation { get; set; }

        [Column("MECHANICAL_COMPLICATION_OF_MI")]
        public double? MechanicalComplicationOfMi { get; set; }

        [Column("LOCALIZATION_OF_MI")]
        public double? LocalizationOfMi { get; set; }

        [Column("PREVIOUS_ANGIO_RESULTS")]
        [StringLength(500)]
        [Unicode(false)]
        public string? PreviousAngioResults { get; set; }

        [Column("PREVIOUS_EXERSISE_TEST")]
        [StringLength(500)]
        [Unicode(false)]
        public string? PreviousExersiseTest { get; set; }

        [Column("PREVIOUS_ADMISSION_WITH_ACS")]
        [StringLength(15)]
        [Unicode(false)]
        public string? PreviousAdmissionWithAcs { get; set; }

        [Column("THROMBOLYTIC_THERAPY")]
        [StringLength(15)]
        [Unicode(false)]
        public string? ThrombolyticTherapy { get; set; }

        [Column("PAT_ID")]
        public int? PatId { get; set; }

        [Column("SERIAL")]
        public decimal? Serial { get; set; }

        [ForeignKey("PatId")]
        [InverseProperty("HistoryExaminations")]
        public Pat? Pat { get; set; }
    }
}
