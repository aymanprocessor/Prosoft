using Microsoft.EntityFrameworkCore;
using ProSoft.EF.DbContext;
using ProSoft.EF.DTOs.Medical.HospitalPatData.Reports;
using ProSoft.EF.IRepositories.Medical.HospitalPatData.Reports;
using ProSoft.UI.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProSoft.Core.Repositories.Medical.HospitalPatData.Reports
{
    public class DoctorsAppointmentsInquiryRepo:IDoctorsAppointmentsInquiryRepo
    {
        private readonly AppDbContext _context;

        public DoctorsAppointmentsInquiryRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DoctorsAppointmentsInquiryReportDTO>> GetDoctorsAppointments(int BranchId,int DoctorId,DateTime FromDate,DateTime ToDate)
        {
            var clinicsTrans = from ct in _context.ClinicTrans
                               join drSend in _context.Doctors on new { x1 = ct.BranchId, x2 = ct.DrSend } equals new { x1 = (int?)drSend.BranchId, x2 = (int?)drSend.DrId }
                               join u in _context.Users on new { ct.BranchId, x2 = ct.UserCodeCreate } equals new { u.BranchId, x2 = (int?)u.UserCode }
                               join sc in _context.ServiceClinics on new { x1 = ct.ClinicId, x2 = ct.SClinicId, x3 = ct.ServId } equals new { x1 = sc.ClinicId, x2 = sc.SClinicId, x3 = (int?)sc.ServId }
                               join p in _context.Pats on new { x1 = ct.PatId, x2 = ct.BranchId } equals new { x1 = (int?)p.PatId, x2 = (int?)p.BranchId }
                               join drCode in _context.Doctors on new { x1 = ct.DrCode, x2 = ct.BranchId } equals new { x1 = (int?)drCode.DrId, x2 = (int?)drCode.BranchId }
                               join c in _context.Companies on new { x1 = ct.CompId, x2 = ct.BranchId } equals new { x1 = (int?)c.CompId, x2 = c.BranchId }
                               join pa in _context.PatAdmissions on new { x1 = ct.BranchId, x2 = ct.PatId, x3 = ct.MasterId, x4 = ct.ExYear, x5 = ct.ExDate } equals new { x1 = (int?)pa.BranchId, x2 = pa.PatId, x3 = (int?)pa.MasterId, x4 = (int?)pa.ExYear, x5 = pa.PatAdDate }
                               join r in _context.Regions on new { x1 = ct.SendFr, x2 = ct.BranchId } equals new { x1 = (int?) r.RegionCode, x2 = r.BranchId }
                               where ct.BranchId == BranchId && ct.DrCode == DoctorId && ct.ExDate >= FromDate && ct.ExDate <= ToDate
                               select new DoctorsAppointmentsInquiryReportDTO
                               {
                                   PatName = p.PatName,
                                   CompanyName = c.CompName,
                                   DoctorName = drCode.DrDesc,
                                   PatId=p.PatId,
                                   ServiceName = sc.ServDesc,
                                   PatMobile = p.PatMobile,
                                   Note = pa.Note,
                                   Time = ct.ExDate != null? ct.ExDate!.Value.ToString(GlobalConstants.FormatTime) : "-",
                                   PatientValue = ct.PatientValue,
                                   CompanyValue = ct.CompValue,
                                   ServiceValue = ct.ValueService,
                                   DiscountValue = ct.DiscountVal,
                                   DoctorSendName = drSend.DrDesc,
                                   Date = pa.PatAdDate != null ? pa.PatAdDate!.Value.ToString(GlobalConstants.FormatDate):"-",
                                   KnowUsFrom = ct.KnowUsFr,
                                  UserName = u.UserName,
                                  SendFrom = r.RegionDesc
                               };

            return await clinicsTrans.ToListAsync();
        }   
    }
}
