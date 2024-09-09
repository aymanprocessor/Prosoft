using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProSoft.EF.DbContext;
using ProSoft.EF.DTOs.Stocks;
using ProSoft.EF.IRepositories.Stocks;
using ProSoft.EF.Models.Stocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.Core.Repositories.Stocks
{
    public class UsersGroupRepo : IUsersGroupRepo
    {
        private readonly AppDbContext _context;

        public UsersGroupRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UsersGroup>> GetAllUsersGroupsAsync()
        {
            return await _context.UsersGroups
                                 .ToListAsync();
        }

        public async Task<UsersGroup> GetUsersGroupByIdAsync(int id)
        {
            var group = await _context.UsersGroups.FindAsync(id);
            if (group == null)
            {
                return null;
            }
            return group;
        }

        public async Task AddUsersGroupAsync(UsersGroupDTO usersGroupDto)
        {
            var group = new UsersGroup
            {
                G_ID = usersGroupDto.G_ID,
                G_NAME = usersGroupDto.G_NAME,
                FLAG = usersGroupDto.FLAG
            };
            _context.UsersGroups.Add(group);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUsersGroupAsync(UsersGroupDTO usersGroupDto)
        {
            var group = await _context.UsersGroups.FindAsync(usersGroupDto.G_ID);
            if (group == null)
            {
                return;
            }
            group.G_NAME = usersGroupDto.G_NAME;
            group.FLAG = usersGroupDto.FLAG;
            _context.UsersGroups.Update(group);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUsersGroupAsync(int id)
        {
            var group = await _context.UsersGroups.FindAsync(id);
            if (group != null)
            {
                _context.UsersGroups.Remove(group);
                await _context.SaveChangesAsync();
            }
        }

        public IEnumerable<SelectListItem> GetAllAsEnumerable()
        {
            return _context.UsersGroups.Select(e => new SelectListItem { Value = e.G_ID.ToString(), Text = e.G_NAME }).ToList();

        }
    }

}
