using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProSoft.EF.Models.Stocks;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Stocks
{
    public class StockBalanceViewDTO
    {
        public short? Stkcod { get; set; }
        public string? ItemCode { get; set; }
        public decimal? QtyStart { get; set; }
        public DateTime? QtyStartDt { get; set; }
        public decimal? ItemPrice { get; set; }
        public int? FYear { get; set; }
        public decimal? ItemPrice2 { get; set; }
        public int? ExpM { get; set; }
        public int? ExpYr { get; set; }
        public MainItem? MainItem { get; set; } 
        public SubItem? SubItem { get; set; }
        public UnitCode? UnitCodee { get; set; }

    }
}
