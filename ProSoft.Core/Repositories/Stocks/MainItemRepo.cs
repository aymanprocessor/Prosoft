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

        public async Task<List<MainItemViewDTO>> GetMainItemsByLevelAsync(int level, int flag1)
        {
            List<MainItem> mainItems = await _DbSet.Where(obj => obj.CurrentLevel == level && obj.Flag1 == flag1)
                .ToListAsync();
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
                var secondsup = maincode.Substring(1, 2);
                var thirdsup = maincode.Substring(3);

                var value = int.Parse(firstsup) + 1;

                firstsup = $"{value}";

                maincode = firstsup + secondsup + thirdsup;
            }
            else
                maincode = "100000000";

            return maincode;
        }

        private async Task<string> GetNewMainCode_3_Async(int flag1)
        {
            MainItem lastRecord = await _DbSet.OrderBy(obj => obj.MainCode)
                   .LastOrDefaultAsync(obj => obj.CurrentLevel == 3 && obj.Flag1 == flag1);

            var maincode = "";
            if (lastRecord != null)
            {
                maincode = lastRecord.MainCode;
                var firstsup = maincode.Substring(0, 1);
                var secondsup = maincode.Substring(1, 2);
                var thirdsup = maincode.Substring(3);

                var value = int.Parse(secondsup) + 1;

                firstsup = value <= 9 ? $"0{value}" : $"{value}";

                maincode = firstsup + secondsup + thirdsup;
            }
            else
            {
                maincode = lastRecord.MainCode;
                var firstsup = maincode.Substring(0, 1);
                var thirdsup = maincode.Substring(3);
                maincode = firstsup + "01" + thirdsup;
            }
            return maincode;
        }

        ///////////////////////////////////////////////////////
        // Main Level 2

        public async Task<MainItemEditAddDTO> GetNewMainLevel_2_Async(int flag1)
        {
            var newMainLevel = new MainItemEditAddDTO();
            newMainLevel.MainCode = await GetNewMainCode_2_Async(flag1);

            List<CostCenter> costCenters = await _Context.CostCenters.Where(obj =>
                (obj.CostFlag == 1 || obj.CostFlag == 10) && obj.CostVisible == 1).ToListAsync();
            newMainLevel.CostCenters = _mapper.Map<List<CostCenterViewDTO>>(costCenters);

            return newMainLevel;
        }

        public async Task<MainItemEditAddDTO> GetMainLevel_2_ByIdAsync(int id)
        {
            MainItem mainItem = await GetByIdAsync(id);
            var mainItemDTO = _mapper.Map<MainItemEditAddDTO>(mainItem);

            List<CostCenter> costCenters = await _Context.CostCenters.Where(obj =>
                (obj.CostFlag == 1 || obj.CostFlag == 10) && obj.CostVisible == 1).ToListAsync();
            mainItemDTO.CostCenters = _mapper.Map<List<CostCenterViewDTO>>(costCenters);

            return mainItemDTO;
        }

        public async Task AddMainLevel_2_Async(MainItemEditAddDTO mainItemDTO)
        {
            mainItemDTO.CurrentLevel = 2;
            mainItemDTO.ParentCode = "1";
            mainItemDTO.PolicyPrice = mainItemDTO.PolicyPrice is not (null or 0) ? mainItemDTO.PolicyPrice : 3;

            var mainItem = _mapper.Map<MainItem>(mainItemDTO);
            await AddAsync(mainItem);
            await SaveChangesAsync();
        }

        public async Task EditMainLevel_2_Async(int id, MainItemEditAddDTO mainItemDTO)
        {
            mainItemDTO.CurrentLevel = 2;
            mainItemDTO.ParentCode = "1";
            mainItemDTO.PolicyPrice = mainItemDTO.PolicyPrice is not (null or 0) ? mainItemDTO.PolicyPrice : 3;

            MainItem mainItem = await GetByIdAsync(id);
            _mapper.Map(mainItemDTO, mainItem);
            mainItem.MainId = id;
            await UpdateAsync(mainItem);
            await SaveChangesAsync();
        }

        ///////////////////////////////////////////////////////
        // Main Level 3

        public async Task<MainItemEditAddDTO> GetNewMainLevel_3_Async(int flag1)
        {
            var newMainLevel = new MainItemEditAddDTO();
            newMainLevel.MainCode = await GetNewMainCode_3_Async(flag1);

            List<CostCenter> costCenters = await _Context.CostCenters.Where(obj =>
                (obj.CostFlag == 1 || obj.CostFlag == 10) && obj.CostVisible == 1).ToListAsync();
            newMainLevel.CostCenters = _mapper.Map<List<CostCenterViewDTO>>(costCenters);

            return newMainLevel;
        }
    }
}
