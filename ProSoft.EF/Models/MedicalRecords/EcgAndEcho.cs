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
    [Table("ECG_AND_ECHO")]
    public partial class EcgAndEcho
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

        [Column("RHYTHM_OF_ADMISSION")]
        [StringLength(20)]
        [Unicode(false)]
        public string? RhythmOfAdmission { get; set; }

        [Column("EJECTION_FRACTION")]
        [StringLength(100)]
        [Unicode(false)]
        public string? EjectionFraction { get; set; }

        [Column("SWMA_AT_REST")]
        public double? SwmaAtRest { get; set; }

        [Column("EDD")]
        [StringLength(100)]
        [Unicode(false)]
        public string? Edd { get; set; }

        [Column("ESD")]
        [StringLength(100)]
        [Unicode(false)]
        public string? Esd { get; set; }

        [Column("PAT_ID")]
        public int? PatId { get; set; }

        [Column("SERIAL", TypeName = "numeric(8, 0)")]
        public decimal? Serial { get; set; }

        [ForeignKey("PatId")]
        [InverseProperty("EcgAndEchoes")]
        public Pat? Pat { get; set; }
    }
}
