using ProSoft.EF.DTOs.Accounts;
using ProSoft.EF.DTOs.Medical.Analysis;
using ProSoft.EF.Models.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.IRepositories.Accounts
{
    public interface IAccMainCodeRepo
    {
        Task<List<AccMainCodeDTO>> GetMainsByLevelAsync(double level);
        Task<AccMainCodeDTO> GetMainByIdAsync(string id);
        Task<string> GetParentCodeAsync(string code);
        Task<string> GetNewMain_3_Async();
        Task<string> GetNewMain_4_Async(string id);
        Task<string> GetNewMain_5_Async(string id);
        Task<string> GetNewMain_6_Async(string id);
        /// Add
        Task AddMainLevel_3_Async(AccMainCodeEditAddDTO mainDTO);
        Task AddMainLevel_4_Async(string Level_3_Code, AccMainCodeEditAddDTO mainDTO);
        Task AddMainLevel_5_Async(string Level_4_Code, AccMainCodeEditAddDTO mainDTO);
        Task AddMainLevel_6_Async(string Level_4_Code, AccMainCodeEditAddDTO mainDTO);

        /// Edit
        Task EditMainLevelAsync(string code, AccMainCodeEditAddDTO mainDTO);
        /// Delete
        Task DeleteMainLevelAsync(string code);
    }
}
