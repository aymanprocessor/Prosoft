using ProSoft.EF.Models.Treasury;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.IRepositories.Treasury
{
    public interface IBankDataRepo :IRepository<BankData,int>
    {
        Task<int> GetNewIdAsync();
    }
}
