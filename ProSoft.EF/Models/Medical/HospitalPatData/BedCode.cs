using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.Models.Medical.HospitalPatData
{
    [Table("BED_CODE")]
    public class BedCode
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("B_CODE")]
        public int? Id { get; set; }
        [Column("B_CODE_SYS")]
        public string? BedCodeSys { get; set; }
        [Column("R_CODE")]
        public int? RoomCode { get; set; }

        [Column("D_CODE")]
        public int? DegreeCode { get; set; }

        [Column("PRICE_PAT")]
        public decimal? PatientPrice { get; set; }

        [Column("PRICE_RELATIVET")]
        public decimal? RelativetPrice { get; set; }
        [Column("BRANCH")]
        public int? Branch { get; set; }
        [Column("B_ON_OFF")]
        public int? BedOnOff { get; set; }
        [Column("BOOK_ID")]
        public int? BookId { get; set; }
        [Column("PAT_ID")]
        public int? PatId { get; set; }
        [Column("PAT_AD_DATE")]
        public DateTime? PatAdDate { get; set; }
    }
}
