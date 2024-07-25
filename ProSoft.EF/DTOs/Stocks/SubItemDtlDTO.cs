using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Stocks
{
    public class SubItemDtlDTO
    {
        public int? SubItemDtlId { get; set; }
        public int? ItemId { get; set; }

        [DisplayName("Unit Type")]
        [Required(ErrorMessage = "The field is required")]
        public int UnitCode { get; set; }

        [DisplayName("Number of Particles")]
        [Required(ErrorMessage = "The field is required")]
        public int PieceCount { get; set; }

        [DisplayName("Last Price")]
        public decimal? SalePrice { get; set; }

        [DisplayName("Discount Percentage")]
        public decimal? DiscoutS1 { get; set; }

        [DisplayName("Discount Value")]
        public decimal? DiscValS1 { get; set; }
        
        [DisplayName("Last Price")]
        public decimal? PurchPrice { get; set; }

        [DisplayName("Price Average")]
        public decimal? PurchAverage { get; set; }

        [DisplayName("Discount Percentage")]
        public decimal? DiscoutP1 { get; set; }

        [DisplayName("Discount Value")]
        public decimal? DiscValP1 { get; set; }

        [DisplayName("Unit Size")]
        [Required(ErrorMessage = "The field is required")]
        public string ItemName { get; set; }
        public string? ItemCode { get; set; }
        public string? MainCode { get; set; }
        public int? Flag1 { get; set; }
        public int? BranchId { get; set; }
        public int? ProtectColumun { get; set; }
        public int? RowOnOff { get; set; }
        public int? UserModifyCode { get; set; }
        public DateTime? UserModifyDate { get; set; }
        public int? SmallUFlag { get; set; }
        ////////////////////
        public string? UnitType { get; set; }
        //public string? ItemSizeName { get; set; }
        public string? SubItemName { get; set; }
        public string? MainLevel { get; set; }
        public string? ParentCode { get; set; }

        public List<UnitCodeDTO>? UnitCodes { get; set; }
    }
}
