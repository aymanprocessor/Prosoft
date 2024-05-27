using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProSoft.EF.DTOs.Accounts;
using ProSoft.EF.DTOs.Treasury;
using ProSoft.EF.IRepositories.Treasury;
using ProSoft.EF.Models.Treasury;
using System.Globalization;

namespace ProSoft.UI.Areas.Treasury.Controllers
{
    [Authorize]
    [Area(nameof(Treasury))]
    public class DisbursementPermissionController : Controller
    {
        private readonly IAccSafeCashRepo _accSafeCashRepo;
        private readonly IUserCashNoRepo _userCashNoRepo;
        public DisbursementPermissionController(IAccSafeCashRepo accSafeCashRepo, IUserCashNoRepo userCashNoRepo)
        {
            _accSafeCashRepo = accSafeCashRepo;
            _userCashNoRepo = userCashNoRepo;
        }
        public async Task<IActionResult> Index(string docType, string? flagType, string? errorMessage)
        {
            var userCode = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "User_Code").Value);
            var userCashNoDTO = await _userCashNoRepo.GetSafeTransByIdAsync(userCode);
            var safeCode = userCashNoDTO.SafeCode;
            var fYear = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "F_Year").Value);
            var branchId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "U_Branch_Id").Value);

            List<AccSafeCashViewDTO> accSafeCashs = await _accSafeCashRepo
            .GetAccSafeCashAsync(docType, flagType, fYear, safeCode);

            // Convert the numbers to Arabic words
            foreach (var cash in accSafeCashs)
            {
                if (cash.ValuePay != null)
                {
                    cash.ValuePayWord = ConvertNumberToArabicWords(decimal.Parse(cash.ValuePay.ToString()));
                }
            }

            ViewBag.docType = docType;
            ViewBag.safeCode = safeCode;
            ViewBag.fYear = fYear;
            ViewBag.error = errorMessage;

            return View(accSafeCashs);
        }

        //// Method to convert numbers to Arabic words
        private string ConvertNumberToArabicWords(decimal number)
        {
            long wholePart = (long)number;
            int fractionalPart = (int)((number - wholePart) * 100); // Adjust this if you need more precision

            string wholePartInWords = wholePart.ToWords(new CultureInfo("ar"));
            string fractionalPartInWords = fractionalPart.ToWords(new CultureInfo("ar"));

            if (fractionalPart > 0)
            {
                return $"{wholePartInWords} و {fractionalPartInWords}";
            }
            else
            {
                return wholePartInWords;
            }
        }

        //Ajax In Add_StockTrans
        public async Task<IActionResult> GetSubCodesFromAcc(string id)
        {
            List<AccSubCodeDTO> subAccCodesDTO = await _accSafeCashRepo
                .GetSubCodesFromAccAsync(id);
            return Json(subAccCodesDTO);
        }
        //Get Add
        public async Task<IActionResult> Add_DisbursementPermission(string docType, int safeCode, int fYear)
        {
            // ViewBag.AccSafeCashID = await _accSafeCashRepo.GetNewIdAsync();
            ViewBag.SerialID = await _accSafeCashRepo.GetNewSerialAsync(docType, safeCode, fYear);
            AccSafeCashEditAddDTO accSafeCashDTO = await _accSafeCashRepo.GetEmptyAccSafeCashAsync();
            ViewBag.docType = docType;
            ViewBag.fYear = fYear;

            return View(accSafeCashDTO);
        }
        //Post add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add_DisbursementPermission(AccSafeCashEditAddDTO accSafeCashDTO)
        {
            if (ModelState.IsValid)
            {
                await _accSafeCashRepo.AddAccSafeCashAsync(accSafeCashDTO);
                return RedirectToAction("Index", "DisbursementPermission", new { docType = "SFCOT", flagType = "oneANDtwo" });
            }
            return View();
        }

        //Get Edit
        public async Task<IActionResult> Edit_DisbursementPermission(int id)
        {
            AccSafeCashEditAddDTO accSafeCashDTO = await _accSafeCashRepo.GetAccSafeCashByIdAsync(id);
            return View(accSafeCashDTO);
        }

        ////Post Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_DisbursementPermission(int id, AccSafeCashEditAddDTO accSafeCashDTO)
        {
            if (ModelState.IsValid)
            {
                await _accSafeCashRepo.EditAccSafeCashAsync(id, accSafeCashDTO);
                return RedirectToAction("Index", "DisbursementPermission", new { docType = "SFCOT", flagType = "oneANDtwo" });
            }
            return View(accSafeCashDTO);
        }

        // Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete_PaymentReceipt(int id)
        {
            AccSafeCash accSafeCash = await _accSafeCashRepo.GetByIdAsync(id);

            bool hasRelatedData = await _accSafeCashRepo.HasRelatedDataAsync(id, accSafeCash.DocType);
            if (hasRelatedData)
            {
                var errorMessage = "Cannot delete this record because it has related data in another table!";
                return RedirectToAction("Index", "DisbursementPermission", new { docType = "SFCOT", flagType = "oneANDtwo", errorMessage });
            }
            await _accSafeCashRepo.DeleteAsync(accSafeCash);
            await _accSafeCashRepo.SaveChangesAsync();
            return RedirectToAction("Index", "DisbursementPermission", new { docType = "SFCOT", flagType = "oneANDtwo" });
        }
    }
}
