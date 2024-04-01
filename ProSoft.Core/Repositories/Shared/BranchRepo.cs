using ProSoft.EF.DbContext;
using ProSoft.EF.IRepositories.Shared;
using ProSoft.EF.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.Core.Repositories.Shared
{
    public class BranchRepo : Repository<Branch, int>, IBranchRepo
    {
        public BranchRepo(AppDbContext Context) : base(Context)
        {
        }
    }
}
