using Microsoft.EntityFrameworkCore;
using ProSoft.EF.DbContext;
using ProSoft.EF.DTOs.Medical.HospitalPatData.Reports;
using ProSoft.EF.IRepositories.Medical.HospitalPatData.Reports;
using ProSoft.Shared.Enums;
using ProSoft.UI.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.Core.Repositories.Medical.HospitalPatData.Reports
{
    
    public class ClinicsAppointmentsInquiryRepo:IClinicsAppointmentsInquiryRepo
    {
        private readonly AppDbContext _context;

        public ClinicsAppointmentsInquiryRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ClinicsAppointmentsInquiryReportDTO>> GetClinicsAppointments(int branchId, int doctorId, DateTime Date)
        {
            var query = from ct in _context.ClinicTrans
                        join p in _context.Pats on new {x1 =  ct.PatId ,x2 = ct.BranchId} equals new { x1 = (int?)p.PatId ,x2 = (int?)p.BranchId}
                        join d in _context.Doctors on new { x1 = ct.DrCode, x2 = ct.BranchId } equals new { x1 = (int?)d.DrId, x2 = (int?)d.BranchId }
                        join c in _context.Companies on new { x1 = ct.CompId, x2 = ct.BranchId } equals new { x1 = (int?)c.CompId, x2 = c.BranchId }
                        join sc in _context.ServiceClinics on new { x1 = ct.ServId, x2 = ct.BranchId ,x3 = ct.ClinicId,x4 = ct.SClinicId} equals new { x1 = (int?)sc.ServId, x2 = sc.BranchId ,x3 = sc.ClinicId,x4 = sc.SClinicId}
                        where ct.BranchId == branchId && ct.DrCode == doctorId && ct.EntryDate == Date && ct.CheckIdCancel == 2
                        select new ClinicsAppointmentsInquiryReportDTO
                        {
                            PatientName = p.PatName,
                            CompanyName = c.CompName,
                            Time = ct.PatAdDate!=null? ct.PatAdDate!.Value.ToString(GlobalConstants.FormatTime): "-",
                            EntryDate = ct.EntryDate != null?ct.EntryDate!.Value.ToString(GlobalConstants.FormatDate):"-",
                            ServiceName = sc.ServDesc,
                            FileNumber = (int?)p.SheetNo,
                            PatientMobile = p.PatMobile
                        };

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<DoctorTimeSheetReportDTO>> GetDoctorTimeSheet(int branchId, int doctorId)
        {
            var query =from dts in _context.Drtimsheets
                       join d in _context.Doctors on new { x1 = dts.DrId, x2 = dts.BranchId } equals new { x1 = (int?)d.DrId, x2 = (int?)d.BranchId }
                       where dts.BranchId == branchId && dts.DrId == doctorId
                       select new DoctorTimeSheetReportDTO
                       {
                           Day =((Day)dts.DayNumber).ToString()??"-",
                           ExPeriod =((Period)dts.ExPeriod).ToString()??"-" ,
                           TimeFrom = dts.Timfrom != null ? dts.Timfrom.Value.ToString(GlobalConstants.FormatTime) : "-",
                           TimeTo = dts.Timto != null ? dts.Timto.Value.ToString(GlobalConstants.FormatTime) : "-"
                       };

            return await query.ToListAsync();
        }
    }
}
