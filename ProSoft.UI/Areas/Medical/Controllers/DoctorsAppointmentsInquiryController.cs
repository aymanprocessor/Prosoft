using FastReport.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProSoft.EF.DTOs.Medical.HospitalPatData.Reports;
using ProSoft.EF.IRepositories.Medical.HospitalPatData;
using ProSoft.EF.IRepositories.Medical.HospitalPatData.Reports;
using ProSoft.UI.Global;

namespace ProSoft.UI.Areas.Medical.Controllers
{
    [Area(nameof(Medical))]
    [Authorize]
    public class DoctorsAppointmentsInquiryController : Controller
    {
        private readonly IDoctorRepo _doctorRepo;
        private readonly IDoctorsAppointmentsInquiryRepo _doctorsAppointmentsInquiryRepo;
        private readonly ICurrentUserService _currentUserService;

        public DoctorsAppointmentsInquiryController(IDoctorRepo doctorRepo, IDoctorsAppointmentsInquiryRepo doctorsAppointmentsInquiryRepo, ICurrentUserService currentUserService)
        {
            _doctorRepo = doctorRepo;
            _doctorsAppointmentsInquiryRepo = doctorsAppointmentsInquiryRepo;
            _currentUserService = currentUserService;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Doctors = (await _doctorRepo.GetAllAsync()).ToSelectListItem(d => d.DrDesc, d => d.DrId.ToString());

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(DoctorsAppointmentsInquiryRequestDTO model)
        {
            ViewBag.Doctors = (await _doctorRepo.GetAllAsync()).ToSelectListItem(d => d.DrDesc, d => d.DrId.ToString());
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var table = await _doctorsAppointmentsInquiryRepo.GetDoctorsAppointments(_currentUserService.BranchId, model.DoctorId, model.FromDate, model.ToDate);

            WebReport webReport = new();
            webReport.Report.Load(Path.Combine(Environment.CurrentDirectory, "Reports\\Medical\\DoctorsAppointmentsInquiry.frx"));


            webReport.Report.RegisterData(table, "Table");

            var drName = (await _doctorRepo.GetByIdAsync(model.DoctorId)).DrDesc;
            webReport.Report.SetParameterValue("DrName", drName); 
            webReport.Report.SetParameterValue("FromDate", model.FromDate.ToString(GlobalConstants.FormatDate)); 
            webReport.Report.SetParameterValue("ToDate", model.ToDate.ToString(GlobalConstants.FormatDate));

            webReport.Report.Prepare();

            ViewBag.WebReport = webReport;
            return View(model);
        }
    }
}
