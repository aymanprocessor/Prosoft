using ProSoft.EF.DTOs.Medical.HospitalPatData;
using ProSoft.EF.Models.Medical.HospitalPatData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.IRepositories.Medical.HospitalPatData
{
    public interface ICompanyDtlRepo : IRepository<CompanyDtl, int>
    {
        Task<List<CompanyDtlViewDTO>> GetAllCompanyDtlAsync(int id);
        //Task<int> GetNewIdAsync();
        //Task<CompanyEditAddDTO> GetEmptyCompanyAsync(int id);
        //Task AddCompanylAsync(int id, CompanyEditAddDTO companyDTO);

        //Task<CompanyEditAddDTO> GetCompanyByIdAsync(int id);
        //Task EditCompanyAsync(int id, CompanyEditAddDTO companyDTO);
    }
}
