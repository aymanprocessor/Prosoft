using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ProSoft.EF.DbContext;
using ProSoft.EF.DTOs.Medical.HospitalPatData;
using ProSoft.EF.IRepositories.Medical.HospitalPatData;
using ProSoft.EF.Models.Medical.HospitalPatData;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.Core.Repositories.Medical.HospitalPatData
{
    public class DoctorRepo : Repository<Doctor, int>, IDoctorRepo
    {
        private readonly AppDbContext _Context;
        private readonly IMapper _mapper;
        public DoctorRepo(AppDbContext Context, IMapper mapper) : base(Context)
        {
            _Context = Context;
            _mapper = mapper;
        }

        public async Task<List<DoctorViewDTO>> GetAllDoctor()
        {
            List<DoctorViewDTO> doctorsDTO = await _Context.Doctors
                .Select(obj => new DoctorViewDTO()
                {
                    DrId = obj.DrId,
                    DrDesc =obj.DrDesc,
                    DrType = obj.DrType,
                    DegreeDesc =obj.DrDegreeNavigation.DegreeDesc,
                    Taxable = obj.Taxable,
                    Shareholder=obj.Shareholder,
                    DrOnOff = obj.DrOnOff
                })
                .ToListAsync();

            return doctorsDTO;
        }

        public async Task<DoctorEditAddDTO> GetEmptyDoctorAsync()
        {
            DoctorEditAddDTO doctorDTO = new DoctorEditAddDTO();

            List<DrDegree> drDegrees = await _Context.DrDegrees.ToListAsync();
            doctorDTO.drDegrees = _mapper.Map<List<DrDegreeDTO>>(drDegrees);

            return doctorDTO;
        }

        public async Task<int> GetNewIdAsync()
        {
            int newID;
            if (_DbSet.Count() != 0)
            {
                var lastID = await _DbSet.MaxAsync(obj => obj.DrId);
                newID = lastID + 1;
            }
            else
                newID = 1;
            return newID;
        }
        public async Task AddDoctorAsync(DoctorEditAddDTO doctorDTO)
        {
            Doctor doctor = _mapper.Map<Doctor>(doctorDTO);
            doctor.ClinicId = 1;
            doctor.VisitorPerDay = 20;
            doctor.ContractTyp = 1;
            doctor.JopTyp=1;
            await _Context.AddAsync(doctor);
            await _Context.SaveChangesAsync();
        }

        public async Task<DoctorEditAddDTO> GetDoctorByIdAsync(int id)
        {
            Doctor doctor = await _Context.Doctors.FirstOrDefaultAsync(obj=>obj.DrId == id);
            DoctorEditAddDTO doctorDTO=_mapper.Map<DoctorEditAddDTO>(doctor);

            List<DrDegree> drDegrees =await _Context.DrDegrees.ToListAsync();
            doctorDTO.drDegrees = _mapper.Map<List<DrDegreeDTO>>(drDegrees);

            return doctorDTO;
        }

        public async Task EditDoctotAsync(int id, DoctorEditAddDTO doctorDTO)
        {
            Doctor doctor = await _Context.Doctors.FirstOrDefaultAsync(obj=>obj.DrId ==id);
            _mapper.Map(doctorDTO,doctor);
            _Context.Update(doctor);
            await _Context.SaveChangesAsync();
        }
    }
}
