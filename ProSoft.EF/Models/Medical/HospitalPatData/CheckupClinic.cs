using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.Models.Medical.HospitalPatData
{
    [PrimaryKey("BranchId", "CheckupId", "Flag1")]
    [Table("CHECKUP_CLINIC")]
    [Index("BranchId", "CheckupId", "Flag1", Name = "PK_CHECKUP_CLINIC_2", IsUnique = true)]
    public class CheckupClinic
    {
        [Key]
        [Column("CHECKUP_ID")]
        public int CheckupId { get; set; }

        [Column("CHECKUP_DESC")]
        [StringLength(100)]
        [Unicode(false)]
        public string CheckupDesc { get; set; } = null!;

        [Column("CHECKUP_DESC2")]
        [StringLength(100)]
        [Unicode(false)]
        public string? CheckupDesc2 { get; set; }

        [Key]
        [Column("BRANCH_ID")]
        public int BranchId { get; set; }

        [Key]
        [Column("FLAG1")]
        public int Flag1 { get; set; }
    }
}
