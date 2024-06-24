using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.Models.Stocks
{
    [Keyless]
    [Table("ITEM_BATCH")]
    public class ItemBatch
    {
        [Column("ITEM_MASTER")]
        [StringLength(30)]
        [Unicode(false)]
        public string? ItemMaster { get; set; }

        [Column("ITM_BATCH")]
        [StringLength(15)]
        [Unicode(false)]
        public string? ItmBatch { get; set; }

        [Column("SER_BATCH")]
        public int? SerBatch { get; set; }

        [Column("UNIT_QTY", TypeName = "decimal(11, 2)")]
        public decimal? UnitQty { get; set; }

        [Column("PRICE", TypeName = "decimal(11, 3)")]
        public decimal? Price { get; set; }

        [Column("REQ_DATE", TypeName = "datetime")]
        public DateTime? ReqDate { get; set; }

        [Column("USER_CODE")]
        public int? UserCode { get; set; }

        [Column("EXP_DATE", TypeName = "datetime")]
        public DateTime? ExpDate { get; set; }

        [Column("TRANS_N")]
        public long? TransN { get; set; }

        [Column("STOCK_CODE")]
        public short? StockCode { get; set; }

        [Column("TM_BARCODE")]
        [StringLength(50)]
        [Unicode(false)]
        public string? TmBarcode { get; set; }

        [Column("BATCH_COUNTER")]
        public int? BatchCounter { get; set; }

        [Column("BR_REPLC")]
        [StringLength(20)]
        [Unicode(false)]
        public string? BrReplc { get; set; }

        [Column("FLAG1")]
        public int? Flag1 { get; set; }

        [Column("F_YEAR")]
        public int? FYear { get; set; }

        [Column("ITM_BATCH_MAX")]
        public long? ItmBatchMax { get; set; }

        [Column("BRANCH_ID")]
        public int? BranchId { get; set; }

        [Column("USER_CODE_MODIFY")]
        public int? UserCodeModify { get; set; }

        [Column("SERIAL")]
        public int? Serial { get; set; }
    }
}
