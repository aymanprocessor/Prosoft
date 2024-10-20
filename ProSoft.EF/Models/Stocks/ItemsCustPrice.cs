using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ProSoft.EF.Models.Stocks
{
    [Table("ITEMS_CUST_PRICE", Schema = "dbo")]
    [PrimaryKey(nameof(ItemCode), nameof(CustNo))] 

    public class ItemsCustPrice
    {
        [Column("ITEM_CODE")]
        public string ItemCode { get; set; }

        [Column("CUST_NO")]
        public string CustNo { get; set; }

        [Column("BAR_CODE")]
        public string BarCode { get; set; }

        [Column("NAME1")]
        public string Name { get; set; }

        [Column("UNIT_CODE")]
        public int? UnitCode { get; set; }

        [Column("ITEM_PRICE")]
        public decimal? ItemPrice { get; set; }

        [Column("ITEM_TAX1")]
        public string ItemTax1 { get; set; }

        [Column("ITEM_TAX2")]
        public string ItemTax2 { get; set; }

        [Column("BRANCH_ID")]
        public float? BranchId { get; set; }

        [Column("TAX_PRC")]
        public decimal? TaxPrc { get; set; }

        [Column("MAIN_CODE")]
        public string MainCode { get; set; }

        [Column("FLAG1")]
        public float? Flag1 { get; set; }

        [Column("CATEGORY_PRICE")]
        public float? CategoryPrice { get; set; }

        [Column("DISCOUNT_RATE")]
        public decimal? DiscountRate { get; set; }
    }
}
