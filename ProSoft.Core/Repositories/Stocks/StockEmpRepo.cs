using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ProSoft.EF.DbContext;
using ProSoft.EF.DTOs.Accounts;
using ProSoft.EF.DTOs.Shared;
using ProSoft.EF.DTOs.Stocks;
using ProSoft.EF.IRepositories.Stocks;
using ProSoft.EF.Models.Accounts;
using ProSoft.EF.Models.Shared;
using ProSoft.EF.Models.Stocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.Core.Repositories.Stocks
{
    public class StockEmpRepo : Repository<StockEmp, int>, IStockEmpRepo
    {
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        public StockEmpRepo(AppDbContext Context, IMapper mapper, ICurrentUserService currentUserService) : base(Context)
        {
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<List<StockEmp>> GetStockForUserAsync(int userCode)
        {
            var branchId = int.Parse(_currentUserService.BranchId);
            
            List<StockEmp> stock = await _DbSet
              .Where(x => x.UserId == userCode)
              .Where(x => x.BranchId == branchId)
              .ToListAsync();

            return stock;
        }
        public async Task<List<StockEmpViewDTO>> GetStockTransForUserAsync(int userCode)
        {
            List<StockEmp> stockTrans = await _DbSet
                .Where(obj => obj.UserId == userCode).OrderBy(obj => obj.Stkcod)
                .ThenBy(obj => obj.TransType).ToListAsync();

            var stockTransDTO = new List<StockEmpViewDTO>();
            foreach (var item in stockTrans)
            {
                var myStockTransDTO = _mapper.Map<StockEmpViewDTO>(item);
                myStockTransDTO.PermissionName = (await _Context.GeneralCodes
                    .FirstOrDefaultAsync(obj => obj.UniqueType == item.TransType)).GDesc;

                myStockTransDTO.StockName = (await _Context.Stocks
                    .FirstOrDefaultAsync(obj => obj.Stkcod == item.Stkcod)).Stknam;

                myStockTransDTO.AccountStk = await GetAccountName(item.MainCodeStk, item.SubCodeStk);
                myStockTransDTO.AccountAcc = await GetAccountName(item.MainCodeAcc, item.SubCodeAcc);
                myStockTransDTO.JornalName = (await _Context.JournalTypes.FindAsync(int.Parse(item.JornalCode))).JournalName;
                stockTransDTO.Add(myStockTransDTO);
            }
            return stockTransDTO;
        }

        private async Task<string> GetAccountName(string mainCode, string subCode)
        {
            AccMainCode myMainCode = await _Context.AccMainCodes
                .FirstOrDefaultAsync(obj => obj.MainCode == mainCode);
            string mainName = myMainCode.MainName;

            AccSubCode mySubCode = await _Context.AccSubCodes
                .FirstOrDefaultAsync(obj => obj.SubCode == subCode && obj.MainCode == mainCode);
            string subName = mySubCode! != null ? mySubCode.SubName : string.Empty;

            return subName != "" ? $"{mainName} / {subName}" : mainName;
        }

        public async Task<List<AccSubCodeDTO>> GetSubCodesFromAccAsync(string mainAccCode)
        {
            List<AccSubCode> subAccCodes = await _Context.AccSubCodes
                .Where(obj => obj.MainCode == mainAccCode).ToListAsync();
            var subAccCodesDTO = _mapper.Map<List<AccSubCodeDTO>>(subAccCodes);
            return subAccCodesDTO;
        }

        public async Task<StockEmpEditAddDTO> GetEmptyStockTransAsync(int userCode)
        {
            var stockTransDTO = new StockEmpEditAddDTO();

            List<Stock> stocks = await _Context.Stocks.ToListAsync();
            List<AccMainCode> mainAccCodes = await _Context.AccMainCodes.ToListAsync();
            List<JournalType> journalTypes = await _Context.JournalTypes.ToListAsync();

            stockTransDTO.UserId = userCode;
            stockTransDTO.Stocks = _mapper.Map<List<StockViewDTO>>(stocks);
            stockTransDTO.MainAccCodes = _mapper.Map<List<AccMainCodeDTO>>(mainAccCodes);
            stockTransDTO.JournalTypes = _mapper.Map<List<JournalTypeDTO>>(journalTypes);
            return stockTransDTO;
        }

        public async Task<List<PermissionDefViewDTO>> GetUserStockPermissionsAsync(int userCode, int stockCode)
        {
            List<StockEmp> stockTransList = await _DbSet.Where(obj =>
                obj.Stkcod == stockCode && obj.UserId == userCode).ToListAsync();

            List<UserTranss> userTransList = await _Context.UserTransactions
                .Where(obj => obj.UsrId == userCode && obj.TransFlag == 1).ToListAsync();

            var permissionsDTO = new List<PermissionDefViewDTO>();
            foreach (var userTrans in userTransList)
            {
                GeneralCode permission = await _Context.GeneralCodes.FindAsync(userTrans.GId);
                var isExisted = false;
                foreach (var stockTrans in stockTransList)
                {
                    if (permission.UniqueType == stockTrans.TransType)
                    {
                        isExisted = true;
                        break;
                    }
                    else
                        isExisted = false;
                }
                if (!isExisted)
                {
                    var permDTO = _mapper.Map<PermissionDefViewDTO>(permission);
                    permissionsDTO.Add(permDTO);
                }
            }
            return permissionsDTO;
        }

        public async Task<List<PermissionDefViewDTO>> GetUserStockPermissionsForEditAsync(int userCode, int stockCode, int transType)
        {
            List<PermissionDefViewDTO> permissionsDTO = await GetUserStockPermissionsAsync(userCode, stockCode);
            GeneralCode permission = await _Context.GeneralCodes
                .FirstOrDefaultAsync(obj => obj.UniqueType == transType);
            var permDTO = _mapper.Map<PermissionDefViewDTO>(permission);
            permissionsDTO.Add(permDTO);
            return permissionsDTO;
        }

        public async Task<StockEmpEditAddDTO> GetStockTransByIdAsync(int id)
        {
            StockEmp stockTrans = await _DbSet.FindAsync(id);

            var stockTransDTO = _mapper.Map<StockEmpEditAddDTO>(stockTrans);

            List<Stock> stocks = await _Context.Stocks.ToListAsync();
            List<AccMainCode> mainAccCodes = await _Context.AccMainCodes.ToListAsync();
            List<JournalType> journalTypes = await _Context.JournalTypes.ToListAsync();

            stockTransDTO.Permissions = await GetUserStockPermissionsForEditAsync(stockTransDTO.UserId, stockTransDTO.Stkcod, stockTransDTO.TransType);
            stockTransDTO.Stocks = _mapper.Map<List<StockViewDTO>>(stocks);
            stockTransDTO.MainAccCodes = _mapper.Map<List<AccMainCodeDTO>>(mainAccCodes);
            stockTransDTO.JournalTypes = _mapper.Map<List<JournalTypeDTO>>(journalTypes);

            return stockTransDTO;
        }

        public async Task AddStockTransAsync(StockEmpEditAddDTO stockTransDTO)
        {
            StockEmp stockTrans = _mapper.Map<StockEmp>(stockTransDTO);
            await AddAsync(stockTrans);
            await SaveChangesAsync();
        }

        public async Task UpdateStockTransAsync(int id, StockEmpEditAddDTO stockTransDTO)
        {
            StockEmp stockTrans = await _DbSet.FindAsync(id);
            _mapper.Map(stockTransDTO, stockTrans);

            if (stockTrans != null)
            {
                await UpdateAsync(stockTrans);
                await SaveChangesAsync();
            }
        }

        public async Task DeleteStockTransAsync(int id)
        {
            StockEmp stockTrans = await _DbSet.FindAsync(id);

            if (stockTrans != null)
            {
                await DeleteAsync(stockTrans);
                await SaveChangesAsync();
            }
        }
    }
}
