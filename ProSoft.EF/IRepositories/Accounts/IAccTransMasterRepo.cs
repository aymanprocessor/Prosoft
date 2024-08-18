using ProSoft.EF.DTOs.Accounts;
using ProSoft.EF.DTOs.Treasury;
using ProSoft.EF.Models.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.IRepositories.Accounts
{
    public interface IAccTransMasterRepo :IRepository<AccTransMaster,int>
    {
        Task<List<AccTransMasterViewDTO>> GetAccTransMasterAsync(int journalCode,int fYear);

        Task<AccTransMasterEditAddDTO> GetEmptyAccTransMasterAsync(int journalCode);
        Task<int> GetNewtranNoAsync(int journalCode, int fYear, int branchId);
        Task AddAccTransMasterAsync(AccTransMasterEditAddDTO accTransMasterDTO);
        Task<AccTransMasterEditAddDTO> GetAccTransMasterByIdAsync(int id);
        Task EditAccTransMasterAsync(int id, AccTransMasterEditAddDTO accTransMasterDTO);
    }
}
