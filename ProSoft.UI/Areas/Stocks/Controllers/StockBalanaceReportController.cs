using Microsoft.AspNetCore.Mvc;

namespace ProSoft.UI.Areas.Stocks.Controllers
{
    public class StockBalanaceReportController : Controller
    {
        public IActionResult StockBalanceReport()
        {
            return View();
        }

        [HttpPost]
        public IActionResult StockBalanceReport(string model)
        {
            return View();
        }
    }
}
