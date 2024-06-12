using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProSoft.EF.DbContext;
using ProSoft.EF.DTOs.Accounts;
using ProSoft.EF.DTOs.Medical.HospitalPatData;
using ProSoft.EF.DTOs.Shared;
using ProSoft.EF.DTOs.Treasury;
using ProSoft.EF.IRepositories.Accounts;
using ProSoft.EF.Models.Accounts;
using ProSoft.EF.Models.Treasury;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Security.Permissions;
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
        public async Task<UserJournalTypeDTO> GetEmptyUserJournalTypeAsync(int userCode)
        {
            UserJournalTypeDTO userJournalTypeDTO = new UserJournalTypeDTO();

            List<JournalType> journalTypes = new List<JournalType>();
            List<UserJournalType> userJournalTypes = await _Context.UserJournalTypes.Where(obj=>obj.UserCode == userCode).ToListAsync();
            List<JournalType> allJournalTypes = await _Context.JournalTypes.ToListAsync();
            foreach (var jou in allJournalTypes)
            {
                var isExisted = false;
                foreach (var user in userJournalTypes)
                {
                    if (jou.JournalCode == user.JournalCode)
                    {
                        isExisted = true;
                        break;
                    }
                    else
                        isExisted = false;
                }
                if (!isExisted)
                {
                    journalTypes.Add(jou);
                }
            }
            userJournalTypeDTO.JournalTypes = _mapper.Map<List<JournalTypeDTO>>(journalTypes);
            return userJournalTypeDTO;
        }
    }
}
