using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProSoft.EF.DbContext;
using ProSoft.EF.DTOs.Medical.HospitalPatData;
using ProSoft.EF.IRepositories.Medical.HospitalPatData;
using ProSoft.EF.Models.Medical.HospitalPatData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.Core.Repositories.Medical.HospitalPatData
{
    public class PatientRepo : Repository<Pat, int>, IPatientRepo
    {
        private readonly IMapper _mapper;

        public PatientRepo(AppDbContext Context, IMapper mapper) : base(Context)
        {
            _mapper = mapper;
        }

        // ------------------ Ayman Saad ------------------ //
        public async Task<List<PatViewDTO>> GetPatientsByDoctorIdAndDateAsync(int doctorId, DateTime date)
        {
            var query = from pat in _Context.Pats
                        join pat_admission in _Context.PatAdmissions on new { X = pat.BranchId, Y = pat.PatId } equals new { X = (double?)pat_admission.BranchId, Y = (int)pat_admission.PatId! }
                        join doctor in _Context.Doctors on new { X = pat_admission.DrCode, Y = pat_admission.BranchId } equals new { X = (int?)doctor.DrId, Y = (decimal?)doctor.BranchId }
                        where doctor.DrId == doctorId && pat_admission.PatAdDate == date
                        select new PatViewDTO { PatId = pat.PatId, PatMobile = pat.PatMobile, PatName = pat.PatName };
            return query.ToList();
        }

        public int GetPatientCounts()
        {
            return _Context.Pats.Count();
        }

        public int GetPatientCountsDaily()
        {
            return _Context.Pats.Count(p => p.PatDate!.Value.Date == DateTime.Now.Date);
        }

        public int GetPatientCountsWeekly()
        {
            var today = DateTime.Today;
            var startOfWeek = today.AddDays(-(int)today.DayOfWeek);
            return _Context.Pats.Count(p => p.PatDate!.Value.Date <= DateTime.Now.Date && p.PatDate.Value.Date >= startOfWeek);
        }

        // ------------------ Ayman Saad ------------------ //

        public async Task<List<PatViewDTO>> GetAllPatsAsync()
        {
            List<Pat> patients = await _Context.Pats.ToListAsync();
            List<PatViewDTO> patientsDTO = _mapper.Map<List<PatViewDTO>>(patients);
            return patientsDTO;
        }

        public async Task<List<Pat>> GetAllPatientsAsync()
        {
            List<Pat> patients = await _Context.Pats.ToListAsync();
            return patients;
        }

        public async Task AddPatientAsync(PatEditAddDTO patDTO)
        {
            Pat pat = _mapper.Map<Pat>(patDTO);
            await _Context.AddAsync(pat);
            await _Context.SaveChangesAsync();
        }

        public async Task AddBatchPatientsAsync(List<PatEditAddDTO> patDTOs)
        {
            List<Pat> patients = _mapper.Map<List<Pat>>(patDTOs);
            await _Context.AddRangeAsync(patients);
            await _Context.SaveChangesAsync();
        }

        public async Task EditBatchPatientsAsync(List<PatEditAddDTO> patientDTOs)
        {
            var ids = patientDTOs.Select(dto => dto.PatId).ToList();
            var existingPatients = await _Context.Pats
                .Where(p => ids.Contains(p.PatId))
                .ToListAsync();

            foreach (var dto in patientDTOs)
            {
                var patient = existingPatients.FirstOrDefault(p => p.PatId == dto.PatId);
                if (patient != null)
                {
                    _mapper.Map(dto, patient);
                }
            }

            _Context.UpdateRange(existingPatients);
            await _Context.SaveChangesAsync();
        }

        public async Task<PatEditAddDTO> GetPatientByIdAsync(int id)
        {
            Pat patient = await _Context.Pats.FirstOrDefaultAsync(obj => obj.PatId == id);
            PatEditAddDTO patientDTO = _mapper.Map<PatEditAddDTO>(patient);
            return patientDTO;
        }

        public async Task EditPatientAsync(int id, PatEditAddDTO patientDTO)
        {
            Pat patient = await _Context.Pats.FirstOrDefaultAsync(obj => obj.PatId == id);

            _mapper.Map(patientDTO, patient);
            _Context.Update(patient);
            await _Context.SaveChangesAsync();
        }

        public async Task DeletePatientAsync(int id)
        {
            Pat patient = await _Context.Pats.FirstOrDefaultAsync(obj => obj.PatId == id);
            _Context.Remove(patient);
            await _Context.SaveChangesAsync();
        }
    }
}