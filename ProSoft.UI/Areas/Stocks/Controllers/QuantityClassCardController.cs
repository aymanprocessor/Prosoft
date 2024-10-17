using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProSoft.EF.DTOs.Stocks;
using ProSoft.EF.IRepositories.Stocks;

namespace ProSoft.UI.Areas.Stocks.Controllers
{
    [Authorize]
    [Area(nameof(Stocks))]
    public class QuantityClassCardController : Controller
    {
        private readonly IReportQuantityClassCardRepo _reportQuantityClassCardRepo;
        public QuantityClassCardController(IReportQuantityClassCardRepo reportQuantityClassCardRepo)
        {
            _reportQuantityClassCardRepo = reportQuantityClassCardRepo;
        }
        public async Task<IActionResult> Index()
        {
            var fYear = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "F_Year").Value);
            var userCode = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "User_Code").Value);
            ReportQuantityClassCardDTO reportQuantityClassCardDTO = await _reportQuantityClassCardRepo.GetAllDataAsync(userCode);
            reportQuantityClassCardDTO.FYear= fYear;
            return View(reportQuantityClassCardDTO);
        }
        public async Task<IActionResult> QuantityClassCardOnly(int id, string subItem, DateTime? fromPeriod, DateTime? toPeriod)
        {
            var branch = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "U_Branch_Id").Value);

            var reportData = await _reportQuantityClassCardRepo.GetQuantityCardOnly(id, subItem, fromPeriod, toPeriod, branch);
            return Json(reportData);
        }
    }
}
