using ProSoft.EF.DTOs.Medical.HospitalPatData;
using ProSoft.EF.DTOs.Treasury;
using ProSoft.EF.Models.Medical.HospitalPatData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.IRepositories.Medical.HospitalPatData
{
    public interface IDrTimeSheetRepo : IRepository<Drtimsheet,int>
    {
        Task<List<DrTimeSheetDTO>> GetAppointmentsForDrAsync(int DrId);

    }
}
