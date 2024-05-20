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
    public class CoronaryRepo : Repository<CoronaryAngiographyReport, int>, ICoronaryRepo
    {
        private readonly IMapper _mapper;
        public CoronaryRepo(AppDbContext Context, IMapper mapper) : base(Context)
        {
            _mapper = mapper;
        }

        public async Task<List<CoronaryDTO>> GetAllByPatIdAsync(int patId)
        {
            List<CoronaryAngiographyReport> coronaryList = await _DbSet
                .Where(obj => obj.PatId == patId).ToListAsync();
            var coronaryListDTO = _mapper.Map<List<CoronaryDTO>>(coronaryList);
            return coronaryListDTO;
        }

        public async Task<CoronaryDTO> GetCoronaryByIdAsync(int serialId)
        {
            CoronaryAngiographyReport coronary = await GetByIdAsync(serialId);
            var coronaryDTO = _mapper.Map<CoronaryDTO>(coronary);
            return coronaryDTO;
        }

        public async Task AddCoronaryAsync(int patId, CoronaryDTO coronaryDTO)
        {
            var newCoronary = _mapper.Map<CoronaryAngiographyReport>(coronaryDTO);

            var lastRecord = await _DbSet.OrderBy(obj => obj.Serial)
                .LastOrDefaultAsync(obj => obj.PatId == patId);
            if (lastRecord == null)
                newCoronary.Serial = 1;
            else
                newCoronary.Serial = lastRecord.Serial + 1;

            await AddAsync(newCoronary);
            await SaveChangesAsync();
        }

        public async Task EditCoronaryAsync(int serialId, CoronaryDTO coronaryDTO)
        {
            CoronaryAngiographyReport coronary = await GetByIdAsync(serialId);
            _mapper.Map(coronaryDTO, coronary);

            await UpdateAsync(coronary);
            await SaveChangesAsync();
        }
    }
}
