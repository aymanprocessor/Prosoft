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
    [Table("ITM_REORDER")]
    public class ItmReorder
    {
        [Key]
        public int ItemReorderID { get; set; }

        [Column("BRANCH_ID")]
        public int? BranchId { get; set; }

        [Column("F_DATE")]
        public DateTime? FDate { get; set; }

        [Column("T_DATE")]
        public DateTime? TDate { get; set; }

        [Column("STORE_ID")]
        public int? StoreId { get; set; }

        [Column("ITEM_CD")]
        [StringLength(20)]
        [Unicode(false)]
        public string? ItemCd { get; set; }

        [Column("REORD_QTY")]
        public decimal? ReordQty { get; set; }

        [Column("MIN_QTY")]
        public decimal? MinQty { get; set; }

        [Column("MAX_QTY")]
        public decimal? MaxQty { get; set; }
    }
}
