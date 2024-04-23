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
    public class CompanyRepo : Repository<Company,int> ,ICompanyRepo
    {
        public CompanyRepo(AppDbContext Context) : base(Context)
        {
        }

        public async Task<List<CompanyViewDTO>> GetAllCompanyAsync(int id)
        {
            List<CompanyViewDTO> companyDTOs =await _Context.Companies.Where(obj => obj.GroupId == id)
                 .Select(obj => new CompanyViewDTO()
                 {
                     CompId = (int)obj.CompId,
                     CompName = obj.CompName,
                     PLId = (int)obj.PL.PLId,
                     PlDesc = obj.PL.PlDesc,
                     EInvMainFlg = obj.EInvMainFlg,
                     SubCode =obj.SubCode,
                     MainCode =obj.MainCode,
                     Stamp =(decimal)obj.Stamp,
                     StampPer =(decimal)obj.StampPer,
                     TaxPer =(decimal)obj.TaxPer,
                     KName = obj.KindStoreNavigation.KName,
                     ComAdd =obj.ComAdd,
                     CompTelephone=obj.CompTelephone,
                     CompFax=obj.CompFax,
                     MedicalManager=obj.MedicalManager,
                     InsurancePeriod=obj.InsurancePeriod,
                 })
                 .ToListAsync();
            return companyDTOs;
        }

        public Task<CompanyEditAddDTO> GetCompanyByIdAsync(int id)
        {
            throw new NotImplementedException();
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
    }
}
