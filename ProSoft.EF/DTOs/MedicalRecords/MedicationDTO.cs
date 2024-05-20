using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.MedicalRecords
{
    public class MedicationDTO
    {
        public string? PatName { get; set; }
        public int SerialHistory { get; set; }
        public string? PatSsn { get; set; }
        public DateTime? EntryDate { get; set; }
        public double? Asa { get; set; }
        public double? Brilique { get; set; }
        public double? Clexane { get; set; }
        public double? Eptifibtide { get; set; }
        public double? NitratesSl { get; set; }
        public double? FluidLv { get; set; }
        public double? DopamineLvl { get; set; }
        public double? Lasix { get; set; }
        public double? Dormicum { get; set; }
        public double? Plavix { get; set; }
        public double? Heparin { get; set; }
        public double? TirofibanLv { get; set; }
        public double? Bb { get; set; }
        public double? NitratesLvl { get; set; }
        public double? LevophidLvl { get; set; }
        public double? DobutamineLvl { get; set; }
        public double? Morphea { get; set; }
        public double? MechanicalVentilation { get; set; }
        public int? PatId { get; set; }
        public int? Serial { get; set; }

    }
}
