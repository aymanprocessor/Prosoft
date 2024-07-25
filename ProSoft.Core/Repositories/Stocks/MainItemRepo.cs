using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProSoft.EF.DbContext;
using ProSoft.EF.DTOs.Accounts;
using ProSoft.EF.DTOs.Stocks;
using ProSoft.EF.IRepositories.Stocks;
using ProSoft.EF.Models.Accounts;
using ProSoft.EF.Models.Stocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.Core.Repositories.Stocks
{
    public class MainItemRepo : Repository<MainItem, int>, IMainItemRepo
    {
        private readonly IMapper _mapper;
        public MainItemRepo(AppDbContext Context, IMapper mapper) : base(Context)
        {
            _mapper = mapper;
        }

        public async Task<List<MainItemViewDTO>> GetMainItemsByLevelAsync(int level, int flag1, string parentCode = "")
        {
            var mainItems = Enumerable.Empty<MainItem>();
            if (parentCode == "")
            {
                mainItems = await _DbSet.Where(obj => obj.CurrentLevel == level &&
                    obj.Flag1 == flag1).ToListAsync();
            }
            else
            {
                mainItems = await _DbSet.Where(obj => obj.CurrentLevel == level &&
                    obj.Flag1 == flag1 && obj.ParentCode == parentCode).ToListAsync();
            }
            var mainItemsDTO = _mapper.Map<List<MainItemViewDTO>>(mainItems);
            return mainItemsDTO;
        }

        public async Task<MainItemViewDTO> GetParentItemAsync(string mainCode, int flag1)
        {
            MainItem mainItem = await _DbSet.FirstOrDefaultAsync(obj =>
                obj.MainCode == mainCode && obj.Flag1 == flag1);
            var mainItemDTO = _mapper.Map<MainItemViewDTO>(mainItem);
            return mainItemDTO;
        }

        private async Task<string> GetNewMainCode_2_Async(int flag1)
        {
            MainItem lastRecord = await _DbSet.OrderBy(obj => obj.MainCode)
                   .LastOrDefaultAsync(obj => obj.CurrentLevel == 2 && obj.Flag1 == flag1);

            var maincode = "";
            if (lastRecord != null)
            {
                maincode = lastRecord.MainCode;
                var firstsup = maincode.Substring(0, 1);
                var secondsup = maincode.Substring(1);

                var value = int.Parse(firstsup) + 1;

                firstsup = $"{value}";

                maincode = firstsup + secondsup;
            }
            else
                maincode = "100000000";

            return maincode;
        }

        private async Task<string> GetNewMainCode_3_Async(string parentCode, int flag1)
        {
            MainItem lastRecord = await _DbSet.OrderBy(obj => obj.MainCode)
                .LastOrDefaultAsync(obj => obj.CurrentLevel == 3 && obj.ParentCode == parentCode && obj.Flag1 == flag1);

            var maincode = "";
            if (lastRecord != null)
            {
                maincode = lastRecord.MainCode;
                var firstsup = maincode.Substring(0, 1);
                var secondsup = maincode.Substring(1, 2);
                var thirdsup = maincode.Substring(3);

                var value = int.Parse(secondsup) + 1;

                secondsup = value <= 9 ? $"0{value}" : $"{value}";

                maincode = firstsup + secondsup + thirdsup;
            }
            else
            {
                maincode = parentCode;
                var firstsup = maincode.Substring(0, 1);
                var thirdsup = maincode.Substring(3);
                maincode = firstsup + "01" + thirdsup;
            }
            return maincode;
        }

        private async Task<string> GetNewMainCode_4_Async(string parentCode, int flag1)
        {
            MainItem lastRecord = await _DbSet.OrderBy(obj => obj.MainCode)
                .LastOrDefaultAsync(obj => obj.CurrentLevel == 4 && obj.ParentCode == parentCode && obj.Flag1 == flag1);

            var maincode = "";
            if (lastRecord != null)
            {
                maincode = lastRecord.MainCode;
                var firstsup = maincode.Substring(0, 3);
                var secondsup = maincode.Substring(3, 2);
                var thirdsup = maincode.Substring(5);

                var value = int.Parse(secondsup) + 1;

                secondsup = value <= 9 ? $"0{value}" : $"{value}";

                maincode = firstsup + secondsup + thirdsup;
            }
            else
            {
                maincode = parentCode;
                var firstsup = maincode.Substring(0, 3);
                var thirdsup = maincode.Substring(5);
                maincode = firstsup + "01" + thirdsup;
            }
            return maincode;
        }

        private async Task<string> GetNewMainCode_5_Async(string parentCode, int flag1)
        {
            MainItem lastRecord = await _DbSet.OrderBy(obj => obj.MainCode)
                .LastOrDefaultAsync(obj => obj.CurrentLevel == 5 && obj.ParentCode == parentCode && obj.Flag1 == flag1);

            var maincode = "";
            if (lastRecord != null)
            {
                maincode = lastRecord.MainCode;
                var firstsup = maincode.Substring(0, 5);
                var secondsup = maincode.Substring(5, 2);
                var thirdsup = maincode.Substring(7);

                var value = int.Parse(secondsup) + 1;

                secondsup = value <= 9 ? $"0{value}" : $"{value}";

                maincode = firstsup + secondsup + thirdsup;
            }
            else
            {
                maincode = parentCode;
                var firstsup = maincode.Substring(0, 5);
                var thirdsup = maincode.Substring(7);
                maincode = firstsup + "01" + thirdsup;
            }
            return maincode;
        }

        private async Task<string> GetNewMainCode_6_Async(string parentCode, int flag1)
        {
            MainItem lastRecord = await _DbSet.OrderBy(obj => obj.MainCode)
                .LastOrDefaultAsync(obj => obj.CurrentLevel == 6 && obj.ParentCode == parentCode && obj.Flag1 == flag1);

            var maincode = "";
            if (lastRecord != null)
            {
                maincode = lastRecord.MainCode;
                var firstsup = maincode.Substring(0, 7);
                var secondsup = maincode.Substring(7, 2);

                var value = int.Parse(secondsup) + 1;

                secondsup = value <= 9 ? $"0{value}" : $"{value}";

                maincode = firstsup + secondsup;
            }
            else
            {
                maincode = parentCode;
                var firstsup = maincode.Substring(0, 7);
                maincode = firstsup + "01";
            }
            return maincode;
        }

        public async Task<MainItemEditAddDTO> GetMainLevelByIdAsync(int id)
        {
            MainItem mainItem = await GetByIdAsync(id);
            var mainItemDTO = _mapper.Map<MainItemEditAddDTO>(mainItem);

            MainItem parentItem = await _DbSet.FirstOrDefaultAsync(obj =>
                obj.MainCode == mainItemDTO.ParentCode && obj.Flag1 == mainItemDTO.Flag1);
            mainItemDTO.ParentName = parentItem != null ? parentItem.MainName : "";

            List<CostCenter> costCenters = await _Context.CostCenters.Where(obj =>
                (obj.CostFlag == 1 || obj.CostFlag == 10) && obj.CostVisible == 1).ToListAsync();
            mainItemDTO.CostCenters = _mapper.Map<List<CostCenterViewDTO>>(costCenters);

            return mainItemDTO;
        }

        public async Task AddMainLevelAsync(MainItemEditAddDTO mainItemDTO)
        {
            mainItemDTO.PolicyPrice = mainItemDTO.PolicyPrice is not (null or 0) ? mainItemDTO.PolicyPrice : 3;

            var mainItem = _mapper.Map<MainItem>(mainItemDTO);
            if(mainItem.ParentCode.Length == 1)
            {
                mainItem.MainNameAll = mainItem.MainName;
            }
            else if(mainItem.ParentCode.Length > 1)
            {
                MainItem parentItem = await _DbSet.FirstOrDefaultAsync(obj => obj.MainCode == mainItem.ParentCode &&
                    obj.Flag1 == mainItem.Flag1);
                mainItem.MainNameAll = parentItem.MainNameAll + "/" + mainItem.MainName;
            }
            mainItem.RowOnOff = 1;
            await AddAsync(mainItem);
            await SaveChangesAsync();
        }

        public async Task EditMainLevelAsync(int id, MainItemEditAddDTO mainItemDTO)
        {
            mainItemDTO.PolicyPrice = mainItemDTO.PolicyPrice is not (null or 0) ? mainItemDTO.PolicyPrice : 3;

            MainItem mainItem = await GetByIdAsync(id);
            _mapper.Map(mainItemDTO, mainItem);
            mainItem.RowOnOff = 1;
            mainItem.MainId = id;
            await UpdateAsync(mainItem);
            await SaveChangesAsync();
        }

        ///////////////////////////////////////////////////////
        // Main Level 2

        public async Task<MainItemEditAddDTO> GetNewMainLevel_2_Async(int flag1)
        {
            var newMainLevel = new MainItemEditAddDTO();
            newMainLevel.MainCode = await GetNewMainCode_2_Async(flag1);
            newMainLevel.Flag1 = flag1;

            List<CostCenter> costCenters = await _Context.CostCenters.Where(obj =>
                (obj.CostFlag == 1 || obj.CostFlag == 10) && obj.CostVisible == 1).ToListAsync();
            newMainLevel.CostCenters = _mapper.Map<List<CostCenterViewDTO>>(costCenters);

            return newMainLevel;
        }

        ///////////////////////////////////////////////////////
        // Main Level 3

        public async Task<MainItemEditAddDTO> GetNewMainLevel_3_Async(string parentCode, int flag1)
        {
            var newMainLevel = new MainItemEditAddDTO();
            newMainLevel.MainCode = await GetNewMainCode_3_Async(parentCode, flag1);
            newMainLevel.Flag1 = flag1;
            newMainLevel.ParentCode = parentCode;
            newMainLevel.ParentName = (await _DbSet.FirstOrDefaultAsync(obj =>
                obj.MainCode == parentCode && obj.Flag1 == flag1)).MainName;

            List<CostCenter> costCenters = await _Context.CostCenters.Where(obj =>
                (obj.CostFlag == 1 || obj.CostFlag == 10) && obj.CostVisible == 1).ToListAsync();
            newMainLevel.CostCenters = _mapper.Map<List<CostCenterViewDTO>>(costCenters);

            return newMainLevel;
        }

        ///////////////////////////////////////////////////////
        // Main Level 4

        public async Task<MainItemEditAddDTO> GetNewMainLevel_4_Async(string parentCode, int flag1)
        {
            var newMainLevel = new MainItemEditAddDTO();
            newMainLevel.MainCode = await GetNewMainCode_4_Async(parentCode, flag1);
            newMainLevel.Flag1 = flag1;
            newMainLevel.ParentCode = parentCode;
            newMainLevel.ParentName = (await _DbSet.FirstOrDefaultAsync(obj =>
                obj.MainCode == parentCode && obj.Flag1 == flag1)).MainName;

            List<CostCenter> costCenters = await _Context.CostCenters.Where(obj =>
                (obj.CostFlag == 1 || obj.CostFlag == 10) && obj.CostVisible == 1).ToListAsync();
            newMainLevel.CostCenters = _mapper.Map<List<CostCenterViewDTO>>(costCenters);

            return newMainLevel;
        }

        ///////////////////////////////////////////////////////
        // Main Level 5

        public async Task<MainItemEditAddDTO> GetNewMainLevel_5_Async(string parentCode, int flag1)
        {
            var newMainLevel = new MainItemEditAddDTO();
            newMainLevel.MainCode = await GetNewMainCode_5_Async(parentCode, flag1);
            newMainLevel.Flag1 = flag1;
            newMainLevel.ParentCode = parentCode;
            newMainLevel.ParentName = (await _DbSet.FirstOrDefaultAsync(obj =>
                obj.MainCode == parentCode && obj.Flag1 == flag1)).MainName;

            List<CostCenter> costCenters = await _Context.CostCenters.Where(obj =>
                (obj.CostFlag == 1 || obj.CostFlag == 10) && obj.CostVisible == 1).ToListAsync();
            newMainLevel.CostCenters = _mapper.Map<List<CostCenterViewDTO>>(costCenters);

            return newMainLevel;
        }

        ///////////////////////////////////////////////////////
        // Main Level 6

        public async Task<MainItemEditAddDTO> GetNewMainLevel_6_Async(string parentCode, int flag1)
        {
            var newMainLevel = new MainItemEditAddDTO();
            newMainLevel.MainCode = await GetNewMainCode_6_Async(parentCode, flag1);
            newMainLevel.Flag1 = flag1;
            newMainLevel.ParentCode = parentCode;
            newMainLevel.ParentName = (await _DbSet.FirstOrDefaultAsync(obj =>
                obj.MainCode == parentCode && obj.Flag1 == flag1)).MainName;

            List<CostCenter> costCenters = await _Context.CostCenters.Where(obj =>
                (obj.CostFlag == 1 || obj.CostFlag == 10) && obj.CostVisible == 1).ToListAsync();
            newMainLevel.CostCenters = _mapper.Map<List<CostCenterViewDTO>>(costCenters);

            return newMainLevel;
        }

        ///////////////////////////////////////////////////////

        public async Task<MainItemStockDTO> GetNewMainItemStockAsync(int mainId)
        {
            var mainItemStock = new MainItemStockDTO();
            MainItem mainItem = await GetByIdAsync(mainId);

            List<MainItemStock> mainItemStockList = await _Context.MainItemStocks.Where(obj => obj.MainCode ==
                mainItem.MainCode && obj.Flag1 == mainItem.Flag1 && obj.BranchId == mainItem.BranchId)
                .ToListAsync();
            mainItemStock.Stocks = new List<StockViewDTO>();
            foreach (var item in mainItemStockList)
            {
                Stock stock = await _Context.Stocks.FindAsync(item.Stkcod);
                var stockDTO = _mapper.Map<StockViewDTO>(stock);
                mainItemStock.Stocks.Add(stockDTO);
            }

            List<Stock> stocks = await _Context.Stocks.ToListAsync();
            mainItemStock.AllStocks = _mapper.Map<List<StockViewDTO>>(stocks);

            mainItemStock.Flag1 = mainItem.Flag1;
            mainItemStock.ParentCode = mainItem.ParentCode;
            mainItemStock.MainName = mainItem.MainName;
            mainItemStock.MainLevel = mainItem.ParentCode == "1" ? "MainLevel_2" : $"MainLevel_{mainItem.CurrentLevel}";

            return mainItemStock;
        }

        public async Task<List<string>> CheckIfExistsAsync(int mainId, int[] stocksIDs)
        {
            MainItem mainItem = await GetByIdAsync(mainId);
            List<string> messages = new List<string>();
            foreach (var stockID in stocksIDs)
            {
                MainItemStock mainItemStock = await _Context.MainItemStocks.FirstOrDefaultAsync(obj => obj.MainCode ==
                    mainItem.MainCode && obj.Flag1 == mainItem.Flag1 && obj.BranchId == mainItem.BranchId &&
                    obj.Stkcod == stockID);
                if(mainItemStock != null)
                {
                    Stock stock = await _Context.Stocks.FindAsync(stockID);
                    messages.Add($"{stock.Stknam} is already added before.");
                }
            }
            if (messages.Count == 0)
                messages.Add("1");
            return messages;
        }

        private async Task<bool> CheckIfStockAddedAsync(MainItem mainItem, int stocksID)
        {
            List<MainItemStock> mainItemStockList = await _Context.MainItemStocks.Where(obj =>
                obj.MainCode == mainItem.MainCode && obj.Flag1 == mainItem.Flag1 && obj.BranchId == mainItem.BranchId)
                .ToListAsync();
            foreach (var item in mainItemStockList)
            {
                if (item.Stkcod == stocksID)
                    return true;
            }
            return false;
        }

        private async Task AddStockToGroupAsync(MainItem mainItem, int stockID)
        {
            var newMainItemStock = new MainItemStock();
            newMainItemStock.MainCode = mainItem.MainCode;
            newMainItemStock.Flag1 = (int)mainItem.Flag1;
            newMainItemStock.BranchId = (int)mainItem.BranchId;
            newMainItemStock.Stkcod = stockID;
            await _Context.AddAsync(newMainItemStock);
        }

        public async Task UpdateStocksForGroupAsync(int mainId, int[] stocksIDs)
        {
            MainItem mainItem = await GetByIdAsync(mainId);
            List<MainItemStock> mainItemStockList = await _Context.MainItemStocks.Where(obj =>
                obj.MainCode == mainItem.MainCode && obj.Flag1 == mainItem.Flag1 && obj.BranchId == mainItem.BranchId)
                .ToListAsync();
            foreach (var item in mainItemStockList)
            {
                if (!stocksIDs.Contains(item.Stkcod))
                    _Context.Remove(item);
            }
            foreach (var stockID in stocksIDs)
            {
                if(!(await CheckIfStockAddedAsync(mainItem, stockID)))
                    await AddStockToGroupAsync(mainItem, stockID);
            }
            await SaveChangesAsync();
        }
    }
}
