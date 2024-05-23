using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProSoft.Core.Repositories.Treasury;
using ProSoft.EF.DTOs.Accounts;
using ProSoft.EF.DTOs.Treasury;
using ProSoft.EF.IRepositories.Treasury;
using System.Globalization;

namespace ProSoft.UI.Areas.Treasury.Controllers
{
    [Authorize]
    [Area(nameof(Treasury))]
    public class AccSafeCheckController : Controller
    {
        private readonly IAccSafeCheckRepo _accSafeCheckRepo;
        private readonly IUserCashNoRepo _userCashNoRepo;
        public AccSafeCheckController(IAccSafeCheckRepo accSafeCheckRepo, IUserCashNoRepo userCashNoRepo)
        {
            _accSafeCheckRepo = accSafeCheckRepo;
            _userCashNoRepo = userCashNoRepo;
        }
        public async Task<IActionResult> Index(string docType, string? flagType, string? errorMessage)
        {
            var userCode = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "User_Code").Value);
            var userCashNoDTO = await _userCashNoRepo.GetSafeTransByIdAsync(userCode);
            var safeCode = userCashNoDTO.SafeCode;
            var fYear = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "F_Year").Value);
            var branchId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "U_Branch_Id").Value);

            List<AccSafeCheckViewDTO> accSafeChecks = await _accSafeCheckRepo
            .GetAccSafeCashAsync(docType, flagType, fYear, safeCode);

            // Convert the numbers to Arabic words
            foreach (var cash in accSafeChecks)
            {
                if (cash.ValuePay != null)
                {
                    cash.ValuePayWord = ConvertNumberToArabicWords(decimal.Parse(cash.ValuePay.ToString()));
                }
            }
            ViewBag.tranType = docType;
            ViewBag.safeCode = safeCode;
            ViewBag.fYear = fYear;
            ViewBag.error = errorMessage;
            return View(accSafeChecks);
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
            List<AccSubCodeDTO> subAccCodesDTO = await _accSafeCheckRepo
                .GetSubCodesFromAccAsync(id);
            return Json(subAccCodesDTO);
        }
        //Get Add
        public async Task<IActionResult> Add_DepositCheck(string tranType, int safeCode, int fYear)
        {
            // ViewBag.AccSafeCashID = await _accSafeCashRepo.GetNewIdAsync();
            ViewBag.SerialID = await _accSafeCheckRepo.GetNewSerialAsync(tranType, safeCode, fYear);
            AccSafeCheckEditAddDTO accSafeCeckDTO = await _accSafeCheckRepo.GetEmptyAccSafeCeckAsync();
            ViewBag.tranType = tranType;
            ViewBag.fYear = fYear;

            return View(accSafeCeckDTO);
        }
        //Post add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add_DepositCheck(AccSafeCheckEditAddDTO accSafeCeckDTO)
        {
            if (ModelState.IsValid)
            {
                await _accSafeCheckRepo.AddAccSafeCeckAsync(accSafeCeckDTO);
                return RedirectToAction("Index", "AccSafeCheck", new { docType = "SFSIN", flagType = "oneANDtwo" });
            }
            return View();
        }
    }
}
