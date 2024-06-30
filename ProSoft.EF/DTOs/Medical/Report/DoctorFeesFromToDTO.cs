using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Medical.Report
{
    public class DoctorFeesFromToDTO
    {
        public string? DrDesc { get; set; }
        public string RegionDesc { get; set; }
        public int PatId { get; set; }
        public string PatName { get; set; }
        public string CompanyName { get; set; }
        public string ServDesc { get; set; }
        public int? MainInvNo { get; set; } //patadmission
        public DateTime? PatAdDate { get; set; } //patadmission
        public decimal? DrDueVal { get; set; } //clinic Trans
        public decimal? DrTax { get; set; } //patadmission
        public decimal? DrSendVal { get; set; } //clinic Trans




    }
}
