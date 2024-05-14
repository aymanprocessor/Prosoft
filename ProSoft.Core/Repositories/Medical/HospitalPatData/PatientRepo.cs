using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProSoft.EF.DbContext;
using ProSoft.EF.DTOs.Medical.HospitalPatData;
using ProSoft.EF.IRepositories.Medical.HospitalPatData;
using ProSoft.EF.Models.Medical.HospitalPatData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.Core.Repositories.Medical.HospitalPatData
{
    public class PatientRepo : Repository<Pat, int>, IPatientRepo
    {
        private readonly IMapper _mapper;
        public PatientRepo(AppDbContext Context, IMapper mapper): base(Context)
        {
            _mapper = mapper;
        }

        public async Task<List<PatViewDTO>> GetAllPatsAsync()
        {
            List<Pat> patients = await _Context.Pats.ToListAsync();
            List<PatViewDTO> patientsDTO = _mapper.Map<List<PatViewDTO>>(patients);
            return patientsDTO;
        }

        public async Task AddPatientAsync(PatEditAddDTO patDTO)
        {
            Pat pat = _mapper.Map<Pat>(patDTO);
            await _Context.AddAsync(pat);
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

            _mapper.Map(patientDTO,patient);
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
