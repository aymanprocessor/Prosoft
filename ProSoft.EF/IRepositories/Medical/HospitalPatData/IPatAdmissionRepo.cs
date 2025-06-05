using ProSoft.EF.DTOs.Medical.HospitalPatData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.IRepositories.Medical.HospitalPatData
{
    public interface IPatAdmissionRepo
    {
        Task<List<PatAdmissionViewDTO>> GetAdmissionsByPatAsync(int patId);
        Task<PatAdmissionEditAddDTO> GetEmptyPatAdmissionAsync(int patId);

        ///////////////////////////////////Get for Ajax//////////////////////////////////
        Task<List<CompanyViewDTO>> GetCompany(int id);
        Task<List<CompanyDtlViewDTO>> GetSubCompany(int id);
        Task<List<RegionViewDTO>> GetSection(int id);
        /////////////////////////////////////////////////////////////////////////////////
        Task AddPatAdmissionAsync(int patId, PatAdmissionEditAddDTO patAdmissionDTO);
        Task AddBatchPatAdmissionsAsync(int patId, List<PatAdmissionEditAddDTO> patAdmissionDTOs);

        Task<PatAdmissionEditAddDTO> GetPatAdmissionByIdAsync(int id);
        Task EditPatAdmissionAsync(int id, PatAdmissionEditAddDTO patAdmissionDTO);
        Task EditPatAdmissionsBatchAsync(List<PatAdmissionEditAddDTO> admissionsToEdit);
        Task DeletePatAdmissionAsync(int id);


    }
}
