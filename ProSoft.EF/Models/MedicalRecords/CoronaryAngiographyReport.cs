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
    [Table("CORONARY_ANGIOGRAPHY_REPORT")]
    public partial class CoronaryAngiographyReport
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

        [Column("FIRST_OPERATOR")]
        [StringLength(20)]
        [Unicode(false)]
        public string? FirstOperator { get; set; }

        [Column("SECOND_OPERATOR")]
        [StringLength(20)]
        [Unicode(false)]
        public string? SecondOperator { get; set; }

        [Column("CATHETER_FOR_RCA")]
        [StringLength(20)]
        [Unicode(false)]
        public string? CatheterForRca { get; set; }

        [Column("CATHETER_FOR_LEFT_CORONARY")]
        [StringLength(20)]
        [Unicode(false)]
        public string? CatheterForLeftCoronary { get; set; }

        [Column("ACCESS_")]
        [StringLength(20)]
        [Unicode(false)]
        public string? Access { get; set; }

        [Column("AMOUNT_OF_CONTRAST_ML")]
        [StringLength(50)]
        [Unicode(false)]
        public string? AmountOfContrastMl { get; set; }

        [Column("PREOPRATIVE_ASSESSMENT")]
        public double? PreoprativeAssessment { get; set; }

        [Column("LEFT_MAIN")]
        [StringLength(20)]
        [Unicode(false)]
        public string? LeftMain { get; set; }

        [Column("LAD")]
        [StringLength(20)]
        [Unicode(false)]
        public string? Lad { get; set; }

        [Column("LCX")]
        [StringLength(20)]
        [Unicode(false)]
        public string? Lcx { get; set; }

        [Column("RCA")]
        [StringLength(20)]
        [Unicode(false)]
        public string? Rca { get; set; }

        [Column("HEMATOMA_D")]
        public double? HematomaD { get; set; }

        [Column("CIN_DIAGNOSTIC")]
        public double? CinDiagnostic { get; set; }

        [Column("PREOPRATIVE_ASSESSMENT_")]
        public double? PreoprativeAssessment1 { get; set; }

        [Column("LEFT_MAIN_CORONARY")]
        [StringLength(100)]
        [Unicode(false)]
        public string? LeftMainCoronary { get; set; }

        [Column("LEFT_ANTERIOR_DESCENDING")]
        [StringLength(100)]
        [Unicode(false)]
        public string? LeftAnteriorDescending { get; set; }

        [Column("LEFT_CIRCUMFLEX")]
        [StringLength(100)]
        [Unicode(false)]
        public string? LeftCircumflex { get; set; }

        [Column("RIGHT_CORONARY_ARTERY")]
        [StringLength(100)]
        [Unicode(false)]
        public string? RightCoronaryArtery { get; set; }

        [Column("OTHER_VESSELS")]
        [StringLength(100)]
        [Unicode(false)]
        public string? OtherVessels { get; set; }

        [Column("DIAGNOSIS")]
        [StringLength(100)]
        [Unicode(false)]
        public string? Diagnosis { get; set; }

        [Column("RECOMENDATION")]
        [StringLength(100)]
        [Unicode(false)]
        public string? Recomendation { get; set; }

        [Column("PAT_ID")]
        public int? PatId { get; set; }

        [Column("SERIAL", TypeName = "numeric(8, 0)")]
        public decimal? Serial { get; set; }

        [ForeignKey("PatId")]
        [InverseProperty("CoronaryAngiographyReports")]
        public Pat? Pat { get; set; }
    }
}
