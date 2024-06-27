using Microsoft.AspNetCore.Mvc;

namespace ProSoft.UI.Areas.Stocks.Controllers
{
    public class OrderLimitController : Controller
    {
        public OrderLimitController()
        {
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
