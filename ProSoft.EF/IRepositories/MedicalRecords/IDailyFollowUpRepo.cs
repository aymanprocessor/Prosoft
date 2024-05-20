using ProSoft.EF.DTOs.Medical.HospitalPatData;
using ProSoft.EF.DTOs.MedicalRecords;
using ProSoft.EF.Models.MedicalRecords;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.IRepositories.MedicalRecords
{
    public interface IDailyFollowUpRepo : IRepository<DailyFollowUpCcuChant,int>
    {
        Task<List<DailyFollowUpDTO>> GetAllByPatIdAsync(int patId);
        Task<DailyFollowUpDTO> GetDailyFollowUpByIdAsync(int serialId);
        Task AddDailyFollowUpAsync(int id, DailyFollowUpDTO dailyFollowUpDTO);
        Task EditDailyFollowUpAsync(int serialId, DailyFollowUpDTO dailyFollowUpDTO);
    }
}
