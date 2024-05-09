using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Stocks
{
    public class StockEmpViewDTO
    {
        public string PermissionName { get; set; }
        public string StockName { get; set; }
        public int StockDef { get; set; }
        public int ShowPrice { get; set; }
        public string AccountStk { get; set; }
        public string AccountAcc { get; set; }
        //public string MainNameStk { get; set; }
        //public string SubNameStk { get; set; }
        //public string MainNameAcc { get; set; }
        //public string SubNameAcc { get; set; }
        public string JornalName { get; set; }
    }
}
