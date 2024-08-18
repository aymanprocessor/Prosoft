using ProSoft.EF.Models.Medical.HospitalPatData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.IRepositories.Stocks
{
    public interface IClassificationCustRepo : IRepository<ClassificationCust, int>
    {
        Task<int> GetNewIdAsync();
    }
}
