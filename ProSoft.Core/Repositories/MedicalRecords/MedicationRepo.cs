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
    public class MedicationRepo : Repository<MedicationAtCcu, int>, IMedicationRepo
    {
        private readonly IMapper _mapper;

        public MedicationRepo(AppDbContext Context, IMapper mapper) : base(Context)
        {
            _mapper = mapper;
        }

        public async Task<List<MedicationDTO>> GetAllByPatIdAsync(int patId)
        {
            List<MedicationAtCcu> medicationAtCcus = await _DbSet
              .Where(obj => obj.PatId == patId).ToListAsync();
            var medicationDTOs = _mapper.Map<List<MedicationDTO>>(medicationAtCcus);
            return medicationDTOs;
        }

        public async Task<MedicationDTO> GetMedicationByIdAsync(int serialId)
        {
            MedicationAtCcu medicationAtCcu = await GetByIdAsync(serialId);
            var medicationDTO = _mapper.Map<MedicationDTO>(medicationAtCcu);
            return medicationDTO;
        }
        public async Task AddMedicationAsync(int id, MedicationDTO medicationDTO)
        {
            var medicationAtCcu = _mapper.Map<MedicationAtCcu>(medicationDTO);

            var lastRecord = await _DbSet.OrderBy(obj => obj.Serial)
                .LastOrDefaultAsync(obj => obj.PatId == id);

            if (lastRecord == null)
                medicationAtCcu.Serial = 1;
            else
                medicationAtCcu.Serial = lastRecord.Serial + 1;

            await AddAsync(medicationAtCcu);
            await SaveChangesAsync();
        }

        public async Task EditMedicationAsync(int serialId, MedicationDTO medicationDTO)
        {
            MedicationAtCcu medicationAtCcu = await GetByIdAsync(serialId);
            _mapper.Map(medicationDTO, medicationAtCcu);

            await UpdateAsync(medicationAtCcu);
            await SaveChangesAsync();
        }

    }
}
