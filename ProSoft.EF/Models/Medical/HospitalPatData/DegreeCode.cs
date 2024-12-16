using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.Models.Medical.HospitalPatData
{
    [Table("DEGREE_CODE")]
    public class DegreeCode
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column("D_CODE")]
        public int? Id { get; set; }

        [Column("D_NAME")]
        public string? DegreeName { get; set; } = string.Empty;

        [Column("D_NAME2")]
        public string? DegreeName2 { get; set; } = string.Empty;

        [Column("D_TYPE")]
        public int? DegreeType { get; set; }

        [Column("BRANCH")]
        public int? Branch { get; set; }
        [Column("D_ON_OFF")]
        public int? DegreeOnOff { get; set; }

    }
}
