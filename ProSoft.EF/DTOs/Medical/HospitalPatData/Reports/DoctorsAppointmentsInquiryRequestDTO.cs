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
        public string DoctorName { get; set; } = string.Empty;
        
        public string SearchText { get; set; } = string.Empty;

    }


}
