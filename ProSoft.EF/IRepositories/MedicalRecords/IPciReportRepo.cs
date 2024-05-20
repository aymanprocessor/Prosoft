using ProSoft.EF.DTOs.MedicalRecords;
using ProSoft.EF.Models.MedicalRecords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.IRepositories.MedicalRecords
{
    public interface IPciReportRepo :IRepository<PciReport,int>
    {
        Task<List<PciReportDTO>> GetAllByPatIdAsync(int patId);
        Task<PciReportDTO> GetPciReportByIdAsync(int serialId);
        Task AddPciReportAsync(int id, PciReportDTO pciReportDTO);
        Task EditPciReportAsync(int serialId, PciReportDTO pciReportDTO);
    }
}
