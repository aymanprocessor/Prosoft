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
        // Disburse Form => اذن صرف
        Task<TransMasterEditAddDTO> GetDisburseFormByIdAsync(int transMasterID);
        Task<TransMasterEditAddDTO> GetNewDisburseFormAsync(int stockID, int userCode, int permissionID, int branchID);
        Task<TransMaster> AddDisburseFormAsync(TransMasterEditAddDTO permissionFormDTO);
        Task UpdateDisburseFormAsync(int id, TransMasterEditAddDTO permissionFormDTO);
        //////////////////////////////////////////////////
        // Sales Invoice => فاتورة مبيعات
        Task<TransMasterEditAddDTO> GetSalesInvoiceByIdAsync(int transMasterID);
        Task<TransMasterEditAddDTO> GetNewSalesInvoiceAsync(int stockID, int userCode, int permissionID, int branchID);
        Task<TransMaster> AddSalesInvoiceAsync(TransMasterEditAddDTO permissionFormDTO);
        Task UpdateSalesInvoiceAsync(int id, TransMasterEditAddDTO permissionFormDTO);
        //////////////////////////////////////////////////
        // Debit or Credit Settlement => تسوية مدينةاو دائنة
        Task<TransMasterEditAddDTO> GetSettlementByIdAsync(int transMasterID);
        Task<TransMasterEditAddDTO> GetNewSettlementAsync(int stockID, int userCode, int permissionID, int branchID);
        Task<TransMaster> AddSettlementAsync(TransMasterEditAddDTO permissionFormDTO);
        Task UpdateSettlementAsync(int id, TransMasterEditAddDTO permissionFormDTO);
        //////////////////////////////////////////////////
        // Requirements Disburse Form => اذن صرف مستلزمات
        Task<TransMasterEditAddDTO> GetReqDisburseByIdAsync(int transMasterID);
        Task<TransMasterEditAddDTO> GetNewReqDisburseAsync(int stockID, int userCode, int permissionID, int branchID);
        Task<TransMaster> AddReqDisburseAsync(TransMasterEditAddDTO permissionFormDTO);
        Task UpdateReqDisburseAsync(int id, TransMasterEditAddDTO permissionFormDTO);
        //////////////////////////////////////////////////
        // Return Permission Form => اذن ارتجاع لمورد
        Task<TransMasterEditAddDTO> GetReturnPermissionByIdAsync(int transMasterID);
        Task<TransMasterEditAddDTO> GetNewReturnPermissionAsync(int stockID, int userCode, int permissionID, int branchID);
        Task<TransMaster> AddReturnPermissionAsync(TransMasterEditAddDTO permissionFormDTO);
        Task UpdateReturnPermissionAsync(int id, TransMasterEditAddDTO permissionFormDTO);
    }
}
