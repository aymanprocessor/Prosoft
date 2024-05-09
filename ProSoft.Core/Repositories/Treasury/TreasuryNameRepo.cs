using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProSoft.EF.DbContext;
using ProSoft.EF.DTOs.Accounts;
using ProSoft.EF.DTOs.Medical.HospitalPatData;
using ProSoft.EF.DTOs.Shared;
using ProSoft.EF.DTOs.Treasury;
using ProSoft.EF.IRepositories.Treasury;
using ProSoft.EF.Models.Accounts;
using ProSoft.EF.Models.Medical.HospitalPatData;
using ProSoft.EF.Models.Shared;
using ProSoft.EF.Models.Stocks;
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
            List<SafeName> safeNames = await _Context.SafeNames.ToListAsync();
            List<TreasuryNameViewDTO> treasuryNameDTOs = new();

            foreach (var item in safeNames)
            {
               var treasuryNameDTO = _mapper.Map<TreasuryNameViewDTO>(item);
                treasuryNameDTO.JournalName = (await _Context.JournalTypes.FirstOrDefaultAsync(obj => obj.JournalCode == int.Parse(item.JeDocTyp))).JournalName;
                treasuryNameDTOs.Add(treasuryNameDTO);
            }
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
        public async Task<TreasuryNameEditAddDTO> GetEmptyTreasuryNameAsync(int id)
        {
            TreasuryNameEditAddDTO treasuryNameDTO = new TreasuryNameEditAddDTO();
            List<JournalType> journalTypes = await _Context.JournalTypes.ToListAsync();
            treasuryNameDTO.JournalTypes = _mapper.Map<List<JournalTypeDTO>>(journalTypes);
            return treasuryNameDTO;
        }

        public async Task AddTreasuryNameAsync(TreasuryNameEditAddDTO treasuryNameDTO)
        {
            SafeName safeName = _mapper.Map<SafeName>(treasuryNameDTO);
            await _Context.AddAsync(safeName);
            await _Context.SaveChangesAsync();
        }

        public async Task<TreasuryNameEditAddDTO> GetTreasuryNameByIdAsync(int id)
        {
            SafeName safeName = await _Context.SafeNames.FirstOrDefaultAsync(obj=>obj.SafeCode ==id);
            TreasuryNameEditAddDTO treasuryNameDTO = _mapper.Map<TreasuryNameEditAddDTO>(safeName);
            List<JournalType> journalTypes = await _Context.JournalTypes.ToListAsync();
            treasuryNameDTO.JournalTypes = _mapper.Map<List<JournalTypeDTO>>(journalTypes);
            return treasuryNameDTO;
        }

        public async Task EditTreasuryNameAsync(int id, TreasuryNameEditAddDTO treasuryNameDTO)
        {
            SafeName safeName =await _Context.SafeNames.FirstOrDefaultAsync(obj=>obj.SafeCode ==id);
            _mapper.Map(treasuryNameDTO, safeName);
            _Context.Update(safeName);
            await _Context.SaveChangesAsync();
        }
    }
}
