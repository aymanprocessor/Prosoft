using ProSoft.EF.DTOs.Accounts;
using ProSoft.EF.DTOs.Accounts.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.IRepositories.Accounts
{
    public interface IReportReviewJournalVouchersRepo
    {
        Task<ReportReviewJournalVouchersDTO> GetAllDataAsync();
        Task<List<ReviewJournalVouchersDTO>> GetReviewDisplay(int journal, int branche, DateTime? fromPeriod, DateTime? toPeriod, int displayType);

    }
}
