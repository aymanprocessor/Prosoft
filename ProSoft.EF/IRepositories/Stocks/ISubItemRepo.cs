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
        Task<List<SubItemViewDTO>> GetSubItemsByMainIdAsync(int mainId);
    }
}
