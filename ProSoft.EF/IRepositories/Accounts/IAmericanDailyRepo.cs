using ProSoft.EF.DTOs.Accounts;
using ProSoft.EF.DTOs.Accounts.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.IRepositories.Accounts
{
    public interface IAmericanDailyRepo
    {
        Task<ReportAmericanDailyDTO> GetAllDataAsync();
        ///
        Task<List<AmericanDailyDTO>> GetAmericanDailyAsync(int branch, int journal, DateTime? fromPeriod, DateTime? toPeriod);
        Task<List<string>> GetMainNameAsync(int branch, int journal, DateTime? fromPeriod, DateTime? toPeriod);
        /////////
        Task<List<AmericanDailyDTO>> GetAmericanDailyForMainSubAsync(int branch, int journal, DateTime? fromPeriod, DateTime? toPeriod, string? mainCodeFilter, string? subCodeFilter);
        Task<List<string>> GetSubNameAsync(int branch, int journal, DateTime? fromPeriod, DateTime? toPeriod, string? mainCodeFilter, string? subCodeFilter);
        /////////
        Task<List<AmericanTotalJournalDTO>> GetAmericanTotalJournal(int journal, int branch, DateTime? fromPeriod, DateTime? toPeriod);
    }
}
