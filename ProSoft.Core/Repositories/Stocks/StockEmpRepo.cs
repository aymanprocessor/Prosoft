using AutoMapper;
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
    public class StockEmpRepo: IStockEmpRepo
    {
        private readonly AppDbContext _Context;
        private readonly IMapper _mapper;
        public StockEmpRepo(AppDbContext Context, IMapper mapper)
        {
            _Context = Context;
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

        public async Task<StockEmpEditAddDTO> GetStockTransByIdAsync(int userCode, int permit_uniqueType)
        {
            StockEmp stockTrans = await _Context.StockEmps.FirstOrDefaultAsync(
                obj => obj.UserId == userCode && obj.TransType == permit_uniqueType);

            var stockTransDTO = _mapper.Map<StockEmpEditAddDTO>(stockTrans);

            List<Stock> stocks = await _Context.Stocks.ToListAsync();
            List<AccMainCode> mainAccCodes = await _Context.AccMainCodes.ToListAsync();
            List<JournalType> journalTypes = await _Context.JournalTypes.ToListAsync();

            stockTransDTO.Permissions = await GetPermissionsFor(userCode, "Edit", permit_uniqueType);
            stockTransDTO.Stocks = _mapper.Map<List<StockViewDTO>>(stocks);
            stockTransDTO.MainAccCodes = _mapper.Map<List<AccMainCodeDTO>>(mainAccCodes);
            stockTransDTO.JournalTypes = _mapper.Map<List<JournalTypeDTO>>(journalTypes);

            return stockTransDTO;
        }

        public async Task AddStockTransAsync(StockEmp stockTrans)
        {
            await _Context.AddAsync(stockTrans);
            await _Context.SaveChangesAsync();
        }

        public async Task UpdateStockTransAsync(int userCode, int transType, StockEmpEditAddDTO stockTransDTO)
        {
            StockEmp stockTrans = await _Context.StockEmps.FirstOrDefaultAsync(
                obj => obj.UserId == userCode && obj.TransType == transType);
            _mapper.Map(stockTransDTO, stockTrans);

            if (stockTrans != null)
            {
                _Context.Update(stockTrans);
                await _Context.SaveChangesAsync();
            }
        }

        public async Task DeleteStockTransAsync(int userCode, int transType)
        {
            StockEmp stockTrans = await _Context.StockEmps.FirstOrDefaultAsync(
                obj => obj.UserId == userCode && obj.TransType == transType);

            if (stockTrans != null)
            {
                _Context.Remove(stockTrans);
                await _Context.SaveChangesAsync();
            }
        }
    }
}
