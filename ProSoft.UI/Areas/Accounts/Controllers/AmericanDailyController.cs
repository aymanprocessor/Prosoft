using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProSoft.Core.Repositories.Accounts;
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
        //get American Daily
        public async Task<IActionResult> GetAmericanDaily(int id, int journal, DateTime? fromPeriod, DateTime? toPeriod)
        {
            //var fYear = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "F_Year").Value);
            var reportData = await _americanDailyRepo.GetAmericanDailyAsync(id, journal, fromPeriod, toPeriod);
            return Json(reportData);
        }
        //get mainNames
        public async Task<IActionResult> GetMainName(int id, int journal, DateTime? fromPeriod, DateTime? toPeriod)
        {
            var mainNames = await _americanDailyRepo.GetMainNameAsync( id,  journal,  fromPeriod, toPeriod);
            return Json(mainNames);
        }

        //////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////
       
        // Get American Daily
        public async Task<IActionResult> GetAmericanDailyForMainSub(int id, int journal, DateTime? fromPeriod, DateTime? toPeriod, string? mainCode, string? subCode)
        {
            var reportData = await _americanDailyRepo.GetAmericanDailyForMainSubAsync(id, journal, fromPeriod, toPeriod, mainCode, subCode);
            return Json(reportData);
        }

        // Get Sub Names
        public async Task<IActionResult> GetSubName(int id, int journal, DateTime? fromPeriod, DateTime? toPeriod, string? mainCode, string? subCode)
        {
            var subNames = await _americanDailyRepo.GetSubNameAsync(id, journal, fromPeriod, toPeriod, mainCode, subCode);
            return Json(subNames);
        }
        //AmericanTotalJournal
        public async Task<IActionResult> GetAmericanTotalJournal(int id, int branch, DateTime? fromPeriod, DateTime? toPeriod)
        {
            var reportData = await _americanDailyRepo.GetAmericanTotalJournal(id, branch, fromPeriod, toPeriod);
            return Json(reportData);
        }
    }
}
