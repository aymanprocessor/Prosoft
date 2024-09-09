using Microsoft.AspNetCore.Mvc.Rendering;
using ProSoft.EF.Models;
using ProSoft.EF.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.IRepositories
{
    public interface IUserRepo 
    {
        // ------------------- Coded By Ayman Saad ------------------- //
        public IEnumerable<SelectListItem> GetAllUsersAsEnumerable();
        // ------------------- Coded By Ayman Saad ------------------- //
        Task<List<AppUser>> GetAllUsersAsync();
        Task<AppUser> GetUserByIdAsync(int id);
        Task<Branch> GetUserBranchAsync(int branchID);
    }
}
