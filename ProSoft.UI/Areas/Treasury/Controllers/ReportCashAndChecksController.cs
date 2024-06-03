using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProSoft.EF.DTOs.Treasury;
using ProSoft.EF.IRepositories.Treasury;

namespace ProSoft.UI.Areas.Treasury.Controllers
{
    [Authorize]
    [Area(nameof(Treasury))]
    public class ReportCashAndChecksController : Controller
    {
        private readonly IReportCashAndChecksRepo _reportCashChecksRepo;
        public ReportCashAndChecksController(IReportCashAndChecksRepo reportCashChecksRepo)
        {
            _reportCashChecksRepo = reportCashChecksRepo;
        }
        public async Task<IActionResult> Index()
        {
            var userCode = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "User_Code").Value);

            ReportCashAndChecksDTO reportCashAndChecks =await _reportCashChecksRepo.GetAllDataAsync(userCode);
            return View(reportCashAndChecks);
        }

        //Cash Treasury
        public async Task<IActionResult> GetCashTreasury(int id,int userCode, int safeCode, int? fromReceipt, int? toReceipt, DateTime? fromPeriod, DateTime? toPeriod,int treasuryOrBills)
        {
            var reportData = await _reportCashChecksRepo.GetCashTreasuryData(id, userCode, safeCode, fromReceipt, toReceipt, fromPeriod, toPeriod, treasuryOrBills);
            return Json(reportData);
        }

        //Incoming Checks
        public async Task<IActionResult> GetIncomingChecks(int id, int userCode, int safeCode, int? fromReceipt, int? toReceipt, DateTime? fromPeriod, DateTime? toPeriod, int treasuryOrBills)
        {
            var reportData = await _reportCashChecksRepo.GetIncomingChecksData(id, userCode, safeCode, fromReceipt, toReceipt, fromPeriod, toPeriod, treasuryOrBills);
            return Json(reportData);
        }

        //Follow Checks
        public async Task<IActionResult> GetFollowChecks(int id, int userCode, int safeCode, int? fromReceipt, int? toReceipt, DateTime? fromPeriod, DateTime? toPeriod,int treasuryOrBills)
        {
            var reportData = await _reportCashChecksRepo.GetFollowChecksData(id, userCode, safeCode, fromReceipt, toReceipt, fromPeriod, toPeriod, treasuryOrBills);
            return Json(reportData);
        }
    }
}
