using ProSoft.EF.DTOs.Stocks;
using ProSoft.EF.Models.Stocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.IRepositories.Stocks
{
    public interface IUserSideRepo : IRepository<UserSide,int>
    {
        Task<IEnumerable<UserSide>> GetAllUserSidesAsync();
        Task<UserSide> GetUserSideByIdAsync(int userCode, int sideId, int deptId, int branchId);
        //Task AddUserSideAsync(UserSideEditAddDTO userSideDto);
        //Task UpdateUserSideAsync(UserSideEditAddDTO userSideDto);
        //Task DeleteUserSideAsync(int userCode, int sideId, int deptId, int branchId);
    }
}
