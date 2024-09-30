using ProSoft.EF.Models.Stocks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ProSoft.EF.DTOs.Stocks.ItemUnits
{
    public class ItemUnitsAddEditDTO
    {
        [DisplayName("The Unit")]
        [Required(ErrorMessage = "The field is required")]
        public int UnitCode { get; set; }
        [DisplayName("Number of Particles")]

        [Required(ErrorMessage = "The field is required")]
        [Range(1, 99999, ErrorMessage = "Enter Between 1 - 99999")]
        public float ItemQty { get; set; }
        [DisplayName("Common Used")]
        public bool DefaultUnit { get; set; }
        public string ItemCode { get; set; } = string.Empty;
        public int Flag1 { get; set; }
        public int BranchId { get; set; }
        public string BrReplc { get; set; } = string.Empty;

        public IEnumerable<SelectListItem> Units { get; set; } = Enumerable.Empty<SelectListItem>();

    }
}