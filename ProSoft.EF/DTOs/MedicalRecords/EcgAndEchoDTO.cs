using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.MedicalRecords
{
    public class EcgAndEchoDTO
    {
        public string? PatName { get; set; }
        public int SerialHistory { get; set; }
        public string? PatSsn { get; set; }
        public DateTime? EntryDate { get; set; }
        public string? RhythmOfAdmission { get; set; }
        public string? EjectionFraction { get; set; }
        public double? SwmaAtRest { get; set; }
        public string? Edd { get; set; }
        public string? Esd { get; set; }
        public int? PatId { get; set; }
        public int? Serial { get; set; }
    }
}
