using ProSoft.EF.DTOs.Stocks;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.Core.Custom_Classes
{
    public class StockViewDTOComparer : IEqualityComparer<StockViewDTO>
    {
        public bool Equals(StockViewDTO? x, StockViewDTO? y)
        {
            return x.Stkcod == y.Stkcod;
        }

        public int GetHashCode([DisallowNull] StockViewDTO obj)
        {
            throw new NotImplementedException();
        }
    }
}
