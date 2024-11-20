using ProSoft.EF.DTOs.Stocks.TransferSuppliesToStocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.IRepositories.Stocks
{
    public interface IPostSuppliesToStocksRepo
    {
        Task<List<TransferSuppliesToStocksMasterDTO>> GetPatAdmissions(int branchId, int RegionId, DateTime FromDate, DateTime ToDate);
    }
}
