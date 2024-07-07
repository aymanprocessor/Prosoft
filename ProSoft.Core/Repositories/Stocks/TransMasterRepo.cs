using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProSoft.EF.DbContext;
using ProSoft.EF.DTOs.Auth;
using ProSoft.EF.DTOs.Medical.HospitalPatData;
using ProSoft.EF.DTOs.Shared;
using ProSoft.EF.DTOs.Stocks;
using ProSoft.EF.DTOs.Treasury;
using ProSoft.EF.IRepositories.Stocks;
using ProSoft.EF.Models;
using ProSoft.EF.Models.Accounts;
using ProSoft.EF.Models.Medical.HospitalPatData;
using ProSoft.EF.Models.Shared;
using ProSoft.EF.Models.Stocks;
using ProSoft.EF.Models.Treasury;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.Core.Repositories.Stocks
{
    public class TransMasterRepo : Repository<TransMaster, int>, ITransMasterRepo
    {
        private readonly IMapper _mapper;
        public TransMasterRepo(AppDbContext Context, IMapper mapper) : base(Context)
        {
            _mapper = mapper;
        }

        public async Task<TransMasterEditAddDTO> GetPermissionFormByIdAsync(int transMasterID)
        {
            TransMaster permissionForm = await GetByIdAsync(transMasterID);
            var permissionFormDTO = _mapper.Map<TransMasterEditAddDTO>(permissionForm);

            List<TransMaster> permissionForms = await _DbSet
                .Where(obj => obj.StockCode == permissionFormDTO.StockCode
                && obj.TransType == permissionFormDTO.TransType
                && obj.Flag3 == "1").ToListAsync();

            List<SupCode> suppliers = await _Context.SupCodes.ToListAsync();
            permissionFormDTO.Suppliers = _mapper.Map<List<SupCodeViewDTO>>(suppliers);

            Stock stock = await _Context.Stocks.FindAsync((int)permissionFormDTO.StockCode);
            GeneralCode permission = await _Context.GeneralCodes
                .FirstOrDefaultAsync(obj => obj.UniqueType == permissionFormDTO.TransType);
            AppUser user = await _Context.Users.FirstOrDefaultAsync(obj => obj.UserCode ==
                permissionFormDTO.UserCode);

            permissionFormDTO.MonthName = CultureInfo.CurrentCulture.
                DateTimeFormat.GetMonthName((int)permissionFormDTO.FMonth);
            permissionFormDTO.PermissionsCount = permissionForms.Count();
            permissionFormDTO.StockName = stock?.Stknam;
            permissionFormDTO.PermissionName = permission?.GDesc;
            permissionFormDTO.UserName = user?.UserName;
            return permissionFormDTO;
        }

        public async Task<List<StockViewDTO>> GetActiveStocksForUserAsync(int userCode)
        {
            List<StockEmp> stockTransList = await _Context.StockEmps
                .Where(obj => obj.UserId == userCode && obj.StockDef == 1).ToListAsync();
            List<Stock> stocksList = await _Context.Stocks.ToListAsync();

            var stocksDTO = new List<StockViewDTO>();
            var isExisted = false;
            foreach (var stock in stocksList)
            {
                foreach (var stockTrans in stockTransList)
                {
                    if (stock.Stkcod == stockTrans.Stkcod)
                    {
                        isExisted = true;
                        break;
                    }
                    else
                        isExisted = false;
                }
                if (isExisted)
                {
                    var stockDTO = new StockViewDTO();
                    stockDTO.Stkcod = stock.Stkcod;
                    stockDTO.Stknam = (await _Context.Stocks
                        .FindAsync(stock.Stkcod)).Stknam;
                    stocksDTO.Add(stockDTO);
                }
            }
            return stocksDTO;
        }

        public async Task<List<PermissionDefViewDTO>> GetUserPermissionsForStockAsync(int userCode, int stockID)
        {
            List<StockEmp> stockTrans = await _Context.StockEmps
                .Where(obj => obj.UserId == userCode && obj.Stkcod == stockID && obj.StockDef == 1)
                .ToListAsync();

            var permissionsDTO = new List<PermissionDefViewDTO>();
            foreach (var item in stockTrans)
            {
                GeneralCode permission = await _Context.GeneralCodes
                    .FirstOrDefaultAsync(obj => obj.UniqueType == item.TransType);
                var permissionDTO = _mapper.Map<PermissionDefViewDTO>(permission);
                permissionsDTO.Add(permissionDTO);
            }
            return permissionsDTO;
        }

        public async Task<bool> CheckForDetailsAsync(int transMAsterID)
        {
            List<TransDtl> transDetails = await _Context.TransDtls
                .Where(obj => obj.TransMAsterID == transMAsterID).ToListAsync();
            return transDetails.Count != 0;
        }

        public async Task<int> IfPossibleToDeleteAsync(int transMAsterID)
        {
            TransMaster transMaster = await GetByIdAsync(transMAsterID);
            UserJournalType userJournal = await _Context.UserJournalTypes
                .FirstOrDefaultAsync(obj => obj.UserCode == transMaster.UserCode &&
                obj.BranchId == transMaster.BranchId);

            AccTransMaster accTransMaster = await _Context.AccTransMasters.FirstOrDefaultAsync(obj =>
                obj.CoCode == transMaster.BranchId && obj.FYear == transMaster.FYear &&
                obj.TransType == userJournal.JournalCode.ToString() && obj.TransNo == transMaster.AccTransNo);

            return accTransMaster != null ? 1 : transMaster.Flag3 == "2" ? 2 : 3;
        }

        public async Task ApprovePermissionAsync(int transMAsterID)
        {
            TransMaster transMaster = await GetByIdAsync(transMAsterID);
            transMaster.Flag3 = "2";
            await SaveChangesAsync();
        }

        public async Task DeletePermissionFormAsync(int transMAsterID)
        {
            TransMaster permissionForm = await GetByIdAsync(transMAsterID);
            List<TransDtl> transDtls = await _Context.TransDtls.Where(obj => obj.TransMAsterID == transMAsterID)
                .ToListAsync();

            foreach (var item in transDtls)
                _Context.Remove(item);

            await DeleteAsync(permissionForm);
            await SaveChangesAsync();
        }

        public async Task<TransMasterViewDTO> GetForViewAsync(TransMaster permissionForm)
        {
            if (permissionForm != null)
            {
                var permFormDTO = _mapper.Map<TransMasterViewDTO>(permissionForm);
                permFormDTO.PermissionName = (await _Context.GeneralCodes
                .FirstOrDefaultAsync(obj => obj.UniqueType == permissionForm.TransType)).GDesc;
                permFormDTO.StockName = (await _Context.Stocks
                    .FirstOrDefaultAsync(obj => obj.Stkcod == permissionForm.StockCode)).Stknam;
                if (permissionForm.StockCode2 is not (null or 0))
                {
                    permFormDTO.StockName2 = (await _Context.Stocks
                    .FirstOrDefaultAsync(obj => obj.Stkcod == permissionForm.StockCode2)).Stknam;
                }
                permFormDTO.UserName = (await _Context.Users
                    .FirstOrDefaultAsync(obj => obj.UserCode == permissionForm.UserCode)).UserName;
                if (permissionForm.SupNo is not (null or "0"))
                {
                    permFormDTO.SupplierName = (await _Context.SupCodes
                        .FirstOrDefaultAsync(obj => obj.SupCode1 == permissionForm.SupNo)).SupName;
                }
                StockEmp stockTrans = await _Context.StockEmps.FirstOrDefaultAsync(obj => 
                    obj.Stkcod == permissionForm.StockCode && obj.TransType == permissionForm.TransType &&
                    obj.UserId == permFormDTO.UserCode);
                permFormDTO.ShowTransPrice = (int)stockTrans.ShowPrice;
                return permFormDTO;
            }
            return null;
        }

        public async Task<List<TransMasterViewDTO>> GetPermissionsFormsAsync(int stockID, int transType)
        {
            List<TransMaster> permissionsForms = await _DbSet.Where(obj => obj.StockCode == stockID
                && obj.TransType == transType && obj.Flag3 == "1").ToListAsync();
            var permissionsFormsDTO = new List<TransMasterViewDTO>();
            foreach (var item in permissionsForms)
            {
                TransMasterViewDTO permFormDTO = await GetForViewAsync(item);
                StockEmp stockTrans = await _Context.StockEmps.FirstOrDefaultAsync(obj => obj.Stkcod == stockID &&
                    obj.TransType == transType && obj.UserId == permFormDTO.UserCode);
                if(stockTrans != null)
                    permFormDTO.ShowTransPrice = (int)stockTrans.ShowPrice;
                
                permissionsFormsDTO.Add(permFormDTO);
            }
            return permissionsFormsDTO;
        }

        // Add Permission Form
        public async Task<TransMasterEditAddDTO> GetNewTransMasterAsync(int stockID, int userCode, int uniqueType)
        {
            var permissionFormDTO = new TransMasterEditAddDTO();
            List<TransMaster> permissionForms = await _DbSet
                .Where(obj => obj.StockCode == stockID && obj.TransType == uniqueType
                && obj.Flag3 == "1").ToListAsync();

            EisUserObject userObj = await _Context.EisUserObjects
                .FirstOrDefaultAsync(obj => obj.UserId == userCode &&
                obj.ObjectName == "w_stores_trans" && obj.DefultId == 1);

            permissionFormDTO.EnableModify = userObj != null;

            List<SupCode> suppliers = await _Context.SupCodes.ToListAsync();
            permissionFormDTO.Suppliers = _mapper.Map<List<SupCodeViewDTO>>(suppliers);

            Stock stock = await _Context.Stocks.FindAsync(stockID);
            permissionFormDTO.StockCode = (short)stockID;
            GeneralCode permission = await _Context.GeneralCodes
                .FirstOrDefaultAsync(obj => obj.UniqueType == uniqueType);
            permissionFormDTO.TransType = uniqueType;

            permissionFormDTO.DocDate = DateTime.Now;
            permissionFormDTO.FYear = DateTime.Now.Year;
            permissionFormDTO.MonthName = DateTime.Now.ToString("MMMM");
            permissionFormDTO.PermissionsCount = permissionForms.Count();
            permissionFormDTO.StockName = stock?.Stknam;
            permissionFormDTO.PermissionName = permission?.GDesc;
            return permissionFormDTO;
        }

        public async Task<TransMaster> AddPermissionFormAsync(TransMasterEditAddDTO permissionFormDTO)
        {
            permissionFormDTO.DocDate = DateTime.Now;
            permissionFormDTO.FYear = DateTime.Now.Year;
            permissionFormDTO.FMonth = DateTime.Now.Month;
            permissionFormDTO.Flag2 = "0";
            permissionFormDTO.AmountVisa = 0;
            permissionFormDTO.CashAmount = 0;
            permissionFormDTO.SaleStatus = "N";
            permissionFormDTO.AddPers = 0;
            permissionFormDTO.InvType = "0";
            permissionFormDTO.TransConfirm = 0;
            permissionFormDTO.ShowRow = 3;

            var filteredPermissions = await _DbSet.Where(obj => obj.TransType == permissionFormDTO.TransType
                && obj.StockCode == permissionFormDTO.StockCode && obj.BranchId ==
                permissionFormDTO.BranchId && obj.FYear == permissionFormDTO.FYear)
                .ToListAsync();
            permissionFormDTO.DocNo = filteredPermissions.Count != 0 ? filteredPermissions.Max(obj => obj.DocNo) + 1 : 1;
            permissionFormDTO.SerSys = filteredPermissions.Count != 0 ? filteredPermissions.Max(obj => obj.SerSys) + 1 : 1;

            var permissionForm = _mapper.Map<TransMaster>(permissionFormDTO);
            permissionForm.EntryDate = DateTime.Now;

            await AddAsync(permissionForm);
            await SaveChangesAsync();
            return permissionForm;
        }

        public async Task UpdateTransMasterAsync(int id, TransMasterEditAddDTO permissionFormDTO)
        {
            permissionFormDTO.Flag2 = "0";
            permissionFormDTO.AmountVisa = 0;
            permissionFormDTO.CashAmount = 0;
            permissionFormDTO.SaleStatus = "N";
            permissionFormDTO.AddPers = 0;
            permissionFormDTO.InvType = "0";
            permissionFormDTO.TransConfirm = 0;
            permissionFormDTO.ShowRow = 3;

            TransMaster permissionForm = await GetByIdAsync(id);
            permissionFormDTO.DocNo = permissionForm.DocNo;
            permissionFormDTO.SerSys = permissionForm.SerSys;

            _mapper.Map(permissionFormDTO, permissionForm);
            permissionForm.ModifyDate = DateTime.Now;

            await UpdateAsync(permissionForm);
            await SaveChangesAsync();
        }

        //////////////////////////////////////////////////
        // Disburse or Convert Permission => اذن صرف او تحويل

        public async Task<TransMasterEditAddDTO> GetDisburseOrConvertFormByIdAsync(int transMasterID)
        {
            TransMaster permissionForm = await GetByIdAsync(transMasterID);
            var permissionFormDTO = _mapper.Map<TransMasterEditAddDTO>(permissionForm);

            List<TransMaster> permissionForms = await _DbSet
                .Where(obj => obj.StockCode == permissionFormDTO.StockCode
                && obj.TransType == permissionFormDTO.TransType
                && obj.Flag3 == "1").ToListAsync();

            EisUserObject userObj = await _Context.EisUserObjects
                .FirstOrDefaultAsync(obj => obj.UserId == permissionFormDTO.UserCode &&
                obj.ObjectName == "w_stores_trans" && obj.DefultId == 1);

            permissionFormDTO.EnableModify = userObj != null;

            List<Stock> stocks = await _Context.Stocks.Where(obj => obj.Stkcod != permissionFormDTO.StockCode).ToListAsync();
            permissionFormDTO.Stocks = _mapper.Map<List<StockViewDTO>>(stocks);

            Stock stock = await _Context.Stocks.FindAsync((int)permissionFormDTO.StockCode);
            GeneralCode permission = await _Context.GeneralCodes
                .FirstOrDefaultAsync(obj => obj.UniqueType == permissionFormDTO.TransType);
            AppUser user = await _Context.Users.FirstOrDefaultAsync(obj => obj.UserCode ==
                permissionFormDTO.UserCode);

            permissionFormDTO.MonthName = CultureInfo.CurrentCulture.
                DateTimeFormat.GetMonthName((int)permissionFormDTO.FMonth);
            permissionFormDTO.PermissionsCount = permissionForms.Count();
            permissionFormDTO.StockName = stock?.Stknam;
            permissionFormDTO.PermissionName = permission?.GDesc;
            permissionFormDTO.UserName = user?.UserName;

            UserJournalType userJournal = await _Context.UserJournalTypes
                .FirstOrDefaultAsync(obj => obj.UserCode == permissionFormDTO.UserCode &&
                obj.BranchId == permissionFormDTO.BranchId);
            permissionFormDTO.JournalName = (await _Context.JournalTypes
                .FirstOrDefaultAsync(obj => obj.JournalCode == userJournal.JournalCode))
                .JournalName;

            return permissionFormDTO;
        }

        public async Task<TransMasterEditAddDTO> GetNewDisburseOrConvertFormAsync(int stockID, int userCode,
            int uniqueType, int branchID)
        {
            var permissionFormDTO = new TransMasterEditAddDTO();
            List<TransMaster> permissionForms = await _DbSet
                .Where(obj => obj.StockCode == stockID && obj.TransType == uniqueType
                && obj.Flag3 == "1").ToListAsync();

            EisUserObject userObj = await _Context.EisUserObjects
                .FirstOrDefaultAsync(obj => obj.UserId == userCode &&
                obj.ObjectName == "w_stores_trans" && obj.DefultId == 1);

            permissionFormDTO.EnableModify = userObj != null;

            List<Stock> stocks = await _Context.Stocks.Where(obj => obj.Stkcod != stockID).ToListAsync();
            permissionFormDTO.Stocks = _mapper.Map<List<StockViewDTO>>(stocks);

            Stock stock = await _Context.Stocks.FindAsync(stockID);
            permissionFormDTO.StockCode = (short)stockID;
            GeneralCode permission = await _Context.GeneralCodes
                .FirstOrDefaultAsync(obj => obj.UniqueType == uniqueType);
            permissionFormDTO.TransType = uniqueType;

            permissionFormDTO.DocDate = DateTime.Now;
            permissionFormDTO.FYear = DateTime.Now.Year;
            permissionFormDTO.MonthName = DateTime.Now.ToString("MMMM");

            var filteredPermissions = await _DbSet.Where(obj => obj.TransType == uniqueType
                && obj.StockCode == stockID && obj.BranchId == branchID
                && obj.FYear == permissionFormDTO.FYear)
                .ToListAsync();
            permissionFormDTO.DocNo = filteredPermissions.Count != 0 ?
                filteredPermissions.Max(obj => obj.DocNo) + 1 : 1;

            permissionFormDTO.PermissionsCount = permissionForms.Count();
            permissionFormDTO.StockName = stock?.Stknam;
            permissionFormDTO.PermissionName = permission?.GDesc;

            UserJournalType userJournal = await _Context.UserJournalTypes
                .FirstOrDefaultAsync(obj => obj.UserCode == userCode && obj.BranchId == branchID);
            permissionFormDTO.JournalName = (await _Context.JournalTypes
                .FirstOrDefaultAsync(obj => obj.JournalCode == userJournal.JournalCode))
                .JournalName;

            return permissionFormDTO;
        }

        public async Task<TransMaster> AddDisburseOrConvertFormAsync(TransMasterEditAddDTO permissionFormDTO)
        {
            permissionFormDTO.DocDate = DateTime.Now;
            permissionFormDTO.FYear = DateTime.Now.Year;
            permissionFormDTO.FMonth = DateTime.Now.Month;
            permissionFormDTO.RefDocNo = "0";
            permissionFormDTO.TotTransVal = 0;
            permissionFormDTO.Descount = 0;
            permissionFormDTO.CustDisc1 = 0;
            permissionFormDTO.StatusBal = "R";
            permissionFormDTO.DiscPers = 0;
            permissionFormDTO.Flag = "1";
            permissionFormDTO.SaleStatus = "N";
            permissionFormDTO.Flag2 = "0";
            permissionFormDTO.AmountVisa = 0;
            permissionFormDTO.CustDisc2 = 0;
            permissionFormDTO.CustDisc4 = 0;
            permissionFormDTO.TaxPrc = 0;
            permissionFormDTO.TaxValue = 0;
            permissionFormDTO.DueValue = 0;
            permissionFormDTO.InvNo = 0;
            permissionFormDTO.Flag3 = "1";
            permissionFormDTO.InvType = "0";
            permissionFormDTO.ShowRow = 3;

            var filteredPermissions = await _DbSet.Where(obj => obj.TransType == permissionFormDTO.TransType
                && obj.StockCode == permissionFormDTO.StockCode && obj.BranchId ==
                permissionFormDTO.BranchId && obj.FYear == permissionFormDTO.FYear)
                .ToListAsync();
            permissionFormDTO.SerSys = filteredPermissions.Count != 0 ? filteredPermissions.Max(obj => obj.SerSys) + 1 : 1;

            var permissionForm = _mapper.Map<TransMaster>(permissionFormDTO);
            permissionForm.EntryDate = DateTime.Now;

            //if It Is a Convert Permission
            if (permissionForm.TransType == 13)
            {
                var ReceivePermissionForm = _mapper.Map<TransMaster>(permissionFormDTO);
                ReceivePermissionForm.TransType = 23;
                ReceivePermissionForm.AddPers = 10;
                ReceivePermissionForm.CustDisc5 = 0;
                ReceivePermissionForm.CashAmount = 0;
                ReceivePermissionForm.DocNoFr = permissionForm.DocNo;
                var temp = ReceivePermissionForm.StockCode2;
                ReceivePermissionForm.StockCode2 = ReceivePermissionForm.StockCode;
                ReceivePermissionForm.StockCode = temp;

                var filteredPermissions2 = await _DbSet.Where(obj => obj.TransType == ReceivePermissionForm.TransType
                    && obj.StockCode == ReceivePermissionForm.StockCode &&
                    obj.BranchId == ReceivePermissionForm.BranchId && obj.FYear == ReceivePermissionForm.FYear)
                    .ToListAsync();
                ReceivePermissionForm.DocNo = filteredPermissions2.Count != 0 ?
                    filteredPermissions2.Max(obj => obj.DocNo) + 1 : 1;
                ReceivePermissionForm.SerSys = filteredPermissions2.Count != 0 ?
                    filteredPermissions2.Max(obj => obj.SerSys) + 1 : 1;

                await AddAsync(ReceivePermissionForm);
            }

            await AddAsync(permissionForm);
            await SaveChangesAsync();
            return permissionForm;
        }

        public async Task UpdateDisburseOrConvertFormAsync(int id, TransMasterEditAddDTO permissionFormDTO)
        {
            permissionFormDTO.RefDocNo = "0";
            permissionFormDTO.TotTransVal = 0;
            permissionFormDTO.Descount = 0;
            permissionFormDTO.CustDisc1 = 0;
            permissionFormDTO.StatusBal = "R";
            permissionFormDTO.DiscPers = 0;
            permissionFormDTO.Flag = "1";
            permissionFormDTO.SaleStatus = "N";
            permissionFormDTO.Flag2 = "0";
            permissionFormDTO.AmountVisa = 0;
            permissionFormDTO.CustDisc2 = 0;
            permissionFormDTO.CustDisc4 = 0;
            permissionFormDTO.TaxPrc = 0;
            permissionFormDTO.TaxValue = 0;
            permissionFormDTO.DueValue = 0;
            permissionFormDTO.InvNo = 0;
            permissionFormDTO.Flag3 = "1";
            permissionFormDTO.InvType = "0";
            permissionFormDTO.ShowRow = 3;

            TransMaster permissionForm = await GetByIdAsync(id);

            TransMaster receiveTransMaster = null;
            if (permissionForm.TransType == 13)
            {
                receiveTransMaster = await _Context.TransMasters
                    .FirstOrDefaultAsync(obj => obj.TransType == 23 &&
                    obj.DocNoFr == permissionForm.DocNo && obj.StockCode == permissionForm.StockCode2);
                receiveTransMaster.StockCode = permissionFormDTO.StockCode2;
                receiveTransMaster.ModifyDate = DateTime.Now;
                await UpdateAsync(receiveTransMaster);
            }
            _mapper.Map(permissionFormDTO, permissionForm);
            permissionForm.ModifyDate = DateTime.Now;

            await UpdateAsync(permissionForm);
            await SaveChangesAsync();
        }

        //////////////////////////////////////////////////
        // Stock Receive Permission => اذن استلام من مخزن

        public async Task<TransMasterEditAddDTO> GetStockReceiveFormByIdAsync(int transMasterID)
        {
            TransMaster permissionForm = await GetByIdAsync(transMasterID);
            var permissionFormDTO = _mapper.Map<TransMasterEditAddDTO>(permissionForm);

            List<TransMaster> permissionForms = await _DbSet
                .Where(obj => obj.StockCode == permissionFormDTO.StockCode
                && obj.TransType == permissionFormDTO.TransType
                && obj.Flag3 == "1").ToListAsync();

            EisUserObject userObj = await _Context.EisUserObjects
                .FirstOrDefaultAsync(obj => obj.UserId == permissionFormDTO.UserCode &&
                obj.ObjectName == "w_stores_trans" && obj.DefultId == 1);

            permissionFormDTO.EnableModify = userObj != null;

            List<Stock> stocks = await _Context.Stocks.Where(obj => obj.Stkcod != permissionFormDTO.StockCode).ToListAsync();
            permissionFormDTO.Stocks = _mapper.Map<List<StockViewDTO>>(stocks);

            Stock stock = await _Context.Stocks.FindAsync((int)permissionFormDTO.StockCode);
            GeneralCode permission = await _Context.GeneralCodes
                .FirstOrDefaultAsync(obj => obj.UniqueType == permissionFormDTO.TransType);
            AppUser user = await _Context.Users.FirstOrDefaultAsync(obj => obj.UserCode ==
                permissionFormDTO.UserCode);

            permissionFormDTO.MonthName = CultureInfo.CurrentCulture.
                DateTimeFormat.GetMonthName((int)permissionFormDTO.FMonth);
            permissionFormDTO.PermissionsCount = permissionForms.Count();
            permissionFormDTO.StockName = stock?.Stknam;
            permissionFormDTO.PermissionName = permission?.GDesc;
            permissionFormDTO.UserName = user?.UserName;

            UserJournalType userJournal = await _Context.UserJournalTypes
                .FirstOrDefaultAsync(obj => obj.UserCode == permissionFormDTO.UserCode &&
                obj.BranchId == permissionFormDTO.BranchId);
            permissionFormDTO.JournalName = (await _Context.JournalTypes
                .FirstOrDefaultAsync(obj => obj.JournalCode == userJournal.JournalCode))
                .JournalName;

            return permissionFormDTO;
        }

        public async Task UpdateStockReceiveFormAsync(int id, TransMasterEditAddDTO permissionFormDTO)
        {
            permissionFormDTO.RefDocNo = "0";
            permissionFormDTO.TotTransVal = 0;
            permissionFormDTO.Descount = 0;
            permissionFormDTO.CustDisc1 = 0;
            permissionFormDTO.StatusBal = "R";
            permissionFormDTO.DiscPers = 0;
            permissionFormDTO.Flag = "1";
            permissionFormDTO.SaleStatus = "N";
            permissionFormDTO.Flag2 = "0";
            permissionFormDTO.AmountVisa = 0;
            permissionFormDTO.CustDisc2 = 0;
            permissionFormDTO.CustDisc4 = 0;
            permissionFormDTO.TaxPrc = 0;
            permissionFormDTO.TaxValue = 0;
            permissionFormDTO.DueValue = 0;
            permissionFormDTO.InvNo = 0;
            permissionFormDTO.Flag3 = "1";
            permissionFormDTO.InvType = "0";
            permissionFormDTO.ShowRow = 3;
            permissionFormDTO.CustDisc5 = 0;
            permissionFormDTO.AddPers = 10;
            permissionFormDTO.CashAmount = 0;

            TransMaster permissionForm = await GetByIdAsync(id);
            _mapper.Map(permissionFormDTO, permissionForm);
            permissionForm.ModifyDate = DateTime.Now;

            await UpdateAsync(permissionForm);
            await SaveChangesAsync();
        }

        //////////////////////////////////////////////////
        // Sales Invoice => فاتورة مبيعات

        public async Task<TransMasterEditAddDTO> GetSalesInvoiceByIdAsync(int transMasterID)
        {
            TransMaster permissionForm = await GetByIdAsync(transMasterID);
            var permissionFormDTO = _mapper.Map<TransMasterEditAddDTO>(permissionForm);

            List<TransMaster> permissionForms = await _DbSet
                .Where(obj => obj.StockCode == permissionFormDTO.StockCode
                && obj.TransType == permissionFormDTO.TransType
                && obj.Flag3 == "1").ToListAsync();

            EisUserObject userObj = await _Context.EisUserObjects
                .FirstOrDefaultAsync(obj => obj.UserId == permissionFormDTO.UserCode &&
                obj.ObjectName == "w_stores_trans" && obj.DefultId == 1);

            permissionFormDTO.EnableModify = userObj != null;

            List<SafeName> receivers = await _Context.SafeNames.ToListAsync();
            permissionFormDTO.Receivers = _mapper.Map<List<TreasuryNameViewDTO>>(receivers);

            List<CustCode> customers = await _Context.CustCodes.ToListAsync();
            permissionFormDTO.Customers = _mapper.Map<List<CustCodeViewDTO>>(customers);

            List<SalesmenDatum> salesMen = await _Context.SalesmenData.ToListAsync();
            permissionFormDTO.SalesMen = _mapper.Map<List<SalesmenDataViewDTO>>(salesMen);

            Stock stock = await _Context.Stocks.FindAsync((int)permissionFormDTO.StockCode);
            GeneralCode permission = await _Context.GeneralCodes
                .FirstOrDefaultAsync(obj => obj.UniqueType == permissionFormDTO.TransType);
            AppUser user = await _Context.Users.FirstOrDefaultAsync(obj => obj.UserCode ==
                permissionFormDTO.UserCode);

            permissionFormDTO.MonthName = CultureInfo.CurrentCulture.
                DateTimeFormat.GetMonthName((int)permissionFormDTO.FMonth);
            permissionFormDTO.PermissionsCount = permissionForms.Count();
            permissionFormDTO.StockName = stock?.Stknam;
            permissionFormDTO.PermissionName = permission?.GDesc;
            permissionFormDTO.UserName = user?.UserName;

            UserJournalType userJournal = await _Context.UserJournalTypes
                .FirstOrDefaultAsync(obj => obj.UserCode == permissionFormDTO.UserCode &&
                obj.BranchId == permissionFormDTO.BranchId);
            permissionFormDTO.JournalName = (await _Context.JournalTypes
                .FirstOrDefaultAsync(obj => obj.JournalCode == userJournal.JournalCode))
                .JournalName;

            return permissionFormDTO;
        }

        public async Task<TransMasterEditAddDTO> GetNewSalesInvoiceAsync(int stockID, int userCode,
            int uniqueType, int branchID)
        {
            var permissionFormDTO = new TransMasterEditAddDTO();
            List<TransMaster> permissionForms = await _DbSet
                .Where(obj => obj.StockCode == stockID && obj.TransType == uniqueType
                && obj.Flag3 == "1").ToListAsync();

            EisUserObject userObj = await _Context.EisUserObjects
                .FirstOrDefaultAsync(obj => obj.UserId == userCode &&
                obj.ObjectName == "w_stores_trans" && obj.DefultId == 1);

            permissionFormDTO.EnableModify = userObj != null;

            List<SafeName> receivers = await _Context.SafeNames.ToListAsync();
            permissionFormDTO.Receivers = _mapper.Map<List<TreasuryNameViewDTO>>(receivers);

            List<CustCode> customers = await _Context.CustCodes.ToListAsync();
            permissionFormDTO.Customers = _mapper.Map<List<CustCodeViewDTO>>(customers);

            List<SalesmenDatum> salesMen = await _Context.SalesmenData.ToListAsync();
            permissionFormDTO.SalesMen = _mapper.Map<List<SalesmenDataViewDTO>>(salesMen);

            Stock stock = await _Context.Stocks.FindAsync(stockID);
            permissionFormDTO.StockCode = (short)stockID;
            GeneralCode permission = await _Context.GeneralCodes
                .FirstOrDefaultAsync(obj => obj.UniqueType == uniqueType);
            permissionFormDTO.TransType = uniqueType;

            permissionFormDTO.DocDate = DateTime.Now;
            permissionFormDTO.FYear = DateTime.Now.Year;
            permissionFormDTO.MonthName = DateTime.Now.ToString("MMMM");

            var filteredPermissions = await _DbSet.Where(obj => obj.TransType == uniqueType
                && obj.StockCode == stockID && obj.BranchId == branchID
                && obj.FYear == permissionFormDTO.FYear)
                .ToListAsync();
            permissionFormDTO.DocNo = filteredPermissions.Count != 0 ? filteredPermissions.Max(obj => obj.DocNo) + 1 : 1;

            permissionFormDTO.PermissionsCount = permissionForms.Count();
            permissionFormDTO.StockName = stock?.Stknam;
            permissionFormDTO.PermissionName = permission?.GDesc;

            UserJournalType userJournal = await _Context.UserJournalTypes
                .FirstOrDefaultAsync(obj => obj.UserCode == userCode && obj.BranchId == branchID);
            permissionFormDTO.JournalName = (await _Context.JournalTypes
                .FirstOrDefaultAsync(obj => obj.JournalCode == userJournal.JournalCode))
                .JournalName;

            return permissionFormDTO;
        }

        public async Task<TransMaster> AddSalesInvoiceAsync(TransMasterEditAddDTO permissionFormDTO)
        {
            permissionFormDTO.DocDate = DateTime.Now;
            permissionFormDTO.FYear = DateTime.Now.Year;
            permissionFormDTO.FMonth = DateTime.Now.Month;
            permissionFormDTO.RefDocNo = "0";
            permissionFormDTO.TotTransVal = 0;
            permissionFormDTO.StatusBal = "R";
            permissionFormDTO.Flag = "1";
            permissionFormDTO.Flag2 = "0";
            permissionFormDTO.AmountVisa = 0;
            permissionFormDTO.CashAmount = 0;
            permissionFormDTO.SaleStatus = "N";
            permissionFormDTO.AddPers = 10;
            permissionFormDTO.CustDisc1 = 0;
            permissionFormDTO.CustDisc2 = 0;
            permissionFormDTO.CustDisc4 = 0;
            permissionFormDTO.CustDisc5 = 0;
            permissionFormDTO.DueValue = 0;
            permissionFormDTO.InvType = permissionFormDTO.InvType == "1" ? permissionFormDTO.InvType : "0";
            permissionFormDTO.PriceIncTax = permissionFormDTO.PriceIncTax == 1 ? permissionFormDTO.PriceIncTax : 0;
            permissionFormDTO.ShowRow = 3;

            var filteredPermissions = await _DbSet.Where(obj => obj.TransType == permissionFormDTO.TransType
                && obj.StockCode == permissionFormDTO.StockCode && obj.BranchId ==
                permissionFormDTO.BranchId && obj.FYear == permissionFormDTO.FYear)
                .ToListAsync();
            permissionFormDTO.SerSys = filteredPermissions.Count != 0 ? filteredPermissions.Max(obj => obj.SerSys) + 1 : 1;

            var permissionForm = _mapper.Map<TransMaster>(permissionFormDTO);
            permissionForm.EntryDate = DateTime.Now;

            await AddAsync(permissionForm);
            await SaveChangesAsync();
            return permissionForm;
        }

        public async Task UpdateSalesInvoiceAsync(int id, TransMasterEditAddDTO permissionFormDTO)
        {
            permissionFormDTO.RefDocNo = "0";
            permissionFormDTO.TotTransVal = 0;
            permissionFormDTO.StatusBal = "R";
            permissionFormDTO.Flag = "1";
            permissionFormDTO.Flag2 = "0";
            permissionFormDTO.AmountVisa = 0;
            permissionFormDTO.CashAmount = 0;
            permissionFormDTO.SaleStatus = "N";
            permissionFormDTO.AddPers = 10;
            permissionFormDTO.CustDisc1 = 0;
            permissionFormDTO.CustDisc2 = 0;
            permissionFormDTO.CustDisc4 = 0;
            permissionFormDTO.CustDisc5 = 0;
            permissionFormDTO.DueValue = 0;
            permissionFormDTO.InvType = permissionFormDTO.InvType == "1" ? permissionFormDTO.InvType : "0";
            permissionFormDTO.PriceIncTax = permissionFormDTO.PriceIncTax == 1 ? permissionFormDTO.PriceIncTax : 0;
            permissionFormDTO.ShowRow = 3;

            TransMaster permissionForm = await GetByIdAsync(id);
            _mapper.Map(permissionFormDTO, permissionForm);
            permissionForm.ModifyDate = DateTime.Now;

            await UpdateAsync(permissionForm);
            await SaveChangesAsync();
        }

        //////////////////////////////////////////////////
        // Debit or Credit Settlement => تسوية مدينةاو دائنة

        public async Task<TransMasterEditAddDTO> GetSettlementByIdAsync(int transMasterID)
        {
            TransMaster permissionForm = await GetByIdAsync(transMasterID);
            var permissionFormDTO = _mapper.Map<TransMasterEditAddDTO>(permissionForm);

            List<TransMaster> permissionForms = await _DbSet
                .Where(obj => obj.StockCode == permissionFormDTO.StockCode
                && obj.TransType == permissionFormDTO.TransType
                && obj.Flag3 == "1").ToListAsync();

            EisUserObject userObj = await _Context.EisUserObjects
                .FirstOrDefaultAsync(obj => obj.UserId == permissionFormDTO.UserCode &&
                obj.ObjectName == "w_stores_trans" && obj.DefultId == 1);

            permissionFormDTO.EnableModify = userObj != null;

            List<AppUser> users = await _Context.Users.ToListAsync();
            permissionFormDTO.Users = _mapper.Map<List<UserDTO>>(users);

            Stock stock = await _Context.Stocks.FindAsync((int)permissionFormDTO.StockCode);
            GeneralCode permission = await _Context.GeneralCodes
                .FirstOrDefaultAsync(obj => obj.UniqueType == permissionFormDTO.TransType);
            AppUser user = await _Context.Users.FirstOrDefaultAsync(obj => obj.UserCode ==
                permissionFormDTO.UserCode);

            permissionFormDTO.MonthName = CultureInfo.CurrentCulture.
                DateTimeFormat.GetMonthName((int)permissionFormDTO.FMonth);
            permissionFormDTO.PermissionsCount = permissionForms.Count();
            permissionFormDTO.StockName = stock?.Stknam;
            permissionFormDTO.PermissionName = permission?.GDesc;
            permissionFormDTO.UserName = user?.UserName;

            UserJournalType userJournal = await _Context.UserJournalTypes
                .FirstOrDefaultAsync(obj => obj.UserCode == permissionFormDTO.UserCode &&
                obj.BranchId == permissionFormDTO.BranchId);
            permissionFormDTO.JournalName = (await _Context.JournalTypes
                .FirstOrDefaultAsync(obj => obj.JournalCode == userJournal.JournalCode))
                .JournalName;

            return permissionFormDTO;
        }

        public async Task<TransMasterEditAddDTO> GetNewSettlementAsync(int stockID, int userCode,
            int uniqueType, int branchID)
        {
            var permissionFormDTO = new TransMasterEditAddDTO();
            List<TransMaster> permissionForms = await _DbSet
                .Where(obj => obj.StockCode == stockID && obj.TransType == uniqueType
                && obj.Flag3 == "1").ToListAsync();

            EisUserObject userObj = await _Context.EisUserObjects
                .FirstOrDefaultAsync(obj => obj.UserId == userCode &&
                obj.ObjectName == "w_stores_trans" && obj.DefultId == 1);

            permissionFormDTO.EnableModify = userObj != null;

            List<AppUser> users = await _Context.Users.ToListAsync();
            permissionFormDTO.Users = _mapper.Map<List<UserDTO>>(users);

            Stock stock = await _Context.Stocks.FindAsync(stockID);
            permissionFormDTO.StockCode = (short)stockID;
            GeneralCode permission = await _Context.GeneralCodes
                .FirstOrDefaultAsync(obj => obj.UniqueType == uniqueType);
            permissionFormDTO.TransType = uniqueType;

            permissionFormDTO.DocDate = DateTime.Now;
            permissionFormDTO.FYear = DateTime.Now.Year;
            permissionFormDTO.MonthName = DateTime.Now.ToString("MMMM");

            var filteredPermissions = await _DbSet.Where(obj => obj.TransType == uniqueType
                && obj.StockCode == stockID && obj.BranchId == branchID
                && obj.FYear == permissionFormDTO.FYear)
                .ToListAsync();
            permissionFormDTO.DocNo = filteredPermissions.Count != 0 ? filteredPermissions.Max(obj => obj.DocNo) + 1 : 1;

            permissionFormDTO.PermissionsCount = permissionForms.Count();
            permissionFormDTO.StockName = stock?.Stknam;
            permissionFormDTO.PermissionName = permission?.GDesc;

            UserJournalType userJournal = await _Context.UserJournalTypes
                .FirstOrDefaultAsync(obj => obj.UserCode == userCode && obj.BranchId == branchID);
            permissionFormDTO.JournalName = (await _Context.JournalTypes
                .FirstOrDefaultAsync(obj => obj.JournalCode == userJournal.JournalCode))
                .JournalName;

            return permissionFormDTO;
        }

        public async Task<TransMaster> AddSettlementAsync(TransMasterEditAddDTO permissionFormDTO)
        {
            permissionFormDTO.DocDate = DateTime.Now;
            permissionFormDTO.FYear = DateTime.Now.Year;
            permissionFormDTO.FMonth = DateTime.Now.Month;
            permissionFormDTO.RefDocNo = permissionFormDTO.RefDocNo is not (null or "") ?
                permissionFormDTO.RefDocNo : "0";
            permissionFormDTO.TotTransVal = 0;
            permissionFormDTO.Descount = 0;
            permissionFormDTO.StatusBal = "S";
            permissionFormDTO.Flag = "1";
            permissionFormDTO.Pay = "Y";
            permissionFormDTO.Flag2 = "0";
            permissionFormDTO.AmountVisa = 0;
            permissionFormDTO.CashAmount = permissionFormDTO.CashAmount is not (null or 0) ?
                permissionFormDTO.CashAmount : 0;

            permissionFormDTO.SaleStatus = "N";
            permissionFormDTO.AddPers = 10;
            permissionFormDTO.DiscPers = 0;
            permissionFormDTO.CustDisc1 = 0;
            permissionFormDTO.CustDisc2 = 0;
            permissionFormDTO.TaxPrc = 0;
            permissionFormDTO.TaxValue = 0;
            permissionFormDTO.DueValue = 0;
            permissionFormDTO.ShowRow = 3;

            var filteredPermissions = await _DbSet.Where(obj => obj.TransType == permissionFormDTO.TransType
                && obj.StockCode == permissionFormDTO.StockCode && obj.BranchId ==
                permissionFormDTO.BranchId && obj.FYear == permissionFormDTO.FYear)
                .ToListAsync();
            permissionFormDTO.SerSys = filteredPermissions.Count != 0 ? filteredPermissions.Max(obj => obj.SerSys) + 1 : 1;

            var permissionForm = _mapper.Map<TransMaster>(permissionFormDTO);
            permissionForm.EntryDate = DateTime.Now;

            await AddAsync(permissionForm);
            await SaveChangesAsync();
            return permissionForm;
        }

        public async Task UpdateSettlementAsync(int id, TransMasterEditAddDTO permissionFormDTO)
        {
            permissionFormDTO.RefDocNo = permissionFormDTO.RefDocNo is not (null or "") ?
                permissionFormDTO.RefDocNo : "0";
            permissionFormDTO.TotTransVal = 0;
            permissionFormDTO.Descount = 0;
            permissionFormDTO.StatusBal = "S";
            permissionFormDTO.Flag = "1";
            permissionFormDTO.Pay = "Y";
            permissionFormDTO.Flag2 = "0";
            permissionFormDTO.AmountVisa = 0;
            permissionFormDTO.CashAmount = permissionFormDTO.CashAmount is not (null or 0) ?
                permissionFormDTO.CashAmount : 0;

            permissionFormDTO.SaleStatus = "N";
            permissionFormDTO.AddPers = 10;
            permissionFormDTO.DiscPers = 0;
            permissionFormDTO.CustDisc1 = 0;
            permissionFormDTO.CustDisc2 = 0;
            permissionFormDTO.TaxPrc = 0;
            permissionFormDTO.TaxValue = 0;
            permissionFormDTO.DueValue = 0;
            permissionFormDTO.ShowRow = 3;

            TransMaster permissionForm = await GetByIdAsync(id);
            _mapper.Map(permissionFormDTO, permissionForm);
            permissionForm.ModifyDate = DateTime.Now;

            await UpdateAsync(permissionForm);
            await SaveChangesAsync();
        }

        //////////////////////////////////////////////////
        // Requirements Disburse Form => اذن صرف مستلزمات

        public async Task<TransMasterEditAddDTO> GetReqDisburseByIdAsync(int transMasterID)
        {
            TransMaster permissionForm = await GetByIdAsync(transMasterID);
            var permissionFormDTO = _mapper.Map<TransMasterEditAddDTO>(permissionForm);

            List<TransMaster> permissionForms = await _DbSet
                .Where(obj => obj.StockCode == permissionFormDTO.StockCode
                && obj.TransType == permissionFormDTO.TransType
                && obj.Flag3 == "1").ToListAsync();

            EisUserObject userObj = await _Context.EisUserObjects
                .FirstOrDefaultAsync(obj => obj.UserId == permissionFormDTO.UserCode &&
                obj.ObjectName == "w_stores_trans" && obj.DefultId == 1);

            permissionFormDTO.EnableModify = userObj != null;

            permissionFormDTO.Stocks = await GetActiveStocksForUserAsync((int)permissionFormDTO.UserCode);

            List<CustCode> customers = await _Context.CustCodes.ToListAsync();
            permissionFormDTO.Customers = _mapper.Map<List<CustCodeViewDTO>>(customers);

            List<Pat> patients = await _Context.Pats.ToListAsync();
            permissionFormDTO.Patients = _mapper.Map<List<PatViewDTO>>(patients);

            List<SafeName> receivers = await _Context.SafeNames.ToListAsync();
            permissionFormDTO.Receivers = _mapper.Map<List<TreasuryNameViewDTO>>(receivers);

            Stock stock = await _Context.Stocks.FindAsync((int)permissionFormDTO.StockCode);
            GeneralCode permission = await _Context.GeneralCodes
                .FirstOrDefaultAsync(obj => obj.UniqueType == permissionFormDTO.TransType);
            AppUser user = await _Context.Users.FirstOrDefaultAsync(obj => obj.UserCode ==
                permissionFormDTO.UserCode);
            
            permissionFormDTO.MonthName = CultureInfo.CurrentCulture.
                DateTimeFormat.GetMonthName((int)permissionFormDTO.FMonth);
            permissionFormDTO.PermissionsCount = permissionForms.Count();
            permissionFormDTO.StockName = stock?.Stknam;
            permissionFormDTO.PermissionName = permission?.GDesc;
            permissionFormDTO.UserName = user?.UserName;

            UserJournalType userJournal = await _Context.UserJournalTypes
                .FirstOrDefaultAsync(obj => obj.UserCode == permissionFormDTO.UserCode &&
                obj.BranchId == permissionFormDTO.BranchId);
            permissionFormDTO.JournalName = (await _Context.JournalTypes
                .FirstOrDefaultAsync(obj => obj.JournalCode == userJournal.JournalCode))
                .JournalName;

            return permissionFormDTO;
        }

        public async Task<TransMasterEditAddDTO> GetNewReqDisburseAsync(int stockID, int userCode,
            int uniqueType, int branchID)
        {
            var permissionFormDTO = new TransMasterEditAddDTO();
            List<TransMaster> permissionForms = await _DbSet
                .Where(obj => obj.StockCode == stockID && obj.TransType == uniqueType
                && obj.Flag3 == "1").ToListAsync();

            EisUserObject userObj = await _Context.EisUserObjects
                .FirstOrDefaultAsync(obj => obj.UserId == userCode &&
                obj.ObjectName == "w_stores_trans" && obj.DefultId == 1);

            permissionFormDTO.EnableModify = userObj != null;

            permissionFormDTO.Stocks = await GetActiveStocksForUserAsync(userCode);

            List<CustCode> customers = await _Context.CustCodes.ToListAsync();
            permissionFormDTO.Customers = _mapper.Map<List<CustCodeViewDTO>>(customers);

            List<Pat> patients = await _Context.Pats.ToListAsync();
            permissionFormDTO.Patients = _mapper.Map<List<PatViewDTO>>(patients);

            List<SafeName> receivers = await _Context.SafeNames.ToListAsync();
            permissionFormDTO.Receivers = _mapper.Map<List<TreasuryNameViewDTO>>(receivers);

            Stock stock = await _Context.Stocks.FindAsync(stockID);
            permissionFormDTO.StockCode = (short)stockID;
            GeneralCode permission = await _Context.GeneralCodes
                .FirstOrDefaultAsync(obj => obj.UniqueType == uniqueType);
            permissionFormDTO.TransType = uniqueType;

            permissionFormDTO.DocDate = DateTime.Now;
            permissionFormDTO.FYear = DateTime.Now.Year;
            permissionFormDTO.MonthName = DateTime.Now.ToString("MMMM");

            var filteredPermissions = await _DbSet.Where(obj => obj.TransType == uniqueType
                && obj.StockCode == stockID && obj.BranchId == branchID
                && obj.FYear == permissionFormDTO.FYear)
                .ToListAsync();
            permissionFormDTO.DocNo = filteredPermissions.Count != 0 ? filteredPermissions.Max(obj => obj.DocNo) + 1 : 1;

            permissionFormDTO.PermissionsCount = permissionForms.Count();
            permissionFormDTO.StockName = stock?.Stknam;
            permissionFormDTO.PermissionName = permission?.GDesc;

            UserJournalType userJournal = await _Context.UserJournalTypes
                .FirstOrDefaultAsync(obj => obj.UserCode == userCode && obj.BranchId == branchID);
            permissionFormDTO.JournalName = (await _Context.JournalTypes
                .FirstOrDefaultAsync(obj => obj.JournalCode == userJournal.JournalCode))
                .JournalName;

            return permissionFormDTO;
        }

        public async Task<TransMaster> AddReqDisburseAsync(TransMasterEditAddDTO permissionFormDTO)
        {
            permissionFormDTO.DocDate = DateTime.Now;
            permissionFormDTO.FYear = DateTime.Now.Year;
            permissionFormDTO.FMonth = DateTime.Now.Month;
            permissionFormDTO.TotTransVal = 0;
            permissionFormDTO.Descount = 0;
            permissionFormDTO.StatusBal = "S";
            permissionFormDTO.Flag = "1";
            permissionFormDTO.Pay = "Y";
            permissionFormDTO.Flag2 = "0";
            permissionFormDTO.AmountVisa = 0;
            permissionFormDTO.CashAmount = 0;
            permissionFormDTO.SaleStatus = "N";
            permissionFormDTO.AddPers = 10;
            permissionFormDTO.DiscPers = 0;
            permissionFormDTO.TaxPrc = 0;
            permissionFormDTO.TaxValue = 0;
            permissionFormDTO.DueValue = 0;
            permissionFormDTO.Flag3 = "1";

            var filteredPermissions = await _DbSet.Where(obj => obj.TransType == permissionFormDTO.TransType
                && obj.StockCode == permissionFormDTO.StockCode && obj.BranchId ==
                permissionFormDTO.BranchId && obj.FYear == permissionFormDTO.FYear)
                .ToListAsync();
            permissionFormDTO.SerSys = filteredPermissions.Count != 0 ? filteredPermissions.Max(obj => obj.SerSys) + 1 : 1;

            var permissionForm = _mapper.Map<TransMaster>(permissionFormDTO);
            permissionForm.EntryDate = DateTime.Now;

            await AddAsync(permissionForm);
            await SaveChangesAsync();
            return permissionForm;
        }

        public async Task UpdateReqDisburseAsync(int id, TransMasterEditAddDTO permissionFormDTO)
        {
            permissionFormDTO.TotTransVal = 0;
            permissionFormDTO.Descount = 0;
            permissionFormDTO.StatusBal = "S";
            permissionFormDTO.Flag = "1";
            permissionFormDTO.Pay = "Y";
            permissionFormDTO.Flag2 = "0";
            permissionFormDTO.AmountVisa = 0;
            permissionFormDTO.CashAmount = 0;
            permissionFormDTO.SaleStatus = "N";
            permissionFormDTO.AddPers = 10;
            permissionFormDTO.DiscPers = 0;
            permissionFormDTO.TaxPrc = 0;
            permissionFormDTO.TaxValue = 0;
            permissionFormDTO.DueValue = 0;
            permissionFormDTO.Flag3 = "1";

            TransMaster permissionForm = await GetByIdAsync(id);
            _mapper.Map(permissionFormDTO, permissionForm);
            permissionForm.ModifyDate = DateTime.Now;

            await UpdateAsync(permissionForm);
            await SaveChangesAsync();
        }

        //////////////////////////////////////////////////
        // Return Permission Form => اذن ارتجاع لمورد

        public async Task<TransMasterEditAddDTO> GetReturnPermissionByIdAsync(int transMasterID)
        {
            TransMaster permissionForm = await GetByIdAsync(transMasterID);
            var permissionFormDTO = _mapper.Map<TransMasterEditAddDTO>(permissionForm);

            List<TransMaster> permissionForms = await _DbSet
                .Where(obj => obj.StockCode == permissionFormDTO.StockCode
                && obj.TransType == permissionFormDTO.TransType
                && obj.Flag3 == "1").ToListAsync();

            EisUserObject userObj = await _Context.EisUserObjects
                .FirstOrDefaultAsync(obj => obj.UserId == permissionFormDTO.UserCode &&
                obj.ObjectName == "w_stores_trans" && obj.DefultId == 1);

            permissionFormDTO.EnableModify = userObj != null;

            List<SupCode> suppliers = await _Context.SupCodes.ToListAsync();
            permissionFormDTO.Suppliers = _mapper.Map<List<SupCodeViewDTO>>(suppliers);

            Stock stock = await _Context.Stocks.FindAsync((int)permissionFormDTO.StockCode);
            GeneralCode permission = await _Context.GeneralCodes
                .FirstOrDefaultAsync(obj => obj.UniqueType == permissionFormDTO.TransType);
            AppUser user = await _Context.Users.FirstOrDefaultAsync(obj => obj.UserCode ==
                permissionFormDTO.UserCode);

            permissionFormDTO.MonthName = CultureInfo.CurrentCulture.
                DateTimeFormat.GetMonthName((int)permissionFormDTO.FMonth);
            permissionFormDTO.PermissionsCount = permissionForms.Count();
            permissionFormDTO.StockName = stock?.Stknam;
            permissionFormDTO.PermissionName = permission?.GDesc;
            permissionFormDTO.UserName = user?.UserName;

            UserJournalType userJournal = await _Context.UserJournalTypes
                .FirstOrDefaultAsync(obj => obj.UserCode == permissionFormDTO.UserCode &&
                obj.BranchId == permissionFormDTO.BranchId);
            permissionFormDTO.JournalName = (await _Context.JournalTypes
                .FirstOrDefaultAsync(obj => obj.JournalCode == userJournal.JournalCode))
                .JournalName;

            return permissionFormDTO;
        }

        public async Task<TransMasterEditAddDTO> GetNewReturnPermissionAsync(int stockID, int userCode,
            int uniqueType, int branchID)
        {
            var permissionFormDTO = new TransMasterEditAddDTO();
            List<TransMaster> permissionForms = await _DbSet
                .Where(obj => obj.StockCode == stockID && obj.TransType == uniqueType
                && obj.Flag3 == "1").ToListAsync();

            EisUserObject userObj = await _Context.EisUserObjects
                .FirstOrDefaultAsync(obj => obj.UserId == userCode &&
                obj.ObjectName == "w_stores_trans" && obj.DefultId == 1);

            permissionFormDTO.EnableModify = userObj != null;

            List<SupCode> suppliers = await _Context.SupCodes.ToListAsync();
            permissionFormDTO.Suppliers = _mapper.Map<List<SupCodeViewDTO>>(suppliers);

            Stock stock = await _Context.Stocks.FindAsync(stockID);
            permissionFormDTO.StockCode = (short)stockID;
            GeneralCode permission = await _Context.GeneralCodes
                .FirstOrDefaultAsync(obj => obj.UniqueType == uniqueType);
            permissionFormDTO.TransType = uniqueType;

            permissionFormDTO.DocDate = DateTime.Now;
            permissionFormDTO.FYear = DateTime.Now.Year;
            permissionFormDTO.MonthName = DateTime.Now.ToString("MMMM");

            var filteredPermissions = await _DbSet.Where(obj => obj.TransType == uniqueType
                && obj.StockCode == stockID && obj.BranchId == branchID
                && obj.FYear == permissionFormDTO.FYear)
                .ToListAsync();
            permissionFormDTO.DocNo = filteredPermissions.Count != 0 ? filteredPermissions.Max(obj => obj.DocNo) + 1 : 1;

            permissionFormDTO.PermissionsCount = permissionForms.Count();
            permissionFormDTO.StockName = stock?.Stknam;
            permissionFormDTO.PermissionName = permission?.GDesc;

            UserJournalType userJournal = await _Context.UserJournalTypes
                .FirstOrDefaultAsync(obj => obj.UserCode == userCode && obj.BranchId == branchID);
            permissionFormDTO.JournalName = (await _Context.JournalTypes
                .FirstOrDefaultAsync(obj => obj.JournalCode == userJournal.JournalCode))
                .JournalName;

            return permissionFormDTO;
        }

        public async Task<TransMaster> AddReturnPermissionAsync(TransMasterEditAddDTO permissionFormDTO)
        {
            permissionFormDTO.DocDate = DateTime.Now;
            permissionFormDTO.FYear = DateTime.Now.Year;
            permissionFormDTO.FMonth = DateTime.Now.Month;
            permissionFormDTO.RefDocNo = "0";
            permissionFormDTO.TotTransVal = 0;
            permissionFormDTO.Descount = 0;
            permissionFormDTO.StatusBal = "R";
            permissionFormDTO.Flag = "1";
            permissionFormDTO.Flag2 = "0";
            permissionFormDTO.AmountVisa = 0;
            permissionFormDTO.CashAmount = 0;
            permissionFormDTO.SaleStatus = "N";
            permissionFormDTO.AddPers = 10;
            permissionFormDTO.DiscPers = 0;
            permissionFormDTO.CustDisc1 = 0;
            permissionFormDTO.CustDisc2 = 0;
            permissionFormDTO.CustDisc4 = 0;
            permissionFormDTO.CustDisc5 = 0;
            permissionFormDTO.TaxPrc = 0;
            permissionFormDTO.TaxValue = 0;
            permissionFormDTO.DueValue = 0;
            permissionFormDTO.InvNo = 0;
            permissionFormDTO.InvType = "0";
            permissionFormDTO.ShowRow = 3;

            var filteredPermissions = await _DbSet.Where(obj => obj.TransType == permissionFormDTO.TransType
                && obj.StockCode == permissionFormDTO.StockCode && obj.BranchId ==
                permissionFormDTO.BranchId && obj.FYear == permissionFormDTO.FYear)
                .ToListAsync();
            permissionFormDTO.SerSys = filteredPermissions.Count != 0 ? filteredPermissions.Max(obj => obj.SerSys) + 1 : 1;

            var permissionForm = _mapper.Map<TransMaster>(permissionFormDTO);
            permissionForm.EntryDate = DateTime.Now;

            await AddAsync(permissionForm);
            await SaveChangesAsync();
            return permissionForm;
        }

        public async Task UpdateReturnPermissionAsync(int id, TransMasterEditAddDTO permissionFormDTO)
        {
            permissionFormDTO.RefDocNo = "0";
            permissionFormDTO.TotTransVal = 0;
            permissionFormDTO.Descount = 0;
            permissionFormDTO.StatusBal = "R";
            permissionFormDTO.Flag = "1";
            permissionFormDTO.Flag2 = "0";
            permissionFormDTO.AmountVisa = 0;
            permissionFormDTO.CashAmount = 0;
            permissionFormDTO.SaleStatus = "N";
            permissionFormDTO.AddPers = 10;
            permissionFormDTO.DiscPers = 0;
            permissionFormDTO.CustDisc1 = 0;
            permissionFormDTO.CustDisc2 = 0;
            permissionFormDTO.CustDisc4 = 0;
            permissionFormDTO.CustDisc5 = 0;
            permissionFormDTO.TaxPrc = 0;
            permissionFormDTO.TaxValue = 0;
            permissionFormDTO.DueValue = 0;
            permissionFormDTO.InvNo = 0;
            permissionFormDTO.InvType = "0";
            permissionFormDTO.ShowRow = 3;

            TransMaster permissionForm = await GetByIdAsync(id);
            _mapper.Map(permissionFormDTO, permissionForm);
            permissionForm.ModifyDate = DateTime.Now;

            await UpdateAsync(permissionForm);
            await SaveChangesAsync();
        }
    }
}
