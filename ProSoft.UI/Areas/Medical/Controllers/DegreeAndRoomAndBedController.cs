using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ProSoft.UI.Areas.Medical.Controllers
{
    [Authorize]
    [Area(nameof(Medical))]
    public class DegreeAndRoomAndBedController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
