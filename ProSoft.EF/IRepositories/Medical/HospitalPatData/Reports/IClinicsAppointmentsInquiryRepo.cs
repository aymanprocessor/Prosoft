using ProSoft.EF.DTOs.Medical.HospitalPatData.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.EF.IRepositories.Medical.HospitalPatData.Reports
{
    public interface IClinicsAppointmentsInquiryRepo
    {
        Task<IEnumerable<ClinicsAppointmentsInquiryReportDTO>> GetClinicsAppointments(int branchId, int doctorId, DateTime Date);
        Task<IEnumerable<DoctorTimeSheetReportDTO>> GetDoctorTimeSheet(int branchId, int doctorId);
    }
}
