using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProSoft.EF.DbContext;
using ProSoft.EF.DTOs.Shared;
using ProSoft.EF.DTOs.Stocks;
using ProSoft.EF.IRepositories.Stocks;
using ProSoft.EF.Models;
using ProSoft.EF.Models.Shared;
using ProSoft.EF.Models.Stocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.Core.Repositories.Stocks
{
    public class UserTransRepo: IUserTransRepo
    {
        private readonly AppDbContext _Context;
        private readonly IMapper _mapper;
        public UserTransRepo(AppDbContext Context, IMapper mapper)
        {
            _Context = Context;
            _mapper = mapper;
        }

        public async Task<List<UserTransViewDTO>> GetAllUserTransAsync()
        {
            List<AppUser> users = await _Context.Users.ToListAsync();
            List<UserTransViewDTO> userTransDTO = _mapper.Map<List<UserTransViewDTO>>(users);
            foreach (var item in userTransDTO)
            {
                //List<UserTranss> userTrans = await _Context.UserTransactions
                //    .Where(obj => obj.UsrId == item.UserCode).ToListAsync();
                //List<PermissionDefViewDTO> permissionsDTO = new();
                //foreach (var uTrans in userTrans)
                //{
                //    GeneralCode permission = await _Context.GeneralCodes
                //        .FirstOrDefaultAsync(obj => obj.GId == uTrans.GId);
                //    var permissionDTO = _mapper.Map<PermissionDefViewDTO>(permission);
                //    permissionsDTO.Add(permissionDTO);
                //}
                //item.Permissions = permissionsDTO;
                item.Permissions = await GetPermissionsForUserAsync(item.UserCode);
            }
            return userTransDTO;
        }

        public async Task<UserTransEditAddDTO> GetEmptyUserTransAsync(int id)
        {
            UserTransEditAddDTO userTransDTO = new();
            List<StoreTran> storeTrans = await _Context.StoreTrans.ToListAsync();
            
            userTransDTO.TransactionTypes = _mapper.Map<List<StoreTransDTO>>(storeTrans);
            userTransDTO.Transactions = await GetPermissionsForUserAsync(id);
            return userTransDTO;
        }

        public async Task<List<PermissionDefViewDTO>> GetPermissionsByTransTypeAsync(string transType)
        {
            List<GeneralCode> permissions = await _Context.GeneralCodes
                .Where(obj => obj.GType == transType).ToListAsync();
            List<PermissionDefViewDTO> permissionsDTO = _mapper.Map<List<PermissionDefViewDTO>>(permissions);

            return permissionsDTO;
        }

        public async Task<List<PermissionDefViewDTO>> GetPermissionsByTransTypeAsync(string transType, int userCode)
        {
            List<GeneralCode> permissions = await _Context.GeneralCodes
                .Where(obj => obj.GType == transType).ToListAsync();

            List<UserTranss> userTrans = await _Context.UserTransactions
                .Where(obj => obj.UsrId == userCode).ToListAsync();
            foreach (var item in userTrans)
            {
                GeneralCode permission = permissions.Find(obj => obj.GId == item.GId);
                if (permission != null)
                    permissions.Remove(permission);
            }
            List<PermissionDefViewDTO> permissionsDTO = _mapper.Map<List<PermissionDefViewDTO>>(permissions);

            return permissionsDTO;
        }

        public async Task<List<PermissionDefViewDTO>> GetPermissionsForUserAsync(int userCode)
        {
            List<UserTranss> userTrans = await _Context.UserTransactions
                .Where(obj => obj.UsrId == userCode).ToListAsync();
            List<PermissionDefViewDTO> permissionsDTO = new();
            foreach (var item in userTrans)
            {
                GeneralCode permission = await _Context.GeneralCodes
                    .FirstOrDefaultAsync(obj => obj.GId == item.GId);

                var permissionDTO = _mapper.Map<PermissionDefViewDTO>(permission);
                permissionDTO.TransactionType = (await _Context.StoreTrans
                    .FindAsync(int.Parse(permission.GType))).TransDesc;
                permissionsDTO.Add(permissionDTO);
            }
            return permissionsDTO;
        }

        public async Task AddUserTransAsync(UserTransEditAddDTO userTransDTO)
        {
            UserTranss userTrans = _mapper.Map<UserTranss>(userTransDTO);
            userTrans.TransFlag = 0;
            await _Context.AddAsync(userTrans);
        }

        public async Task DeleteUserTransAsync(int id, int userCode)
        {
            UserTranss userTrans = await _Context.UserTransactions
                .FirstOrDefaultAsync(obj => obj.UsrId == userCode && obj.GId == id);
            _Context.Remove(userTrans);
        }

        public async Task SaveChangesAsync()
        {
            await _Context.SaveChangesAsync();
        }
    }
}
