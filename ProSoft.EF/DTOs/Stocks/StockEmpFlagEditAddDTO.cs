using Microsoft.AspNetCore.Mvc.Rendering;
using ProSoft.EF.DTOs.Auth;
using ProSoft.EF.DTOs.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Stocks
{
    public class StockEmpFlagEditAddDTO
    {
        [Display(Name = "Stock")]
        public int Stkcod { get; set; }
        public int orgStkcod { get; set; }

        [Display(Name ="Branch")]
        public int BranchId { get; set; }
        
        [Display(Name = "Stock Type")]
        public int KID { get; set; }
        public int orgKID { get; set; }
        public int UserCode { get; set; }
        public List<UserDTO>? Users { get; set; }

        public IEnumerable<SelectListItem> Stocks { get; set; } = Enumerable.Empty<SelectListItem>();
        public IEnumerable<SelectListItem> StockTypes { get; set; } = Enumerable.Empty<SelectListItem>();
        public IEnumerable<SelectListItem> Branchs { get; set; } = Enumerable.Empty<SelectListItem>();


    }
}
