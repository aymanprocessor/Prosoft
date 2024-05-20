using ProSoft.EF.DTOs.MedicalRecords;
using ProSoft.EF.Models.MedicalRecords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.IRepositories.MedicalRecords
{
    public interface IPastHistoryRepo: IRepository<PastHistory, int>
    {
        Task<List<PastHistoryDTO>> GetAllByPatIdAsync(int patId);
        Task<PastHistoryDTO> GetPastHistoryByIdAsync(int serialId);
        Task AddPastHistoryAsync(int patId, PastHistoryDTO pastHistoryDTO);
        Task EditPastHistoryAsync(int serialId, PastHistoryDTO pastHistoryDTO);
    }
}
