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
    public class TermsPriceListController : Controller
    {
        private readonly ITermsPriceListRepo _termsPriceListRepo;
        public TermsPriceListController(ITermsPriceListRepo termsPriceListRepo)
        {
            _termsPriceListRepo = termsPriceListRepo;
        }
        public async Task<IActionResult> GetTermOfPriceLists(int id)
        {
            List<TermsPriceListViewDTO> termsPriceListDTOs = await _termsPriceListRepo.GetAllTermsPriceList(id);
            return Json(termsPriceListDTOs);
        }
        // Get Add
        public async Task<IActionResult> Add_TermOfPriceList()
        {
            ViewBag.SortID = await _termsPriceListRepo.GetNewIdSortAsync();
            TermsPriceListEditAddDTO termsPriceListDTO = await _termsPriceListRepo.GetEmptyTermsPriceListAsync();
            return View(termsPriceListDTO);
        }

        // Post Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add_TermOfPriceList(int id, TermsPriceListEditAddDTO termsPriceListDTO)
        {
            if (ModelState.IsValid)
            {
                await _termsPriceListRepo.AddTermPriceListAsync(id,termsPriceListDTO);

                return RedirectToAction("Index", "PriceList");
            }
            return View(termsPriceListDTO);
        }

        //Get Edit
        public async Task<IActionResult> Edit_TermOfPriceList(int id)
        {
            TermsPriceListEditAddDTO termsPriceListDTO = await _termsPriceListRepo.GetTermPriceListByIdAsync(id);
            return View(termsPriceListDTO);
        }

        //Post Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_TermOfPriceList(int id, TermsPriceListEditAddDTO termsPriceListDTO)
        {
            if (ModelState.IsValid)
            {
                await _termsPriceListRepo.EditDoctorPercentAsync(id, termsPriceListDTO);
                return RedirectToAction("Index", "PriceList");
            }
            return View(termsPriceListDTO);
        }

        // Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete_TermOfPriceList(int id)
        {
            PriceListDetail priceListDetail = await _termsPriceListRepo.GetByIdAsync(id);

            await _termsPriceListRepo.DeleteAsync(priceListDetail);
            await _termsPriceListRepo.SaveChangesAsync();
            return RedirectToAction("Index", "PriceList");
        }
    }
}
