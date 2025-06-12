using ProSoft.EF.DTOs.Medical.HospitalPatData;
using ProSoft.EF.Models.Medical.HospitalPatData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.IRepositories.Medical.HospitalPatData
{
    public interface IPatientRepo: IRepository<Pat, int>
    {
        // ----------------- Ayman Saad ----------------- //
        Task<List<PatViewDTO>> GetPatientsByDoctorIdAndDateAsync(int doctorId, DateTime date);
        int GetPatientCounts();
        int GetPatientCountsDaily();
        int GetPatientCountsWeekly();
        // ----------------- Ayman Saad ----------------- //

        Task<List<PatViewDTO>> GetAllPatsAsync();
        Task<List<Pat>> GetAllPatientsAsync();

        Task<PatEditAddDTO> GetPatientByIdAsync(int id);

        ////////////////////////////////////////////////////
        Task AddPatientAsync(PatEditAddDTO patDTO);
        Task AddBatchPatientsAsync(List<PatEditAddDTO> patDTOs);
        Task EditPatientAsync(int id, PatEditAddDTO patientDTO);
        Task EditBatchPatientsAsync(List<PatEditAddDTO> patientDTOs);
        Task DeletePatientAsync(int id);


    }
}
