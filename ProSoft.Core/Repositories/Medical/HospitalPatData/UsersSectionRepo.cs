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
    public class UsersSectionRepo : Repository<UsersSection, int>, IUsersSectionRepo
    {
        public UsersSectionRepo(AppDbContext Context) : base(Context)
        {
        }
    }
}
