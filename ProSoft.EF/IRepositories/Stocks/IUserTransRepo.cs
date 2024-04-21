using ProSoft.EF.DTOs.Shared;
using ProSoft.EF.DTOs.Stocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.IRepositories.Stocks
{
    public interface IUserTransRepo
    {
        Task<List<UserTransViewDTO>> GetAllUserTransAsync();
        Task<UserTransEditAddDTO> GetEmptyUserTransAsync();
        Task<List<PermissionDefViewDTO>> GetPermissionsByTransTypeAsync(string transType);
        Task<List<PermissionDefViewDTO>> GetPermissionsForUserAsync(int userCode);
        Task AddUserTransAsync(UserTransEditAddDTO userTransDTO);
        Task SaveChangesAsync();
    }
}
