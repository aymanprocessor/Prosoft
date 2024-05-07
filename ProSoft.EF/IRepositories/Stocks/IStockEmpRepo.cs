using ProSoft.EF.DTOs.Stocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.IRepositories.Stocks
{
    public interface IStockEmpRepo
    {
        //Task<StockEmpEditAddDTO> GetUserTransByIdAsync(int userCode, int gId);
        //Task<List<PermissionDefViewDTO>> GetPermissionsForUserAsync(int userCode);
        //Task<List<StockEmpEditAddDTO>> GetPermissionsForUserAsync(int userCode, int transType);
        Task<StockEmpEditAddDTO> GetEmptyStockTransAsync();
        Task UpdateUserTransAsync(int userCode, StockEmpEditAddDTO userStockDTO);
        Task SaveChangesAsync();
    }
}
