using ProSoft.EF.DTOs.Stocks;
using ProSoft.EF.Models.Stocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.IRepositories.Stocks
{
    public interface ISubItemDtlRepo : IRepository<SubItemDtl, int>
    {
        Task<List<SubItemDtlDTO>> GetSubItemDetailsAsync(int subItemID);
        Task<SubItemDtlDTO> GetNewSubItemDtlAsync(int subItemID);
        Task<SubItemDtlDTO> GetSubItemDtlByIdAsync(int subItemDtlID);
        Task AddSubItemDtlAsync(int subItemID, SubItemDtlDTO subItemDtlDTO);
        Task EditSubItemDtlAsync(int subItemDtlID, SubItemDtlDTO subItemDtlDTO);
    }
}
