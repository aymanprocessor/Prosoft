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
    public class LabReportRepo : Repository<LabReport, int>, ILabReportRepo
    {
        private readonly IMapper _mapper;

        public LabReportRepo(AppDbContext Context, IMapper mapper) : base(Context)
        {
            _mapper = mapper;
        }
        public async Task<List<LabReportDTO>> GetAllByPatIdAsync(int patId)
        {
            List<LabReport> labReports = await _DbSet
                  .Where(obj => obj.PatId == patId).ToListAsync();
            var labReportDTOs = _mapper.Map<List<LabReportDTO>>(labReports);
            return labReportDTOs;
        }

        public async Task<LabReportDTO> GetLabReportByIdAsync(int serialId)
        {
            LabReport labReport = await GetByIdAsync(serialId);
            var labReportDTO = _mapper.Map<LabReportDTO>(labReport);
            return labReportDTO;
        }
        public async Task AddLabReportAsync(int id, LabReportDTO labReportDTO)
        {
            var labReport = _mapper.Map<LabReport>(labReportDTO);

            var lastRecord = await _DbSet.OrderBy(obj => obj.Serial)
                .LastOrDefaultAsync(obj => obj.PatId == id);

            if (lastRecord == null)
                labReport.Serial = 1;
            else
                labReport.Serial = lastRecord.Serial + 1;

            await AddAsync(labReport);
            await SaveChangesAsync();
        }

        public async Task EditLabReportAsync(int serialId, LabReportDTO labReportDTO)
        {
            LabReport labReport = await GetByIdAsync(serialId);
            _mapper.Map(labReportDTO, labReport);

            await UpdateAsync(labReport);
            await SaveChangesAsync();
        }

       
    }
}
