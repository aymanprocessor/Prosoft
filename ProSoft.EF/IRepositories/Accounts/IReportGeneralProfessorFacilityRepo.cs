using ProSoft.EF.DTOs.Accounts.Report;
using ProSoft.EF.DTOs.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.IRepositories.Accounts
{
    public interface IReportGeneralProfessorFacilityRepo
    {
        Task<ReportGeneralProfessorFacilityDTO> GetAllDataAsync();
        Task<List<GeneralProfessorFacilityDTO>> GetGeneralProfessorAsync(int branch, DateTime toPeriod);

    }
}
