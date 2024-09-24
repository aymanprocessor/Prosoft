using Microsoft.AspNetCore.Mvc.Rendering;
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
        

        public List<Stkbalance> StockBalances { get; set; }

        public List<SelectListItem> UnitCodesList { get; set; }


    }
}
