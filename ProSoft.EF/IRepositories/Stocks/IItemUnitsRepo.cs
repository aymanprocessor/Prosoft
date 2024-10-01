using ProSoft.EF.Models.Stocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.IRepositories.Stocks
{
    public interface IItemUnitsRepo :IRepository<ItemUnit,int>
    {
        public IEnumerable<ItemUnit> GetItemsByStockTypeAndItemCode(int stockType,string itemCode);
        public ItemUnit? GetItemByStockTypeAndItemCodeAndUnitCodeAndBranchId(int stockType, string itemCode, int unitCode, int branchId);
    }
}
