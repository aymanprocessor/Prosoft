using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using ProSoft.EF.DTOs.Shared;
using ProSoft.EF.IRepositories.Medical.HospitalPatData;
using ProSoft.UI.Models;
using System.Diagnostics;

namespace ProSoft.UI.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IStringLocalizer<HomeController> _localizer;
        private readonly IPatientRepo _patientRepo;
        private readonly IClinicTransRepo _clinicTransRepo;
        private readonly IDoctorRepo _doctorRepo;

        public HomeController(ILogger<HomeController> logger, IStringLocalizer<HomeController> localizer, IPatientRepo patientRepo, IClinicTransRepo clinicTransRepo, IDoctorRepo doctorRepo)
        {
            _logger = logger;
            _localizer = localizer;
            _patientRepo = patientRepo;
            _clinicTransRepo = clinicTransRepo;
            _doctorRepo = doctorRepo;
        }
        public IActionResult Index()
        {

            DashboardVMDTO dashboardVMDTO = new DashboardVMDTO
            {
                PatientCounts = _patientRepo.GetPatientCounts(),
                PatientCountsDaily = _patientRepo.GetPatientCountsDaily(),
                PatientCountsWeekly = _patientRepo.GetPatientCountsWeekly(),
                ClinicTransCounts = _clinicTransRepo.ClinicTransCounts(),
                ClinicTransCountsDaily = _clinicTransRepo.ClinicTransCountsDaily(),
                ClinicTransCountsWeekly = _clinicTransRepo.ClinicTransCountsWeekly(),

            };
            return View(dashboardVMDTO);
        }
        [HttpGet]
        public async Task<IActionResult> FilterClinicTrans(string range)
        {
            var Clinics = await _clinicTransRepo.ClinicTransRangeFilter(range);

            var Trans = from ct in Clinics
                        join d in (await _doctorRepo.GetAllAsync()) on new { x1 = ct.BranchId, x2 = ct.DrCode } equals new { x1 = (int?)d.BranchId, x2 = (int?)d.DrId }
                        join p in (await _patientRepo.GetAllAsync()) on new { x1 = ct.BranchId, x2 = ct.PatId } equals new { x1 = (int?)p.BranchId, x2 = (int?)p.PatId }
                        select new
                        {
                            patientName = p.PatName,
                            //time = ct.,
                            doctor = d.DrDesc,
                            date = ct.ExDate.Value.ToString("yyyy-MM-dd")
                        };

            return Json(Trans);
        }

            public IActionResult Privacy()
        {
            return View();
        }
        //For Localization
        [AllowAnonymous]
        [HttpPost]
        public IActionResult SetLanguage(string culture ,string redirctURL)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1)}
                );
            return LocalRedirect(redirctURL);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
