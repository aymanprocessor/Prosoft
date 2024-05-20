using ProSoft.EF.DTOs.MedicalRecords;
using ProSoft.EF.Models.MedicalRecords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.IRepositories.MedicalRecords
{
    public interface IHistoryExaminationRepo : IRepository<HistoryExamination,int>
    {
        Task<List<HistoryExaminationDTO>> GetAllByPatIdAsync(int patId);
        Task<HistoryExaminationDTO> GetHistoryExaminationByIdAsync(int serialId);
        Task AddHistoryExaminationAsync(int id, HistoryExaminationDTO historyExaminationDTO);
        Task EditHistoryExaminationAsync(int serialId, HistoryExaminationDTO historyExaminationDTO);
    }
}
