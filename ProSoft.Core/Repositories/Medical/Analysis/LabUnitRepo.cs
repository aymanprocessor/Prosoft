using Microsoft.EntityFrameworkCore;
using ProSoft.EF.DbContext;
using ProSoft.EF.IRepositories.Medical.Analysis;
using ProSoft.EF.Models.Medical.Analysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.Core.Repositories.Medical.Analysis
{
    public class LabUnitRepo : Repository<LabUnit, short>, ILabUnitRepo
    {
        public LabUnitRepo(AppDbContext Context) : base(Context)
        {
        }

        public async Task<short> GetNewIdAsync()
        {
            short newID;
            if (_DbSet.Count() != 0)
            {
                var lastID = await _DbSet.MaxAsync(obj => obj.LabUnitCode);
                newID = (short)(lastID + 1);
            }
            else
                newID = 1;
            return newID;
        }
    }
}
