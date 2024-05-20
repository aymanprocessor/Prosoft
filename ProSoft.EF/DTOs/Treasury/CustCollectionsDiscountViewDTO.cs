using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Treasury
{
    public class CustCollectionsDiscountViewDTO
    {
        public decimal? DiscPrc { get; set; }
        public decimal? DiscValue { get; set; }
        public string? MainCode { get; set; }
        public string? SubCode { get; set; }
        public string CodeDesc { get; set; }//for maindec/subdec

    }
}
