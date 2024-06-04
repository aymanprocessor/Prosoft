using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProSoft.EF.DbContext;
using ProSoft.EF.DTOs.Accounts;
using ProSoft.EF.DTOs.Medical.HospitalPatData;
using ProSoft.EF.IRepositories.Accounts;
using ProSoft.EF.Models.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.Core.Repositories.Accounts
{
    public class UserJournalTypeRepo : Repository<UserJournalType, int>, IUserJournalTypeRepo
    {
        private readonly IMapper _mapper;

        public UserJournalTypeRepo(AppDbContext Context, IMapper mapper) : base(Context)
        {
            _mapper = mapper;
        }

        public async Task<List<UserJournalTypeDTO>> GetUserJournalTypesForUser(int userCode)
        {
            List<UserJournalTypeDTO> userJournalTypeDTOs = await _Context.UserJournalTypes.Where(obj => obj.UserCode == userCode)
                .Select(obj => new UserJournalTypeDTO()
                {
                    JournalCode = obj.JournalCode,
                    JournalName = obj.JournalType.JournalName
                })
                .ToListAsync();
            return userJournalTypeDTOs;
        }
    }
}
