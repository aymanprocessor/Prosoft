using ProSoft.EF.DTOs.Stocks;
using ProSoft.EF.Models.Stocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.IRepositories.Stocks
{
    public interface IOrderLimitRepo: IRepository<ItmReorder, int>
    {
        Task<List<ItmReorderDTO>> GetItemsLimitsAsync(DateTime date1, DateTime date2, int stockID, int branchID);
    }
}
