using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Stocks
{
    public class SubItemEditAddDTO
    {
        public int SubId { get; set; }

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

        [DisplayName("The Supplier")]
        [Required(ErrorMessage = "The field is required")]
        public int Sub { get; set; }

        [DisplayName("Tax Percentage")]
        [Required(ErrorMessage = "The field is required")]
        public decimal ItemTax { get; set; }
    }
}
