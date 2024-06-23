using ProSoft.EF.DTOs.Treasury.Report;
using ProSoft.EF.DTOs.Treasury;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProSoft.EF.DTOs.Accounts;
using ProSoft.EF.DTOs.Accounts.Report;

namespace ProSoft.EF.IRepositories.Accounts
{
    public interface IReportFromToVoucherRepo
    {
        Task<ReportFromToVoucherDTO> GetAllDataAsync();
        Task<List<FromToVoucherDataDTO>> GetFromToVoucherData(int journal, int? fromReceipt, int? toReceipt, DateTime? fromPeriod, DateTime? toPeriod, int fYear);

    }
}
