using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Stocks
{
    public class StockEditAddDTO
    {
        [DisplayName("Stock ID")]
        public int Stkcod { get; set; }

        [DisplayName("Stock Name")]
        public string Stknam { get; set; }

        [DisplayName("Stock Type")]
        public int Flag1 { get; set; }

        [DisplayName("Branch")]
        public int BranchId { get; set; }

        [DisplayName("System Code")]
        public int StockType { get; set; }

        public int? StkDefult { get; set; }

        [DisplayName("Stock Kind")]
        public int StockPurchOnshelf { get; set; }

        [DisplayName("Jornal Code")]
        public string JornalCode { get; set; }

        [DisplayName("Is Active")]
        public int StkOnOff { get; set; }
    }
}
