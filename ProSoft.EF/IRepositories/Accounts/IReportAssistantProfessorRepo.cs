using ProSoft.EF.DTOs.Accounts;
using ProSoft.EF.DTOs.Accounts.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.IRepositories.Accounts
{
    public interface IReportAssistantProfessorRepo
    {
        Task<ReportAssistantProfessorDTO> GetAllDataAsync();
        Task<List<AssistantProfessorAnalyticalDTO>> GetAnalyticalAsync(int branch, int journal, string mainCode, string subCode, int costCode, DateTime? fromPeriod, DateTime? toPeriod,int fYear);
        Task<List<AssistantProfessorOverAllDTO>> GetOverAllAsync(int branch, int journal, string mainCode, int costCode, DateTime? fromPeriod, DateTime? toPeriod, int fYear);
    }
}
