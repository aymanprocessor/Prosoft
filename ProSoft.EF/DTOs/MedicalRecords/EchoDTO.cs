using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.MedicalRecords
{
    public class EchoDTO
    {
        public string? PatName { get; set; }
        public int SerialHistory { get; set; }
        public string? PatSsn { get; set; }
        public DateTime? EntryDate { get; set; }
        public string? FirstOperator { get; set; }
        public string? EddCm { get; set; }
        public string? EsdCm { get; set; }
        public string? IvsdCm { get; set; }
        public string? LvpwdCm { get; set; }
        public string? Ef { get; set; }
        public string? Fs { get; set; }
        public string? La { get; set; }
        public string? Aod { get; set; }
        public string? Acs { get; set; }
        public string? Tapse { get; set; }
        public string? EARatio { get; set; }
        public string? E { get; set; }
        public string? EE { get; set; }
        public string? LeftVentricle2d { get; set; }
        public string? RightVentricle2d { get; set; }
        public string? LeftAtrium2d { get; set; }
        public string? RightAtrium2d { get; set; }
        public string? MitralValve2d { get; set; }
        public string? AorticValve2d { get; set; }
        public string? TricuspidValve2d { get; set; }
        public string? PulmonaryValve2d { get; set; }
        public string? MitralFlow { get; set; }
        public string? AorticFlow { get; set; }
        public string? PulmonaryFlow { get; set; }
        public string? TricsupidFlow { get; set; }
        public string? Comment1 { get; set; }
        public string? Comment2 { get; set; }
        public string? Comment3 { get; set; }
        public string? Comment4 { get; set; }
        public string? Comment5 { get; set; }
        public string? Comment6 { get; set; }
        public string? Comment7 { get; set; }
        public string? Comment8 { get; set; }
        public string? Comment9 { get; set; }
        public string? Comment10 { get; set; }
        public string? Conclusion { get; set; }
        public int? PatId { get; set; }
        public int? Serial { get; set; }
    }
}
