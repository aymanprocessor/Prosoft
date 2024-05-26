using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Stocks
{
    public class TransMasterViewDTO
    {
        public int TransMAsterID { get; set; }
        public long? DocNo { get; set; }
        public string PermissionName { get; set; }
        public string StockName { get; set; }
        public string UserName { get; set; }
    }
}
