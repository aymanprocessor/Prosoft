using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProSoft.Core.Repositories.Accounts;
using ProSoft.EF.DTOs.Accounts;
using ProSoft.EF.IRepositories.Accounts;
using ProSoft.EF.Models.Shared;

namespace ProSoft.UI.Areas.Accounts.Controllers
{
    [Authorize]
    [Area(nameof(Accounts))]
    public class MonthlyClosingController : Controller
    {
        private readonly IMonthlyClosingRepo _monthlyClosingRepo;
        public MonthlyClosingController(IMonthlyClosingRepo monthlyClosingRepo)
        {
            _monthlyClosingRepo = monthlyClosingRepo;
        }
        public async Task<IActionResult> Index()
        {
            MonthlyClosingDTO monthlyClosingDTO = await _monthlyClosingRepo.GetAllDataAsync();
            return View(monthlyClosingDTO);
        }
        
        public async Task<IActionResult> MonthlyCloseOrCancel(int id,int branch, int? fromVoucher, int? toVoucher, DateTime? fromPeriod, DateTime? toPeriod,int closOrCanc)
        {
            var changeData = "";
            if (closOrCanc == 1)
            {
                changeData = await _monthlyClosingRepo.CloseAsync(id, branch, fromVoucher, toVoucher, fromPeriod, toPeriod);
            }
            else if (closOrCanc == 2)
            {
                changeData = await _monthlyClosingRepo.CancelAsync(id, branch, fromVoucher, toVoucher, fromPeriod, toPeriod);
            }
            return Json(changeData);
        }
    }
}
