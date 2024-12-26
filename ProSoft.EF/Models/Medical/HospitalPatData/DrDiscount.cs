using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.Models.Medical.HospitalPatData
{
    [Table("DR_DISCOUNT")]
    public class DrDiscount
    {
        [Key]
        [Column("DR_DISCOUNT_ID")]
        public int Id { get; set; }
        [Column("DR_ID")]
        public int DrId { get; set; }
        [Column("BRANCH_ID")]
        public int? BranchId { get; set; }
        [Column("DR_PRC")]
        public decimal? DrPercentage {  get; set; }
        [Column("DR_PRC_CONTRACT")]
        public decimal? DrPercentageContract {  get; set; }
        [Column("FLAG_DISC")]
        public int? FlagDisc {  get; set; }



    }
}
