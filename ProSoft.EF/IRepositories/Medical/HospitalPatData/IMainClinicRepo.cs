using ProSoft.EF.DTOs.Medical.HospitalPatData;
using ProSoft.EF.Models.Medical.HospitalPatData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.IRepositories.Medical.HospitalPatData
{
    public interface IMainClinicRepo :IRepository<MainClinic,int>
    {
        Task<List<MainClinicViewDTO>> GetAllClinics();
        Task<int> GetNewIdAsync();
        Task<MainClinicEditAddDTO> GetMainClinicByIdAsync(int id);
        Task<MainClinicEditAddDTO> GetEmptyClinicAsync ();
        /////////
        Task AddMainClinicAsync(MainClinicEditAddDTO mainClinicDTO);
        Task EditMainClinicAsync(int id,MainClinicEditAddDTO mainClinicDTO);
    }
}
