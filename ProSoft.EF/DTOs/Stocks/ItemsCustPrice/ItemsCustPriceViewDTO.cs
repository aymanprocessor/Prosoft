using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Stocks.ItemsCustPrice
{
    public class ItemsCustPriceViewDTO
    {
        [Required]
        [Display(Name = "Pricing Category")]
        public int adjectiveCode { get; set; }
        [Display(Name = "Search By Item Name")]
        public string? SearchByItemName { get; set; }
        [Display(Name = "Search By Item Code")]

        public string? SearchByItemCode { get; set; }
    }
}
