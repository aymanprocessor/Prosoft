using ProSoft.EF.DTOs.Treasury;
using ProSoft.EF.DTOs.Treasury.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.IRepositories.Treasury
{
    public interface IReportCashAndChecksRepo
    {
        Task<ReportCashAndChecksDTO> GetAllDataAsync(int userCode);
        Task<List<CashTreasuryDataDTO>> GetCashTreasuryData(int branchId, int userCode, int safeCode, int? fromReceipt, int? toReceipt, DateTime? fromPeriod, DateTime? toPeriod);
        Task<List<FollowChecksDataDTO>> GetFollowChecksData(int branchId, int userCode, int safeCode, int? fromReceipt, int? toReceipt, DateTime? fromPeriod, DateTime? toPeriod);

    }
}
