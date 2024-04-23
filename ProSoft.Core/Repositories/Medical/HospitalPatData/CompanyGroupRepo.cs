using Microsoft.EntityFrameworkCore;
using ProSoft.EF.DbContext;
using ProSoft.EF.IRepositories.Medical.HospitalPatData;
using ProSoft.EF.Models.Medical.HospitalPatData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.Core.Repositories.Medical.HospitalPatData
{
    public class CompanyGroupRepo : Repository<CompanyGroup, int>, ICompanyGroupRepo
    {
        public CompanyGroupRepo(AppDbContext Context) : base(Context)
        {

        }

        public async Task<int> GetNewIdAsync()
        {
            int newID;
            if (_DbSet.Count() != 0)
            {
                var lastID = await _DbSet.MaxAsync(obj => obj.GroupId);
                newID = lastID + 1;
            }
            else
                newID = 1;
            return newID;
        }
    }
}
