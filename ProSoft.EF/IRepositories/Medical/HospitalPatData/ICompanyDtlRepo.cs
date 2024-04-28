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
        Task<int> GetNewIdAsync();
        Task AddCompanyDtllAsync(int id, CompanyDtlEditAddDTO companyDtlDTO);
        Task<CompanyDtlEditAddDTO> GetCompanyDtlByIdAsync(int id);
        Task EditCompanyDtlAsync(int id, CompanyDtlEditAddDTO companyDtlDTO);
    }
}
