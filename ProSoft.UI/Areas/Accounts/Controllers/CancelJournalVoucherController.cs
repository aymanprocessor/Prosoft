using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProSoft.EF.DTOs.Accounts;
using ProSoft.EF.IRepositories.Accounts;

namespace ProSoft.UI.Areas.Accounts.Controllers
{
    [Authorize]
    [Area(nameof(Accounts))]
    public class CancelJournalVoucherController : Controller
    {
        private readonly ICancelJournalVoucherRepo _cancelJournalVoucherRepo;
        public CancelJournalVoucherController(ICancelJournalVoucherRepo cancelJournalVoucherRepo)
        {
            _cancelJournalVoucherRepo = cancelJournalVoucherRepo;
        }
        public async Task<IActionResult> Index()
        {
            CancelJournalVoucherDTO reportExpenseAnalysisDTO = await _cancelJournalVoucherRepo.GetAllDataAsync();
            return View(reportExpenseAnalysisDTO);
        }
    }
}
