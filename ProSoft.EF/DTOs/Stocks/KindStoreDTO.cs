using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Stocks
{
    public class KindStoreDTO
    {
        [DisplayName("StockType ID")]
        public int KId { get; set; }

        [DisplayName("Stock Type")]
        public string KName { get; set; }
        public int? KType { get; set; }

        [DisplayName("Stock Kind")]
        public int StockType { get; set; }
        public int? KStkOnOff { get; set; }
    }
}
