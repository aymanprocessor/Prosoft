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
    public class CompanyDtlRepo : Repository<CompanyDtl, int>, ICompanyDtlRepo
    {
        public CompanyDtlRepo(AppDbContext Context) : base(Context)
        {
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
    }
}
