using ProSoft.EF.DTOs.Accounts;
using ProSoft.EF.DTOs.Treasury;
using ProSoft.EF.Migrations;
using ProSoft.EF.Models.Treasury;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.IRepositories.Treasury
{
    public interface IAccSafeCheckRepo : IRepository<AccSafeCheck,int>
    {
        Task<List<AccSafeCheckViewDTO>> GetAccSafeCashAsync(string tranType, string flagType, int fYear, int safeCode);
        //Task<int> GetNewIdAsync();
        Task<int> GetNewSerialAsync(string tranType, int safeCode, int fYear);
        Task<List<AccSubCodeDTO>> GetSubCodesFromAccAsync(string mainAccCode);
        Task<AccSafeCheckEditAddDTO> GetEmptyAccSafeCeckAsync();
        Task AddAccSafeCeckAsync(AccSafeCheckEditAddDTO accSafeCeckDTO);
        //Task<AccSafeCashEditAddDTO> GetAccSafeCashByIdAsync(int id);
        //Task EditAccSafeCashAsync(int id, AccSafeCheckEditAddDTO accSafeCeckDTO);
        //Task<bool> HasRelatedDataAsync(int id);
    }
}
