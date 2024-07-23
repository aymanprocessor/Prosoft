using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.Models.Stocks
{
    [PrimaryKey("MainCode", "Flag1", "BranchId", "Stkcod")]
    [Table("MAIN_ITEM_STOCK")]
    public class MainItemStock
    {
        [Key]
        [Column("MAIN_CODE")]
        [StringLength(10)]
        [Unicode(false)]
        public string MainCode { get; set; }

        [Key]
        [Column("FLAG1")]
        public int Flag1 { get; set; }

        [Key]
        [Column("BRANCH_ID")]
        public int BranchId { get; set; }

        [Key]
        [Column("STKCOD")]
        public int Stkcod { get; set; }
    }
}
