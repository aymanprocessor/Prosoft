using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Stocks
{
    // Not Used Until Now
    public class MainItemStockDTO
    {
        public string? ParentCode { get; set; }
        public string? MainName { get; set; }
        public string? MainLevel { get; set; }
        public int? Flag1 { get; set; }

        public List<StockViewDTO> Stocks {  get; set; }
        public List<StockViewDTO>? AddedStocks {  get; set; }
    }
}
