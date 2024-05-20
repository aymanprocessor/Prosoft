using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.MedicalRecords
{
    public class DischargeDTO
    {
        public string? PatName { get; set; }
        public int SerialHistory { get; set; }
        public string? PatSsn { get; set; }
        public DateTime? EntryDate { get; set; }
        public string? ReferingDr { get; set; }
        public string? ConditionAtDischarge { get; set; }
        public string? CourseDuringStay { get; set; }
        public string? EchoAtDischarge { get; set; }
        public string? EcgAtDischage { get; set; }
        public string? BlprAtDischarge { get; set; }
        public string? ImportantInstructionsAtDisc { get; set; }
        public string? FinalDiagnosis { get; set; }
        public string? Prescription { get; set; }
        public DateTime? NextFollwupDate { get; set; }
        public int? PatId { get; set; }
        public int? Serial { get; set; }
    }
}
