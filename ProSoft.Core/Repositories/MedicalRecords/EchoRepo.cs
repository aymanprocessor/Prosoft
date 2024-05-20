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
    public class EchoRepo: Repository<Echo, int>, IEchoRepo
    {
        private readonly IMapper _mapper;
        public EchoRepo(AppDbContext Context, IMapper mapper) : base(Context)
        {
            _mapper = mapper;
        }

        public async Task<List<EchoDTO>> GetAllByPatIdAsync(int patId)
        {
            List<Echo> echoList = await _DbSet
                .Where(obj => obj.PatId == patId).ToListAsync();
            var echoListDTO = _mapper.Map<List<EchoDTO>>(echoList);
            return echoListDTO;
        }

        public async Task<EchoDTO> GetEchoByIdAsync(int serialId)
        {
            Echo echo = await GetByIdAsync(serialId);
            var echoDTO = _mapper.Map<EchoDTO>(echo);
            return echoDTO;
        }

        public async Task AddEchoAsync(int patId, EchoDTO echoDTO)
        {
            var newEcho = _mapper.Map<Echo>(echoDTO);

            var lastRecord = await _DbSet.OrderBy(obj => obj.Serial)
                .LastOrDefaultAsync(obj => obj.PatId == patId);
            if (lastRecord == null)
                newEcho.Serial = 1;
            else
                newEcho.Serial = lastRecord.Serial + 1;

            await AddAsync(newEcho);
            await SaveChangesAsync();
        }

        public async Task EditEchoAsync(int serialId, EchoDTO echoDTO)
        {
            Echo echo = await GetByIdAsync(serialId);
            _mapper.Map(echoDTO, echo);

            await UpdateAsync(echo);
            await SaveChangesAsync();
        }
    }
}
