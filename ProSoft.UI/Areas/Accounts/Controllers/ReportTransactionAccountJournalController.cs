using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProSoft.EF.DTOs.Accounts;
using ProSoft.EF.IRepositories.Accounts;

namespace ProSoft.UI.Areas.Accounts.Controllers
{
    [Authorize]
    [Area(nameof(Accounts))]
    public class ReportTransactionAccountJournalController : Controller
    {
        private readonly IReportTransactionAccountJournalRepo _reportTransactionAccountJournalRepo;
        public ReportTransactionAccountJournalController(IReportTransactionAccountJournalRepo reportTransactionAccountJournalRepo)
        {
            _reportTransactionAccountJournalRepo = reportTransactionAccountJournalRepo;
        }
        public async Task<IActionResult> Index()
        {
            ReportTransactionAccountJournalDTO reportTransactionAccountJournalDTO = await _reportTransactionAccountJournalRepo.GetAllDataAsync();
            return View(reportTransactionAccountJournalDTO);
        }
        public async Task<IActionResult> GetTransactionAccountJournal(int id, DateTime? fromPeriod, DateTime? toPeriod ,string mainCode, string subCode, int branche)
        {
            //var fYear = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "F_Year").Value);
            var reportData = await _reportTransactionAccountJournalRepo.GetTransactionAccountJournal(id, fromPeriod, toPeriod, mainCode, subCode, branche);
            return Json(reportData);
        }
    }
}
