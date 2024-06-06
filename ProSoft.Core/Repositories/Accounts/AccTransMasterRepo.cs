using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProSoft.EF.DbContext;
using ProSoft.EF.DTOs.Accounts;
using ProSoft.EF.DTOs.Stocks;
using ProSoft.EF.DTOs.Treasury;
using ProSoft.EF.IRepositories.Accounts;
using ProSoft.EF.Models.Accounts;
using ProSoft.EF.Models.Shared;
using ProSoft.EF.Models.Stocks;
using ProSoft.EF.Models.Treasury;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.Core.Repositories.Accounts
{
    public class AccTransMasterRepo : Repository<AccTransMaster, int>, IAccTransMasterRepo
    {
        private readonly IMapper _mapper;

        public AccTransMasterRepo(AppDbContext Context, IMapper mapper) : base(Context)
        {
            _mapper = mapper;
        }

        public async Task<List<AccTransMasterViewDTO>> GetAccTransMasterAsync(int journalCode)
        {
            List<AccTransMaster> accTransMasters = await _Context.AccTransMasters.Where(obj => obj.TransType == journalCode.ToString())
                .OrderByDescending(obj=>obj.TransId).ToListAsync();
            List<AccTransMasterViewDTO> accTransMasterDTOs = _mapper.Map<List<AccTransMasterViewDTO>>(accTransMasters);
            return accTransMasterDTOs;
        }
        public async Task<int> GetNewtranNoAsync(int journalCode, int fYear, int branchId)
        {
            int newTranNo;
            var accTransMaster = await _Context.AccTransMasters
                .Where(obj => obj.CoCode == branchId && obj.FYear == fYear && obj.TransType == journalCode.ToString()).CountAsync();

            if (accTransMaster != 0)
            {
                //var lastID = await _DbSet.MaxAsync(obj => obj.DocNo);
                newTranNo = (int)accTransMaster + 1;
            }
            else
                newTranNo = 1;
            return newTranNo;
        }

        public async Task<AccTransMasterEditAddDTO> GetEmptyAccTransMasterAsync(int journalCode)
        {
            AccTransMasterEditAddDTO accTransMasterDTO = new AccTransMasterEditAddDTO();
            List<AccGlobalDef> accGlobalDefs = await _Context.accGlobalDefs.ToListAsync();
            accTransMasterDTO.accGlobalDefs = _mapper.Map<List<AccGlobalDefDTO>>(accGlobalDefs);

            accTransMasterDTO.JournalName = (await _Context.JournalTypes.FindAsync(journalCode)).JournalName;
            accTransMasterDTO.JournalCode = (await _Context.JournalTypes.FindAsync(journalCode)).JournalCode;
            return accTransMasterDTO;
        }

        public async Task AddAccTransMasterAsync(AccTransMasterEditAddDTO accTransMasterDTO)
        {
            AccTransMaster accTransMaster = _mapper.Map<AccTransMaster>(accTransMasterDTO);
            accTransMaster.EntryDate = DateTime.Now;
            accTransMaster.DocStatus = 1;
            accTransMaster.MCodeDtl = 1;
            accTransMaster.CurRate = 1;
            accTransMaster.YearTransNo = (accTransMasterDTO.FYear +"_"+ accTransMasterDTO.TransNo).ToString();
            await _Context.AddAsync(accTransMaster);
            await _Context.SaveChangesAsync();
        }
        public async Task<AccTransMasterEditAddDTO> GetAccTransMasterByIdAsync(int id)
        {
            AccTransMaster accTransMaster = await _Context.AccTransMasters.FirstOrDefaultAsync(obj => obj.TransId == id);

            AccTransMasterEditAddDTO accTransMasterDTO = _mapper.Map<AccTransMasterEditAddDTO>(accTransMaster);

            List<AccGlobalDef> accGlobalDefs = await _Context.accGlobalDefs.ToListAsync();
            accTransMasterDTO.accGlobalDefs = _mapper.Map<List<AccGlobalDefDTO>>(accGlobalDefs);

            return accTransMasterDTO;
        }
        public async Task EditAccTransMasterAsync(int id, AccTransMasterEditAddDTO accTransMasterDTO)
        {

            AccTransMaster accTransMaster = await _Context.AccTransMasters.FirstOrDefaultAsync(obj => obj.TransId == id);
            _mapper.Map(accTransMasterDTO, accTransMaster);
            accTransMaster.DocStatus = 1;
            accTransMaster.MCodeDtl = 1;
            accTransMaster.CurRate = 1;
            accTransMaster.UserDateModify = DateTime.Now;
            _Context.Update(accTransMaster);
            await _Context.SaveChangesAsync();
        }

    }
}
