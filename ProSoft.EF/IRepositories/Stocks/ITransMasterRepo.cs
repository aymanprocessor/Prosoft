using ProSoft.EF.DTOs.Shared;
using ProSoft.EF.DTOs.Stocks;
using ProSoft.EF.Models.Stocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.IRepositories.Stocks
{
    public interface ITransMasterRepo: IRepository<TransMaster, int>
    {
        Task<List<StockViewDTO>> GetActiveStocksForUserAsync(int userCode);
        Task<List<PermissionDefViewDTO>> GetUserPermissionsForStockAsync(int userCode, int stockID);
    }
}
