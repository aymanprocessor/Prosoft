using Microsoft.EntityFrameworkCore;
using ProSoft.EF.DbContext;
using ProSoft.EF.DTOs.Stocks;
using ProSoft.EF.IRepositories.Stocks;
using ProSoft.EF.Models.Stocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.Core.Repositories.Stocks
{
    public class StockRepo : Repository<Stock, int>, IStockRepo
    {
        public StockRepo(AppDbContext Context) : base(Context)
        {
        }

        public Task<List<StockViewDTO>> GetAllStocksAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetNewIdAsync()
        {
            int newID;
            if (_DbSet.Count() != 0)
            {
                var lastID = await _DbSet.MaxAsync(obj => obj.Stkcod);
                newID = lastID + 1;
            }
            else
                newID = 1;
            return newID;
        }
    }
}
