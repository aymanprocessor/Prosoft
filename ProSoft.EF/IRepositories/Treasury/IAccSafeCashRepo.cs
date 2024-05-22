using ProSoft.EF.DTOs.Accounts;
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
    public interface IAccSafeCashRepo : IRepository<AccSafeCash, int>
    {
        Task<List<AccSafeCashViewDTO>> GetAccSafeCashAsync(string docType, string flagType, int fYear ,int safeCode);
       // Task<int> GetNewIdAsync();
        Task<int> GetNewSerialAsync(string docType, int safeCode, int fYear);
        Task<List<AccSubCodeDTO>> GetSubCodesFromAccAsync(string mainAccCode);
        Task<AccSafeCashEditAddDTO> GetEmptyAccSafeCashAsync();
        Task AddAccSafeCashAsync(AccSafeCashEditAddDTO accSafeCashDTO);
        Task<AccSafeCashEditAddDTO> GetAccSafeCashByIdAsync(int id);
        Task EditAccSafeCashAsync(int id, AccSafeCashEditAddDTO accSafeCashDTO);
    }
}
