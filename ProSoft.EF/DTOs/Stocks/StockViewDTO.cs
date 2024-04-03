using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace ProSoft.EF.DTOs.Stocks
{
    public class StockViewDTO
    {
        public int Stkcod { get; set; }

        public string Stknam { get; set; }

        // => Flag1
        public string StockTypeName { get; set; }

        public string BranchName { get; set; }

        public int StockType { get; set; }

        public int StkDefult { get; set; }

        public int StockPurchOnshelf { get; set; }

        public string JornalCode { get; set; }

        public int StkOnOff { get; set; }
    }
}
