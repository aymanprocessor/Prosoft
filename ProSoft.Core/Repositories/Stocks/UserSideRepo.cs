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
    public class UserSideRepo : Repository<UserSide,int>, IUserSideRepo 
    {
        private readonly AppDbContext _context;

        public UserSideRepo(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserSide>> GetAllUserSidesAsync()
        {
            return await _context.UserSides
                .Include(u => u.Users)
                .Include(u => u.UserGroups)
                .Include(u => u.Branchs)
                .Include(u => u.EisSectionTypes)
                .Include(u => u.Regions)
                             .ToListAsync();
        }

        public async Task<UserSide> GetUserSideByIdAsync(int userCode, int sideId, int deptId, int branchId)
        {
            var side = await _context.UserSides
                                      .FindAsync(userCode, sideId, deptId, branchId);
            if (side == null)
            {
                return null;
            }
            return side;
        }

        //public async Task AddUserSideAsync(UserSideEditAddDTO userSideDto)
        //{
        //    //var side = new UserSide
        //    //{
        //    //    USER_CODE = userSideDto.UserCode,
        //    //    SIDE_ID = userSideDto.SideId,
        //    //    DEPT_ID = userSideDto.DeptId,
        //    //    BRANCH_ID = userSideDto.BranchId,
        //    //    USER_G_ID = userSideDto.UserGId,
        //    //    FLAG1 = userSideDto.FLAG1,
        //    //    EIS_SYS_ID = userSideDto.EIS_SYS_ID
        //    //};
        //    //_context.UserSides.Add(side);
        //    await _context.SaveChangesAsync();
        //}

        //public async Task UpdateUserSideAsync(UserSideEditAddDTO userSideDto)
        //{
        //    var side = await _context.UserSides
        //                              .FindAsync(userSideDto.UserCode, userSideDto.SideId, userSideDto.DeptId, userSideDto.BranchId);
        //    if (side == null)
        //    {
        //        return;
        //    }
        //    //side.USER_G_ID = userSideDto.UserGId;
        //    //side.FLAG1 = userSideDto.FLAG1;
        //    //side.EIS_SYS_ID = userSideDto.EIS_SYS_ID;
        //    _context.UserSides.Update(side);
        //    await _context.SaveChangesAsync();
        //}

        //public async Task DeleteUserSideAsync(int userCode, int sideId, int deptId, int branchId)
        //{
        //    var side = await _context.UserSides
        //                              .FindAsync(userCode, sideId, deptId, branchId);
        //    if (side != null)
        //    {
        //        _context.UserSides.Remove(side);
        //        await _context.SaveChangesAsync();
        //    }
        //}
    }

}
