using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Stocks.Report.TotalItemCards
{
    public class TotalItemCardsRequestDTO
    {
        public int BranchId { get; set; }
        [Required]
        public int StockId { get; set; }
        [Required]
        public DateTime FromDate { get; set; }
        [Required]
        public DateTime ToDate { get; set; }
        public int? SupCode { get; set; }
        public int? MainCode { get; set; }
        public string? SearchByItemCode { get; set; }
        public string? SearchByItemName { get; set; }
        public bool? ItemsNotHasBalance { get; set; } 
    }
}
