using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProSoft.EF.Models.Medical.HospitalPatData;

namespace ProSoft.EF.Models.MedicalRecords
{
    [Table("MEDICATION_AT_CCU")]
    public partial class MedicationAtCcu
    {
        [Key]
        [Column("SERIAL_HISTORY")]
        public int SerialHistory { get; set; }

        [Column("PAT_SSN")]
        [StringLength(14)]
        [Unicode(false)]
        public string? PatSsn { get; set; }

        [Column("ENTRY_DATE", TypeName = "datetime")]
        public DateTime? EntryDate { get; set; }

        [Column("ASA")]
        public double? Asa { get; set; }

        [Column("BRILIQUE")]
        public double? Brilique { get; set; }

        [Column("CLEXANE")]
        public double? Clexane { get; set; }

        [Column("EPTIFIBTIDE")]
        public double? Eptifibtide { get; set; }

        [Column("NITRATES_SL")]
        public double? NitratesSl { get; set; }

        [Column("FLUID_LV")]
        public double? FluidLv { get; set; }

        [Column("DOPAMINE_LVL")]
        public double? DopamineLvl { get; set; }

        [Column("LASIX")]
        public double? Lasix { get; set; }

        [Column("DORMICUM")]
        public double? Dormicum { get; set; }

        [Column("PLAVIX")]
        public double? Plavix { get; set; }

        [Column("HEPARIN")]
        public double? Heparin { get; set; }

        [Column("TIROFIBAN_LV")]
        public double? TirofibanLv { get; set; }

        [Column("BB")]
        public double? Bb { get; set; }

        [Column("NITRATES_LVL")]
        public double? NitratesLvl { get; set; }

        [Column("LEVOPHID_LVL")]
        public double? LevophidLvl { get; set; }

        [Column("DOBUTAMINE_LVL")]
        public double? DobutamineLvl { get; set; }

        [Column("MORPHEA")]
        public double? Morphea { get; set; }

        [Column("MECHANICAL_VENTILATION")]
        public double? MechanicalVentilation { get; set; }

        [Column("PAT_ID")]
        public int? PatId { get; set; }

        [Column("SERIAL", TypeName = "numeric(8, 0)")]
        public decimal? Serial { get; set; }

        [ForeignKey("PatId")]
        [InverseProperty("MedicationAtCcus")]
        public Pat? Pat { get; set; }
    }

}
