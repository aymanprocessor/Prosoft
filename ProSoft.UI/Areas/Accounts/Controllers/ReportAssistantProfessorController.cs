using AutoMapper.Configuration.Annotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProSoft.EF.DTOs.Accounts;
using ProSoft.EF.IRepositories.Accounts;

namespace ProSoft.UI.Areas.Accounts.Controllers
{
    [Authorize]
    [Area(nameof(Accounts))]
    public class ReportAssistantProfessorController : Controller
    {
        private readonly IReportAssistantProfessorRepo _reportAssistantProfessorRepo;
        public ReportAssistantProfessorController(IReportAssistantProfessorRepo reportAssistantProfessorRepo)
        {
            _reportAssistantProfessorRepo = reportAssistantProfessorRepo;
        }
        public async Task<IActionResult> Index()
        {
            ReportAssistantProfessorDTO reportAssistantProfessorDTO = await _reportAssistantProfessorRepo.GetAllDataAsync();
            return View(reportAssistantProfessorDTO);
        }

        //Analytical
        public async Task<IActionResult> GetAnalytical(int id, int journal, string mainCode, string subCode, int costCode, DateTime? fromPeriod, DateTime? toPeriod)
        {
            var fYear = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "F_Year").Value);
            var reportData = await _reportAssistantProfessorRepo.GetAnalyticalAsync(id, journal, mainCode, subCode, costCode, fromPeriod, toPeriod, fYear);
            return Json(reportData);
        }

        //Over All
        public async Task<IActionResult> GetOverAll(int id, int journal, string mainCode, int costCode, DateTime? fromPeriod, DateTime? toPeriod)
        {
            var reportData = await _reportAssistantProfessorRepo.GetOverAllAsync(id, journal, mainCode, costCode, fromPeriod, toPeriod);
            return Json(reportData);
        }
    }
}
