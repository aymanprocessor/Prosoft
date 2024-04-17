using ProSoft.EF.DTOs.Medical.HospitalPatData;
using ProSoft.EF.Models.Medical.HospitalPatData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.IRepositories.Medical.HospitalPatData
{
    public interface IServiceClinicRepo :IRepository<ServiceClinic,int>
    {
        Task<List<ServiceClinicViewDTO>> GetAllServClinicAsync(int id);
        Task<int> GetNewIdAsync();
        Task<ServClinicEditAddDTO> GetServClinicByIdAsync(int id);
        Task<ServClinicEditAddDTO> GetEmptyServClinicAsync(int id);

        Task AddServClinicAsync(int id, int clinicID, ServClinicEditAddDTO servClinicDTO);
        Task EditServClinicAsync(int id, ServClinicEditAddDTO servClinicDTO);
    }
}
