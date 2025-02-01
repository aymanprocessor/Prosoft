using Microsoft.AspNetCore.Mvc;
using ProSoft.EF.IRepositories.Medical.HospitalPatData.Reports;
using ProSoft.EF.IRepositories.Medical.HospitalPatData;
using ProSoft.UI.Global;
using ProSoft.EF.DTOs.Medical.HospitalPatData.Reports;
using Microsoft.AspNetCore.Authorization;
using FastReport.Web;
using ProSoft.Core.Repositories.Medical.HospitalPatData.Reports;

namespace ProSoft.UI.Areas.Medical.Controllers
{
    [Area(nameof(Medical))]
    [Authorize]
    public class ClinicsAppointmentsInquiryController : Controller
    {
        private readonly IDoctorRepo _doctorRepo;
        private readonly IClinicsAppointmentsInquiryRepo _clinicsAppointmentsInquiryRepo;
        private readonly ICurrentUserService _currentUserService;

        public ClinicsAppointmentsInquiryController(ICurrentUserService currentUserService, IDoctorRepo doctorRepo, IClinicsAppointmentsInquiryRepo clinicsAppointmentsInquiryRepo)
        {
            _currentUserService = currentUserService;
            _doctorRepo = doctorRepo;
            _clinicsAppointmentsInquiryRepo = clinicsAppointmentsInquiryRepo;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Doctors = (await _doctorRepo.GetAllAsync()).ToSelectListItem(d => d.DrDesc, d => d.DrId.ToString());

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(ClinicsAppointmentsInquiryRequestDTO model)
        {
            ViewBag.Doctors = (await _doctorRepo.GetAllAsync()).ToSelectListItem(d => d.DrDesc, d => d.DrId.ToString());

            if(!ModelState.IsValid)
            {
                return View(model);
            }

            var timesheetTable =  await _clinicsAppointmentsInquiryRepo.GetDoctorTimeSheet(_currentUserService.BranchId, model.DoctorId);
            var clinicsTable = await _clinicsAppointmentsInquiryRepo.GetClinicsAppointments(_currentUserService.BranchId, model.DoctorId, model.Date);

            WebReport webReport = new();
            webReport.Report.Load(Path.Combine(Environment.CurrentDirectory, "Reports\\Medical\\ClinicsAppointmentsInquiry.frx"));


            webReport.Report.RegisterData(clinicsTable, "ClinicsTable");
            webReport.Report.RegisterData(timesheetTable, "TimeSheetTable");

            var drName = (await _doctorRepo.GetByIdAsync(model.DoctorId)).DrDesc;
            webReport.Report.SetParameterValue("DrName", drName);
            webReport.Report.SetParameterValue("Datee", model.Date.ToString(GlobalConstants.FormatDate));

            webReport.Report.Prepare();

            ViewBag.WebReport = webReport;
            return View(model);
        }
    }
}
