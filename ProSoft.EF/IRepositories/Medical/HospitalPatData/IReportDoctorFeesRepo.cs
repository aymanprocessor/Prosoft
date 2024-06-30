using ProSoft.EF.DTOs.Accounts.Report;
using ProSoft.EF.DTOs.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProSoft.EF.DTOs.Medical.HospitalPatData;
using ProSoft.EF.DTOs.Medical.Report;

namespace ProSoft.EF.IRepositories.Medical.HospitalPatData
{
    public interface IReportDoctorFeesRepo 
    {
        Task<ReportDoctorFeesDTO> GetAllDataAsync();
        Task<List<DoctorFeesFromToDTO>> GetReportDoctorFeesData(int drCode, DateTime fromPeriod, DateTime toPeriod, int branchId);
    }
}
