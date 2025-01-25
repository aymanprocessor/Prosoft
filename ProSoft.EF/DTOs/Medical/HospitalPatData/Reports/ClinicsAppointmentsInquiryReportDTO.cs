using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Medical.HospitalPatData.Reports
{
    public class ClinicsAppointmentsInquiryReportDTO
    {
        public string? PatientName { get; set; }
        public string? CompanyName { get; set; }
        public string? Time { get; set; }
        public string? EntryDate { get; set; }
        public string? ServiceName { get; set; }
        public int? FileNumber { get; set; }
        public string? PatientMobile { get; set; }


    }
}
