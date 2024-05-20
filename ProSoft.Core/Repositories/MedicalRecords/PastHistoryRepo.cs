using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProSoft.EF.DbContext;
using ProSoft.EF.DTOs.MedicalRecords;
using ProSoft.EF.IRepositories.MedicalRecords;
using ProSoft.EF.Models.Medical.HospitalPatData;
using ProSoft.EF.Models.MedicalRecords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.Core.Repositories.MedicalRecords
{
    public class PastHistoryRepo : Repository<PastHistory, int>, IPastHistoryRepo
    {
        private readonly IMapper _mapper;
        public PastHistoryRepo(AppDbContext Context, IMapper mapper) : base(Context)
        {
            _mapper = mapper;
        }

        public async Task<List<PastHistoryDTO>> GetAllByPatIdAsync(int patId)
        {
            List<PastHistory> pastHistoryList = await _DbSet
                .Where(obj => obj.PatId == patId).ToListAsync();
            var pastHistoryListDTO = _mapper.Map<List<PastHistoryDTO>>(pastHistoryList);
            return pastHistoryListDTO;
        }

        public async Task<PastHistoryDTO> GetPastHistoryByIdAsync(int serialId)
        {
            PastHistory pastHistory = await GetByIdAsync(serialId);
            var pastHistoryDTO = _mapper.Map<PastHistoryDTO>(pastHistory);
            return pastHistoryDTO;
        }

        public async Task AddPastHistoryAsync(int patId, PastHistoryDTO pastHistoryDTO)
        {
            var newPastHistory = _mapper.Map<PastHistory>(pastHistoryDTO);

            var lastRecord = await _DbSet.OrderBy(obj => obj.Serial)
                .LastOrDefaultAsync(obj => obj.PatId == patId);
            if (lastRecord == null)
                newPastHistory.Serial = 1;
            else
                newPastHistory.Serial = lastRecord.Serial + 1;

            await AddAsync(newPastHistory);
            await SaveChangesAsync();
        }

        public async Task EditPastHistoryAsync(int serialId, PastHistoryDTO pastHistoryDTO)
        {
            PastHistory pastHistory = await GetByIdAsync(serialId);
            _mapper.Map(pastHistoryDTO, pastHistory);

            await UpdateAsync(pastHistory);
            await SaveChangesAsync();
        }
    }
}
