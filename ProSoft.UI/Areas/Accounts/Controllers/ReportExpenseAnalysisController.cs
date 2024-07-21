using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProSoft.Core.Repositories.Accounts;
using ProSoft.EF.DTOs.Accounts;
using ProSoft.EF.IRepositories.Accounts;

namespace ProSoft.UI.Areas.Accounts.Controllers
{
    [Authorize]
    [Area(nameof(Accounts))]
    public class ReportExpenseAnalysisController : Controller
    {
        private readonly IReportExpenseAnalysisRepo _reportExpenseAnalysisRepo;
        public ReportExpenseAnalysisController(IReportExpenseAnalysisRepo reportExpenseAnalysisRepo)
        {
            _reportExpenseAnalysisRepo = reportExpenseAnalysisRepo;
        }
        public async Task<IActionResult> Index()
        {
            ReportExpenseAnalysisDTO reportExpenseAnalysisDTO = await _reportExpenseAnalysisRepo.GetAllDataAsync();
            return View(reportExpenseAnalysisDTO);
        }
        public async Task<IActionResult> GetExpenseAnalysis(int id, int journal,string mainCode, int year,int filterBy)
        {
            //var fYear = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "F_Year").Value);
            var reportData = await _reportExpenseAnalysisRepo.GetExpenseAnalysis(id, journal, mainCode, year, filterBy);
            return Json(reportData);
        }
    }
}
