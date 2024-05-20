using ProSoft.EF.DTOs.MedicalRecords;
using ProSoft.EF.Models.MedicalRecords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.IRepositories.MedicalRecords
{
    public interface IMedicationRepo :IRepository<MedicationAtCcu,int>
    {
        Task<List<MedicationDTO>> GetAllByPatIdAsync(int patId);
        Task<MedicationDTO> GetMedicationByIdAsync(int serialId);
        Task AddMedicationAsync(int id, MedicationDTO medicationDTO);
        Task EditMedicationAsync(int serialId, MedicationDTO medicationDTO);
    }
}
