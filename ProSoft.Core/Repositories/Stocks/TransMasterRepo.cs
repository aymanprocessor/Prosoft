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
    }
}
