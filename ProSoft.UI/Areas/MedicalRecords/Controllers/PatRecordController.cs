using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ProSoft.UI.Areas.MedicalRecords.Controllers
{
    [Authorize]
    [Area(nameof(MedicalRecords))]
    public class PatRecordController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
