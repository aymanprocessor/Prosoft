using ProSoft.EF.DTOs.Accounts;
using ProSoft.EF.DTOs.Shared;
using ProSoft.EF.DTOs.Stocks;
using ProSoft.EF.Models.Stocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.IRepositories.Stocks
{
    public interface IStockEmpRepo: IRepository<StockEmp, int>
    {
        Task<List<StockEmpViewDTO>> GetStockTransForUserAsync(int userCode);
        Task<StockEmpEditAddDTO> GetEmptyStockTransAsync(int userCode);
        Task<List<AccSubCodeDTO>> GetSubCodesFromAccAsync(string mainAccCode);
        Task<List<PermissionDefViewDTO>> GetUserStockPermissionsAsync(int userCode, int stockCode);
        Task<List<PermissionDefViewDTO>> GetUserStockPermissionsForEditAsync(int userCode, int stockCode, int transType);
        Task<StockEmpEditAddDTO> GetStockTransByIdAsync(int id);
        Task AddStockTransAsync(StockEmpEditAddDTO stockTransDTO);
        Task UpdateStockTransAsync(int id, StockEmpEditAddDTO userStockDTO);
        Task DeleteStockTransAsync(int id);
    }
}
