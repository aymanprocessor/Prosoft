using ProSoft.EF.DTOs.Stocks;
using ProSoft.EF.Models.Stocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.IRepositories.Stocks
{
    public interface ISubItemRepo : IRepository<SubItem, int>
    {
        
        // Code By Ayman Saad - 22-9-2024
       
        IEnumerable<SubItem> GetAllSubItemByStockId(int stkcod);
        Task<IEnumerable<SubItem>> GetAllSubItemByStockTypeAsync(int stockType);
        
        // Code By Ayman Saad - 22-9-2024

        Task<List<SubItemViewDTO>> GetSubItemsByMainIdAsync(int mainId);
        Task<int> ValidateItemCode(string itemCode);
        Task<SubItemEditAddDTO> GetNewSubItemAsync(int mainId);
        Task<SubItemEditAddDTO> GetSubItemByIdAsync(int id);
        Task AddSubItemAsync(int mainId, SubItemEditAddDTO subItemDTO, int fYear);
        Task EditSubItemAsync(int id, SubItemEditAddDTO subItemDTO);
        Task<int> IfPossibleToDeleteAsync(int subItemID);
    }
}
