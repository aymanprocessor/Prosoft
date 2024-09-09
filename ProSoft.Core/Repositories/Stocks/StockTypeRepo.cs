using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProSoft.EF.DbContext;
using ProSoft.EF.IRepositories.Stocks;
using ProSoft.EF.Models.Stocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.Core.Repositories.Stocks
{
    public class StockTypeRepo : Repository<KindStore, int>, IStockTypeRepo
    {
        private readonly AppDbContext _context;

        public StockTypeRepo(AppDbContext Context) : base(Context)
        {
            _context = Context;
        }


        // ------------------- Coded By Ayman Saad ------------------- //

        public IEnumerable<SelectListItem> GetAllStockTypeAsEnumerable()
        {
            return _context.KindStores.Select(k => new SelectListItem { Value = k.KId.ToString(), Text = k.KName })
                .ToList();
        }
        // ------------------- Coded By Ayman Saad ------------------- //
        public async Task<int> GetNewIdAsync()
        {
            int newID;
            if (_DbSet.Count() != 0)
            {
                var lastID = await _DbSet.MaxAsync(obj => obj.KId);
                newID = lastID + 1;
            }
            else
                newID = 1;
            return newID;
        }
    }
}
