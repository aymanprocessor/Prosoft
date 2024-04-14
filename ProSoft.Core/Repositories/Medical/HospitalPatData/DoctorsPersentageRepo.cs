using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
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
    public class DoctorsPersentageRepo : Repository<DoctorsPercent, int>, IDoctorsPersentageRepo
    {
        private readonly IMapper _mapper;

        public DoctorsPersentageRepo(AppDbContext Context, IMapper mapper) : base(Context)
        {
            _mapper = mapper;
        }

        public async Task<List<Doctor>> GetAllDoctorAsync()
        {
            List<Doctor> doctors =await _Context.Doctors.ToListAsync();
            return doctors;
        }

        public async Task<List<DoctorPrecentViewDTO>> GetAllDoctorPersentageAsync()
        {
            List<DoctorsPercent> doctorsPercents = await _Context.DoctorsPercents.ToListAsync();
            List<DoctorPrecentViewDTO> doctorPrecentDTOs = _mapper.Map<List<DoctorPrecentViewDTO>>(doctorsPercents);
            return doctorPrecentDTOs;
        }

        public async Task<List<DoctorPrecentViewDTO>> GetAllDoctorPersentageAsync(int id)
        {
            List<DoctorPrecentViewDTO> doctorsPercents = await _Context.DoctorsPercents.Where(obj => obj.DrCode == id)
                .Select(obj => new DoctorPrecentViewDTO()
                {
                    DrCode = (int)obj.DrCode,
                    SClinicDesc = obj.SubCodeNavigation.SClinicDesc,
                    ServDesc = obj.SubDetailCodeL1Navigation.ServDesc,
                    ValueService = Convert.ToDecimal(obj.ValueService),
                    DrValFlg = Convert.ToInt32(obj.DrValFlg),
                    DrVal = Convert.ToDecimal(obj.DrVal),
                    DrPerc = Convert.ToInt32(obj.DrPerc),
                    DrValContract = Convert.ToDecimal(obj.DrValContract),
                    DrPercContract = Convert.ToInt32(obj.DrPercContract),

                })
                .ToListAsync();


            return doctorsPercents;
        }
    }
}
