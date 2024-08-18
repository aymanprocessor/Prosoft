using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProSoft.EF.DTOs.Stocks;
using ProSoft.EF.IRepositories.Stocks;

namespace ProSoft.UI.Areas.Stocks.Controllers
{
    [Authorize]
    [Area(nameof(Stocks))]
    public class OpenCloseTransactionController : Controller
    {
        private readonly ITransMasterRepo _transMasterRepo;
        private readonly IOpenColseTransactionRepo _openColseTransactionRepo;

        public OpenCloseTransactionController(ITransMasterRepo transMasterRepo, IOpenColseTransactionRepo openColseTransactionRepo)
        {
            _transMasterRepo = transMasterRepo;
            _openColseTransactionRepo = openColseTransactionRepo;
        }
        public async Task<IActionResult> Index()
        {
            var userCode = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "User_Code").Value);
            List<StockViewDTO> stocksDTO = await _transMasterRepo.GetActiveStocksForUserAsync(userCode);
            ViewBag.Stocks = stocksDTO;
            var permissions = await _openColseTransactionRepo.GetPermissionsAsync();
            ViewBag.Permissionss = permissions;
            return View();
        }
        public async Task<IActionResult> OpenClose(int id, int permission, int? fromVoucher, int? toVoucher, DateTime? fromPeriod, DateTime? toPeriod, int closOrCanc)
        {
            var changeData = "";
            if (closOrCanc == 1)
            {
                changeData = await _openColseTransactionRepo.OpenAsync(id, permission, fromVoucher, toVoucher, fromPeriod, toPeriod, closOrCanc);
            }
            else if (closOrCanc == 2)
            {
                changeData = await _openColseTransactionRepo.CloseAsync(id, permission, fromVoucher, toVoucher, fromPeriod, toPeriod, closOrCanc);
            }
            ViewBag.mes=changeData;
            return Json(changeData);
        }
    }
}
