using ProSoft.EF.DTOs.Stocks.Report.Request_Limit_Items_Report;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.IRepositories.Stocks.Reports
{
    public interface IRequestLimitItemsReportRepo
    {
        Task<List<RequestLimitItemsReportDTO>> GetRequestLimitItemsReport(int stockId, int BranchId, Filter filter);
    }
}
