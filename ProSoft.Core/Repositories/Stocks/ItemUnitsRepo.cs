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
    }
}