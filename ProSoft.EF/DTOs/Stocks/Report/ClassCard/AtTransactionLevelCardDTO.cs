using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Stocks.Report.ClassCard
{
    public class AtTransactionLevelCardDTO
    {
        public string? TransDate { get; set; }
        public int? TransNo { get; set; }
        public int? itemType { get; set; }
        public string? RefNo { get; set; }
        public decimal? ItemIn { get; set; }
        public decimal? ItemOut { get; set; }
        public decimal? ReItemPlus { get; set; }
        public decimal? ReItemMinus { get; set; }
        public decimal? LevelingIn { get; set; }
        public decimal? LevelingOut { get; set; }

    }
}
