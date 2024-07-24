using ProSoft.EF.DTOs.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.IRepositories.Accounts
{
    public interface IMonthlyClosingRepo
    {
        Task<MonthlyClosingDTO> GetAllDataAsync();
        Task<string> CloseAsync(int journal, int branch, int? fromVoucher, int? toVoucher, DateTime? fromPeriod, DateTime? toPeriodint);
        Task<string> CancelAsync(int journal, int branch, int? fromVoucher, int? toVoucher, DateTime? fromPeriod, DateTime? toPeriodint);

    }
}
