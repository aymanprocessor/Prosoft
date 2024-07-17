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
        Task<List<MainItemViewDTO>> GetMainItemsByLevelAsync(int level, int flag1, string parentCode = "");
        Task<MainItemViewDTO> GetParentItemAsync(string mainCode, int flag1);
        Task<MainItemEditAddDTO> GetMainLevelByIdAsync(int id);
        Task AddMainLevelAsync(MainItemEditAddDTO mainItemDTO);
        Task EditMainLevelAsync(int id, MainItemEditAddDTO mainItemDTO);
        ///////////////////////////////////////////////////////
        // Main Level 2
        Task<MainItemEditAddDTO> GetNewMainLevel_2_Async(int flag1);
        //Task<MainItemEditAddDTO> GetMainLevel_2_ByIdAsync(int id);
        //Task AddMainLevel_2_Async(MainItemEditAddDTO mainItemDTO);
        //Task EditMainLevel_2_Async(int id, MainItemEditAddDTO mainItemDTO);
        ///////////////////////////////////////////////////////
        // Main Level 3
        Task<MainItemEditAddDTO> GetNewMainLevel_3_Async(string parentCode, int flag1);
        //Task<MainItemEditAddDTO> GetMainLevel_3_ByIdAsync(int id);
        //Task AddMainLevel_3_Async(MainItemEditAddDTO mainItemDTO);
        //Task EditMainLevel_3_Async(int id, MainItemEditAddDTO mainItemDTO);
        ///////////////////////////////////////////////////////
        // Main Level 4
        Task<MainItemEditAddDTO> GetNewMainLevel_4_Async(string parentCode, int flag1);
        ///////////////////////////////////////////////////////
        // Main Level 5
        Task<MainItemEditAddDTO> GetNewMainLevel_5_Async(string parentCode, int flag1);
        ///////////////////////////////////////////////////////
        // Main Level 6
        Task<MainItemEditAddDTO> GetNewMainLevel_6_Async(string parentCode, int flag1);
    }
}
