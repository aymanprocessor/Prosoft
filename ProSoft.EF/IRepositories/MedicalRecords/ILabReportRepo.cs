using ProSoft.EF.DTOs.MedicalRecords;
using ProSoft.EF.Models.MedicalRecords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.IRepositories.MedicalRecords
{
    public interface ILabReportRepo :IRepository<LabReport,int>
    {
        Task<List<LabReportDTO>> GetAllByPatIdAsync(int patId);
        Task<LabReportDTO> GetLabReportByIdAsync(int serialId);
        Task AddLabReportAsync(int id, LabReportDTO labReportDTO);
        Task EditLabReportAsync(int serialId, LabReportDTO labReportDTO);

    }
}
