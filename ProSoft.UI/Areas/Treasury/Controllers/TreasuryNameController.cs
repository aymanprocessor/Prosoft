using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProSoft.EF.DTOs.Medical.HospitalPatData;
using ProSoft.EF.DTOs.Treasury;
using ProSoft.EF.IRepositories.Treasury;

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
           // TreasuryNameEditAddDTO treasuryNameDTO = await _treasuryNameRepo.GetEmptyDepositAsync(id);
            return View();
        }
        //Get Edit
        //public async Task<IActionResult> Edit_TreasuryName(int id)
        //{
        //    DepositEditAddDTO depositDTO = await _depositRepo.GetDepositByIdAsync(id);
        //    return View(depositDTO);
        //}
    }
}
