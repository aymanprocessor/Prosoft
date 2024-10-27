using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Stocks.Report.TotalItemCards
{
    public class TotalItemCardQuantityReportDTO
    {
        public string ItemCode  { get; set; } = string.Empty;
        public string ItemName  { get; set; } = string.Empty;

        private decimal _carriedBalance;
        public decimal CarriedBalance
        {
            get => _carriedBalance;
            set => _carriedBalance = Math.Round(value, 0);
        }

        private decimal _totalOfInQuantity;
        public decimal TotalOfInQuantity
        {
            get => _totalOfInQuantity;
            set => _totalOfInQuantity = Math.Round(value, 0);
        }

        private decimal _totalOfOutQuantity;
        public decimal TotalOfOutQuantity
        {
            get => _totalOfOutQuantity;
            set => _totalOfOutQuantity = Math.Round(value, 0);
        }

        private decimal _currentBalance;
        public decimal CurrentBalance
        {
            get => _currentBalance;
            set => _currentBalance = Math.Round(value, 0);
        }

        private decimal _itemValue;
        public decimal ItemValue
        {
            get => _itemValue;
            set => _itemValue = Math.Round(value, 2);
        }

        private decimal _avgPrice;
        public decimal AvgPrice
        {
            get => _avgPrice;
            set => _avgPrice = Math.Round(value, 2);
        }


    }
}
