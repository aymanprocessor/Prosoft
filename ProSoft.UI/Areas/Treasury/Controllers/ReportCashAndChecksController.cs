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
    }
}
