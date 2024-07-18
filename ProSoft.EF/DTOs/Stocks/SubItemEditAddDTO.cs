using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProSoft.EF.DTOs.Accounts;

namespace ProSoft.EF.DTOs.Stocks
{
    public class SubItemEditAddDTO
    {
        public int SubId { get; set; }
        public int? MainId { get; set; }

        [DisplayName("Item Name")]
        [Required(ErrorMessage = "The field is required")]
        public string SubName { get; set; }

        [DisplayName("Item Code")]
        [Required(ErrorMessage = "The field is required")]
        public string ItemCode { get; set; }

        [DisplayName("User Sort")]
        public int? UserSort { get; set; }

        [DisplayName("Item Counter")]
        [Required(ErrorMessage = "The field is required")]
        public int BatchCounter { get; set; }

        [DisplayName("Item Sell Price")]
        [Required(ErrorMessage = "The field is required")]
        public decimal ItemPrice { get; set; }

        [DisplayName("Cost Center")]
        [Required(ErrorMessage = "The field is required")]
        public int Replcate { get; set; }

        [DisplayName("Stent Or Catheter")]
        public int? StentId { get; set; }

        [DisplayName("The Supplier")]
        [Required(ErrorMessage = "The field is required")]
        public int Sub { get; set; }

        [DisplayName("Tax Percentage")]
        [Required(ErrorMessage = "The field is required")]
        public decimal ItemTax { get; set; }
        public string? MainLevel { get; set; }
        public int? Flag1 { get; set; }
        public string? ParentCode { get; set; }
        public int? BranchId { get; set; }

        public List<CostCenterViewDTO>? CostCenters { get; set; }
        public List<StentDesDTO>? StentDess { get; set; }
        public List<SupCodeViewDTO>? Suppliers { get; set; }
    }
}
