using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ProSoft.EF.DTOs.Accounts;
using ProSoft.EF.IRepositories.Accounts;
using ProSoft.EF.IRepositories.Medical.HospitalPatData;
using ProSoft.EF.DTOs.Medical.HospitalPatData;
using ProSoft.Core.Repositories.Accounts;


namespace ProSoft.UI.Areas.Medical.Controllers
{
    [Authorize]
    [Area("Medical")]
    public class ReportDoctorFeesController : Controller
    {
        private readonly IReportDoctorFeesRepo _reportDoctorFeesRepo;
        public ReportDoctorFeesController(IReportDoctorFeesRepo reportDoctorFeesRepo)
        {
            _reportDoctorFeesRepo = reportDoctorFeesRepo;
        }
        public async Task<IActionResult> Index()
        {
            ReportDoctorFeesDTO reportDoctorFeesDTO = await _reportDoctorFeesRepo.GetAllDataAsync();
            return View(reportDoctorFeesDTO);
        }
        // Doctor Fees
        public async Task<IActionResult> GetDoctorFees(int id, DateTime fromPeriod, DateTime toPeriod)
        {
            //var fYear = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "F_Year").Value);
            var branchId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "U_Branch_Id").Value);
            var reportData = await _reportDoctorFeesRepo.GetReportDoctorFeesData(id, fromPeriod, toPeriod, branchId);
            return Json(reportData);
        }
    }
}
