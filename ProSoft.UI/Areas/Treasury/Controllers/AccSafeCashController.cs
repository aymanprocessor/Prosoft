using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProSoft.Core.Repositories.Accounts;
using ProSoft.Core.Repositories.Medical.HospitalPatData;
using ProSoft.EF.DTOs.Accounts;
using ProSoft.EF.DTOs.Medical.HospitalPatData;
using ProSoft.EF.DTOs.Treasury;
using ProSoft.EF.IRepositories.Medical.HospitalPatData;
using ProSoft.EF.IRepositories.Treasury;
using ProSoft.EF.Models.Medical.HospitalPatData;
using ProSoft.EF.Models.Treasury;

namespace ProSoft.UI.Areas.Treasury.Controllers
{
    [Authorize]
    [Area(nameof(Treasury))]
    public class AccSafeCashController : Controller
    {
        private readonly IAccSafeCashRepo _accSafeCashRepo;
        private readonly IUserCashNoRepo _userCashNoRepo;

        public AccSafeCashController(IAccSafeCashRepo accSafeCashRepo, IUserCashNoRepo userCashNoRepo)
        {
            _accSafeCashRepo = accSafeCashRepo;
            _userCashNoRepo = userCashNoRepo;
        }
        public async Task<IActionResult> Index(string docType,string? flagType)
        {
            var userCode = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "User_Code").Value);
            var userCashNoDTO = await _userCashNoRepo.GetSafeTransByIdAsync(userCode);
            var safeCode = userCashNoDTO.SafeCode;
            var fYear = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "F_Year").Value);
            var branchId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "U_Branch_Id").Value);

            List<AccSafeCashViewDTO> accSafeCashs = await _accSafeCashRepo
            .GetAccSafeCashAsync(docType, flagType, fYear, safeCode);
            ViewBag.docType = docType;
            ViewBag.safeCode = safeCode;
            ViewBag.fYear = fYear;

            return View(accSafeCashs);
        }
        //Ajax In Add_StockTrans
        public async Task<IActionResult> GetSubCodesFromAcc(string id)
        {
            List<AccSubCodeDTO> subAccCodesDTO = await _accSafeCashRepo
                .GetSubCodesFromAccAsync(id);
            return Json(subAccCodesDTO);
        }
        //Get Add
        public async Task<IActionResult> Add_PaymentReceipt(string docType ,int safeCode, int fYear)
        {
           // ViewBag.AccSafeCashID = await _accSafeCashRepo.GetNewIdAsync();
            ViewBag.SerialID = await _accSafeCashRepo.GetNewSerialAsync(docType, safeCode, fYear);
            AccSafeCashEditAddDTO accSafeCashDTO = await _accSafeCashRepo.GetEmptyPaymentReceiptAsync();
            ViewBag.docType = docType;
            ViewBag.fYear = fYear;

            return View(accSafeCashDTO);
        }
        //Post add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add_PaymentReceipt(AccSafeCashEditAddDTO accSafeCashDTO)
        {
            if (ModelState.IsValid)
            {
                await _accSafeCashRepo.AddPaymentReceiptAsync(accSafeCashDTO);
                return RedirectToAction("Index", "AccSafeCash" ,new { docType = "SFCIN", flagType = "oneANDtwo" });
            }
            return View();
        }

        //Get Edit
        public async Task<IActionResult> Edit_PaymentReceipt(int id)
        {
            AccSafeCashEditAddDTO accSafeCashDTO = await _accSafeCashRepo.GetPaymentReceiptByIdAsync(id);
            return View(accSafeCashDTO);
        }

        ////Post Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_PaymentReceipt(int id, AccSafeCashEditAddDTO accSafeCashDTO)
        {
            if (ModelState.IsValid)
            {
                await _accSafeCashRepo.EditPaymentReceiptAsync(id, accSafeCashDTO);
                return RedirectToAction("Index", "AccSafeCash", new { docType = "SFCIN", flagType = "oneANDtwo" });
            }
            return View(accSafeCashDTO);
        }

        // Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete_PaymentReceipt(int id)
        {
            AccSafeCash accSafeCash = await _accSafeCashRepo.GetByIdAsync(id);

            await _accSafeCashRepo.DeleteAsync(accSafeCash);
            await _accSafeCashRepo.SaveChangesAsync();
            return RedirectToAction("Index", "AccSafeCash",new { docType = "SFCIN", flagType = "oneANDtwo" });
        }
    }
}
