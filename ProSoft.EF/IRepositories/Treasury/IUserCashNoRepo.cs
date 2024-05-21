using ProSoft.EF.DTOs.Accounts;
using ProSoft.EF.DTOs.Stocks;
using ProSoft.EF.DTOs.Treasury;
using ProSoft.EF.Models.Stocks;
using ProSoft.EF.Models.Treasury;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.IRepositories.Treasury
{
    public interface IUserCashNoRepo : IRepository<UserCashNo,int>
    {
        Task<List<UserCashNoViewDTO>> GetCashForUserAsync(int userCode);
        Task<UserCashNoEditAddDTO> GetEmptySafeTransAsync(int userCode);
        Task AddSafeTransAsync(UserCashNoEditAddDTO userCashNoDTO);
        Task<UserCashNoEditAddDTO> GetSafeTransByIdAsync(int id);
        Task<List<AccSubCodeDTO>> GetSubCodesFromAccAsync(string mainAccCode);

        Task EditSafeTransAsync(int userCode, UserCashNoEditAddDTO userCashNoDTO);



    }
}
