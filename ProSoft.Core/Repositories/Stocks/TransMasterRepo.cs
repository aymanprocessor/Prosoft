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
                return permFormDTO;
            }
            return null;
        }

        public async Task<List<TransMasterViewDTO>> GetPermissionsFormsAsync(int stockID, int transType)
        {
            List<TransMaster> permissionsForms = await _DbSet.Where(obj => obj.StockCode == stockID
                && obj.TransType == transType && obj.Flag3 == "1").ToListAsync();
            var permissionsFormsDTO = new List<TransMasterViewDTO>(); //_mapper.Map<List<TransMasterViewDTO>>(permissionsForms);
            foreach (var item in permissionsForms)
            {
                TransMasterViewDTO permFormDTO = await GetForViewAsync(item);
                permissionsFormsDTO.Add(permFormDTO);
            }
            return permissionsFormsDTO;
        }

        public async Task<TransMasterEditAddDTO> GetNewTransMasterAsync(int stockID, int permissionID)
        {
            var permissionFormDTO = new TransMasterEditAddDTO();
            List<TransMaster> permissionForms = await _DbSet
                .Where(obj => obj.StockCode == stockID && obj.TransType == permissionID
                && obj.Flag3 == "1").ToListAsync();

            List<SupCode> suppliers = await _Context.SupCodes.ToListAsync();
            permissionFormDTO.Suppliers = _mapper.Map<List<SupCodeViewDTO>>(suppliers);

            Stock stock = await _Context.Stocks.FindAsync(stockID);
            permissionFormDTO.StockCode = (short)stockID;
            GeneralCode permission = await _Context.GeneralCodes.FindAsync(permissionID);
            permissionFormDTO.TransType = permissionID;

            permissionFormDTO.FYear = DateTime.Now.Year;
            permissionFormDTO.MonthName = DateTime.Now.ToString("MMMM");
            permissionFormDTO.PermissionsCount = permissionForms.Count();
            permissionFormDTO.StockName = stock?.Stknam;
            permissionFormDTO.PermissionName = permission?.GDesc;
            return permissionFormDTO;
        }

        //private async Task<object> GetDTOWithDefaultsAsync(int stockID, int permissionID)
        //{
        //    List<TransMaster> permissionForms = await _DbSet
        //        .Where(obj => obj.StockCode == stockID && obj.TransType == permissionID
        //        && obj.Flag3 == "1").ToListAsync();

        //    List<SupCode> suppliers = await _Context.SupCodes.ToListAsync();
        //    //permissionFormDTO.Suppliers = _mapper.Map<List<SupCodeViewDTO>>(suppliers);

        //    Stock stock = await _Context.Stocks.FindAsync(stockID);
        //    //permissionFormDTO.StockCode = (short)stockID;
        //    GeneralCode permission = await _Context.GeneralCodes.FindAsync(permissionID);
        //    //permissionFormDTO.TransType = permissionID;
        //    return new {
        //        PermissionForms = permissionForms,
        //        Suppliers = suppliers,
        //        Stock = stock,
        //        Permission  = permission
        //    };
        //}

        public async Task<TransMaster> AddPermissionFormAsync(TransMasterEditAddDTO permissionFormDTO)
        {
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
            permissionFormDTO.DocNo = filteredPermissions.Count() != 0 ? filteredPermissions.Max(obj => obj.DocNo) : 1;
            permissionFormDTO.SerSys = filteredPermissions.Count() != 0 ? filteredPermissions.Max(obj => obj.SerSys) : 1;

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
    }
}
