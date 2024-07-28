using ProSoft.EF.DTOs.Accounts;
using ProSoft.EF.DTOs.Accounts.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.IRepositories.Accounts
{
    public interface IReportDailyAssistanceRepo
    {
        Task<ReportDailyAssistanceDTO> GetAllDataAsync();
        Task<List<DailyAssistanceDTO>> GetDailyAssistanceAsync(int journal, DateTime fromPeriod, DateTime toPeriod, int branche, int fYear);
    }
}
