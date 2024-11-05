using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Stocks.Report.StockBalance
{
     public class StockBalanceReportRowByItemDTO
    {
        public string? ItemCode { get; set; }
        public string? ItemName { get; set; }
        public string? StockName { get; set; }

        private decimal? _carriedBalanceItemQty = 0;
        public decimal? CarriedBalanceItemQty
        {
            get => _carriedBalanceItemQty;
            set => _carriedBalanceItemQty = Math.Round(value ?? 0, 0);
        }

        private decimal? _carriedBalanceItemPrice = 0;
        public decimal? CarriedBalanceItemPrice
        {
            get => _carriedBalanceItemPrice;
            set => _carriedBalanceItemPrice = Math.Round(value ?? 0, 2);
        }

        private decimal? _carriedBalanceItemValue = 0;
        public decimal? CarriedBalanceItemValue
        {
            get => _carriedBalanceItemValue;
            set => _carriedBalanceItemValue = Math.Round(value ?? 0, 2);
        }

        private decimal? _inItemQty = 0;
        public decimal? InItemQty
        {
            get => _inItemQty;
            set => _inItemQty = Math.Round(value ?? 0, 0);
        }

        private decimal? _inItemPrice = 0;
        public decimal? InItemPrice
        {
            get => _inItemPrice;
            set => _inItemPrice = Math.Round(value ?? 0, 2);
        }

        private decimal? _inItemValue = 0;
        public decimal? InItemValue
        {
            get => _inItemValue;
            set => _inItemValue = Math.Round(value ?? 0, 2);
        }

        private decimal? _outItemQty = 0;
        public decimal? OutItemQty
        {
            get => _outItemQty;
            set => _outItemQty = Math.Round(value ?? 0, 0);
        }

        private decimal? _outItemPrice = 0;
        public decimal? OutItemPrice
        {
            get => _outItemPrice;
            set => _outItemPrice = Math.Round(value ?? 0, 2);
        }

        private decimal? _outItemValue = 0;
        public decimal? OutItemValue
        {
            get => _outItemValue;
            set => _outItemValue = Math.Round(value ?? 0, 2);
        }

        private decimal? _currentBalanceItemQty = 0;
        public decimal? CurrentBalanceItemQty
        {
            get => _currentBalanceItemQty;
            set => _currentBalanceItemQty = Math.Round(value ?? 0, 0);
        }

        private decimal? _currentBalanceItemPrice = 0;
        public decimal? CurrentBalanceItemPrice
        {
            get => _currentBalanceItemPrice;
            set => _currentBalanceItemPrice = Math.Round(value ?? 0, 2);
        }

        private decimal? _currentBalanceItemValue = 0;
        public decimal? CurrentBalanceItemValue
        {
            get => _currentBalanceItemValue;
            set => _currentBalanceItemValue = Math.Round(value ?? 0, 2);
        }
    }
}
