using ProSoft.EF.DTOs.Medical.HospitalPatData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.IRepositories.Medical.HospitalPatData
{
    public interface IPatientRepo
    {
        Task<List<PatViewDTO>> GetAllPatsAsync();
        Task<PatEditAddDTO> GetPatientByIdAsync(int id);

        ////////////////////////////////////////////////////
        Task AddPatientAsync(PatEditAddDTO patDTO);
        Task EditPatientAsync(int id, PatEditAddDTO patientDTO);
        Task DeletePatientAsync(int id);


    }
}
