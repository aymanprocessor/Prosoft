using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.MedicalRecords
{
    public class HistoryExaminationDTO
    {
        public string? PatName { get; set; }
        public int SerialHistory { get; set; }
        public string? PatSsn { get; set; }
        public DateTime? EntryDate { get; set; }
        public string? BelongingToSubSpeciality { get; set; }
        public string? ComplaintAndPresentHistory { get; set; }
        public string? PastHistory { get; set; }
        public string? CardiacExamination { get; set; }
        public string? GeneralExamination { get; set; }
        public decimal? Weight { get; set; }
        public decimal? Height { get; set; }
        public decimal? SytolicBloodPressure { get; set; }
        public decimal? DiastolicBloodPressure { get; set; }
        public decimal? Temp { get; set; }
        public decimal? RespRate { get; set; }
        public decimal? OxygenSat { get; set; }
        public string? EchoAdmission { get; set; }
        public string? EcgExamination { get; set; }
        public string? ProvisionalDiagnosis { get; set; }
        public decimal? DoorToBalloonH { get; set; }
        public decimal? SympToDoorH { get; set; }
        public double? KillipClass { get; set; }
        public double? CardiacArrestInPresentation { get; set; }
        public double? MechanicalComplicationOfMi { get; set; }
        public double? LocalizationOfMi { get; set; }
        public string? PreviousAngioResults { get; set; }
        public string? PreviousExersiseTest { get; set; }
        public string? PreviousAdmissionWithAcs { get; set; }
        public string? ThrombolyticTherapy { get; set; }
        public int? PatId { get; set; }
        public int? Serial { get; set; }
    }
}
