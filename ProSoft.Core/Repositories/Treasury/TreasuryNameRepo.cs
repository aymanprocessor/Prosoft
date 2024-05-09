using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProSoft.EF.DbContext;
using ProSoft.EF.DTOs.Treasury;
using ProSoft.EF.IRepositories.Treasury;
using ProSoft.EF.Models.Treasury;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.Core.Repositories.Treasury
{
    public class TreasuryNameRepo : Repository<SafeName, int>, ITreasuryNameRepo
    {
        private readonly IMapper _mapper;
        public TreasuryNameRepo(AppDbContext Context, IMapper mapper) : base(Context)
        {
            _mapper = mapper;
        }

        public async Task<List<TreasuryNameViewDTO>> GetAllTreasurysAsync()
        {
            List<TreasuryNameViewDTO> treasuryNameDTOs = await _Context.SafeNames
                .Select(obj => new TreasuryNameViewDTO()
                {
                    SafeCode = (int)obj.SafeCode,
                    SafeNames = obj.SafeNames,
                    PostAccount = (int)obj.PostAccount,
                    Flag1 = (int)obj.Flag1,
                }).ToListAsync();
            return treasuryNameDTOs;
        }

        public async Task<int> GetNewIdAsync()
        {
            int newID;
            if (_DbSet.Count() != 0)
            {
                var lastID = await _DbSet.MaxAsync(obj => obj.SafeCode);
                newID = lastID + 1;
            }
            else
                newID = 1;
            return newID;
        }
    }
}
