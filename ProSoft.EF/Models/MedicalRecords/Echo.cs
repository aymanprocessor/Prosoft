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
    [Table("ECHO")]
    public partial class Echo
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

        [Column("FIRST_OPERATOR")]
        [StringLength(20)]
        [Unicode(false)]
        public string? FirstOperator { get; set; }

        [Column("EDD_CM")]
        [StringLength(20)]
        [Unicode(false)]
        public string? EddCm { get; set; }

        [Column("ESD_CM")]
        [StringLength(20)]
        [Unicode(false)]
        public string? EsdCm { get; set; }

        [Column("IVSD_CM")]
        [StringLength(20)]
        [Unicode(false)]
        public string? IvsdCm { get; set; }

        [Column("LVPWD_CM")]
        [StringLength(20)]
        [Unicode(false)]
        public string? LvpwdCm { get; set; }

        [Column("EF")]
        [StringLength(20)]
        [Unicode(false)]
        public string? Ef { get; set; }

        [Column("FS")]
        [StringLength(20)]
        [Unicode(false)]
        public string? Fs { get; set; }

        [Column("LA")]
        [StringLength(20)]
        [Unicode(false)]
        public string? La { get; set; }

        [Column("AOD")]
        [StringLength(20)]
        [Unicode(false)]
        public string? Aod { get; set; }

        [Column("ACS")]
        [StringLength(20)]
        [Unicode(false)]
        public string? Acs { get; set; }

        [Column("TAPSE")]
        [StringLength(20)]
        [Unicode(false)]
        public string? Tapse { get; set; }

        [Column("E_A_RATIO")]
        [StringLength(20)]
        [Unicode(false)]
        public string? EARatio { get; set; }

        [Column("E_")]
        [StringLength(20)]
        [Unicode(false)]
        public string? E { get; set; }

        [Column("E_E")]
        [StringLength(20)]
        [Unicode(false)]
        public string? EE { get; set; }

        [Column("LEFT_VENTRICLE_2D")]
        [StringLength(20)]
        [Unicode(false)]
        public string? LeftVentricle2d { get; set; }

        [Column("RIGHT_VENTRICLE_2D")]
        [StringLength(20)]
        [Unicode(false)]
        public string? RightVentricle2d { get; set; }

        [Column("LEFT_ATRIUM_2D")]
        [StringLength(20)]
        [Unicode(false)]
        public string? LeftAtrium2d { get; set; }

        [Column("RIGHT_ATRIUM_2D")]
        [StringLength(20)]
        [Unicode(false)]
        public string? RightAtrium2d { get; set; }

        [Column("MITRAL_VALVE_2D")]
        [StringLength(20)]
        [Unicode(false)]
        public string? MitralValve2d { get; set; }

        [Column("AORTIC_VALVE_2D")]
        [StringLength(20)]
        [Unicode(false)]
        public string? AorticValve2d { get; set; }

        [Column("TRICUSPID_VALVE_2D")]
        [StringLength(20)]
        [Unicode(false)]
        public string? TricuspidValve2d { get; set; }

        [Column("PULMONARY_VALVE_2D")]
        [StringLength(20)]
        [Unicode(false)]
        public string? PulmonaryValve2d { get; set; }

        [Column("MITRAL_FLOW")]
        [StringLength(20)]
        [Unicode(false)]
        public string? MitralFlow { get; set; }

        [Column("AORTIC_FLOW")]
        [StringLength(20)]
        [Unicode(false)]
        public string? AorticFlow { get; set; }

        [Column("PULMONARY_FLOW")]
        [StringLength(20)]
        [Unicode(false)]
        public string? PulmonaryFlow { get; set; }

        [Column("TRICSUPID_FLOW")]
        [StringLength(20)]
        [Unicode(false)]
        public string? TricsupidFlow { get; set; }

        [Column("COMMENT_1")]
        [StringLength(100)]
        [Unicode(false)]
        public string? Comment1 { get; set; }

        [Column("COMMENT_2")]
        [StringLength(100)]
        [Unicode(false)]
        public string? Comment2 { get; set; }

        [Column("COMMENT_3")]
        [StringLength(100)]
        [Unicode(false)]
        public string? Comment3 { get; set; }

        [Column("COMMENT_4")]
        [StringLength(100)]
        [Unicode(false)]
        public string? Comment4 { get; set; }

        [Column("COMMENT_5")]
        [StringLength(100)]
        [Unicode(false)]
        public string? Comment5 { get; set; }

        [Column("COMMENT_6")]
        [StringLength(100)]
        [Unicode(false)]
        public string? Comment6 { get; set; }

        [Column("COMMENT_7")]
        [StringLength(100)]
        [Unicode(false)]
        public string? Comment7 { get; set; }

        [Column("COMMENT_8")]
        [StringLength(100)]
        [Unicode(false)]
        public string? Comment8 { get; set; }

        [Column("COMMENT_9")]
        [StringLength(100)]
        [Unicode(false)]
        public string? Comment9 { get; set; }

        [Column("COMMENT_10")]
        [StringLength(100)]
        [Unicode(false)]
        public string? Comment10 { get; set; }

        [Column("CONCLUSION")]
        [StringLength(100)]
        [Unicode(false)]
        public string? Conclusion { get; set; }

        [Column("PAT_ID")]
        public int? PatId { get; set; }

        [Column("SERIAL")]
        public decimal? Serial { get; set; }

        [ForeignKey("PatId")]
        [InverseProperty("Echoes")]
        public Pat? Pat { get; set; }
    }
}
