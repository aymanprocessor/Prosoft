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
        public async Task<IActionResult> Index(int id ,string docType)
        {
            List<CustCollectionsDiscountViewDTO> custCollectionsDiscounts = await _custCollectionsDiscountRepo.GetAllCustCollectionsDiscountAsync(id, docType);
            ViewBag.SafeCashID = id;
            ViewBag.docType = docType;
            return Json(custCollectionsDiscounts);
        }

        // Get Add
        public async Task<IActionResult> Add_CustDiscount(int id,string docType)
        {
            ViewBag.custDiscountID = id;
            CustCollectionsDiscountEditAddDTO custCollectionsDiscountDTO = await _custCollectionsDiscountRepo.GetEmptycustCollectionsDiscountAsync(id, docType);
            ViewBag.ValuePay = custCollectionsDiscountDTO.ValuePay;
            ViewBag.docType = docType;
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
                if (custCollectionsDiscountDTO.DocType == "SFCIN")
                {
                  return RedirectToAction("Index", "AccSafeCash", new {docType = custCollectionsDiscountDTO.DocType, flagType = "oneANDtwo", redirect = id }); 
                }
                else if (custCollectionsDiscountDTO.DocType == "SFCOT")
                {
                    return RedirectToAction("Index", "DisbursementPermission", new { docType = custCollectionsDiscountDTO.DocType, flagType = "oneANDtwo", redirect = id });

                }
                else if (custCollectionsDiscountDTO.DocType == "SFTIN")
                {
                    return RedirectToAction("Index", "ReceivePermission", new { docType = custCollectionsDiscountDTO.DocType, flagType = "oneANDtwoAndthree", redirect = id });

                }
                return RedirectToAction("Index", "TransferPermission", new { docType = custCollectionsDiscountDTO.DocType, flagType = "oneANDtwoAndthree", redirect = id });

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
                if (custCollectionsDiscountDTO.DocType == "SFCIN")
                {
                    return RedirectToAction("Index", "AccSafeCash", new { docType = custCollectionsDiscountDTO.DocType, flagType = "oneANDtwo" });
                }
                else if (custCollectionsDiscountDTO.DocType == "SFCOT")
                {
                    return RedirectToAction("Index", "DisbursementPermission", new { docType = custCollectionsDiscountDTO.DocType, flagType = "oneANDtwo" });

                }
                else if (custCollectionsDiscountDTO.DocType == "SFTIN")
                {
                    return RedirectToAction("Index", "ReceivePermission", new { docType = custCollectionsDiscountDTO.DocType, flagType = "oneANDtwoAndthree" });

                }
                return RedirectToAction("Index", "TransferPermission", new { docType = custCollectionsDiscountDTO.DocType, flagType = "oneANDtwoAndthree" });
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
            if (custCollectionsDiscount.DocType == "SFCIN")
            {
                return RedirectToAction("Index", "AccSafeCash", new { docType = custCollectionsDiscount.DocType, flagType = "oneANDtwo" });
            }
            else if (custCollectionsDiscount.DocType == "SFCOT")
            {
                return RedirectToAction("Index", "DisbursementPermission", new { docType = custCollectionsDiscount.DocType, flagType = "oneANDtwo" });

            }
            else if (custCollectionsDiscount.DocType == "SFTIN")
            {
                return RedirectToAction("Index", "ReceivePermission", new { docType = custCollectionsDiscount.DocType, flagType = "oneANDtwoAndthree" });

            }
            return RedirectToAction("Index", "TransferPermission", new { docType = custCollectionsDiscount.DocType, flagType = "oneANDtwoAndthree" });

        }
    }
}
