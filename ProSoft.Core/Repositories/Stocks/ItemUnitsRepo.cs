using Microsoft.EntityFrameworkCore;
using ProSoft.EF.DbContext;
using ProSoft.EF.IRepositories.Stocks;
using ProSoft.EF.Models.Stocks;

namespace ProSoft.Core.Repositories.Stocks
{
    public class ItemUnitsRepo : Repository<ItemUnit, int>, IItemUnitsRepo
    {
        public ItemUnitsRepo(AppDbContext Context) : base(Context)
        {
        }

        public  IEnumerable<ItemUnit> GetItemsByStockTypeAndItemCode(int stockType, string itemCode)
        {
            return  _Context.ItemUnits
                .Include(i => i.UnitCodee)
                .Where(i => i.ItemCode == itemCode && i.Flag1 == stockType).ToList();
        }
    }
}