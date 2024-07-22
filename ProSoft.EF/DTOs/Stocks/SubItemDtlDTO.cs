using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Stocks
{
    public class SubItemDtlDTO
    {
        public int SubItemDtlId { get; set; }
        public int? ItemId { get; set; }
        public string? ItemName { get; set; }
        public string? ItemCode { get; set; }
        public string? MainCode { get; set; }
        public int? Flag1 { get; set; }
        public int? BranchId { get; set; }
        public int? UnitCode { get; set; }
        public decimal? SalePrice { get; set; }
        public decimal? DiscoutS1 { get; set; }
        public decimal? DiscValS1 { get; set; }
        public decimal? PurchPrice { get; set; }
        public decimal? DiscoutP1 { get; set; }
        public decimal? DiscValP1 { get; set; }
        public int? PieceCount { get; set; }
        public decimal? PurchAverage { get; set; }
        public int? ProtectColumun { get; set; }
        public int? RowOnOff { get; set; }
        public int? UserModifyCode { get; set; }
        public DateTime? UserModifyDate { get; set; }
        public int? SmallUFlag { get; set; }
    }
}
