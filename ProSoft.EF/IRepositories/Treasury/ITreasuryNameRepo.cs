using ProSoft.EF.DTOs.Medical.HospitalPatData;
using ProSoft.EF.DTOs.Treasury;
using ProSoft.EF.Models.Treasury;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.IRepositories.Treasury
{
    public interface ITreasuryNameRepo : IRepository<SafeName, int>
    {
        Task<List<TreasuryNameViewDTO>> GetAllTreasurysAsync();
        Task<int> GetNewIdAsync();

    }
}
