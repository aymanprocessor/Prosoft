using ProSoft.EF.DTOs.Stocks.Report.TotalItemCards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.IRepositories.Stocks.Reports
{
    public interface ITotalItemCardsRepo
    {
        Task<List<TotalItemCardQuantityReportDTO>> GetTotalItemCardQuantity(DateTime fromDate, DateTime toDate);
    }
}
