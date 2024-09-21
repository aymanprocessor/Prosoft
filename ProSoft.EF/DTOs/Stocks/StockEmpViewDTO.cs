using ProSoft.EF.Models.Stocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Stocks
{
    public class StockEmpViewDTO
    {
        public int StockEmpID { get; set; }
        public string PermissionName { get; set; }
        public int TransType { get; set; }
        public int Stkcod { get; set; }
        public string StockName { get; set; }
        public int StockDef { get; set; }
        public int ShowPrice { get; set; }
        public string AccountStk { get; set; }
        public string AccountAcc { get; set; }
        public string JornalName { get; set; }
    }
}
