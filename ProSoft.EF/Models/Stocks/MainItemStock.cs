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
    [Keyless]
    [Table("MAIN_ITEM_STOCK")]
    public class MainItemStock
    {
        [Column("MAIN_CODE")]
        [StringLength(10)]
        [Unicode(false)]
        public string? MainCode { get; set; }

        [Column("FLAG1")]
        public int? Flag1 { get; set; }

        [Column("BRANCH_ID")]
        public int? BranchId { get; set; }

        [Column("STKCOD")]
        public int? Stkcod { get; set; }
    }
}
