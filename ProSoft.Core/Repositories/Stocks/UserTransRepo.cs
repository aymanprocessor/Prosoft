using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProSoft.EF.DbContext;
using ProSoft.EF.DTOs.Shared;
using ProSoft.EF.DTOs.Stocks;
using ProSoft.EF.IRepositories.Stocks;
using ProSoft.EF.Models;
using ProSoft.EF.Models.Shared;
using ProSoft.EF.Models.Stocks;

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

        public async Task<UserTransEditAddDTO> GetUserTransByIdAsync(int userCode, int gId)
        {
            UserTranss userTrans = await _Context.UserTransactions
                .FirstOrDefaultAsync(obj => obj.UsrId == userCode && obj.GId == gId);
            var userTransDTO = _mapper.Map<UserTransEditAddDTO>(userTrans);
            return userTransDTO;
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

        public async Task<List<PermissionDefViewDTO>> GetPermissionsForUserAsync(int userCode, int transType)
        {
            List<UserTranss> userTrans = await _Context.UserTransactions
                .Where(obj => obj.UsrId == userCode &&
                    obj.DType == transType.ToString()).ToListAsync();
            List<GeneralCode> permissions = await _Context.GeneralCodes
                .Where(obj => obj.GType == transType.ToString() &&
                obj.ShowHide == 1).ToListAsync();
            List<PermissionDefViewDTO> permissionsDTO = new();
            
            if(userTrans.Count() == 0)
            {
                if(permissions.Count() != 0)
                {
                    foreach (var item in permissions)
                    {
                        var newUserTrans = new UserTranss();
                        newUserTrans.UsrId = userCode;
                        newUserTrans.GId = item.GId;
                        newUserTrans.DType = transType.ToString();
                        newUserTrans.TransFlag = 0;
                        newUserTrans.UeIns = 0;
                        newUserTrans.UeDel = 0;
                        newUserTrans.UeSav = 0;
                        await _Context.AddAsync(newUserTrans);

                        var permissionDTO = _mapper.Map<PermissionDefViewDTO>(item);
                        permissionDTO.TransactionType = (await _Context.StoreTrans
                            .FindAsync(transType)).TransDesc;
                        permissionsDTO.Add(permissionDTO);
                    }
                    await SaveChangesAsync();
                }
            }
            else
            {
                if (permissions.Count() != 0)
                {
                    foreach (var item in permissions)
                    {
                        UserTranss userTranss = await _Context.UserTransactions
                            .FindAsync(userCode, item.GId);
                        if (userTranss == null)
                        {
                            var newUserTrans = new UserTranss();
                            newUserTrans.UsrId = userCode;
                            newUserTrans.GId = item.GId;
                            newUserTrans.DType = transType.ToString();
                            newUserTrans.TransFlag = 0;
                            newUserTrans.UeIns = 0;
                            newUserTrans.UeDel = 0;
                            newUserTrans.UeSav = 0;
                            await _Context.AddAsync(newUserTrans);
                        }

                        var permissionDTO = _mapper.Map<PermissionDefViewDTO>(item);
                        permissionDTO.TransactionType = (await _Context.StoreTrans
                            .FindAsync(transType)).TransDesc;
                        permissionsDTO.Add(permissionDTO);
                    }
                    await SaveChangesAsync();
                }
            }
            return permissionsDTO;
        }

        public async Task UpdateUserTransAsync(int userCode, UserTransEditAddDTO userTransDTO)
        {
            UserTranss userTrans = await _Context.UserTransactions
                .FirstOrDefaultAsync(obj => obj.UsrId == userCode &&
                obj.GId == userTransDTO.GId);

            _mapper.Map(userTransDTO, userTrans);
            _Context.Update(userTrans);
        }

        public async Task SaveChangesAsync()
        {
            await _Context.SaveChangesAsync();
        }
    }
}
