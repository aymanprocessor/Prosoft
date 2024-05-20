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

        [Column("ENTRY_DATE", TypeName = "datetime")]
        public DateTime? EntryDate { get; set; }

        [Column("TROP")]
        [StringLength(10)]
        [Unicode(false)]
        public string? Trop { get; set; }

        [Column("LDH", TypeName = "decimal(6, 2)")]
        public decimal? Ldh { get; set; }

        [Column("PLATELET", TypeName = "decimal(6, 2)")]
        public decimal? Platelet { get; set; }

        [Column("BIL", TypeName = "decimal(6, 2)")]
        public decimal? Bil { get; set; }

        [Column("AST", TypeName = "decimal(6, 2)")]
        public decimal? Ast { get; set; }

        [Column("LDL", TypeName = "decimal(6, 2)")]
        public decimal? Ldl { get; set; }

        [Column("TG", TypeName = "decimal(6, 2)")]
        public decimal? Tg { get; set; }

        [Column("CR_BEFORE_ANGIOPLASTY", TypeName = "decimal(6, 2)")]
        public decimal? CrBeforeAngioplasty { get; set; }

        [Column("UREA", TypeName = "decimal(6, 2)")]
        public decimal? Urea { get; set; }

        [Column(TypeName = "decimal(6, 2)")]
        public decimal? K { get; set; }

        [Column("MG", TypeName = "decimal(6, 2)")]
        public decimal? Mg { get; set; }

        [Column("HCV_AB")]
        [StringLength(10)]
        [Unicode(false)]
        public string? HcvAb { get; set; }

        [Column("HCV_A1C", TypeName = "decimal(6, 2)")]
        public decimal? HcvA1c { get; set; }

        [Column("NOTES")]
        [StringLength(100)]
        [Unicode(false)]
        public string? Notes { get; set; }

        [Column("CKMB", TypeName = "decimal(6, 2)")]
        public decimal? Ckmb { get; set; }

        [Column("HB", TypeName = "decimal(6, 2)")]
        public decimal? Hb { get; set; }

        [Column("WBCS", TypeName = "decimal(6, 2)")]
        public decimal? Wbcs { get; set; }

        [Column("ALT", TypeName = "decimal(6, 2)")]
        public decimal? Alt { get; set; }

        [Column("ALB", TypeName = "decimal(6, 2)")]
        public decimal? Alb { get; set; }

        [Column("HDL", TypeName = "decimal(6, 2)")]
        public decimal? Hdl { get; set; }

        [Column("CHOLESTEROL", TypeName = "decimal(6, 2)")]
        public decimal? Cholesterol { get; set; }

        [Column("CR_48_H_AFTER_ANGIOPLASTY", TypeName = "decimal(6, 2)")]
        public decimal? Cr48HAfterAngioplasty { get; set; }

        [Column("INR", TypeName = "decimal(6, 2)")]
        public decimal? Inr { get; set; }

        [Column("NA", TypeName = "decimal(6, 2)")]
        public decimal? Na { get; set; }

        [Column("CA", TypeName = "decimal(6, 2)")]
        public decimal? Ca { get; set; }

        [Column("HBVSAG")]
        [StringLength(10)]
        [Unicode(false)]
        public string? Hbvsag { get; set; }

        [Column("TSH", TypeName = "decimal(6, 2)")]
        public decimal? Tsh { get; set; }

        [Column("PAT_ID")]
        public int? PatId { get; set; }

        [Column("SERIAL", TypeName = "numeric(8, 0)")]
        public decimal? Serial { get; set; }

        [ForeignKey("PatId")]
        [InverseProperty("LabReports")]
        public Pat? Pat { get; set; }
    }
}
