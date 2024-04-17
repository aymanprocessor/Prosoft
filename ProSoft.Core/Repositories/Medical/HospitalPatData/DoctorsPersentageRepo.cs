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

        public async Task<List<DoctorPrecentViewDTO>> GetAllDoctorPercentageAsync(int id)
        {
            List<DocSubDtl> docSubDtls = await _Context.DocSubDtls.Where(obj=>obj.DrId ==id).ToListAsync();
            List<ServiceClinic> serviceClinics = new();
            foreach (var x in docSubDtls)
            {
                List<ServiceClinic> servClinics = await _Context.ServiceClinics
                    .Where(obj=>obj.SClinicId == x.SClinicId && obj.ClinicId ==x.ClinicId).ToListAsync();   
                 serviceClinics.AddRange(servClinics);
            }

            List<DoctorsPercent> doctorsPercentss = await _Context.DoctorsPercents.ToListAsync();

            foreach (var z in serviceClinics)
            {
                DoctorsPercent doctorsPercent1 = await _Context.DoctorsPercents
                    .FirstOrDefaultAsync(obj=>obj.MainCode == z.ClinicId && obj.SubCode ==z.SClinicId && obj.SubDetailCodeL1 ==z.ServId);
                if (doctorsPercent1 ==null)
                {
                    DoctorsPercent doctorsPercent = new();
                    doctorsPercent.SubCode = z.SClinicId;
                    doctorsPercent.SubDetailCodeL1 = z.ServId;
                    doctorsPercent.MainCode = z.ClinicId;
                    doctorsPercent.DrValFlg = 1;
                    doctorsPercent.DrCode = id;
                    await _Context.AddAsync(doctorsPercent);
                    await _Context.SaveChangesAsync();
                }
            }

            List<DoctorPrecentViewDTO> doctorsPercents = await _Context.DoctorsPercents.Where(obj => obj.DrCode == id)
                .Select(obj => new DoctorPrecentViewDTO()
                {
                    DrPercent=(int)obj.DrPercent,
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

        public async Task<DoctorPrecentEditAddDTO> GetDoctorPrecntByIdAsync(int id)
        {
            DoctorsPercent doctorsPercent = await _Context.DoctorsPercents
                .FirstOrDefaultAsync(obj => obj.DrPercent == id);

            DoctorPrecentEditAddDTO doctorPrecentDTO = _mapper.Map<DoctorPrecentEditAddDTO>(doctorsPercent);
            List<SubClinic> subClinics = await _Context.SubClinics.ToListAsync();
            List<ServiceClinic> servClinics = await _Context.ServiceClinics.ToListAsync();

            doctorPrecentDTO.subClinics = _mapper.Map<List<SubClinicViewDTO>>(subClinics);
            doctorPrecentDTO.services = _mapper.Map<List<ServiceClinicViewDTO>>(servClinics);

            return doctorPrecentDTO;
        }

        public async Task EditDoctorPercentAsync(int id, DoctorPrecentEditAddDTO doctorPrecentDTO)
        {
            DoctorsPercent doctorsPercent = await _Context.DoctorsPercents.FirstOrDefaultAsync(obj => obj.DrPercent == id);

            if (doctorPrecentDTO.DrValFlg == 1)
            {
                doctorPrecentDTO.DrValContract = 0;
                doctorPrecentDTO.DrVal = 0;
            }
            else if (doctorPrecentDTO.DrValFlg ==2)
            {
                doctorPrecentDTO.DrPerc = 0;
                doctorPrecentDTO.DrPercContract = 0;
            }
            _mapper.Map(doctorPrecentDTO, doctorsPercent);
            _Context.Update(doctorsPercent);
            await _Context.SaveChangesAsync();
        }

    }
}
