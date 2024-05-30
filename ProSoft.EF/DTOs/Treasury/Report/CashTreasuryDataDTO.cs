using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Treasury.Report
{
    public class CashTreasuryDataDTO
    {

        public decimal? Expense { get; set; } //مصروف
        public decimal? Revenue { get; set; }//ايراد

        public DateTime? DocDate { get; set; }

        public int? DocNo { get; set; }

        public long? AccTransNo { get; set; }

        public string? PersonName { get; set; }

        public string? Commentt { get; set; }
        public int? CshOrdNum { get; set; }


    }
}
