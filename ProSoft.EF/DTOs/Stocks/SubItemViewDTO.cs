using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Stocks
{
    public class SubItemViewDTO
    {
        public int SubId { get; set; }
        public string SubName { get; set; }
        public string ItemCode { get; set; }
        public string CodeAndName { get; set; }
    }
}
