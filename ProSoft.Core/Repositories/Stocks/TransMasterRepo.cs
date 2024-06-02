using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProSoft.EF.DbContext;
using ProSoft.EF.DTOs.Shared;
using ProSoft.EF.DTOs.Stocks;
using ProSoft.EF.IRepositories.Stocks;
using ProSoft.EF.Models;
using ProSoft.EF.Models.Shared;
using ProSoft.EF.Models.Stocks;
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
    public class TransMasterRepo: Repository<TransMaster, int>, ITransMasterRepo
    {
        private readonly IMapper _mapper;
        private readonly IUserTransRepo _userTransRepo;
        public TransMasterRepo(AppDbContext Context, IMapper mapper,
            IUserTransRepo userTransRepo) : base(Context)
        {
            _mapper = mapper;
            _userTransRepo = userTransRepo;
        }

        public async Task<TransMasterEditAddDTO> GetTransMasterByIdAsync(int transMasterID)
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
            GeneralCode permission = await _Context.GeneralCodes.FindAsync(permissionFormDTO.TransType);
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

        public async Task<TransMasterViewDTO> GetForViewAsync(TransMaster permissionForm)
        {
            if(permissionForm != null)
            {
                var permFormDTO = _mapper.Map<TransMasterViewDTO>(permissionForm);
                permFormDTO.PermissionName = (await _Context.GeneralCodes
                    .FindAsync(permissionForm.TransType)).GDesc;
                permFormDTO.StockName = (await _Context.Stocks
                    .FirstOrDefaultAsync(obj => obj.Stkcod == permissionForm.StockCode)).Stknam;
                permFormDTO.UserName = (await _Context.Users
                    .FirstOrDefaultAsync(obj => obj.UserCode == permissionForm.UserCode)).UserName;
                if(permissionForm.SupNo != null)
                {
                    permFormDTO.SupplierName = (await _Context.SupCodes
                        .FirstOrDefaultAsync(obj => obj.SupCode1 == permissionForm.SupNo)).SupName;
                }
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
                permissionsFormsDTO.Add(permFormDTO);
            }
            return permissionsFormsDTO;
        }

        // Add Permission Form
        public async Task<TransMasterEditAddDTO> GetNewTransMasterAsync(int stockID, int userCode, int permissionID)
        {
            var permissionFormDTO = new TransMasterEditAddDTO();
            List<TransMaster> permissionForms = await _DbSet
                .Where(obj => obj.StockCode == stockID && obj.TransType == permissionID
                && obj.Flag3 == "1").ToListAsync();

            EisUserObject userObj = await _Context.EisUserObjects
                .FirstOrDefaultAsync(obj => obj.UserId == userCode &&
                obj.ObjectName == "w_stores_trans" && obj.DefultId == 1);

            permissionFormDTO.EnableModify = userObj != null;

            List<SupCode> suppliers = await _Context.SupCodes.ToListAsync();
            permissionFormDTO.Suppliers = _mapper.Map<List<SupCodeViewDTO>>(suppliers);

            Stock stock = await _Context.Stocks.FindAsync(stockID);
            permissionFormDTO.StockCode = (short)stockID;
            GeneralCode permission = await _Context.GeneralCodes.FindAsync(permissionID);
            permissionFormDTO.TransType = permissionID;

            permissionFormDTO.DocDate = DateTime.UtcNow;
            permissionFormDTO.FYear = DateTime.UtcNow.Year;
            permissionFormDTO.MonthName = DateTime.UtcNow.ToString("MMMM");
            permissionFormDTO.PermissionsCount = permissionForms.Count();
            permissionFormDTO.StockName = stock?.Stknam;
            permissionFormDTO.PermissionName = permission?.GDesc;
            return permissionFormDTO;
        }

        public async Task<TransMaster> AddPermissionFormAsync(TransMasterEditAddDTO permissionFormDTO)
        {
            permissionFormDTO.FYear = DateTime.UtcNow.Year;
            permissionFormDTO.FMonth = DateTime.UtcNow.Month;
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

            await UpdateAsync(permissionForm);
            await SaveChangesAsync();
        }

        //////////////////////////////////////////////////
        // Add Disburse Form => اذن صرف
        public async Task<TransMasterEditAddDTO> GetNewDisburseFormAsync(int stockID, int userCode, int permissionID, int branchID)
        {
            var permissionFormDTO = new TransMasterEditAddDTO();
            List<TransMaster> permissionForms = await _DbSet
                .Where(obj => obj.StockCode == stockID && obj.TransType == permissionID
                && obj.Flag3 == "1").ToListAsync();

            EisUserObject userObj = await _Context.EisUserObjects
                .FirstOrDefaultAsync(obj => obj.UserId == userCode &&
                obj.ObjectName == "w_stores_trans" && obj.DefultId == 1);

            permissionFormDTO.EnableModify = userObj != null;

            List<Stock> stocks = await _Context.Stocks.ToListAsync();
            permissionFormDTO.Stocks = _mapper.Map<List<StockViewDTO>>(stocks);

            Stock stock = await _Context.Stocks.FindAsync(stockID);
            permissionFormDTO.StockCode = (short)stockID;
            GeneralCode permission = await _Context.GeneralCodes.FindAsync(permissionID);
            permissionFormDTO.TransType = permissionID;

            permissionFormDTO.DocDate = DateTime.UtcNow;
            permissionFormDTO.FYear = DateTime.UtcNow.Year;
            permissionFormDTO.MonthName = DateTime.UtcNow.ToString("MMMM");

            var filteredPermissions = await _DbSet.Where(obj => obj.TransType == permissionID
                && obj.StockCode == stockID && obj.BranchId == branchID
                && obj.FYear == permissionFormDTO.FYear)
                .ToListAsync();
            permissionFormDTO.DocNo = filteredPermissions.Count != 0 ? filteredPermissions.Max(obj => obj.DocNo) + 1 : 1;

            permissionFormDTO.PermissionsCount = permissionForms.Count();
            permissionFormDTO.StockName = stock?.Stknam;
            permissionFormDTO.PermissionName = permission?.GDesc;
            return permissionFormDTO;
        }

        public async Task<TransMaster> AddDisburseFormAsync(TransMasterEditAddDTO permissionFormDTO)
        {
            permissionFormDTO.FYear = DateTime.UtcNow.Year;
            permissionFormDTO.FMonth = DateTime.UtcNow.Month;
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
            //permissionFormDTO.DocNo = filteredPermissions.Count != 0 ? filteredPermissions.Max(obj => obj.DocNo) + 1 : 1;
            permissionFormDTO.SerSys = filteredPermissions.Count != 0 ? filteredPermissions.Max(obj => obj.SerSys) + 1 : 1;

            var permissionForm = _mapper.Map<TransMaster>(permissionFormDTO);
            await AddAsync(permissionForm);
            await SaveChangesAsync();
            return permissionForm;
        }
    }
}
