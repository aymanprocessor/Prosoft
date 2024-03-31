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
    }
}
