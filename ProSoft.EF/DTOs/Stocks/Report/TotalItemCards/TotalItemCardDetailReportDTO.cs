using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Stocks.Report.TotalItemCards
{
    public class TotalItemCardDetailReportDTO
    {
        public string ItemCode { get; set; } = string.Empty;
        public string ItemName { get; set; } = string.Empty;
        public decimal FirstBalance { get; set; }

        // ----- In ----- //
        public decimal InAdd { get; set; }
        public decimal InReturn { get; set; }
        public decimal InOther { get; set; }
        public decimal InTotal { get; set; }

        // ----- Out ----- //
        public decimal OutAdd { get; set;  }
        public decimal OutReturn { get; set; }
        public decimal OutOther { get; set; }
        public decimal OutTotal { get; set; }

        public decimal CurrentBalance { get; set; }
    }
}
