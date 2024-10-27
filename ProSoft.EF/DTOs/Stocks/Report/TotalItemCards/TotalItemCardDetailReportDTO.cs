using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Stocks.Report.TotalItemCards
{
    public class TotalItemCardDetailReportDTO
    {
        public string ItemCode { get; set; } = string.Empty;
        public string ItemName { get; set; } = string.Empty;

        private decimal _carriedBalance;
        public decimal CarriedBalance
        {
            get => _carriedBalance;
            set => _carriedBalance = Math.Round(value, 0);
        }

        // ----- In ----- //
        private decimal _inAdd;
        public decimal InAdd
        {
            get => _inAdd;
            set => _inAdd = Math.Round(value, 0);
        }

        private decimal _inReturn;
        public decimal InReturn
        {
            get => _inReturn;
            set => _inReturn = Math.Round(value, 0);
        }

        private decimal _inOther;
        public decimal InOther
        {
            get => _inOther;
            set => _inOther = Math.Round(value, 0);
        }

        private decimal _inTotal;
        public decimal InTotal
        {
            get => _inTotal;
            set => _inTotal = Math.Round(value, 0);
        }

        // ----- Out ----- //
        private decimal _outAdd;
        public decimal OutAdd
        {
            get => _outAdd;
            set => _outAdd = Math.Round(value, 0);
        }

        private decimal _outReturn;
        public decimal OutReturn
        {
            get => _outReturn;
            set => _outReturn = Math.Round(value, 0);
        }

        private decimal _outOther;
        public decimal OutOther
        {
            get => _outOther;
            set => _outOther = Math.Round(value, 0);
        }

        private decimal _outTotal;
        public decimal OutTotal
        {
            get => _outTotal;
            set => _outTotal = Math.Round(value, 0);
        }

        private decimal _currentBalance;
        public decimal CurrentBalance
        {
            get => _currentBalance;
            set => _currentBalance = Math.Round(value, 0);
        }
    }

}
