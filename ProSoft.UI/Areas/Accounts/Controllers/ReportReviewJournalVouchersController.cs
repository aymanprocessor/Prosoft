using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ProSoft.EF.DTOs.Accounts;
using ProSoft.EF.IRepositories.Accounts;
using ProSoft.Core.Repositories.Accounts;

namespace ProSoft.UI.Areas.Accounts.Controllers
{
    [Authorize]
    [Area(nameof(Accounts))]
    public class ReportReviewJournalVouchersController : Controller
    {
        private readonly IReportReviewJournalVouchersRepo _reportReviewJournalVouchersRepo;
        public ReportReviewJournalVouchersController(IReportReviewJournalVouchersRepo reportReviewJournalVouchersRepo)
        {
            _reportReviewJournalVouchersRepo = reportReviewJournalVouchersRepo;
        }
        public async Task<IActionResult> Index()
        {
            ReportReviewJournalVouchersDTO reportReviewJournalVouchersDTO = await _reportReviewJournalVouchersRepo.GetAllDataAsync();
            return View(reportReviewJournalVouchersDTO);
        }
        public async Task<IActionResult> GetReviewDisplay(int id, int branche, DateTime? fromPeriod, DateTime? toPeriod, int displayType)
        {
            // var fYear = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "F_Year").Value);
            var reportData = await _reportReviewJournalVouchersRepo.GetReviewDisplay(id, branche,  fromPeriod, toPeriod, displayType);
            return Json(reportData);
        }
    }
}
