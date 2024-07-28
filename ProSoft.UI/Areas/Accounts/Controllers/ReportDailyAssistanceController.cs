using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProSoft.EF.DTOs.Accounts;
using ProSoft.EF.IRepositories.Accounts;

namespace ProSoft.UI.Areas.Accounts.Controllers
{
    [Authorize]
    [Area(nameof(Accounts))]
    public class ReportDailyAssistanceController : Controller
    {
        private readonly IReportDailyAssistanceRepo _reportDailyAssistanceRepo;
        public ReportDailyAssistanceController(IReportDailyAssistanceRepo reportDailyAssistanceRepo)
        {
            _reportDailyAssistanceRepo = reportDailyAssistanceRepo;
        }
        public async Task<IActionResult> Index()
        {
            ReportDailyAssistanceDTO reportDailyAssistanceDTO = await _reportDailyAssistanceRepo.GetAllDataAsync();
            return View(reportDailyAssistanceDTO);
        }
        public async Task<IActionResult> GetDailyAssistance(int id, DateTime fromPeriod, DateTime toPeriod ,int branche)
        {
            var fYear = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "F_Year").Value);
            var reportData = await _reportDailyAssistanceRepo.GetDailyAssistanceAsync(id, fromPeriod, toPeriod, branche, fYear);
            return Json(reportData);
        }
    }
}
