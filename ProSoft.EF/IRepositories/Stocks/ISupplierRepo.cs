using ProSoft.EF.DTOs.Stocks;
using ProSoft.EF.Models.Stocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.IRepositories.Stocks
{
    public interface ISupplierRepo: IRepository<SupCode, int>
    {
        Task<string> GetNewIdAsync();
        Task<SupCodeEditAddDTO> GetEmptySupplierAsync();
        Task<SupCodeEditAddDTO> GetSupplierByIdAsync(int id);
    }
}
