using ProSoft.EF.DTOs.Accounts;
using ProSoft.EF.DTOs.Stocks;
using ProSoft.EF.Models.Stocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.IRepositories.Stocks
{
    public interface IStockEmpRepo
    {
        Task<StockEmpEditAddDTO> GetStockTransByIdAsync(int userCode, int permit_uniqueType);
        Task<List<StockEmpViewDTO>> GetStockTransForUserAsync(int userCode);
        Task<StockEmpEditAddDTO> GetEmptyStockTransAsync(int userCode);
        Task<List<AccSubCodeDTO>> GetSubCodesFromAccAsync(string mainAccCode);
        Task AddStockTransAsync(StockEmp userStock);
        Task UpdateStockTransAsync(int userCode, int transType, StockEmpEditAddDTO userStockDTO);
        Task DeleteStockTransAsync(int userCode, int transType);
        Task SaveChangesAsync();
    }
}
