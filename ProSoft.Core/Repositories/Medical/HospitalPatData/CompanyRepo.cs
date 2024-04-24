using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProSoft.EF.DbContext;
using ProSoft.EF.DTOs.Medical.HospitalPatData;
using ProSoft.EF.DTOs.Stocks;
using ProSoft.EF.IRepositories.Medical.HospitalPatData;
using ProSoft.EF.Models.Medical.HospitalPatData;
using ProSoft.EF.Models.Stocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ProSoft.Core.Repositories.Medical.HospitalPatData
{
    public class CompanyRepo : Repository<Company,int> ,ICompanyRepo
    {
        private readonly IMapper _mapper;
        public CompanyRepo(AppDbContext Context, IMapper mapper) : base(Context)
        {
            _mapper= mapper;
        }

        public async Task<List<CompanyViewDTO>> GetAllCompanyAsync(int id)
        {

            List<CompanyViewDTO> companyDTOs = await _Context.Companies.Where(obj => obj.GroupId == id)
                 .Select(obj => new CompanyViewDTO()
                 {
                     CompId = (int)obj.CompId,
                     CompName = obj.CompName,
                     PLId = (int)obj.PL.PLId,
                     PlDesc = obj.PL.PlDesc,
                     EInvMainFlg = obj.EInvMainFlg,
                     TaxNo = obj.TaxNo,
                     SubCode = obj.SubCode,
                     MainCode = obj.MainCode,
                     Stamp = (decimal)obj.Stamp,
                     StampPer = (decimal)obj.StampPer,
                     TaxPer = (decimal)obj.TaxPer,
                     KName = obj.KindStoreNavigation.KName,
                     ComAdd = obj.ComAdd,
                     CompTelephone = obj.CompTelephone,
                     CompFax = obj.CompFax,
                     MedicalManager = obj.MedicalManager,
                     InsurancePeriod = obj.InsurancePeriod,
                 })
                 .ToListAsync();
            return companyDTOs;
        }

        public async Task<CompanyEditAddDTO> GetCompanyByIdAsync(int id)
        {
            Company company = await _Context.Companies.FirstOrDefaultAsync(obj => obj.CompId == id);
            CompanyEditAddDTO companyDTO = _mapper.Map<CompanyEditAddDTO>(company);
            List<PriceList> priceLists = await _Context.PriceLists.ToListAsync();
            List<KindStore> kindStores = await _Context.KindStores.ToListAsync();

            companyDTO.priceLists = _mapper.Map<List<PriceListViewDTO>>(priceLists);
            companyDTO.kindStores = _mapper.Map<List<KindStoreDTO>>(kindStores);

            return companyDTO;
        }

        public async Task<int> GetNewIdAsync()
        {
            int newID;
            if (_DbSet.Count() != 0)
            {
                var lastID = await _DbSet.MaxAsync(obj => obj.CompId);
                newID = lastID + 1;
            }
            else
                newID = 1;
            return newID;
        }
        public async Task<CompanyEditAddDTO> GetEmptyCompanyAsync(int id)
        {
            CompanyEditAddDTO companyDTO = new CompanyEditAddDTO();
            
            List<PriceList> priceLists = await _Context.PriceLists.ToListAsync();
            List<KindStore> kindStores = await _Context.KindStores.ToListAsync();

            companyDTO.priceLists = _mapper.Map<List<PriceListViewDTO>>(priceLists);
            companyDTO.kindStores = _mapper.Map<List<KindStoreDTO>>(kindStores);

            return companyDTO;
        }


        public async Task AddCompanylAsync(int id, CompanyEditAddDTO companyDTO)
        {
            Company company = _mapper.Map<Company>(companyDTO);
            company.GroupId = id;
            company.CompanyType = 2;

            await _Context.AddAsync(company);
            await _Context.SaveChangesAsync();
        }

        public async Task EditCompanyAsync(int id, CompanyEditAddDTO companyDTO)
        {
            Company company = await _Context.Companies.FirstOrDefaultAsync(obj => obj.CompId == id);

            _mapper.Map(companyDTO, company);
            _Context.Update(company);
            await _Context.SaveChangesAsync();
        }
    }

}
