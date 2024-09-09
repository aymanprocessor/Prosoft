using ProSoft.EF.DTOs.Stocks;
using ProSoft.EF.Models.Stocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.IRepositories.Stocks
{
    public interface IStockEmpFlagRepo : IRepository<StockEmpFlag, int>
    {
        // ------------------- Coded By Ayman Saad ------------------- //

        public IEnumerable<StockEmpFlag> GetStockEmpFlags();
        public StockEmpFlag? GetById(int userId, int stkcod, int branchId, int kId);
        public  Task Add(StockEmpFlagEditAddDTO model);
        public  Task Delete(int userId, int stkcod, int branchId, int kId);
        public  Task<IGeneralResponse<StockEmpFlag>> Update(int userId, int stkcod, int branchId, int kId, StockEmpFlagEditAddDTO model);

        // ------------------- Coded By Ayman Saad ------------------- //
    }

    
}
