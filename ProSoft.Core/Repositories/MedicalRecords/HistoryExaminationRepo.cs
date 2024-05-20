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
    public class HistoryExaminationRepo : Repository<HistoryExamination, int>, IHistoryExaminationRepo
    {
        private readonly IMapper _mapper;

        public HistoryExaminationRepo(AppDbContext Context, IMapper mapper) : base(Context)
        {
            _mapper = mapper;
        }
        public async Task<List<HistoryExaminationDTO>> GetAllByPatIdAsync(int patId)
        {
            List<HistoryExamination> historyExaminations = await _DbSet
                   .Where(obj => obj.PatId == patId).ToListAsync();
            var historyExaminationDTOs = _mapper.Map<List<HistoryExaminationDTO>>(historyExaminations);
            return historyExaminationDTOs;
        }
        public async Task<HistoryExaminationDTO> GetHistoryExaminationByIdAsync(int serialId)
        {
            HistoryExamination historyExamination = await GetByIdAsync(serialId);
            var historyExaminationDTO = _mapper.Map<HistoryExaminationDTO>(historyExamination);
            return historyExaminationDTO;
        }
        public async Task AddHistoryExaminationAsync(int id, HistoryExaminationDTO historyExaminationDTO)
        {
            var historyExamination = _mapper.Map<HistoryExamination>(historyExaminationDTO);

            var lastRecord = await _DbSet.OrderBy(obj => obj.Serial)
                .LastOrDefaultAsync(obj => obj.PatId == id);

            if (lastRecord == null)
                historyExamination.Serial = 1;
            else
                historyExamination.Serial = lastRecord.Serial + 1;

            await AddAsync(historyExamination);
            await SaveChangesAsync();
        }

        public async Task EditHistoryExaminationAsync(int serialId, HistoryExaminationDTO historyExaminationDTO)
        {
            HistoryExamination historyExamination = await GetByIdAsync(serialId);
            _mapper.Map(historyExaminationDTO, historyExamination);

            await UpdateAsync(historyExamination);
            await SaveChangesAsync();
        }
    }
}
