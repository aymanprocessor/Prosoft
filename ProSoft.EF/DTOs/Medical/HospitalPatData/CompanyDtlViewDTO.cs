using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Medical.HospitalPatData
{
    public class CompanyDtlViewDTO
    {
        public int CompIdDtl { get; set; }
        public string CompNameDtl { get; set; }
        public string TaxNo { get; set; }
        public string SubCode { get; set; }
        public string MainCode { get; set; }
        public int EinvType { get; set; }
        public string WinvItemsFlg { get; set; }

    }
}
