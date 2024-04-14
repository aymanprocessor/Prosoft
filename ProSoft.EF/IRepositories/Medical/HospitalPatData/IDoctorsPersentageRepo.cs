using ProSoft.EF.DTOs.Medical.HospitalPatData;
using ProSoft.EF.Models.Medical.HospitalPatData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.IRepositories.Medical.HospitalPatData
{
    public interface IDoctorsPersentageRepo :IRepository<DoctorsPercent,int>
    {
        Task<List<DoctorPrecentViewDTO>> GetAllDoctorPersentageAsync();
        Task<List<DoctorPrecentViewDTO>> GetAllDoctorPersentageAsync(int id);
        Task<List<Doctor>> GetAllDoctorAsync();

    }
}
