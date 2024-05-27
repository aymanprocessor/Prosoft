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
    public class StockEmpRepo: Repository<StockEmp, int>, IStockEmpRepo
    {
        private readonly IMapper _mapper;
        public StockEmpRepo(AppDbContext Context, IMapper mapper): base(Context)
        {
            _mapper = mapper;
        }

        public async Task<List<StockEmpViewDTO>> GetStockTransForUserAsync(int userCode)
        {
            List<StockEmp> stockTrans = await _Context.StockEmps
                .Where(obj => obj.UserId == userCode).ToListAsync();

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
            stockTransDTO.Permissions = await GetPermissionsFor(userCode);
            stockTransDTO.Stocks = _mapper.Map<List<StockViewDTO>>(stocks);
            stockTransDTO.MainAccCodes = _mapper.Map<List<AccMainCodeDTO>>(mainAccCodes);
            stockTransDTO.JournalTypes = _mapper.Map<List<JournalTypeDTO>>(journalTypes);
            return stockTransDTO;
        }

        private async Task<List<PermissionDefViewDTO>> GetPermissionsFor(int userCode, string action = "Add", int transType = 0)
        {
            List<UserTranss> userTrans = await _Context.UserTransactions
                .Where(obj => obj.UsrId == userCode && obj.TransFlag == 1).ToListAsync();

            var permissionsDTO = new List<PermissionDefViewDTO>();
            foreach (var item in userTrans)
            {
                GeneralCode permission = await _Context.GeneralCodes
                    .FirstOrDefaultAsync(obj => obj.GId == item.GId);
                StockEmp stockTrans = await _Context.StockEmps.FirstOrDefaultAsync(
                    obj => obj.TransType == permission.UniqueType && obj.UserId == userCode);

                if (permission != null && stockTrans == null)
                {
                    var permissionDTO = _mapper.Map<PermissionDefViewDTO>(permission);
                    permissionsDTO.Add(permissionDTO);
                }
            }
            StockEmp stockTrans_2 = await _Context.StockEmps.FirstOrDefaultAsync(
                obj => obj.TransType == transType && obj.UserId == userCode);
            if(action == "Edit" && stockTrans_2 != null)
            {
                GeneralCode permission_2 = await _Context.GeneralCodes.FirstOrDefaultAsync(
                    obj => obj.UniqueType == transType);
                var permissionDTO_2 = _mapper.Map<PermissionDefViewDTO>(permission_2);
                permissionsDTO.Add(permissionDTO_2);
            }
            return permissionsDTO;
        }

        public async Task<List<PermissionDefViewDTO>> GetStockPermissionsForAddAsync(int userCode, int stockCode)
        {
            List<StockEmp> stockTransList = await _Context.StockEmps.Where(obj =>
                obj.Stkcod == stockCode && obj.UserId == userCode).ToListAsync();
            List<UserTranss> userTransList = await _Context.UserTransactions
                .Where(obj => obj.UsrId == userCode && obj.TransFlag == 1).ToListAsync();

            var permissionsDTO = new List<PermissionDefViewDTO>();
            if (stockTransList.Count() == 0)
            {
                foreach (var item in userTransList)
                {
                    GeneralCode perm = await _Context.GeneralCodes.FindAsync(item.GId);
                    var permDTO = _mapper.Map<PermissionDefViewDTO>(perm);
                    permissionsDTO.Add(permDTO);
                }
            }
            else
            {
                foreach (var item in stockTransList)
                {
                    GeneralCode permission = await _Context.GeneralCodes
                        .FirstOrDefaultAsync(obj => obj.UniqueType == item.TransType);
                    foreach (var item1 in userTransList)
                    {
                        if(item1.GId != permission.GId)
                        {
                            GeneralCode perm = await _Context.GeneralCodes.FindAsync(item1.GId);
                            var permDTO = _mapper.Map<PermissionDefViewDTO>(perm);
                            permissionsDTO.Add(permDTO);
                        }
                    }
                }
            }
            return permissionsDTO;
        }

        public async Task<List<PermissionDefViewDTO>> GetStockPermissionsForEditAsync(int userCode, int stockCode, int transType)
        {
            List<PermissionDefViewDTO> permissionsDTO = await GetStockPermissionsForAddAsync(userCode, stockCode);
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

            stockTransDTO.Permissions = await GetStockPermissionsForEditAsync(stockTransDTO.UserId, stockTransDTO.Stkcod, stockTransDTO.TransType);
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
