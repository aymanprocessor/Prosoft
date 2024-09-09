using Microsoft.AspNetCore.Mvc.Rendering;
using ProSoft.EF.Models.Stocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.IRepositories.Stocks
{
    public interface IStockTypeRepo:  IRepository<KindStore, int>
    {
        // ------------------- Coded By Ayman Saad ------------------- //
        public IEnumerable<SelectListItem> GetAllStockTypeAsEnumerable();
        // ------------------- Coded By Ayman Saad ------------------- //
        Task<int> GetNewIdAsync();
    }
}
