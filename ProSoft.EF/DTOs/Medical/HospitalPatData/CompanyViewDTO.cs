using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Medical.HospitalPatData
{
    public class CompanyViewDTO
    {
        public int CompId { get; set; }
        public string CompName { get; set; }
        public int? PLId { get; set; }
        public string? PlDesc { get; set; }
        public string? TaxNo { get; set; }
        public string? SubCode { get; set; }
        public string? MainCode { get; set; }
        public decimal? StampPer { get; set; }
        public decimal? Stamp { get; set; }
        public decimal? TaxPer { get; set; }
        public string KName { get; set; }
        public string? EInvMainFlg { get; set; }
        public string? ComAdd { get; set; }
        public string? CompTelephone { get; set; }
        public string? CompFax { get; set; }
        public string? MedicalManager { get; set; }
        public string? InsurancePeriod { get; set; }
        public int? CompanyOnOff { get; set; }
    }
}
