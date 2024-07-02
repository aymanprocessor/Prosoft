using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProSoft.EF.DTOs.Stocks;
using ProSoft.EF.IRepositories.Stocks;

namespace ProSoft.UI.Areas.Stocks.Controllers
{
    [Authorize]
    [Area(nameof(Stocks))]
    public class OrderLimitController : Controller
    {
        private readonly IStockRepo _stockRepo;
        private readonly IOrderLimitRepo _orderLimitRepo;
        public OrderLimitController(IStockRepo stockRepo, IOrderLimitRepo orderLimitRepo)
        {
            _stockRepo = stockRepo;
            _orderLimitRepo = orderLimitRepo;
        }

        public async Task<IActionResult> Index()
        {
            List<StockViewDTO> stocksDTO = await _stockRepo.GetAllStocksAsync();
            ViewBag.stocks = stocksDTO;
            return View();
        }

        public async Task<IActionResult> GetOrderLimits(DateTime date1, DateTime date2,
            int stockID)
        {
            var branchID = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "U_Branch_Id").Value);
            List<ItmReorderDTO> orderLimitDTOs = await _orderLimitRepo.GetItemsLimitsAsync(date1, date2, stockID, branchID);
            return Json(orderLimitDTOs);
        }
    }
}
