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
    public class EcgAndEchoRepo: Repository<EcgAndEcho, int>, IEcgAndEchoRepo
    {
        private readonly IMapper _mapper;
        public EcgAndEchoRepo(AppDbContext Context, IMapper mapper) : base(Context)
        {
            _mapper = mapper;
        }

        public async Task<List<EcgAndEchoDTO>> GetAllByPatIdAsync(int patId)
        {
            List<EcgAndEcho> ecgAndEchoList = await _DbSet
                .Where(obj => obj.PatId == patId).ToListAsync();
            var ecgAndEchoListDTO = _mapper.Map<List<EcgAndEchoDTO>>(ecgAndEchoList);
            return ecgAndEchoListDTO;
        }

        public async Task<EcgAndEchoDTO> GetEcgAndEchoByIdAsync(int serialId)
        {
            EcgAndEcho ecgAndEcho = await GetByIdAsync(serialId);
            var ecgAndEchoDTO = _mapper.Map<EcgAndEchoDTO>(ecgAndEcho);
            return ecgAndEchoDTO;
        }

        public async Task AddEcgAndEchoAsync(int patId, EcgAndEchoDTO ecgAndEchoDTO)
        {
            var newEcgAndEcho = _mapper.Map<EcgAndEcho>(ecgAndEchoDTO);

            var lastRecord = await _DbSet.OrderBy(obj => obj.Serial)
                .LastOrDefaultAsync(obj => obj.PatId == patId);
            if (lastRecord == null)
                newEcgAndEcho.Serial = 1;
            else
                newEcgAndEcho.Serial = lastRecord.Serial + 1;

            await AddAsync(newEcgAndEcho);
            await SaveChangesAsync();
        }

        public async Task EditEcgAndEchoAsync(int serialId, EcgAndEchoDTO ecgAndEchoDTO)
        {
            EcgAndEcho ecgAndEcho = await GetByIdAsync(serialId);
            _mapper.Map(ecgAndEchoDTO, ecgAndEcho);

            await UpdateAsync(ecgAndEcho);
            await SaveChangesAsync();
        }
    }
}
