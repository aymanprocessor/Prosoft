using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.Models.Shared
{
    [Table("COMPANY_PROFILE")]
    public class CompanyProfile
    {
        [Key]
        [Column("CO_CODE")]
        public int CoCode { get; set; }

        [Column("CO_NAME_A")]
        [StringLength(50)]
        [Unicode(false)]
        public string? CoNameA { get; set; }

        [Column("CO_NAME_E")]
        [StringLength(50)]
        [Unicode(false)]
        public string? CoNameE { get; set; }

        [Column("F_YEAR")]
        public double? FYear { get; set; }

        [Column("TYPE_DB")]
        public double? TypeDb { get; set; }

        [Column("CO_PROFILE_DB")]
        [StringLength(50)]
        [Unicode(false)]
        public string? CoProfileDb { get; set; }

        [Column("USER_NAME_DB")]
        [StringLength(50)]
        [Unicode(false)]
        public string? UserNameDb { get; set; }

        [Column("PASS_WORD_DB")]
        [StringLength(50)]
        [Unicode(false)]
        public string? PassWordDb { get; set; }

        [Column("SERVICE_DB")]
        [StringLength(50)]
        [Unicode(false)]
        public string? ServiceDb { get; set; }

        [Column("SERIAL_DB")]
        public double? SerialDb { get; set; }

        [Column("COMPANY_LOGO")]
        [StringLength(150)]
        [Unicode(false)]
        public string? CompanyLogo { get; set; }

        [Column("COMPANY_LOGO_DTL")]
        [StringLength(150)]
        [Unicode(false)]
        public string? CompanyLogoDtl { get; set; }

        [Column("ARSHIVE_FILE")]
        [StringLength(50)]
        [Unicode(false)]
        public string? ArshiveFile { get; set; }

        [Column("LOGO_H")]
        public byte[]? LogoH { get; set; }

        [Column("LOGO_D")]
        public byte[]? LogoD { get; set; }

        [Column("LOGO")]
        public byte[]? Logo { get; set; }

        [Column("LOGO_HEADER")]
        [StringLength(150)]
        [Unicode(false)]
        public string? LogoHeader { get; set; }

        [Column("LAST_UP_DATE")]
        public DateTime? LastUpDate { get; set; }
    }
}
