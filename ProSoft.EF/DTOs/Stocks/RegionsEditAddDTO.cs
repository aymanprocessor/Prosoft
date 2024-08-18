using ProSoft.EF.DTOs.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Stocks
{
    public class RegionsEditAddDTO
    {
        
        public int RegionCode { get; set; }

        [DisplayName("Stock")]
        public int? StockCode { get; set; }
        public string? RegionDesc { get; set; }
        public int? ClassificationCust { get; set; }

        [DisplayName("Is Active")]
        public int? OnOff { get; set; }
        [DisplayName("Send Order")]
        public int? RegYear { get; set; }

        [DisplayName("Section Name")]
        [Required(ErrorMessage = "The field is required")]
        public int SectionCode { get; set; }

        public List<StockViewDTO>? Stocks { get; set; }
        public List<SectionViewDTO>? Sections { get; set; }

    }
}
