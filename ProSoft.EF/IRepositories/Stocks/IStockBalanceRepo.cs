/// -------------------- Coded By Ayman Saad -------------------- //
/// 23-9-2024


using Microsoft.AspNetCore.Mvc.Rendering;

using ProSoft.EF.DTOs.Stocks;
using ProSoft.EF.Models.Stocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.IRepositories.Stocks
{
    public interface IStockBalanceRepo : IRepository<Stkbalance, int>
    {
        public void BulkInsert(IEnumerable<Stkbalance> entities);

    }
}
