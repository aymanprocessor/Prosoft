using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Stocks.TransferSuppliesToStocks
{
    public class TransferSuppliesToStocksDetailDTO
    {
        public DateTime Date { get; set; }
        public string ItemCode { get; set; } = string.Empty;
        public string ItemName { get; set; } = string.Empty;

        public decimal Qty { get; set; }
        public string StockName { get; set; } = string.Empty;
        public decimal Cost { get; set; }
        public decimal Sell { get; set; }

        public int PermissionNo { get; set; }

    }
}
