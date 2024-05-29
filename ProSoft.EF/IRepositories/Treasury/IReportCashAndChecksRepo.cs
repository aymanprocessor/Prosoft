using ProSoft.EF.DTOs.Treasury;
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

    }
}
