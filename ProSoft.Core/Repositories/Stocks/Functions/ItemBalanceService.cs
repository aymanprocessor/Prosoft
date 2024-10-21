using ProSoft.EF.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.Core.Repositories.Stocks.Functions
{
    public class ItemBalanceService
    {
        private readonly AppDbContext _dbContext;

        public ItemBalanceService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public decimal GetItemBalance(int branchId, int stockId, DateTime fromDate, string itemCode)
        {
            decimal itemBalance = 0;

            try
            {
                // Query to calculate the balance
                var query = from s in _dbContext.Stkbalances
                            join si in _dbContext.SubItems on s.ItemCode equals si.ItemCode
                            join stock in _dbContext.Stocks on new { y = s.BranchId, x = (int)s.Stkcod } equals new { y = stock.BranchId, x = stock.Stkcod }
                            where s.BranchId == branchId &&
                                  s.Stkcod == stockId &&
                                  s.ItemCode == itemCode &&
                                  s.FYear == fromDate.Year
                            group s by new { si.ItemCode, si.SubName, s.Stkcod, stock.Stknam } into g
                            select new
                            {
                                OpnQty = g.Sum(x => x.QtyStart * (x.UnitCode == g.First().UnitCode ? 1 : 1)),
                                OpnVal = g.Sum(x => x.ItemPrice2),
                                AddPrice = g.FirstOrDefault().ItemPrice // Simplified
                            };

                var result = query.FirstOrDefault();

                if (result != null)
                {
                    // Calculate the balance by adding and subtracting quantities
                    itemBalance = (decimal)result.OpnQty + (decimal)result.AddPrice - (decimal)result.OpnVal;
                }
            }
            catch (Exception ex)
            {
                // Log the exception (Logging mechanism required)
                throw new Exception("Error calculating item balance", ex);
            }

            return itemBalance;
        }
    }
}
