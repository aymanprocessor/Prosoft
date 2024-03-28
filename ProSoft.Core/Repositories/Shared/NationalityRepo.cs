using Microsoft.EntityFrameworkCore;
using ProSoft.EF.DbContext;
using ProSoft.EF.IRepositories;
using ProSoft.EF.IRepositories.Shared;
using ProSoft.EF.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.Core.Repositories.Shared
{
    public class NationalityRepo : Repository<NationalityEi, int>, INationalityRepo
    {
        public NationalityRepo(AppDbContext Context) : base(Context)
        {
        }
        public async Task<int> GetNewIdAsync()
        {
            int newID;
            if (_DbSet.Count() != 0)
            {
                var lastID = await _DbSet.MaxAsync(obj => obj.NationalityId);
                newID = lastID + 1;
            }
            else
                newID = 1;
            return newID;
        }
    }
}
