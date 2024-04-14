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
    public class SideRepo : Repository<Side, int>, ISideRepo
    {
        public SideRepo(AppDbContext Context) : base(Context)
        {
        }

        public async Task<int> GetNewIdAsync()
        {
            int newID;
            if (_DbSet.Count() != 0)
            {
                var lastID = await _DbSet.MaxAsync(obj => obj.SideId);
                newID = lastID + 1;
            }
            else
                newID = 1;
            return newID;
        }
    }
}
