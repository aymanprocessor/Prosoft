using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Medical.HospitalPatData.Reports
{
    public class DoctorsAppointmentsInquiryRequestDTO
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int DoctorId { get; set; }
        
        public string? SearchText { get; set; } = string.Empty;

    }


}
