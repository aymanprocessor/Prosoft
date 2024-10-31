using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Stocks.Report.Transactions
{
    public class TotalPermitsTransactionReportDTO
    {

        public long? DocNo { get; set; }
        public string? DocDate { get; set; }
        public string? RefDocNo { get; set; }
        public decimal? TotTransVal { get; set; }
        public decimal? Descount { get; set; }
        public decimal? NetTransValue { get; set; }

    }
}
