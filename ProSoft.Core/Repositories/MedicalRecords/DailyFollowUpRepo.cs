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
    public class DailyFollowUpRepo : Repository<DailyFollowUpCcuChant, int>, IDailyFollowUpRepo
    {
        private readonly IMapper _mapper;

        public DailyFollowUpRepo(AppDbContext Context, IMapper mapper) : base(Context)
        {
            _mapper = mapper;
        }


        public async Task<List<DailyFollowUpDTO>> GetAllByPatIdAsync(int patId)
        {
            List<DailyFollowUpCcuChant> dailyFollowUpCcuChants = await _DbSet
                .Where(obj => obj.PatId == patId).ToListAsync();
            var dailyFollowUpDTOs = _mapper.Map<List<DailyFollowUpDTO>>(dailyFollowUpCcuChants);
            return dailyFollowUpDTOs;
        }

        public async Task<DailyFollowUpDTO> GetDailyFollowUpByIdAsync(int serialId)
        {
            DailyFollowUpCcuChant dailyFollowUpCcuChant = await GetByIdAsync(serialId);
            var dailyFollowUpDTO = _mapper.Map<DailyFollowUpDTO>(dailyFollowUpCcuChant);
            return dailyFollowUpDTO;
        }
        public async Task AddDailyFollowUpAsync(int id, DailyFollowUpDTO dailyFollowUpDTO)
        {
            var dailyFollowUp = _mapper.Map<DailyFollowUpCcuChant>(dailyFollowUpDTO);

            var lastRecord = await _DbSet.OrderBy(obj => obj.Serial)
                .LastOrDefaultAsync(obj => obj.PatId == id);

            if (lastRecord == null)
                dailyFollowUp.Serial = 1;
            else
                dailyFollowUp.Serial = lastRecord.Serial + 1;

            await AddAsync(dailyFollowUp);
            await SaveChangesAsync();
        }
        public async Task EditDailyFollowUpAsync(int serialId, DailyFollowUpDTO dailyFollowUpDTO)
        {
            DailyFollowUpCcuChant dailyFollowUpCcuChant =await GetByIdAsync(serialId);
            _mapper.Map(dailyFollowUpDTO, dailyFollowUpCcuChant);

            await UpdateAsync(dailyFollowUpCcuChant);
            await SaveChangesAsync();
        }
    }
}
