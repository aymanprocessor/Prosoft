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
    public class PciReportRepo : Repository<PciReport, int>, IPciReportRepo
    {
        private readonly IMapper _mapper;

        public PciReportRepo(AppDbContext Context, IMapper mapper) : base(Context)
        {
            _mapper = mapper;
        }
        public async Task<List<PciReportDTO>> GetAllByPatIdAsync(int patId)
        {
            List<PciReport> pciReports = await _DbSet
            .Where(obj => obj.PatId == patId).ToListAsync();
            var pciReportDTOs = _mapper.Map<List<PciReportDTO>>(pciReports);
            return pciReportDTOs;
        }

        public async Task<PciReportDTO> GetPciReportByIdAsync(int serialId)
        {
            PciReport pciReport = await GetByIdAsync(serialId);
            var pciReportDTO = _mapper.Map<PciReportDTO>(pciReport);
            return pciReportDTO;
        }

        public async Task AddPciReportAsync(int id, PciReportDTO pciReportDTO)
        {
            var pciReport = _mapper.Map<PciReport>(pciReportDTO);

            var lastRecord = await _DbSet.OrderBy(obj => obj.Serial)
                .LastOrDefaultAsync(obj => obj.PatId == id);

            if (lastRecord == null)
                pciReport.Serial = 1;
            else
                pciReport.Serial = lastRecord.Serial + 1;

            await AddAsync(pciReport);
            await SaveChangesAsync();
        }

        public async Task EditPciReportAsync(int serialId, PciReportDTO pciReportDTO)
        {
            PciReport pciReport = await GetByIdAsync(serialId);
            _mapper.Map(pciReportDTO, pciReport);

            await UpdateAsync(pciReport);
            await SaveChangesAsync();
        }
    }
}
