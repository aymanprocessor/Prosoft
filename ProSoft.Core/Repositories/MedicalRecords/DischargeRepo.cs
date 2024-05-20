using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProSoft.EF.DbContext;
using ProSoft.EF.DTOs.MedicalRecords;
using ProSoft.EF.IRepositories.MedicalRecords;
using ProSoft.EF.Models.MedicalRecords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.Core.Repositories.MedicalRecords
{
    public class DischargeRepo: Repository<DischargeSummery, int>, IDischargeRepo
    {
        private readonly IMapper _mapper;
        public DischargeRepo(AppDbContext Context, IMapper mapper) : base(Context)
        {
            _mapper = mapper;
        }

        public async Task<List<DischargeDTO>> GetAllByPatIdAsync(int patId)
        {
            List<DischargeSummery> dischargeList = await _DbSet
                .Where(obj => obj.PatId == patId).ToListAsync();
            var dischargeListDTO = _mapper.Map<List<DischargeDTO>>(dischargeList);
            return dischargeListDTO;
        }

        public async Task<DischargeDTO> GetDischargeByIdAsync(int serialId)
        {
            DischargeSummery discharge = await GetByIdAsync(serialId);
            var dischargeDTO = _mapper.Map<DischargeDTO>(discharge);
            return dischargeDTO;
        }

        public async Task AddDischargeAsync(int patId, DischargeDTO dischargeDTO)
        {
            var newDischarge = _mapper.Map<DischargeSummery>(dischargeDTO);

            var lastRecord = await _DbSet.OrderBy(obj => obj.Serial)
                .LastOrDefaultAsync(obj => obj.PatId == patId);
            if (lastRecord == null)
                newDischarge.Serial = 1;
            else
                newDischarge.Serial = lastRecord.Serial + 1;

            await AddAsync(newDischarge);
            await SaveChangesAsync();
        }

        public async Task EditDischargeAsync(int serialId, DischargeDTO dischargeDTO)
        {
            DischargeSummery discharge = await GetByIdAsync(serialId);
            _mapper.Map(dischargeDTO, discharge);

            await UpdateAsync(discharge);
            await SaveChangesAsync();
        }
    }
}
