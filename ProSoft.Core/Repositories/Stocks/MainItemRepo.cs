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

        public async Task<List<MainItemViewDTO>> GetMainItemsByLevelAsync(int level)
        {
            List<MainItem> mainItems = await _DbSet.Where(obj => obj.CurrentLevel == level).ToListAsync();
            var mainItemsDTO = _mapper.Map<List<MainItemViewDTO>>(mainItems);
            return mainItemsDTO;
        }

        private async Task<string> GetNewMainCode_2_Async()
        {
            MainItem lastRecord = await _DbSet.OrderBy(obj => obj.MainCode)
                   .LastOrDefaultAsync(obj => obj.CurrentLevel == 2);

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

        public async Task<MainItemEditAddDTO> GetNewMainLevel_2_Async()
        {
            var newMainLevel = new MainItemEditAddDTO();
            newMainLevel.MainCode = await GetNewMainCode_2_Async();

            List<CostCenter> costCenters = await _Context.CostCenters.Where(obj =>
                (obj.CostFlag == 1 || obj.CostFlag == 10) && obj.CostVisible == 1).ToListAsync();
            newMainLevel.CostCenters = _mapper.Map<List<CostCenterViewDTO>>(costCenters);

            return newMainLevel;
        }

        public async Task AddMainLevel_2_Async(MainItemEditAddDTO mainItemDTO)
        {
            var mainItem = _mapper.Map<MainItem>(mainItemDTO);
            mainItem.CurrentLevel = 2;
            await AddAsync(mainItem);
            await SaveChangesAsync();
        }
    }
}
