using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Stocks.ItemUnits
{
    public class ItemUnitsViewDTO
    {
        public IEnumerable<SelectListItem> Flag1 { get; set; } = Enumerable.Empty<SelectListItem>();
        public IEnumerable<SelectListItem> SubItems { get; set; } = Enumerable.Empty<SelectListItem>();
    }
}
