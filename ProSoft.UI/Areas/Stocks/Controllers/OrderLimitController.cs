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
        public OrderLimitController(IStockRepo stockRepo)
        {
            _stockRepo = stockRepo;
        }

        public async Task<IActionResult> Index()
        {
            List<StockViewDTO> stocksDTO = await _stockRepo.GetAllStocksAsync();
            ViewBag.stocks = stocksDTO;
            return View();
        }
    }
}
