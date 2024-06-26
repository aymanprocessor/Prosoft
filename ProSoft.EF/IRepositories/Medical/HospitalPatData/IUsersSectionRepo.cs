using ProSoft.EF.DTOs.Medical.HospitalPatData;
using ProSoft.EF.Models.Medical.HospitalPatData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.IRepositories.Medical.HospitalPatData
{
    public interface IUsersSectionRepo :IRepository<UsersSection,int>
    {
        Task<List<UsersSectionDTO>> GetMedicalServicesForUser(int userCode);
        Task<UsersSectionDTO> GetEmptyUsersSectionAsync(int userCode);
        Task<UsersSectionDTO> GetUsersSectionByIdAsync(int id);
    }
}
