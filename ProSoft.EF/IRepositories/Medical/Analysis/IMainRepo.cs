using ProSoft.EF.DTOs.Medical.Analysis;
using ProSoft.EF.Models.Medical.Analysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.IRepositories.Medical.Analysis
{
    public interface IMainRepo 
    {
        Task<List<MainViewDTO>> GetMainsByLevelAsync(double level);
        Task<MainViewDTO> GetMainByIdAsync(string id);
        Task<string> GetParentCodeAsync(string code);
        Task<string> GetNewMain_3_Async();
        Task<string> GetNewMain_4_Async(string id);
        Task<string> GetNewMain_5_Async(string id);
        Task<string> GetNewMain_6_Async(string id);
        /// Add
        Task AddMainLevel_3_Async(MainEditAddDTO mainDTO);
        Task AddMainLevel_4_Async(string Level_3_Code, MainEditAddDTO mainDTO);
        Task AddMainLevel_5_Async(string Level_4_Code, MainEditAddDTO mainDTO);
        Task AddMainLevel_6_Async(string Level_4_Code, MainEditAddDTO mainDTO);

        /// Edit
        Task EditMainLevelAsync(string code, MainEditAddDTO mainDTO);
        /// Delete
        Task DeleteMainLevelAsync(string code);
    }
}
