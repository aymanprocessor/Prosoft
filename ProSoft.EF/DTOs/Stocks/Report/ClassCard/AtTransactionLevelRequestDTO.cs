using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Stocks.Report.ClassCard
{
    public class AtTransactionLevelRequestDTO
    {




        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int BranchId { get; set; }
        public int StockId { get; set; }
        public string ItemMaster { get; set; }
        public int UnitCode { get; set; }
        public List<StockViewDTO>? stocks { get; set; }
        public List<SubItemViewDTO>? subItems { get; set; }


    }
}
