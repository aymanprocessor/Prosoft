using Microsoft.AspNetCore.Mvc.Rendering;
using ProSoft.EF.Models;
using ProSoft.EF.Models.Medical.HospitalPatData;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Medical.HospitalPatData
{
    public class PriceListDTO
    {
        public int? PLId { get; set; }
        public string PlDesc { get; set; }
        public int? Flag1 { get; set; }
        
        public DateTime? PLDate { get; set; }
        public int? Year { get; set; }

        public IEnumerable<PriceList> PriceList { get; set; } = Enumerable.Empty<PriceList>();
        public IEnumerable<PriceListDetailDTO> PriceListDetail { get; set; } = Enumerable.Empty<PriceListDetailDTO>();
        public IEnumerable<SelectListItem> MainClinic { get; set; } = Enumerable.Empty<SelectListItem>();
        public IEnumerable<SelectListItem> SubClinic { get; set; } = Enumerable.Empty<SelectListItem>();
        public IEnumerable<SelectListItem> Services { get; set; } = Enumerable.Empty<SelectListItem>();


    }
}
