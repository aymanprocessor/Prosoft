using ProSoft.EF.DTOs.Accounts;
using ProSoft.EF.DTOs.Medical.Analysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.IRepositories.Accounts
{
    public interface IAccSubCodeRepo 
    {
        Task<List<AccSubCodeDTO>> GetSubsByMainAsync(string id);
        Task<AccSubCodeDTO> GetSubByIDsAsync(string subCode, string maincode);
        Task<string> GetParentCodeAsync(string code);
        Task<string> GetNewSubAsync(string mainCode);

        /// Add
       // Task AddSubAnalysisAsync(string id, SubEditAddDTO subDTO);

        /// Edit
        //Task EditSubAnalysisAsync(string subCode, SubEditAddDTO subDTO);

        ///// Delete
        //Task DeleteSubAnalysisAsync(string subCode, string mainCode);
    }
}
