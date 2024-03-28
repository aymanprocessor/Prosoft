using ProSoft.EF.DTOs.Medical.Analysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.IRepositories.Medical.Analysis
{
    public interface IItemAnalysisRepo
    {
        Task<List<ItemViewDTO>> GetItemsBySubAsync(string id,string maincode);
        Task<ItemViewDTO> GetItemByIDsAsync(int itemcode,string subCode, string maincode);
        Task<int> GetNewItemAsync(string id, string maincode);

        /// Add
        Task AddItemAnalysisAsync(string id, ItemEditAddDTO itemDTO);

        /// Edit
        Task EditItemAnalysisAsync(int id, ItemEditAddDTO itemDTO);

        /// Delete
        Task DeleteItemAnalysisAsync(int id, string subcode, string maincode)
;
    }
}
