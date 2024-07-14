using ProSoft.EF.DTOs.Stocks;
using ProSoft.EF.Models.Stocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.IRepositories.Stocks
{
    public interface IMainItemRepo : IRepository<MainItem, int>
    {
        Task<List<MainItemViewDTO>> GetMainItemsByLevelAsync(int level);
        Task<MainItemEditAddDTO> GetNewMainLevel_2_Async();
        Task AddMainLevel_2_Async(MainItemEditAddDTO mainItemDTO);
    }
}
