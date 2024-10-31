using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Stocks.Report.Transactions
{
    public class TotalPermitsTransactionRequestDTO
    {
        public int BranchId { get; set; }
        public int StockId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int? TransType { get; set; }
    }
}
