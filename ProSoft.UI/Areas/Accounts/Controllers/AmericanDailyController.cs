using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProSoft.EF.DTOs.Accounts;
using ProSoft.EF.IRepositories.Accounts;

namespace ProSoft.UI.Areas.Accounts.Controllers
{
    [Authorize]
    [Area(nameof(Accounts))]
    public class AmericanDailyController : Controller
    {
        private readonly IAmericanDailyRepo _americanDailyRepo;
        public AmericanDailyController(IAmericanDailyRepo americanDailyRepo)
        {
            _americanDailyRepo = americanDailyRepo;
        }
        public async Task<IActionResult> Index()
        {
            ReportAmericanDailyDTO reportAmericanDailyDTO = await _americanDailyRepo.GetAllDataAsync();
            return View(reportAmericanDailyDTO);
        }

        public async Task<IActionResult> GetMainName(int id, int journal, DateTime? fromPeriod, DateTime? toPeriod)
        {
            var mainNames = await _americanDailyRepo.GetMainNameAsync( id,  journal,  fromPeriod, toPeriod);
            return Json(mainNames);
        }
    }
}
