using Microsoft.EntityFrameworkCore;
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
        Task<TransMasterEditAddDTO> GetTransMasterByIdAsync(int transMasterID);
        Task<List<StockViewDTO>> GetActiveStocksForUserAsync(int userCode);
        Task<List<PermissionDefViewDTO>> GetUserPermissionsForStockAsync(int userCode, int stockID);
        Task<List<TransMasterViewDTO>> GetPermissionsFormsAsync(int stockID, int transType);
        Task<TransMasterEditAddDTO> GetNewTransMasterAsync(int stockID, int permissionID);
        Task AddPermissionFormAsync(TransMasterEditAddDTO permissionFormDTO);
        Task UpdateTransMasterAsync(int id, TransMasterEditAddDTO permissionFormDTO);
    }
}
