using Microsoft.AspNetCore.Mvc.Rendering;
using ProSoft.EF.DTOs.Stocks;
using ProSoft.EF.Models.Stocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.IRepositories.Stocks
{
    public interface IUsersGroupRepo
    {

        Task<IEnumerable<UsersGroup>> GetAllUsersGroupsAsync();
        Task<UsersGroup> GetUsersGroupByIdAsync(int id);
        Task AddUsersGroupAsync(UsersGroupDTO usersGroupDto);
        Task UpdateUsersGroupAsync(UsersGroupDTO usersGroupDto);
        Task DeleteUsersGroupAsync(int id);
        public IEnumerable<SelectListItem> GetAllAsEnumerable();
    }

}
