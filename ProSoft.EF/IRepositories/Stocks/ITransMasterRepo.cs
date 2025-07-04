﻿using Microsoft.EntityFrameworkCore;
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
        Task<List<TransMasterViewDTO>> GetAllPermissionsFormsAsync(int stockID, int transType, int? fromReceipt, int? toReceipt, DateTime? fromPeriod, DateTime? toPeriod, string supCode);
        Task<List<SupCodeViewDTO>> GetAllSuPCodesAsync();
        Task<bool> CheckForDetailsAsync(int transMAsterID);
        Task<int> IfPossibleToDeleteAsync(int transMAsterID);
        Task<int> ApprovePermissionAsync(int transMAsterID, int userCode);
        Task<int> CancelApprovePermissionAsync(int transMAsterID);
        Task DeletePermissionFormAsync(int transMAsterID);
        Task<TransMasterViewDTO> GetTransMasterByAsync(int transType, int stockCode, int deletedId);
        Task<TransMasterViewDTO> GetForViewAsync(TransMaster permissionForm);
        // Add Permission Form
        Task<TransMasterEditAddDTO> GetNewTransMasterAsync(int stockID, int userCode, int uniqueType);
        Task<TransMaster> AddPermissionFormAsync(TransMasterEditAddDTO permissionFormDTO);
        Task UpdateTransMasterAsync(int id, TransMasterEditAddDTO permissionFormDTO);
        //////////////////////////////////////////////////
        // Disburse or Convert Permission => اذن صرف او تحويل
        Task<TransMasterEditAddDTO> GetDisburseOrConvertFormByIdAsync(int transMasterID);
        Task<TransMasterEditAddDTO> GetNewDisburseOrConvertFormAsync(int stockID, int userCode, int uniqueType, int branchID);
        Task<TransMaster> AddDisburseOrConvertFormAsync(TransMasterEditAddDTO permissionFormDTO);
        Task UpdateDisburseOrConvertFormAsync(int id, TransMasterEditAddDTO permissionFormDTO);
        //////////////////////////////////////////////////
        // Stock Receive Permission => اذن استلام من مخزن
        //Task<TransMasterEditAddDTO> GetStockReceiveFormByIdAsync(int transMasterID);
        //Task UpdateStockReceiveFormAsync(int id, TransMasterEditAddDTO permissionFormDTO);
        //////////////////////////////////////////////////
        // Sales Invoice => فاتورة مبيعات
        Task<TransMasterEditAddDTO> GetSalesInvoiceByIdAsync(int transMasterID);
        Task<TransMasterEditAddDTO> GetNewSalesInvoiceAsync(int stockID, int userCode, int uniqueType, int branchID);
        Task<TransMaster> AddSalesInvoiceAsync(TransMasterEditAddDTO permissionFormDTO);
        Task UpdateSalesInvoiceAsync(int id, TransMasterEditAddDTO permissionFormDTO);
        //////////////////////////////////////////////////
        // Debit or Credit Settlement => تسوية مدينةاو دائنة
        Task<TransMasterEditAddDTO> GetSettlementByIdAsync(int transMasterID);
        Task<TransMasterEditAddDTO> GetNewSettlementAsync(int stockID, int userCode, int uniqueType, int branchID);
        Task<TransMaster> AddSettlementAsync(TransMasterEditAddDTO permissionFormDTO);
        Task UpdateSettlementAsync(int id, TransMasterEditAddDTO permissionFormDTO);
        //////////////////////////////////////////////////
        // Requirements Disburse Form => اذن صرف مستلزمات
        Task<TransMasterEditAddDTO> GetReqDisburseByIdAsync(int transMasterID);
        Task<TransMasterEditAddDTO> GetNewReqDisburseAsync(int stockID, int userCode, int uniqueType, int branchID);
        Task<TransMaster> AddReqDisburseAsync(TransMasterEditAddDTO permissionFormDTO);
        Task UpdateReqDisburseAsync(int id, TransMasterEditAddDTO permissionFormDTO);
        //////////////////////////////////////////////////
        // Return Permission Form => اذن ارتجاع لمورد
        Task<TransMasterEditAddDTO> GetReturnPermissionByIdAsync(int transMasterID);
        Task<TransMasterEditAddDTO> GetNewReturnPermissionAsync(int stockID, int userCode, int uniqueType, int branchID);
        Task<TransMaster> AddReturnPermissionAsync(TransMasterEditAddDTO permissionFormDTO);
        Task UpdateReturnPermissionAsync(int id, TransMasterEditAddDTO permissionFormDTO);
    }
}
