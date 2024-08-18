using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProSoft.Core.Repositories.Accounts;
using ProSoft.EF.DTOs.Accounts;
using ProSoft.EF.IRepositories.Accounts;

namespace ProSoft.UI.Areas.Accounts.Controllers
{
    [Authorize]
    [Area(nameof(Accounts))]
    public class AccSubCodeEditController : Controller
    {
        private readonly IAccSubCodeEditRepo _accSubCodeEditRepo;
        public AccSubCodeEditController(IAccSubCodeEditRepo accSubCodeEditRepo)
        {
            _accSubCodeEditRepo = accSubCodeEditRepo;
        }

        public async Task<IActionResult> Index()
        {
            DisplayAccSubCodeEditDTO displayAccSubCodeEditDTO = await _accSubCodeEditRepo.GetAllDataAsync();
            return View(displayAccSubCodeEditDTO);
        }
        public async Task<IActionResult> GetAccSubCodeEdit(string id)
        {
            var data = await _accSubCodeEditRepo.GetAccSubCodeEditAsync(id);
            return Json(data);
        }
        //Get Edit
        public async Task<IActionResult> Edit_AccSubCodeEdit(string id,string subFr,string subTo)
        {
            AccSubCodeEditDTO accSubCodeEditDTO = await _accSubCodeEditRepo.GetAccAccSubCodeEditById(id, subFr);
            return View(accSubCodeEditDTO);
        }

        //Post Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_AccSubCodeEdit(string id, AccSubCodeEditDTO accSubCodeEditDTO)
        {
            if (ModelState.IsValid)
            {
                var fYear = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "F_Year").Value);
                var branch = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "U_Branch_Id").Value);
                await _accSubCodeEditRepo.EditAccSubCodeEditAsync(id, fYear, branch, accSubCodeEditDTO);
                return RedirectToAction("Index", "AccSubCodeEdit");
            }
            return View(accSubCodeEditDTO);
        }
    }
}
