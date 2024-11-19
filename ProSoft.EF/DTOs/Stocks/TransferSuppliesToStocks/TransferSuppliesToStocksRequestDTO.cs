using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Stocks.TransferSuppliesToStocks
{
    public class TransferSuppliesToStocksRequestDTO
    {
        public int BranchId { get; set; }
        public int ReqgionId { get; set; }

        public DateTime FromDate { get; set; } = new DateTime(DateTime.Now.Year, 1, 1);
        public DateTime ToDate { get; set; } = DateTime.Now;
    }
}
