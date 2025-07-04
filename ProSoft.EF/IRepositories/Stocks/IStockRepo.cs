﻿using Microsoft.AspNetCore.Mvc.Rendering;
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
        // -------------------- Coded By Ayman Saad -------------------- //

         IEnumerable<SelectListItem> GetAllStockAsEnumerable();
        Task<List<StockViewDTO>> GetActiveStocksForUserAsync(int userCode);

        Task<List<Stock>> GetStocksDependOnFromStock(int stockId);
        // -------------------- Coded By Ayman Saad -------------------- //

        Task<List<StockViewDTO>> GetAllStocksAsync();
        Task<int> GetNewIdAsync();
        Task<StockEditAddDTO> GetEmptyStockAsync();
        Task<StockEditAddDTO> GetStockByIdAsync(int id);
    }
}
