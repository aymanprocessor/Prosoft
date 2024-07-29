using ProSoft.EF.DTOs.Accounts.Report;
using ProSoft.EF.DTOs.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.IRepositories.Accounts
{
    public interface IReportTransactionAccountJournalRepo
    {
        Task<ReportTransactionAccountJournalDTO> GetAllDataAsync();
        Task<List<TransactionAccountJournalDTO>> GetTransactionAccountJournal(int journal, DateTime? fromPeriod, DateTime? toPeriod ,string mainCode, string subCode, int branche);
    }
}
