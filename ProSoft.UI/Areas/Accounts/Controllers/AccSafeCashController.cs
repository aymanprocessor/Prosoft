using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ProSoft.UI.Areas.Accounts.Controllers
{
    [Authorize]
    [Area(nameof(Accounts))]
    public class AccSafeCashController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
