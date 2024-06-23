using ProSoft.EF.DTOs.Treasury;
using ProSoft.EF.Models.Treasury;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.IRepositories.Medical.HospitalPatData
{
    public interface IkinshipRepo :IRepository<GTable ,int>
    {
        Task<List<GTablelDTO>> GetAllkinshipAsync();
    }
}
