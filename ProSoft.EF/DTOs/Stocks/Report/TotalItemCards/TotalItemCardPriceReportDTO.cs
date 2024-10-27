using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Stocks.Report.TotalItemCards
{
    public class TotalItemCardPriceReportDTO
    {
        public string ItemCode { get; set; } = string.Empty;
        public string ItemName { get; set; } = string.Empty;

        // ---- Balance Before ---- //
        private decimal _totalCarriedBalanceQty;
        public decimal TotalCarriedBalanceQty
        {
            get => _totalCarriedBalanceQty;
            set => _totalCarriedBalanceQty = Math.Round(value, 2);
        }

        private decimal _totalCarriedBalanceValue;
        public decimal TotalCarriedBalanceValue
        {
            get => _totalCarriedBalanceValue;
            set => _totalCarriedBalanceValue = Math.Round(value, 2);
        }

        private decimal _totalCarriedBalanceAvgPrice;
        public decimal TotalCarriedBalanceAvgPrice
        {
            get => _totalCarriedBalanceAvgPrice;
            set => _totalCarriedBalanceAvgPrice = Math.Round(value, 2);
        }

        // ---- Total Of In ---- //
        private decimal _totalInQty;
        public decimal TotalInQty
        {
            get => _totalInQty;
            set => _totalInQty = Math.Round(value, 2);
        }

        private decimal _totalInValue;
        public decimal TotalInValue
        {
            get => _totalInValue;
            set => _totalInValue = Math.Round(value, 2);
        }

        private decimal _totalInAvgPrice;
        public decimal TotalInAvgPrice
        {
            get => _totalInAvgPrice;
            set => _totalInAvgPrice = Math.Round(value, 2);
        }

        // ---- Total Of Out ---- //
        private decimal _totalOutQty;
        public decimal TotalOutQty
        {
            get => _totalOutQty;
            set => _totalOutQty = Math.Round(value, 2);
        }

        private decimal _totalOutValue;
        public decimal TotalOutValue
        {
            get => _totalOutValue;
            set => _totalOutValue = Math.Round(value, 2);
        }

        private decimal _totalOutAvgPrice;
        public decimal TotalOutAvgPrice
        {
            get => _totalOutAvgPrice;
            set => _totalOutAvgPrice = Math.Round(value, 2);
        }

        // ---- Current Balance ---- //
        private decimal _totalCurrentBalanceQty;
        public decimal TotalCurrentBalanceQty
        {
            get => _totalCurrentBalanceQty;
            set => _totalCurrentBalanceQty = Math.Round(value, 2);
        }

        private decimal _totalCurrentBalanceValue;
        public decimal TotalCurrentBalanceValue
        {
            get => _totalCurrentBalanceValue;
            set => _totalCurrentBalanceValue = Math.Round(value, 2);
        }

        private decimal _totalCurrentBalanceAvgPrice;
        public decimal TotalCurrentBalanceAvgPrice
        {
            get => _totalCurrentBalanceAvgPrice;
            set => _totalCurrentBalanceAvgPrice = Math.Round(value, 2);
        }
    }

}
