using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProSoft.Core.Repositories.Treasury;
using ProSoft.EF.DTOs.Shared;
using ProSoft.EF.DTOs.Treasury;
using ProSoft.EF.IRepositories.Treasury;
using ProSoft.EF.Models.Treasury;

namespace ProSoft.UI.Areas.Treasury.Controllers
{
    [Authorize]
    [Area(nameof(Treasury))]
    public class CustCollectionsDiscountController : Controller
    {
        private readonly ICustCollectionsDiscountRepo _custCollectionsDiscountRepo;
        public CustCollectionsDiscountController(ICustCollectionsDiscountRepo custCollectionsDiscountRepo)
        {
            _custCollectionsDiscountRepo = custCollectionsDiscountRepo;
        }
        public async Task<IActionResult> Index(int id)
        {
            List<CustCollectionsDiscountViewDTO> custCollectionsDiscounts = await _custCollectionsDiscountRepo.GetAllCustCollectionsDiscountAsync(id);
            ViewBag.SafeCashID = id;
            return View(custCollectionsDiscounts);
        }

        // Get Add
        public async Task<IActionResult> Add_CustDiscount(int id)
        {
            ViewBag.custDiscountID = id;
            CustCollectionsDiscountEditAddDTO custCollectionsDiscountDTO = await _custCollectionsDiscountRepo.GetEmptycustCollectionsDiscountAsync(id);
            ViewBag.ValuePay = custCollectionsDiscountDTO.ValuePay;
            return View(custCollectionsDiscountDTO);
        }

        //// Post Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add_CustDiscount(int id, CustCollectionsDiscountEditAddDTO custCollectionsDiscountDTO)
        {
            if (ModelState.IsValid)
            {
                custCollectionsDiscountDTO.SafeCashId = id;
                await _custCollectionsDiscountRepo.AddcustCollectionsDiscountAsync(id,custCollectionsDiscountDTO);
                return RedirectToAction(nameof(Index),new {id});
            }
            return View(custCollectionsDiscountDTO);
        }

        // Get Edit
        public async Task<IActionResult> Edit_CustDiscount(int id)
        {
            CustCollectionsDiscountEditAddDTO custCollectionsDiscountDTO = await _custCollectionsDiscountRepo.GetcustCollectionsDiscountByIdAsync(id);
            ViewBag.subAccCodesSub = await _custCollectionsDiscountRepo.GetSubCodesFromAccAsync(custCollectionsDiscountDTO.MainCode);
            ViewBag.ValuePay = custCollectionsDiscountDTO.ValuePay;

            return View(custCollectionsDiscountDTO);
        }

        // Post Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_CustDiscount(int id, CustCollectionsDiscountEditAddDTO custCollectionsDiscountDTO)
        {
            if (ModelState.IsValid)
            {
                await _custCollectionsDiscountRepo.EditcustCollectionsDiscountAsync(id, custCollectionsDiscountDTO);
                return RedirectToAction(nameof(Index), new { id = custCollectionsDiscountDTO.SafeCashId});
            }
            return View(custCollectionsDiscountDTO);
        }

        // Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete_CustDiscount(int id)
        {
            CustCollectionsDiscount custCollectionsDiscount = await _custCollectionsDiscountRepo.GetByIdAsync(id);

            await _custCollectionsDiscountRepo.DeleteAsync(custCollectionsDiscount);
            await _custCollectionsDiscountRepo.SaveChangesAsync();
            return RedirectToAction(nameof(Index) ,new {id = custCollectionsDiscount.SafeCashId});
        }
    }
}
