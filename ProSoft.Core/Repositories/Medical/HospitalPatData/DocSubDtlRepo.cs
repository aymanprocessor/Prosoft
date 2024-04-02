using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProSoft.EF.DbContext;
using ProSoft.EF.DTOs.Medical.HospitalPatData;
using ProSoft.EF.IRepositories.Medical.HospitalPatData;
using ProSoft.EF.Models.Medical.HospitalPatData;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.Core.Repositories.Medical.HospitalPatData
{
    public class DocSubDtlRepo : Repository<DocSubDtl, int>, IDocSubDtlRepo
    {
        private readonly IMapper _mapper;

        public DocSubDtlRepo(AppDbContext Context, IMapper mapper) : base(Context)
        {
            _mapper = mapper;
        }

        public async Task<List<DocSubDtlViewDTO>> GetDocSubDtlByDoctorAsync(int id)
        {
            List<DocSubDtlViewDTO> docSubDtlDTOs =await _Context.DocSubDtls.Where(obj=>obj.DrId == id)
                 .Select(obj => new DocSubDtlViewDTO()
                 {
                     DocSubId = (int)obj.DocSubId,  
                     ClinicDesc = obj.Clinic.ClinicDesc,
                     SClinicDesc = obj.SClinic.SClinicDesc,
                     DrOnOff = Convert.ToInt32(obj.DrOnOff),
                     DocSubDef = Convert.ToInt32(obj.DocSubDef),  
                 })
                 .ToListAsync();
            return docSubDtlDTOs;
        }
        public async Task<int> GetNewIdAsync()
        {
            int newID;
            if (_DbSet.Count() != 0)
            {
                var lastID = await _DbSet.MaxAsync(obj => obj.DocSubId);
                newID = lastID + 1;
            }
            else
                newID = 1;
            return newID;
        }

        public async Task<DocSubDtlEditAddDTO> GetEmptyDocSubDtlAsync(int id)
        {
            DocSubDtlEditAddDTO docSubDtlEditAddDTO = new DocSubDtlEditAddDTO();
            Doctor doctor = await _Context.Doctors.FirstOrDefaultAsync(obj => obj.DrId == id);
            var doctorName = doctor.DrDesc;
            List<MainClinic> mainClinics = await _Context.MainClinics.ToListAsync();
            List<SubClinic> subClinics = await _Context.SubClinics.ToListAsync();


            docSubDtlEditAddDTO.MainClinics = _mapper.Map<List<MainClinicViewDTO>>(mainClinics);
            docSubDtlEditAddDTO.SubClinics = _mapper.Map<List<SubClinicViewDTO>>(subClinics);
            docSubDtlEditAddDTO.DrDesc = doctorName;



            return docSubDtlEditAddDTO;
        }

        public async Task AddDocSubDtlAsync(int id, DocSubDtlEditAddDTO docSubDtlDTO)
        {
            DocSubDtl docSubDtl = _mapper.Map<DocSubDtl>(docSubDtlDTO);
            docSubDtl.DrId= id;
            await _Context.AddAsync(docSubDtl);
            await _Context.SaveChangesAsync();
        }

        public async Task<DocSubDtlEditAddDTO> GetDocSubDtlByIdAsync(int id)
        {
            DocSubDtl docSubDtl =await _Context.DocSubDtls.FirstOrDefaultAsync(obj=>obj.DocSubId== id);
            DocSubDtlEditAddDTO docSubDtlDTO = _mapper.Map<DocSubDtlEditAddDTO>(docSubDtl);

            List<MainClinic> mainClinics =await _Context.MainClinics.ToListAsync();
            List<SubClinic> subClinics =await _Context.SubClinics.ToListAsync();

            docSubDtlDTO.MainClinics=_mapper.Map<List<MainClinicViewDTO>>(mainClinics);
            docSubDtlDTO.SubClinics=_mapper.Map<List<SubClinicViewDTO>>(subClinics);
            //sending name of doctor to view
            Doctor doctor = await _Context.Doctors.FirstOrDefaultAsync(obj => obj.DrId == docSubDtl.DrId);
            var doctorName = doctor.DrDesc;
            docSubDtlDTO.DrDesc = doctorName;

            return docSubDtlDTO;
        }

        public async Task EditDocSubDtlAsync(int id, DocSubDtlEditAddDTO docSubDtlDTO)
        {
            DocSubDtl docSubDtl =await _Context.DocSubDtls.FirstOrDefaultAsync(obj=>obj.DocSubId == id);

            _mapper.Map(docSubDtlDTO, docSubDtl);
            _Context.Update(docSubDtl);
            await _Context.SaveChangesAsync();
        }
    }
}
