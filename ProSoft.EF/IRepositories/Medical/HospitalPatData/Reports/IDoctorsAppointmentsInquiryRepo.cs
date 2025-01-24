using ProSoft.EF.DTOs.Medical.HospitalPatData.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.IRepositories.Medical.HospitalPatData.Reports
{
    public interface IDoctorsAppointmentsInquiryRepo
    {
        Task<IEnumerable<DoctorsAppointmentsInquiryReportDTO>> GetDoctorsAppointments(int BranchId, int DoctorId, DateTime FromDate, DateTime ToDate);
    }
}
