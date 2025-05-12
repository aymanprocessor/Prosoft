using ProSoft.EF.DTOs.Medical.HospitalPatData;
using ProSoft.EF.Models.Medical.HospitalPatData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.IRepositories.Medical.HospitalPatData
{
    public interface ICompanyRepo :IRepository<Company,int>
    {
        Task<List<CompanyViewDTO>> GetAllCompanyAsync(int id);
        Task<List<CompanyViewDTO>> GetAllCompanyAsync();
        Task<int> GetNewIdAsync();
        Task<CompanyEditAddDTO> GetEmptyCompanyAsync(int id);
        Task AddCompanylAsync(int id, CompanyEditAddDTO companyDTO);

        Task<CompanyEditAddDTO> GetCompanyByIdAsync(int id);
        Task EditCompanyAsync(int id, CompanyEditAddDTO companyDTO);


    }
}
