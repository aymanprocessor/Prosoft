using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProSoft.EF.DbContext;
using ProSoft.EF.DTOs.Accounts;
using ProSoft.EF.DTOs.Auth;
using ProSoft.EF.DTOs.Shared;
using ProSoft.EF.DTOs.Treasury;
using ProSoft.EF.IRepositories.Treasury;
using ProSoft.EF.Models;
using ProSoft.EF.Models.Accounts;
using ProSoft.EF.Models.Shared;
using ProSoft.EF.Models.Treasury;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.Core.Repositories.Treasury
{
    public class ReportCashAndChecksRepo : IReportCashAndChecksRepo
    {
        private readonly AppDbContext  _Context;
        private readonly IMapper _mapper;
        public ReportCashAndChecksRepo(AppDbContext Context, IMapper mapper)
        {
            _Context = Context;
            _mapper = mapper;
        }
        public async Task<ReportCashAndChecksDTO> GetAllDataAsync(int userCode)
        {
            ReportCashAndChecksDTO reportCashAndChecksDTO = new ReportCashAndChecksDTO();

            var safeCode = (await _Context.userCashNos.FirstOrDefaultAsync(obj => obj.UserCashID == userCode)).SafeCode;
            var safeName = (await _Context.SafeNames.FindAsync(safeCode)).SafeNames;


            List<Branch> branches = await _Context.Branchs.ToListAsync();
            List<AppUser> users = await _Context.Users.ToListAsync();

            reportCashAndChecksDTO.branchDTOs = _mapper.Map<List<BranchDTO>>(branches);
            reportCashAndChecksDTO.userDTOs = _mapper.Map<List<UserDTO>>(users);
            reportCashAndChecksDTO.SafeNames = safeName;

            return reportCashAndChecksDTO;
        }
    }
}
