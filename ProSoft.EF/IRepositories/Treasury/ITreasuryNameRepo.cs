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
        Task<TreasuryNameEditAddDTO> GetEmptyTreasuryNameAsync(int id);

        Task AddTreasuryNameAsync(TreasuryNameEditAddDTO treasuryNameDTO);
        Task<TreasuryNameEditAddDTO> GetTreasuryNameByIdAsync(int id);
        Task EditTreasuryNameAsync(int id, TreasuryNameEditAddDTO treasuryNameDTO);


    }
}
