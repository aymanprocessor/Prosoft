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
    [Table("DISCHARGE_SUMMERY")]
    public partial class DischargeSummery
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

        [Column("REFERING_DR")]
        [StringLength(40)]
        [Unicode(false)]
        public string? ReferingDr { get; set; }

        [Column("CONDITION_AT_DISCHARGE")]
        [StringLength(15)]
        [Unicode(false)]
        public string? ConditionAtDischarge { get; set; }

        [Column("COURSE_DURING_STAY")]
        [StringLength(1000)]
        [Unicode(false)]
        public string? CourseDuringStay { get; set; }

        [Column("ECHO_AT_DISCHARGE")]
        [StringLength(100)]
        [Unicode(false)]
        public string? EchoAtDischarge { get; set; }

        [Column("ECG_AT_DISCHAGE")]
        [StringLength(100)]
        [Unicode(false)]
        public string? EcgAtDischage { get; set; }

        [Column("BLPR_AT_DISCHARGE")]
        [StringLength(100)]
        [Unicode(false)]
        public string? BlprAtDischarge { get; set; }

        [Column("IMPORTANT_INSTRUCTIONS_AT_DISC")]
        [StringLength(100)]
        [Unicode(false)]
        public string? ImportantInstructionsAtDisc { get; set; }

        [Column("FINAL_DIAGNOSIS")]
        [StringLength(100)]
        [Unicode(false)]
        public string? FinalDiagnosis { get; set; }

        [Column("PRESCRIPTION")]
        [StringLength(300)]
        [Unicode(false)]
        public string? Prescription { get; set; }

        [Column("NEXT_FOLLWUP_DATE")]
        public DateTime? NextFollwupDate { get; set; }

        [Column("PAT_ID")]
        public int? PatId { get; set; }

        [Column("SERIAL")]
        public decimal? Serial { get; set; }

        [ForeignKey("PatId")]
        [InverseProperty("DischargeSummeries")]
        public Pat? Pat { get; set; }
    }
}
