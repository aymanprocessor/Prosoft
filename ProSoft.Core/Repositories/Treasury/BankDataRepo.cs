using Microsoft.EntityFrameworkCore;
using ProSoft.EF.DbContext;
using ProSoft.EF.IRepositories.Treasury;
using ProSoft.EF.Models.Treasury;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.Core.Repositories.Treasury
{
    public class BankDataRepo :Repository<BankData,int> ,IBankDataRepo
    {
        public BankDataRepo(AppDbContext Context) : base(Context)
        {
        }

        public async Task<int> GetNewIdAsync()
        {
            int newID;
            if (_DbSet.Count() != 0)
            {
                var lastID = await _DbSet.MaxAsync(obj => obj.BankNo);
                newID = lastID + 1;
            }
            else
                newID = 1;
            return newID;
        }
    }
}
