using ProSoft.EF.DTOs.Treasury;
using ProSoft.EF.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.IRepositories.Treasury
{
    public interface IAccGlobalDefRepo :IRepository<AccGlobalDef,int> 
    {
        Task<List<AccGlobalDefDTO>> GetAllCurrencyAsync();
        Task<int> GetNewIdAsync();

    }
}
