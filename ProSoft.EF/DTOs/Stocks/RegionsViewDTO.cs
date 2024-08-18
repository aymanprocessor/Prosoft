using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Stocks
{
    public class RegionsViewDTO
    {
        public int RegionCode { get; set; }
        public string? RegionDesc { get; set; }
        public int? OnOff { get; set; }
        public int? RegYear { get; set; }
        public string? StockName { get; set; }
       // public string? SectionName { get; set; }
    }
}
