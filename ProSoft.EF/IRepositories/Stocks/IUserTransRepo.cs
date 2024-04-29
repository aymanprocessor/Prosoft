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
        Task<UserTransEditAddDTO> GetUserTransByIdAsync(int userCode, int gId);
        Task<List<PermissionDefViewDTO>> GetPermissionsForUserAsync(int userCode);
        Task<List<PermissionDefViewDTO>> GetPermissionsForUserAsync(int userCode, int transType);
        Task UpdateUserTransAsync(int userCode, UserTransEditAddDTO userTransDTO);
        Task SaveChangesAsync();
    }
}
