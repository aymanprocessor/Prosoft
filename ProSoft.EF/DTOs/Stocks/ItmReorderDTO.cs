using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Stocks
{
    public class ItmReorderDTO
    {
        public int ItemReorderID { get; set; }

        public int BranchId { get; set; }

        public DateTime FDate { get; set; }

        public DateTime TDate { get; set; }

        public int StoreId { get; set; }

        public string ItemCd { get; set; }

        public string ItemName { get; set; }

        public decimal ReordQty { get; set; }

        public decimal MinQty { get; set; }

        public decimal MaxQty { get; set; }
    }
}
