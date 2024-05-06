using ProSoft.EF.DTOs.Accounts;
using ProSoft.EF.DTOs.Shared;
using ProSoft.EF.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.IRepositories.Shared
{
    public interface IEisPostingRepo: IRepository<EisPosting, int>
    {
        Task<List<EisPostingViewDTO>> GetAllCodesAsync();
        Task<EisPostingEditAddDTO> GetEisPostingByIdAsync(int id);
        Task<EisPostingEditAddDTO> GetEmptyEisPostingAsync();
        Task<List<AccSubCodeDTO>> GetSubCodesForAsync(string mainCode);
    }
}
