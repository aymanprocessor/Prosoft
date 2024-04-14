using ProSoft.EF.DTOs.Medical.HospitalPatData;
using ProSoft.EF.Models.Medical.HospitalPatData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.IRepositories.Medical.HospitalPatData
{
    public interface ISubClinicRepo : IRepository<SubClinic,int>
    {
        Task<List<SubClinicViewDTO>> GetAllSubClinicsAsync(int id);
        Task<int> GetNewIdAsync();
        Task<SubClinicEditAddDTO> GetSubClinicByIdAsync(int id);
        Task<SubClinicEditAddDTO> GetEmptySubClinicAsync();

        Task AddSubClinicAsync(int id, SubClinicEditAddDTO subClinicDTO);
        Task EditSubClinicAsync(int id, SubClinicEditAddDTO subClinicDTO);
    }
}
