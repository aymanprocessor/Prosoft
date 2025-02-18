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
    [PrimaryKey(nameof(ItemCode),nameof(UnitCode),nameof(Flag1),nameof(BranchId))]
    [Table("ITEM_UNITS")]
    public class ItemUnit
    {
        [Key]
        [Column("ITEM_CODE")]
        [StringLength(30)]
        [ForeignKey(nameof(SubItem))]
        public string ItemCode { get; set;} = string.Empty;



        [Key]
        [Column("UNIT_CODE")]
        [ForeignKey(nameof(UnitCodee))]

        public int UnitCode { get; set; }


        [Column("ITEM_QTY")]
        public float ItemQty { get; set; }

        [Key]
        [Column("FLAG1")]
        public int Flag1 { get; set; }

        [Key]
        [Column("BRANCH_ID")]
        public int BranchId { get; set; }


        [Column("DEFULT_UNIT")]
        public bool DefaultUnit { get; set; }


        [Column("BR_REPLC")]
        [StringLength(20)]
        public string BrReplc { get; set; } = string.Empty;


        public SubItem SubItem { get; set; } = default!;


        public UnitCode UnitCodee { get; set; } = default!;
    }
}
