using Microsoft.EntityFrameworkCore;
using ProSoft.EF.DbContext;
using ProSoft.EF.IRepositories.Accounts;
using ProSoft.EF.Models.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.Core.Repositories.Accounts
{
    public class CostCenterRepo : Repository<CostCenter, int>, ICostCenterRepo
    {
        public CostCenterRepo(AppDbContext Context) : base(Context)
        {
        }
        public async Task<int> GetNewIdAsync()
        {
            int newID;
            if (_DbSet.Count() != 0)
            {
                var lastID = await _DbSet.MaxAsync(obj => obj.CostCode);
                newID = lastID + 1;
            }
            else
                newID = 1;
            return newID;
        }
    }
}
