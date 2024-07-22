using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProSoft.Core.Repositories.Accounts;
using ProSoft.EF.DTOs.Accounts;
using ProSoft.EF.IRepositories.Accounts;

namespace ProSoft.UI.Areas.Accounts.Controllers
{
    [Authorize]
    [Area(nameof(Accounts))]
    public class ReportGeneralProfessorFacilityController : Controller
    {
        private readonly IReportGeneralProfessorFacilityRepo _reportGeneralProfessorFacilityRepo;
        public ReportGeneralProfessorFacilityController(IReportGeneralProfessorFacilityRepo reportGeneralProfessorFacilityRepo)
        {
            _reportGeneralProfessorFacilityRepo = reportGeneralProfessorFacilityRepo;
        }
        public async Task<IActionResult> Index()
        {
            ReportGeneralProfessorFacilityDTO reportGeneralProfessorFacilityDTO = await _reportGeneralProfessorFacilityRepo.GetAllDataAsync();
            return View(reportGeneralProfessorFacilityDTO);
        }
        public async Task<IActionResult> GetGeneralProfessor(int id,DateTime? toPeriod, int movementToDate)
        {
           // var fYear = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "F_Year").Value);
            var reportData = await _reportGeneralProfessorFacilityRepo.GetGeneralProfessorAsync(id, toPeriod, movementToDate);
            return Json(reportData);
        }
    }
}
