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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ProSoft.Core.Repositories.Medical.HospitalPatData
{
    public class CompanyDtlRepo : Repository<CompanyDtl, int>, ICompanyDtlRepo
    {
        private readonly IMapper _mapper;
        public CompanyDtlRepo(AppDbContext Context, IMapper mapper) : base(Context)
        {
            _mapper = mapper;
        }


        public async Task<List<CompanyDtlViewDTO>> GetAllCompanyDtlAsync(int id)
        {

            List<CompanyDtlViewDTO> companyDtlDTOs = await _Context.CompanyDtls.Where(obj => obj.CompId == id)
                 .Select(obj => new CompanyDtlViewDTO()
                 {
                     CompIdDtl = (int)obj.CompIdDtl,
                     CompNameDtl = obj.CompNameDtl,
                     TaxNo = obj.TaxNo,
                     SubCode = obj.SubCode,
                     MainCode = obj.MainCode,
                     EinvType = (int)obj.EinvType,
                     WinvItemsFlg = obj.WinvItemsFlg,
                 })
                 .ToListAsync();
            return companyDtlDTOs;
        }

        public async Task<int> GetNewIdAsync()
        {
            int newID;
            if (_DbSet.Count() != 0)
            {
                var lastID = await _DbSet.MaxAsync(obj => obj.CompIdDtl);
                newID = lastID + 1;
            }
            else
                newID = 1;
            return newID;
        }
        public async Task AddCompanyDtllAsync(int id, CompanyDtlEditAddDTO companyDtlDTO)
        {
            CompanyDtl companyDtl = _mapper.Map<CompanyDtl>(companyDtlDTO);
            companyDtl.CompId = id;

            await _Context.AddAsync(companyDtl);
            await _Context.SaveChangesAsync();
        }

        public async Task<CompanyDtlEditAddDTO> GetCompanyDtlByIdAsync(int id)
        {
            CompanyDtl companyDtl = await _Context.CompanyDtls.FirstOrDefaultAsync(obj=>obj.CompIdDtl ==id);
            CompanyDtlEditAddDTO companyDtlDTO = _mapper.Map<CompanyDtlEditAddDTO>(companyDtl);

            return companyDtlDTO;
        }

        public async Task EditCompanyDtlAsync(int id, CompanyDtlEditAddDTO companyDtlDTO)
        {
            CompanyDtl companyDtl = await _Context.CompanyDtls.FirstOrDefaultAsync(obj => obj.CompIdDtl == id);

            _mapper.Map(companyDtlDTO, companyDtl);
            _Context.Update(companyDtl);
            await _Context.SaveChangesAsync();
        }
    }
}
