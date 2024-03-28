using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProSoft.EF.DbContext;
using ProSoft.EF.DTOs.Medical.HospitalPatData;
using ProSoft.EF.IRepositories.Medical.HospitalPatData;
using ProSoft.EF.Models.Medical.HospitalPatData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.Core.Repositories.Medical.HospitalPatData
{
    public class PatAdmissionRepo : IPatAdmissionRepo
    {
        private readonly AppDbContext _Context;
        private readonly IMapper _mapper;
        public PatAdmissionRepo(AppDbContext Context, IMapper mapper)
        {
            _Context = Context;
            _mapper = mapper;
        }

        public async Task<List<PatAdmissionViewDTO>> GetAdmissionsByPatAsync(int patId)
        {
            List<PatAdmissionViewDTO> patAdmissionsDTO = await _Context.PatAdmissions
                .Where(obj => obj.PatId == patId)
                .Select(obj => new PatAdmissionViewDTO()
                {
                    MasterId = obj.MasterId,
                    PatId = (int)obj.PatId,
                    PatAdDate = Convert.ToDateTime(obj.PatAdDate),
                    CompName = obj.Comp.CompName,
                    CompNameDtl = obj.CompIdDtlNavigation.CompNameDtl,
                    ClassificationDesc = obj.BrnachInitialNavigation.ClassificationDesc,
                    RegionDesc = obj.SendFrNavigation.RegionDesc,
                    DrDesc = obj.DrCodeNavigation.DrDesc,
                })
                .ToListAsync();

            return patAdmissionsDTO;
        }
        public async Task<PatAdmissionEditAddDTO> GetEmptyPatAdmissionAsync(int patId)
        {
            PatAdmissionEditAddDTO patAdmissionDTO=new PatAdmissionEditAddDTO();
            Pat patient =await _Context.Pats.FirstOrDefaultAsync(obj=>obj.PatId == patId);
            var patName = patient.PatName;

            List<ClassificationCust> classifications= await _Context.ClassificationCusts.ToListAsync();
            List<Doctor> doctors = await _Context.Doctors.ToListAsync();

            patAdmissionDTO.classifications = _mapper.Map<List<ClassificationViewDTO>>(classifications);
            patAdmissionDTO.doctors = _mapper.Map<List<DoctorViewDTO>>(doctors);
            patAdmissionDTO.PatName = patName;
            return patAdmissionDTO;

        }
        ///////////////////////////////////Get for Ajax//////////////////////////////////
        public async Task<List<CompanyViewDTO>> GetCompany(int id)
        {
            List<Company> companies=await _Context.Companies.Where(obj=>obj.GroupId == id && obj.CompanyOnOff == 1).ToListAsync();
            List<CompanyViewDTO> companiesDTO = _mapper.Map<List<CompanyViewDTO>>(companies);
            return companiesDTO;
        }

        public async Task<List<CompanyDtlViewDTO>> GetSubCompany(int id)
        {
            List<CompanyDtl> companyDtls = await _Context.CompanyDtls.Where(obj =>obj.CompId == id).ToListAsync();
            List<CompanyDtlViewDTO> companuDtlsDTO = _mapper.Map<List<CompanyDtlViewDTO>>(companyDtls);
            return companuDtlsDTO;
        }

        public async Task<List<RegionViewDTO>> GetSection(int id)
        {
            List<Region> sections = await _Context.Regions.Where(obj => obj.ClassificationCust == id).ToListAsync();
            List<RegionViewDTO> sectionsDTO = _mapper.Map<List<RegionViewDTO>>(sections);
            return sectionsDTO;
        }
        //////////////////////////////////////////////////////////////////////////////////
        public async Task AddPatAdmissionAsync(int patId, PatAdmissionEditAddDTO patAdmissionDTO)
        {
            patAdmissionDTO.patId = patId;
            PatAdmission patAdmission = _mapper.Map<PatAdmission>(patAdmissionDTO);
            await _Context.AddAsync(patAdmission);
            await _Context.SaveChangesAsync();
        }

        public async Task<PatAdmissionEditAddDTO> GetPatAdmissionByIdAsync(int id)
        {
            PatAdmission patAdmission = await _Context.PatAdmissions
                .FirstOrDefaultAsync(obj => obj.MasterId == id);

            PatAdmissionEditAddDTO patAdmissionDTO = _mapper.Map<PatAdmissionEditAddDTO>(patAdmission);

            Pat pat = await _Context.Pats
                .FirstOrDefaultAsync(obj => obj.PatId == patAdmission.PatId);
            patAdmissionDTO.PatName = pat.PatName;
            // For Lists or dropdows
            List<Company> companies= await _Context.Companies.Where(obj => obj.GroupId == patAdmission.Deal && obj.CompanyOnOff == 1).ToListAsync();
           
            // For Filtering sub company depending on the selected company
            List<CompanyDtl> companyDtls = await _Context.CompanyDtls
               .Where(obj => obj.CompId == patAdmission.CompId).ToListAsync();

            List<ClassificationCust> classifications = await _Context.ClassificationCusts.ToListAsync();
            List<Doctor> doctors = await _Context.Doctors.ToListAsync();

            // For Filtering section depending on the selected department
            List<Region> regions = await _Context.Regions
                .Where(obj => obj.ClassificationCust == patAdmission.BrnachInitial)
                .ToListAsync();
            //Mapper
            patAdmissionDTO.companies = _mapper.Map<List<CompanyViewDTO>>(companies);
            patAdmissionDTO.classifications = _mapper.Map<List<ClassificationViewDTO>>(classifications);
            patAdmissionDTO.doctors = _mapper.Map<List<DoctorViewDTO>>(doctors);
            patAdmissionDTO.companyDtls = _mapper.Map<List<CompanyDtlViewDTO>>(companyDtls);
            patAdmissionDTO.regions = _mapper.Map<List<RegionViewDTO>>(regions);

            return patAdmissionDTO;
        }

        public async Task EditPatAdmissionAsync(int id, PatAdmissionEditAddDTO patAdmissionDTO)
        {
            PatAdmission patAdmission = await _Context.PatAdmissions
                .FirstOrDefaultAsync(obj => obj.MasterId == id);
            var patId = patAdmission.PatId;
            
            _mapper.Map(patAdmissionDTO, patAdmission);
            patAdmission.PatId = patId;
            patAdmission.MasterId = id;
            await _Context.SaveChangesAsync();
        }

        public async Task DeletePatAdmissionAsync(int id)
        {
            PatAdmission patAdmission = await _Context.PatAdmissions.FirstOrDefaultAsync(obj => obj.MasterId == id);
            _Context.Remove(patAdmission);
            await _Context.SaveChangesAsync();
        }
    }
}
