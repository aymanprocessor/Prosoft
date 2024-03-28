using Microsoft.AspNetCore.Identity;
using ProSoft.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.IRepositories
{
    public interface IRoleRepo
    {
        Task<List<IdentityRole>> GetAllRolesAsync();
        Task<IdentityRole> GetRoleByIdAsync(string id);
        Task<List<IdentityUserRole<string>>> GetAllUserRolesAsync();
        Task<List<IdentityUserRole<string>>> GetUserRolesByUserAsync(string id);
    }
}
