using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Stocks
{
    public class ItmReorderEditDTO
    {
        public string? ItemName { get; set; }

        [DisplayName("Order Limit")]
        [Required(ErrorMessage = "The field is required")]
        public decimal ReordQty { get; set; }

        [DisplayName("Min Quantity")]
        [Required(ErrorMessage = "The field is required")]
        public decimal MinQty { get; set; }

        [DisplayName("Max Quantity")]
        [Required(ErrorMessage = "The field is required")]
        public decimal MaxQty { get; set; }
    }
}
