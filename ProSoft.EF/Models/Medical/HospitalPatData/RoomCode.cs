using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.Models.Medical.HospitalPatData
{
    [Table("ROOM_CODE")]
    public class RoomCode
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("R_CODE")]
        public int? Id { get; set; }

        [Column("D_CODE")]
        public int? DegreeCode { get; set; }

        [Column("PRICE_PAT")]
        public decimal? PatientPrice { get; set; }

        [Column("PRICE_RELATIVET")]
        public decimal? RelativetPrice { get; set; }

        [Column("BRANCH")]
        public int? Branch { get; set; }
        [Column("R_ON_OFF")]
        public int? RoomOnOff { get; set; }
    }
}
