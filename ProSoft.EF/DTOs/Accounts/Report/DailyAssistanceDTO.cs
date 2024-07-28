using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Accounts.Report
{
    public class DailyAssistanceDTO
    {
        public int? TransNo { get; set; }
        public DateTime? TransDate { get; set; }
        public string? CurenncyName { get; set; }
        public decimal? TotalTrans { get; set; }
        public List<DetailsDailyAssistanceDTO> DetailsDailyAssistanceDTOs { get; set; }

    }
}
