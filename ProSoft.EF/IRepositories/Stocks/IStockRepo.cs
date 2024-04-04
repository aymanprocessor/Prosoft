using ProSoft.EF.DTOs.Stocks;
using ProSoft.EF.Models.Stocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.IRepositories.Stocks
{
    public interface IStockRepo: IRepository<Stock, int>
    {
        Task<List<StockViewDTO>> GetAllStocksAsync();
        Task<int> GetNewIdAsync();
        Task<StockEditAddDTO> GetEmptyStockAsync();
        Task<StockEditAddDTO> GetStockByIdAsync(int id);
    }
}
