using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Stocks.Report.StockBalance
{
    public class StockBalanceReportColumnDTO
    {
        // Properties
        public int? BranchId { get; set; }
        public string? ItemCode { get; set; }
        public string? ItemName { get; set; }
        public string? StockName { get; set; }

        private decimal? _itemQty = 0;
        public decimal? ItemQty
        {
            get => _itemQty;
            set => _itemQty = value.HasValue ? Math.Floor(value.Value) : 0;
        }

        private decimal? _itemPrice = 0;
        public decimal? ItemPrice
        {
            get => _itemPrice;
            set => _itemPrice = value.HasValue ? Math.Round(value.Value, 2) : 0;
        }

        private decimal? _itemValue = 0;
        public decimal? ItemValue
        {
            get => _itemValue;
            set => _itemValue = value.HasValue ? Math.Round(value.Value, 2) : 0;
        }

        private decimal? _sumItemQty = 0;
        public decimal? SumItemQty
        {
            get => _sumItemQty;
            set => _sumItemQty = value.HasValue ? Math.Floor(value.Value) : 0;
        }

        private decimal? _sumItemValue = 0;
        public decimal? SumItemValue
        {
            get => _sumItemValue;
            set => _sumItemValue = value.HasValue ? Math.Round(value.Value, 2) : 0;
        }
    }
}
