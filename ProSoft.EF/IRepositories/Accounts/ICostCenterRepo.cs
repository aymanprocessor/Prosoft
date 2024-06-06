using ProSoft.EF.Models.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.IRepositories.Accounts
{
    public interface ICostCenterRepo :IRepository<CostCenter,int>
    {
        Task<int> GetNewIdAsync();
    }
}
