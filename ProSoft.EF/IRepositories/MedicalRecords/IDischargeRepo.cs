using ProSoft.EF.DTOs.MedicalRecords;
using ProSoft.EF.Models.MedicalRecords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.IRepositories.MedicalRecords
{
    public interface IDischargeRepo: IRepository<DischargeSummery, int>
    {
        Task<List<DischargeDTO>> GetAllByPatIdAsync(int patId);
        Task<DischargeDTO> GetDischargeByIdAsync(int serialId);
        Task AddDischargeAsync(int patId, DischargeDTO dischargeDTO);
        Task EditDischargeAsync(int serialId, DischargeDTO dischargeDTO);
    }
}
