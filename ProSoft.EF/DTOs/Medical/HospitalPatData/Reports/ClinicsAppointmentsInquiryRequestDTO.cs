using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.DTOs.Medical.HospitalPatData.Reports
{
    public class ClinicsAppointmentsInquiryRequestDTO
    {
        public int DoctorId { get; set; }
        public DateTime Date { get; set; }
    }
}
