using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ProSoft.EF.IRepositories.Treasury;
using ProSoft.EF.IRepositories.Accounts;
using ProSoft.EF.DTOs.Accounts;


namespace ProSoft.UI.Areas.Accounts.Controllers
{
    [Authorize]
    [Area(nameof(Accounts))]
    public class ReportFromVoucherToVoucherController : Controller
    {
        private readonly IReportFromToVoucherRepo _reportFromToVoucherRepo;
        public ReportFromVoucherToVoucherController(IReportFromToVoucherRepo reportFromToVoucherRepo)
        {
            _reportFromToVoucherRepo = reportFromToVoucherRepo;
        }
        public async Task<IActionResult> Index()
        {
            ReportFromToVoucherDTO reportFromToVoucherDTO =await _reportFromToVoucherRepo.GetAllDataAsync();
            return View(reportFromToVoucherDTO);
        }
        // From Voucher to Voucher
        public async Task<IActionResult> GetFromToVoucher(int id, int? fromReceipt, int? toReceipt, DateTime? fromPeriod, DateTime? toPeriod)
        {
            var fYear = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "F_Year").Value);
            var reportData = await _reportFromToVoucherRepo.GetFromToVoucherData(id, fromReceipt, toReceipt, fromPeriod, toPeriod, fYear);
            return Json(reportData);
        }
    }
}
