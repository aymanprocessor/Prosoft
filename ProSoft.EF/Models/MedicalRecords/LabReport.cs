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
    [Table("LAB_REPORTS")]
    public partial class LabReport
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

        [Column("TROP")]
        [StringLength(10)]
        [Unicode(false)]
        public string? Trop { get; set; }

        [Column("LDH")]
        public decimal? Ldh { get; set; }

        [Column("PLATELET")]
        public decimal? Platelet { get; set; }

        [Column("BIL")]
        public decimal? Bil { get; set; }

        [Column("AST")]
        public decimal? Ast { get; set; }

        [Column("LDL")]
        public decimal? Ldl { get; set; }

        [Column("TG")]
        public decimal? Tg { get; set; }

        [Column("CR_BEFORE_ANGIOPLASTY")]
        public decimal? CrBeforeAngioplasty { get; set; }

        [Column("UREA")]
        public decimal? Urea { get; set; }

        [Column(TypeName = "decimal(6, 2)")]
        public decimal? K { get; set; }

        [Column("MG")]
        public decimal? Mg { get; set; }

        [Column("HCV_AB")]
        [StringLength(10)]
        [Unicode(false)]
        public string? HcvAb { get; set; }

        [Column("HCV_A1C")]
        public decimal? HcvA1c { get; set; }

        [Column("NOTES")]
        [StringLength(100)]
        [Unicode(false)]
        public string? Notes { get; set; }

        [Column("CKMB")]
        public decimal? Ckmb { get; set; }

        [Column("HB")]
        public decimal? Hb { get; set; }

        [Column("WBCS")]
        public decimal? Wbcs { get; set; }

        [Column("ALT")]
        public decimal? Alt { get; set; }

        [Column("ALB")]
        public decimal? Alb { get; set; }

        [Column("HDL")]
        public decimal? Hdl { get; set; }

        [Column("CHOLESTEROL")]
        public decimal? Cholesterol { get; set; }

        [Column("CR_48_H_AFTER_ANGIOPLASTY")]
        public decimal? Cr48HAfterAngioplasty { get; set; }

        [Column("INR")]
        public decimal? Inr { get; set; }

        [Column("NA")]
        public decimal? Na { get; set; }

        [Column("CA")]
        public decimal? Ca { get; set; }

        [Column("HBVSAG")]
        [StringLength(10)]
        [Unicode(false)]
        public string? Hbvsag { get; set; }

        [Column("TSH")]
        public decimal? Tsh { get; set; }

        [Column("PAT_ID")]
        public int? PatId { get; set; }

        [Column("SERIAL")]
        public decimal? Serial { get; set; }

        [ForeignKey("PatId")]
        [InverseProperty("LabReports")]
        public Pat? Pat { get; set; }
    }
}
