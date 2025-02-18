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
    [Table("DAILY_FOLLOW_UP_CCU_CHANT")]
    public partial class DailyFollowUpCcuChant
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

        [Column("CURRENT_C_O")]
        [StringLength(100)]
        [Unicode(false)]
        public string? CurrentCO { get; set; }

        [Column("BLPR")]
        [StringLength(100)]
        [Unicode(false)]
        public string? Blpr { get; set; }

        [Column("CONSCIOUS_LEVEL")]
        [StringLength(100)]
        [Unicode(false)]
        public string? ConsciousLevel { get; set; }

        [Column("LOWER_LIMB_EDEMA")]
        [StringLength(100)]
        [Unicode(false)]
        public string? LowerLimbEdema { get; set; }

        [Column("UNINE_OUT_PUT")]
        [StringLength(100)]
        [Unicode(false)]
        public string? UnineOutPut { get; set; }

        [Column("CHEST_CONDITION")]
        [StringLength(100)]
        [Unicode(false)]
        public string? ChestCondition { get; set; }

        [Column("ECG")]
        [StringLength(100)]
        [Unicode(false)]
        public string? Ecg { get; set; }

        [Column("O2_SAT")]
        [StringLength(100)]
        [Unicode(false)]
        public string? O2Sat { get; set; }

        [Column("TEMP")]
        [StringLength(100)]
        [Unicode(false)]
        public string? Temp { get; set; }

        [Column("RBS")]
        [StringLength(100)]
        [Unicode(false)]
        public string? Rbs { get; set; }

        [Column("INSULIN_GIVEN")]
        [StringLength(100)]
        [Unicode(false)]
        public string? InsulinGiven { get; set; }

        [Column("CNP")]
        [StringLength(100)]
        [Unicode(false)]
        public string? Cnp { get; set; }

        [Column("INFUSIONS")]
        [StringLength(100)]
        [Unicode(false)]
        public string? Infusions { get; set; }

        [Column("UPDATES_IN_MEDICATION")]
        [StringLength(100)]
        [Unicode(false)]
        public string? UpdatesInMedication { get; set; }

        [Column("INVESTIGASIONS_ORDERD")]
        [StringLength(100)]
        [Unicode(false)]
        public string? InvestigasionsOrderd { get; set; }

        [Column("CONSULTANT_VISIT_RECOMMENDATI")]
        [StringLength(100)]
        [Unicode(false)]
        public string? ConsultantVisitRecommendati { get; set; }

        [Column("PAT_ID")]
        public int? PatId { get; set; }

        [Column("SERIAL")]
        public decimal? Serial { get; set; }

        [ForeignKey("PatId")]
        [InverseProperty("DailyFollowUpCcuChants")]
        public Pat? Pat { get; set; }
    }
}
