using ProSoft.EF.DTOs.Medical.HospitalPatData;
using ProSoft.EF.DTOs.Treasury;
using ProSoft.EF.Models.Medical.HospitalPatData;
using ProSoft.EF.Models.Treasury;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.IRepositories.Medical.HospitalPatData
{
    public interface ICheckupClinicRepo : IRepository<CheckupClinic, int>
    {
        Task<List<CheckupClinicDTO>> All();
        Task<int> MaxCode();
        Task<CheckupClinicDTO> GetEditAsync(int id);
        Task<CheckupClinic> GetByIdAsync(int id);
    }
}
