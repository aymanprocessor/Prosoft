using ProSoft.EF.Models.Stocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.IRepositories.Medical.HospitalPatData
{
    public interface IStentsRepo : IRepository<StentDes,int>
    {
        Task<int> GetNewIdAsync();
    }
}
