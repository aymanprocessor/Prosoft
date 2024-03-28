using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProSoft.EF.DbContext;
using ProSoft.EF.IRepositories;
using ProSoft.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.Core.Repositories
{
    public class RoleRepo : IRoleRepo
    {
        private readonly AppDbContext _Context;
        public RoleRepo(AppDbContext Context)
        {
            _Context = Context;
        }
        public async Task<List<IdentityRole>> GetAllRolesAsync()
        {
            List<IdentityRole> roless = await _Context.Roles.ToListAsync();
            return roless;
        }
        
        public async Task<IdentityRole> GetRoleByIdAsync(string id)
        {
            IdentityRole roles = await _Context.Roles
                .FirstOrDefaultAsync(obj => obj.Id == id);
            return roles;
        }

        public async Task<List<IdentityUserRole<string>>> GetAllUserRolesAsync()
        {
            List<IdentityUserRole<string>> userRoless = await _Context.UserRoles.ToListAsync();
            return userRoless;
        }

        public async Task<List<IdentityUserRole<string>>> GetUserRolesByUserAsync(string id)
        {
            // Get List of userRoles by user id
            List<IdentityUserRole<string>> userRoless = await _Context.UserRoles
                .Where(obj => obj.UserId == id).ToListAsync();
            return userRoless;
        }
    }
}
