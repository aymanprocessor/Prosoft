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
        Task<TransMasterEditAddDTO> GetPermissionFormByIdAsync(int transMasterID);
        Task<List<StockViewDTO>> GetActiveStocksForUserAsync(int userCode);
        Task<List<PermissionDefViewDTO>> GetUserPermissionsForStockAsync(int userCode, int stockID);
        Task<List<TransMasterViewDTO>> GetPermissionsFormsAsync(int stockID, int transType);
        Task<TransMasterViewDTO> GetForViewAsync(TransMaster permissionForm);
        // Add Permission Form
        Task<TransMasterEditAddDTO> GetNewTransMasterAsync(int stockID, int userCode, int permissionID);
        Task<TransMaster> AddPermissionFormAsync(TransMasterEditAddDTO permissionFormDTO);
        Task UpdateTransMasterAsync(int id, TransMasterEditAddDTO permissionFormDTO);
        //////////////////////////////////////////////////
        // Add Disburse Form => اذن صرف
        Task<TransMasterEditAddDTO> GetDisburseFormByIdAsync(int transMasterID);
        Task<TransMasterEditAddDTO> GetNewDisburseFormAsync(int stockID, int userCode, int permissionID, int branchID);
        Task<TransMaster> AddDisburseFormAsync(TransMasterEditAddDTO permissionFormDTO);
        Task UpdateDisburseFormAsync(int id, TransMasterEditAddDTO permissionFormDTO);
    }
}
