using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.Models.Medical.HospitalPatData
{
    [Table("DRTIMSHEET")]
    public class Drtimsheet
    {
        [Key]
        [Column("DR_TIM_ID")]
        public int? DrTimId { get; set; }

        [Column("BRANCH_ID")]
        public int? BranchId { get; set; }

        [Column("DR_ID")]
        public int? DrId { get; set; }

        [Column("DAY_NUMBER")]
        public int? DayNumber { get; set; }

        [Column("TIMFROM")]
        public DateTime? Timfrom { get; set; }

        [Column("TIMTO")]
        public DateTime? Timto { get; set; }

        [Column("EX_PERIOD")]
        public int? ExPeriod { get; set; }

        [Column("REPLCATE")]
        public int? Replcate { get; set; }
    }
}
