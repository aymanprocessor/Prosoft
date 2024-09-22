using ProSoft.EF.Models.Stocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Stocks
{
    public  class StockBalanceViewDTO
    {
        public int StkbalanceId { get; set; }
        public MainItem MainItem { get; set; }
        public int? UnitCode { get; set; }
        public DateTime? QtyStartDt { get; set; }
        public UnitCode UnitCodee { get; set; }
        public int? ExpM { get; set; }
        public int? ExpYr { get; set; }
        public decimal? QtyStart { get; set; }
        public decimal? ItemPrice { get; set; }
        public decimal? ItemPrice2 { get; set; }


    }
}
