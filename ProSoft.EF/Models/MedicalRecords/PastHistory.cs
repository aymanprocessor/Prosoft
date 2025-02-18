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
    [Table("PAST_HISTORY")]
    public partial class PastHistory
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

        [Column("DIABETES")]
        public double? Diabetes { get; set; }

        [Column("HTN")]
        public double? Htn { get; set; }

        [Column("SOMKER")]
        public double? Somker { get; set; }

        [Column("CSA")]
        public double? Csa { get; set; }

        [Column("PRIOR_CABG")]
        public double? PriorCabg { get; set; }

        [Column("PRIOR_PCI")]
        public double? PriorPci { get; set; }

        [Column("PRIOR_MI")]
        public double? PriorMi { get; set; }

        [Column("HFREF")]
        public double? Hfref { get; set; }

        [Column("HFPEF")]
        public double? Hfpef { get; set; }

        [Column("ISCHEMIC_STROKE")]
        public double? IschemicStroke { get; set; }

        [Column("HEMORTHAGIC_STROKE")]
        public double? HemorthagicStroke { get; set; }

        [Column("TIA")]
        public double? Tia { get; set; }

        [Column("DYSLIPIDEMIA")]
        public double? Dyslipidemia { get; set; }

        [Column("CHRONIC_AF")]
        public double? ChronicAf { get; set; }

        [Column("PROXYSMAI_AF")]
        public double? ProxysmaiAf { get; set; }

        [Column("MALIGNANT_TUMORS")]
        public double? MalignantTumors { get; set; }

        [Column("OS_APNEA")]
        public double? OsApnea { get; set; }

        [Column("CKD_ON_MEDICAL")]
        public double? CkdOnMedical { get; set; }

        [Column("CKD_ON_DIALYSIS")]
        public double? CkdOnDialysis { get; set; }

        [Column("IV_ADDICT")]
        public double? IvAddict { get; set; }

        [Column("HASHISH_ADDICT")]
        public double? HashishAddict { get; set; }

        [Column("TRAMADOL_ADDICT")]
        public double? TramadolAddict { get; set; }

        [Column("PREGNANT")]
        public double? Pregnant { get; set; }

        [Column("RHEUMATIC_HEAT_DISEASE")]
        public double? RheumaticHeatDisease { get; set; }

        [Column("HYPERTHYROIDISM")]
        public double? Hyperthyroidism { get; set; }

        [Column("HYPOTHYROIDISM")]
        public double? Hypothyroidism { get; set; }

        [Column("PROSTHETIC_VALVE")]
        public double? ProstheticValve { get; set; }

        [Column("PAT_ID")]
        public int? PatId { get; set; }

        [Column("SERIAL")]
        public decimal? Serial { get; set; }

        [ForeignKey("PatId")]
        [InverseProperty("PastHistories")]
        public virtual Pat? Pat { get; set; }
    }
}
