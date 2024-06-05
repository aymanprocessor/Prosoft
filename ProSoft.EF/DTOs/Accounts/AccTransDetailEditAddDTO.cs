using ProSoft.EF.DTOs.Stocks;
using ProSoft.EF.DTOs.Treasury;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Accounts
{
    public class AccTransDetailEditAddDTO
    {
        public int? TransDtlId { get; set; }
        public decimal? ValDep { get; set; }
        public decimal? ValCredit { get; set; }
        public decimal? ValDepCur { get; set; }
        public decimal? ValCreditCur { get; set; }
        public string? DocNo { get; set; }
        public DateTime? DocDate { get; set; }
        public string? LineDesc { get; set; }
        public int TransId { get; set; }
        public string? MainCode { get; set; }
        public string? SubCode { get; set; }
        public string? CostCenterCode { get; set; }


        //Lists
        public List<CostCenterViewDTO>? CostCenters { get; set; }
        public List<AccMainCodeDTO>? MainAccCodes { get; set; }
        public List<AccSubCodeDTO>? SubAccCodes { get; set; }
    }
}
