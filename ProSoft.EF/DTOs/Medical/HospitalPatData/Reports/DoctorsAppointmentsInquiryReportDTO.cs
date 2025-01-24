using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Medical.HospitalPatData.Reports
{
    public class DoctorsAppointmentsInquiryReportDTO
    {
        public int? PatId { get; set; }
        public string? PatMobile { get; set; }
        public string? PatName { get; set; }
        public string? CompanyName { get; set; }
        public string? ServiceName { get; set; }
        public string? Time { get; set; }
        public string? Date { get; set; }
        public decimal? ServiceValue { get; set; }
        public decimal? PatientValue { get; set; }
        public decimal? DiscountValue { get; set; }
        public decimal? CompanyValue { get; set; }
        public string? DoctorName { get; set; }
        public string? DoctorSendName { get; set; }
        public string? Note { get; set; }
        public int? KnowUsFrom { get; set; }
        public string? UserName { get; set; }
        public string? SendFrom { get; set; }



    }
}
