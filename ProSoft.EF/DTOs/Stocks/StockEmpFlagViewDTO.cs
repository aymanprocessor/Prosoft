using Microsoft.AspNetCore.Mvc.Rendering;
using ProSoft.EF.DTOs.Auth;
using ProSoft.EF.DTOs.Shared;
using ProSoft.EF.Models.Stocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Stocks
{
    public class StockEmpFlagViewDTO
    {
        public int Stkcod { get; set; }
        public int BranchId { get; set; }
        public int KID { get; set; }
        public int UserCode { get; set; }
        
        public IEnumerable<SelectListItem> Users { get; set; } = Enumerable.Empty<SelectListItem>();

        public IEnumerable<StockEmpFlag> StockEmpFlags  { get; set; } = Enumerable.Empty<StockEmpFlag>();

    }
}
