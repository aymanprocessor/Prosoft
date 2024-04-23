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
        Task<int> GetNewIdAsync();
        Task<CompanyEditAddDTO> GetCompanyByIdAsync(int id);
      //  Task EditDoctorPercentAsync(int id, DoctorPrecentEditAddDTO doctorPrecentDTO);


    }
}
