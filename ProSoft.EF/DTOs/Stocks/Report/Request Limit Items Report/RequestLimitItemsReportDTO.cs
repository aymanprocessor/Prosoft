using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Stocks.Report.Request_Limit_Items_Report
{
    public class RequestLimitItemsReportDTO
    {
        public string StockName { get; set; } = string.Empty;
        public string ItemCode { get; set; } = string.Empty;
        public string ItemName { get; set; } = string.Empty;
        private decimal _currentBalance = 0.0m;
        public decimal CurrentBalance
        {
            get => Math.Floor(_currentBalance);
            set => _currentBalance = value;
        }

        private decimal _requestLimit = 0.0m;
        public decimal RequestLimit
        {
            get => Math.Floor(_requestLimit);
            set => _requestLimit = value;
        }
        
        private decimal _maxRequestLimit = 0.0m;
        public decimal MaxRequestLimit
        {
            get => Math.Floor(_maxRequestLimit);
            set => _maxRequestLimit = value;
        }

        private decimal _minRequestLimit = 0.0m;
        public decimal MinRequestLimit
        {
            get => Math.Floor(_minRequestLimit);
            set => _minRequestLimit = value;
        }


    }
}
