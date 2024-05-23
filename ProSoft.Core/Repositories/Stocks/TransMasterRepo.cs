using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProSoft.EF.DbContext;
using ProSoft.EF.DTOs.Shared;
using ProSoft.EF.DTOs.Stocks;
using ProSoft.EF.IRepositories.Stocks;
using ProSoft.EF.Models.Shared;
using ProSoft.EF.Models.Stocks;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<List<StockViewDTO>> GetActiveStocksForUserAsync(int userCode)
        {
            List<StockEmp> stockTrans = await _Context.StockEmps
                .Where(obj => obj.UserId == userCode && obj.StockDef == 1).ToListAsync();

            var stocksDTO = new List<StockViewDTO>();
            foreach (var item in stockTrans)
            {
                var stockDTO = new StockViewDTO();
                stockDTO.Stkcod = item.Stkcod;
                stockDTO.Stknam = (await _Context.Stocks
                    .FindAsync(item.Stkcod)).Stknam;
                stocksDTO.Add(stockDTO);
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

        public async Task<List<TransMasterViewDTO>> GetPermissionsFormsAsync(int stockID, int transType)
        {
            List<TransMaster> permissionsForms = await _DbSet.Where(obj => obj.StockCode == stockID
                && obj.TransType == transType).ToListAsync();
            var permissionsFormsDTO = _mapper.Map<List<TransMasterViewDTO>>(permissionsForms);
            return permissionsFormsDTO;
        }

        public async Task<TransMasterEditAddDTO> GetDTOWithDefaultsAsync(int stockID, int permissionID)
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

        public async Task AddPermissionFormAsync(TransMasterEditAddDTO permissionFormDTO)
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
        }
    }
}
