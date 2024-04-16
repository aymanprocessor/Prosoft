using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProSoft.Core.Repositories.Medical.HospitalPatData;
using ProSoft.EF.DTOs.Medical.HospitalPatData;
using ProSoft.EF.IRepositories.Medical.HospitalPatData;
using ProSoft.EF.Models.Medical.HospitalPatData;

namespace ProSoft.UI.Areas.Medical.Controllers
{
    [Authorize]
    [Area("Medical")]
    public class PriceListController : Controller
    {
        private readonly IPriceListRepo _priceListRepo;
        public PriceListController(IPriceListRepo priceListRepo)
        {
            _priceListRepo = priceListRepo;
        }
        public async Task<IActionResult> Index()
        {
            List<PriceListViewDTO> priceListDTOs = await _priceListRepo.GetAllPriceList(); 
            return View(priceListDTOs);
        }

        // Get Add
        public async Task<IActionResult> Add_PriceList()
        {
            ViewBag.ListID = await _priceListRepo.GetNewIdAsync();
            return View();
        }

        // Post Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add_PriceList(PriceListEditAddDTO priceListDTO)
        {
            if (ModelState.IsValid)
            {
                await _priceListRepo.AddPriceListAsync(priceListDTO);

                return RedirectToAction(nameof(Index));
            }
            return View(priceListDTO);
        }
        // Get Edit
        public async Task<IActionResult> Edit_PriceList(int id)
        {
            PriceListEditAddDTO priceListDTO = await _priceListRepo.GetPriceListByIdAsync(id);
            return View(priceListDTO);
        }

        // Post Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_PriceList(int id, PriceListEditAddDTO priceListDTO)
        {
            if (ModelState.IsValid)
            {
                await _priceListRepo.EditPriceListAsync(id, priceListDTO);
                return RedirectToAction(nameof(Index));
            }
            return View(priceListDTO);
        }

        // Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete_Doctor(int id)
        {
            PriceList priceList = await _priceListRepo.GetByIdAsync(id);

            await _priceListRepo.DeleteAsync(priceList);
            await _priceListRepo.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
