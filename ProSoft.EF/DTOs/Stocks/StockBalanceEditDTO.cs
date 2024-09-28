using ProSoft.EF.Models.Stocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Stocks
{
    public class StockBalanceEditDTO
    {
        public float ItemPrice { get; set; }
        public float ItemPrice2 { get; set; }
        public float QtyStart { get; set; }
        public int UnitCode { get; set; }
    }
}
