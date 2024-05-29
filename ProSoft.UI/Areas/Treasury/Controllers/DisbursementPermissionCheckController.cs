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
    public class DisbursementPermissionCheckController : Controller
    {
        private readonly IAccSafeCheckRepo _accSafeCheckRepo;
        private readonly IUserCashNoRepo _userCashNoRepo;

        public DisbursementPermissionCheckController(IAccSafeCheckRepo accSafeCheckRepo, IUserCashNoRepo userCashNoRepo)
        {
            _accSafeCheckRepo = accSafeCheckRepo;
            _userCashNoRepo = userCashNoRepo;
        }

        public async Task<IActionResult> Index(string tranType, string? flagType, string? errorMessage)
        {
            var userCode = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "User_Code").Value);
            var userCashNoDTO = await _userCashNoRepo.GetSafeTransByIdAsync(userCode);
            var safeCode = userCashNoDTO.SafeCode;
            var fYear = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "F_Year").Value);
            var branchId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "U_Branch_Id").Value);

            List<AccSafeCheckViewDTO> accSafeChecks = await _accSafeCheckRepo
            .GetAccSafeCashAsync(tranType, flagType, fYear, safeCode);

            // Convert the numbers to Arabic words
            foreach (var cash in accSafeChecks)
            {
                if (cash.ValuePay != null)
                {
                    cash.ValuePayWord = ConvertNumberToArabicWords(decimal.Parse(cash.ValuePay.ToString()));
                }
            }
            ViewBag.tranType = tranType;
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
                return $"{wholePartInWords} و {fractionalPartInWords} جنيها فقط";
            }
            else
            {
                return $"{wholePartInWords} جنيها فقط";
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
        public async Task<IActionResult> Add_DisbursementCheck(string tranType, int safeCode, int fYear)
        {
            // ViewBag.AccSafeCashID = await _accSafeCashRepo.GetNewIdAsync();
            ViewBag.SerialID = await _accSafeCheckRepo.GetNewSerialAsync(tranType, safeCode, fYear);
            AccSafeCheckEditAddDTO accSafeCeckDTO = await _accSafeCheckRepo.GetEmptyAccSafeCeckAsync();
            ViewBag.tranType = tranType;
            ViewBag.fYear = fYear;
            ViewBag.mainName13 = accSafeCeckDTO.mainName13;
            ViewBag.mainName17 = accSafeCeckDTO.mainName17;

            return View(accSafeCeckDTO);
        }
        //Post add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add_DisbursementCheck(AccSafeCheckEditAddDTO accSafeCeckDTO)
        {
            if (ModelState.IsValid)
            {
                await _accSafeCheckRepo.AddAccSafeCeckAsync(accSafeCeckDTO);
                return RedirectToAction("Index", "DisbursementPermissionCheck", new { tranType = "SFOUT", flagType = "oneANDtwo" });
            }
            return View();
        }


        //Get Edit
        public async Task<IActionResult> Edit_DisbursementCheck(int id)
        {
            AccSafeCheckEditAddDTO accSafeCheckDTO = await _accSafeCheckRepo.GetAccSafeCheckByIdAsync(id);
            ViewBag.mainName13 = accSafeCheckDTO.mainName13;
            ViewBag.mainName17 = accSafeCheckDTO.mainName17;
            return View(accSafeCheckDTO);
        }

        ////Post Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_DisbursementCheck(int id, AccSafeCheckEditAddDTO accSafeCheckDTO)
        {
            if (ModelState.IsValid)
            {
                await _accSafeCheckRepo.EditAccSafeCheckAsync(id, accSafeCheckDTO);
                return RedirectToAction("Index", "DisbursementPermissionCheck", new { tranType = "SFOUT", flagType = "oneANDtwo" });
            }
            return View(accSafeCheckDTO);
        }

        // Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete_DisbursementCheck(int id)
        {
            AccSafeCheck accSafeCheck = await _accSafeCheckRepo.GetByIdAsync(id);

            bool hasRelatedData = await _accSafeCheckRepo.HasRelatedDataAsync(id, accSafeCheck.TranType);
            if (hasRelatedData)
            {
                var errorMessage = "Cannot delete this record because it has related data in another table!";
                //ViewBag.ErrorMessage = "Cannot delete this record because it has related data in another table.";
                return RedirectToAction("Index", "DisbursementPermissionCheck", new { tranType = "SFOUT", flagType = "oneANDtwo", errorMessage });
            }

            await _accSafeCheckRepo.DeleteAsync(accSafeCheck);
            await _accSafeCheckRepo.SaveChangesAsync();
            return RedirectToAction("Index", "DisbursementPermissionCheck", new { tranType = "SFOUT", flagType = "oneANDtwo" });
        }
    }
}
