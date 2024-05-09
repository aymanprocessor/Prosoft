using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProSoft.EF.DTOs.Medical.HospitalPatData;
using ProSoft.EF.DTOs.Treasury;
using ProSoft.EF.IRepositories.Treasury;
using ProSoft.EF.Models.Medical.HospitalPatData;
using ProSoft.EF.Models.Treasury;

namespace ProSoft.UI.Areas.Treasury.Controllers
{
    [Authorize]
    [Area("Treasury")]
    public class TreasuryNameController : Controller
    {
        private readonly ITreasuryNameRepo _treasuryNameRepo;
        public TreasuryNameController(ITreasuryNameRepo treasuryNameRepo)
        {
            _treasuryNameRepo = treasuryNameRepo;
        }
        public async Task<IActionResult> Index()
        {
            List<TreasuryNameViewDTO> treasuryNameDTOs = await _treasuryNameRepo.GetAllTreasurysAsync();
            return View(treasuryNameDTOs);
        }

        //Get add
        public async Task<IActionResult> Add_TreasuryName(int id)
        {
            ViewBag.SafeID = await _treasuryNameRepo.GetNewIdAsync();
            TreasuryNameEditAddDTO treasuryNameDTO = await _treasuryNameRepo.GetEmptyTreasuryNameAsync(id);
            return View(treasuryNameDTO);
        }

        //Post add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add_TreasuryName(TreasuryNameEditAddDTO treasuryNameDTO)
        {
            if (ModelState.IsValid)
            {
                await _treasuryNameRepo.AddTreasuryNameAsync(treasuryNameDTO);
                return RedirectToAction("Index", "TreasuryName");
            }
            return View();
        }

        //Get Edit
        public async Task<IActionResult> Edit_TreasuryName(int id)
        {
            TreasuryNameEditAddDTO treasuryNameDTO = await _treasuryNameRepo.GetTreasuryNameByIdAsync(id);
            return View(treasuryNameDTO);
        }

        //Post Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_TreasuryName(int id, TreasuryNameEditAddDTO treasuryNameDTO)
        {
            if (ModelState.IsValid)
            {
                await _treasuryNameRepo.EditTreasuryNameAsync(id, treasuryNameDTO);
                return RedirectToAction("Index", "TreasuryName");
            }
            return View(treasuryNameDTO);
        }

        // Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete_TreasuryName(int id)
        {
            SafeName safeName = await _treasuryNameRepo.GetByIdAsync(id);

            await _treasuryNameRepo.DeleteAsync(safeName);
            await _treasuryNameRepo.SaveChangesAsync();
            return RedirectToAction("Index", "TreasuryName");
        }
    }
}
