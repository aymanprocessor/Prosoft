using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProSoft.EF.DbContext;
using ProSoft.EF.DTOs.Accounts;
using ProSoft.EF.DTOs.Stocks;
using ProSoft.EF.DTOs.Treasury;
using ProSoft.EF.IRepositories.Treasury;
using ProSoft.EF.Models;
using ProSoft.EF.Models.Accounts;
using ProSoft.EF.Models.Medical.HospitalPatData;
using ProSoft.EF.Models.Stocks;
using ProSoft.EF.Models.Treasury;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.Core.Repositories.Treasury
{
    public class UserCashNoRepo : Repository<UserCashNo, int>, IUserCashNoRepo
    {
        private readonly IMapper _mapper;
        public UserCashNoRepo(AppDbContext Context, IMapper mapper) : base(Context)
        {
            _mapper = mapper;
        }

        public async Task<List<UserCashNoViewDTO>> GetCashForUserAsync(int userCode)
        {
            List<UserCashNo> userCashNos = await _Context.userCashNos
                .Where(obj => obj.UserCode == userCode).ToListAsync();

            var userCashNoViewDTOs = new List<UserCashNoViewDTO>();
            foreach (var item in userCashNos)
            {
                var myUserCashNoViewDTO = _mapper.Map<UserCashNoViewDTO>(item);
                myUserCashNoViewDTO.SafeNames = (await _Context.SafeNames
                    .FirstOrDefaultAsync(obj => obj.SafeCode == item.SafeCode)).SafeNames;
                myUserCashNoViewDTO.SafeSub = await GetAccountName(item.MainCode, item.SubCode);
                myUserCashNoViewDTO.SafeMain = await GetAccountName(item.MainCode2, item.SubCode2);
                userCashNoViewDTOs.Add(myUserCashNoViewDTO);
            }
            return userCashNoViewDTOs;
        }

        private async Task<string> GetAccountName(string mainCode, string subCode)
        {
            AccMainCode myMainCode = await _Context.AccMainCodes
                .FirstOrDefaultAsync(obj => obj.MainCode == mainCode);
            string mainName = myMainCode.MainName;

            AccSubCode mySubCode = await _Context.AccSubCodes
                .FirstOrDefaultAsync(obj => obj.SubCode == subCode && obj.MainCode == mainCode);
            string subName = mySubCode! != null ? mySubCode.SubName : string.Empty;

            return subName != "" ? $"{mainName} / {subName}" : mainName;
        }

        public async Task<UserCashNoEditAddDTO> GetEmptySafeTransAsync(int userCode)
        {
            var userCashNoDTO = new UserCashNoEditAddDTO();

            List<SafeName> safeNames = await _Context.SafeNames.ToListAsync();
            List<AccMainCode> mainAccCodes = await _Context.AccMainCodes.ToListAsync();

            userCashNoDTO.treasuryNames = _mapper.Map<List<TreasuryNameViewDTO>>(safeNames);
            userCashNoDTO.MainAccCodes = _mapper.Map<List<AccMainCodeDTO>>(mainAccCodes);
            return userCashNoDTO;
        }

        public async Task AddSafeTransAsync(UserCashNoEditAddDTO userCashNoDTO)
        {
            var userCashNo = _mapper.Map<UserCashNo>(userCashNoDTO);
            userCashNo.MCode = 11;

            await _Context.AddAsync(userCashNo);
            await _Context.SaveChangesAsync();
        }


        public async Task<UserCashNoEditAddDTO> GetSafeTransByIdAsync(int id)
        {
            UserCashNo userCashNo = await _DbSet.FirstOrDefaultAsync(obj => obj.UserCashID == id);
            var userCashNoDTO = _mapper.Map<UserCashNoEditAddDTO>(userCashNo);

            List<SafeName> safeNames = await _Context.SafeNames.ToListAsync();
            List<AccMainCode> mainAccCodes = await _Context.AccMainCodes.ToListAsync();

            userCashNoDTO.treasuryNames = _mapper.Map<List<TreasuryNameViewDTO>>(safeNames);
            userCashNoDTO.MainAccCodes = _mapper.Map<List<AccMainCodeDTO>>(mainAccCodes);
            return userCashNoDTO;
        }

        public async Task<List<AccSubCodeDTO>> GetSubCodesFromAccAsync(string mainAccCode)
        {
            List<AccSubCode> subAccCodes = await _Context.AccSubCodes
                .Where(obj => obj.MainCode == mainAccCode).ToListAsync();
            var subAccCodesDTO = _mapper.Map<List<AccSubCodeDTO>>(subAccCodes);
            return subAccCodesDTO;
        }
        public async Task EditSafeTransAsync(int id, UserCashNoEditAddDTO userCashNoDTO)
        {
            UserCashNo userCashNo = await _Context.userCashNos.FirstOrDefaultAsync(
               obj => obj.UserCashID == id);
            var userId = userCashNo.UserCode;

            _mapper.Map(userCashNoDTO, userCashNo);
            userCashNo.UserCode = userId ?? 0;
            if (userCashNo != null)
              _Context.Update(userCashNo);
            await _Context.SaveChangesAsync();
        }
    }
}
