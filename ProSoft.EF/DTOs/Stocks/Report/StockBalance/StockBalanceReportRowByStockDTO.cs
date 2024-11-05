using System;

namespace ProSoft.EF.DTOs.Stocks.Report.StockBalance
{
    public class StockBalanceReportRowByStockDTO
    {
        public string? ItemCode { get; set; }
        public string? ItemName { get; set; }
        public string? StockName { get; set; }

        private decimal _carriedBalanceItemQty;
        private decimal _carriedBalanceItemPrice;
        private decimal _carriedBalanceItemValue;

        private decimal _inItemQty;
        private decimal _inItemPrice;
        private decimal _inItemValue;

        private decimal _outItemQty;
        private decimal _outItemPrice;
        private decimal _outItemValue;

        private decimal _currentBalanceItemQty;
        private decimal _currentBalanceItemPrice;
        private decimal _currentBalanceItemValue;

        // Properties with Qty - no fraction
        public decimal CarriedBalanceItemQty
        {
            get => Math.Floor(_carriedBalanceItemQty);
            set => _carriedBalanceItemQty = value;
        }

        public decimal InItemQty
        {
            get => Math.Floor(_inItemQty);
            set => _inItemQty = value;
        }

        public decimal OutItemQty
        {
            get => Math.Floor(_outItemQty);
            set => _outItemQty = value;
        }

        public decimal CurrentBalanceItemQty
        {
            get => Math.Floor(_currentBalanceItemQty);
            set => _currentBalanceItemQty = value;
        }

        // Properties with Price and Value - round to 2 decimal places
        public decimal CarriedBalanceItemPrice
        {
            get => Math.Round(_carriedBalanceItemPrice, 2);
            set => _carriedBalanceItemPrice = value;
        }

        public decimal CarriedBalanceItemValue
        {
            get => Math.Round(_carriedBalanceItemValue, 2);
            set => _carriedBalanceItemValue = value;
        }

        public decimal InItemPrice
        {
            get => Math.Round(_inItemPrice, 2);
            set => _inItemPrice = value;
        }

        public decimal InItemValue
        {
            get => Math.Round(_inItemValue, 2);
            set => _inItemValue = value;
        }

        public decimal OutItemPrice
        {
            get => Math.Round(_outItemPrice, 2);
            set => _outItemPrice = value;
        }

        public decimal OutItemValue
        {
            get => Math.Round(_outItemValue, 2);
            set => _outItemValue = value;
        }

        public decimal CurrentBalanceItemPrice
        {
            get => Math.Round(_currentBalanceItemPrice, 2);
            set => _currentBalanceItemPrice = value;
        }

        public decimal CurrentBalanceItemValue
        {
            get => Math.Round(_currentBalanceItemValue, 2);
            set => _currentBalanceItemValue = value;
        }
    }
}
