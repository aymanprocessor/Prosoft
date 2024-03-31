using ProSoft.EF.DTOs.Medical.HospitalPatData;
using ProSoft.EF.Models.Medical.HospitalPatData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.IRepositories.Medical.HospitalPatData
{
    public interface IDoctorRepo :IRepository<Doctor,int>
    {
        Task<List<DoctorViewDTO>> GetAllDoctor ();
        Task<int> GetNewIdAsync();
        Task<DoctorEditAddDTO> GetEmptyDoctorAsync();

    }
}
