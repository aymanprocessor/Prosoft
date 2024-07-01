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
using ArabicNumbersConverter;
using Humanizer;
using System.Globalization;
using System.Security.Policy;

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

        public async Task<IActionResult> Index(string docType,string? flagType ,string? errorMessage ,string? message = "")
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
                if (cash.ValuePay !=null)
                {
                    cash.ValuePayWord = ConvertNumberToArabicWords(decimal.Parse(cash.ValuePay.ToString()));
                }
            }
            ViewBag.docType = docType;
            ViewBag.safeCode = safeCode;
            ViewBag.fYear = fYear;
            ViewBag.error = errorMessage;
            ViewBag.message = message;
            return View(accSafeCashs);
        }

        ///// ajacx//////
        public async Task<IActionResult> GetNumbersToWord(decimal id)
        {
            string numberWord = ConvertNumberToArabicWords(id);
            return Json(numberWord);
        }

        //// Method to convert numbers to Arabic words
        private string ConvertNumberToArabicWords(decimal number)
        {
            long wholePart = (long)number;
            int fractionalPart = (int)((number - wholePart) * 100); // Consider only two decimal places

            string wholePartInWords = wholePart.ToWords(new CultureInfo("ar"));
            string fractionalPartInWords = fractionalPart.ToWords(new CultureInfo("ar"));

            if (fractionalPart > 0)
            {
                return $"{wholePartInWords} جنيها و {fractionalPartInWords} قرشاً فقط";
            }
            else
            {
                return $"{wholePartInWords} جنيها فقط";
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
        public async Task<IActionResult> Add_PaymentReceipt(string docType ,int safeCode, int fYear)
        {
           // ViewBag.AccSafeCashID = await _accSafeCashRepo.GetNewIdAsync();
            ViewBag.SerialID = await _accSafeCashRepo.GetNewSerialAsync(docType, safeCode, fYear);
            var userCode = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "User_Code").Value);
            AccSafeCashEditAddDTO accSafeCashDTO = await _accSafeCashRepo.GetEmptyAccSafeCashAsync(userCode);
            ViewBag.docType = docType;
            ViewBag.fYear = fYear;
            ViewBag.userCode = userCode;
            ViewBag.branchId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "U_Branch_Id").Value);
            
            return View(accSafeCashDTO);
        }
        //Post add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add_PaymentReceipt(AccSafeCashEditAddDTO accSafeCashDTO)
        {
            if (ModelState.IsValid)
            {
              string message=  await _accSafeCashRepo.AddAccSafeCashAsync(accSafeCashDTO);
                return RedirectToAction("Index", "AccSafeCash" ,new { docType = "SFCIN", flagType = "oneANDtwo", message });
            }
            return View();
        }

        //Get Edit
        public async Task<IActionResult> Edit_PaymentReceipt(int id)
        {
            var userCode = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "User_Code").Value);
            ViewBag.userCode = userCode;
            ViewBag.branchId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "U_Branch_Id").Value);
            AccSafeCashEditAddDTO accSafeCashDTO = await _accSafeCashRepo.GetAccSafeCashByIdAsync(id, userCode);
            return View(accSafeCashDTO);
        }

        ////Post Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_PaymentReceipt(int id, AccSafeCashEditAddDTO accSafeCashDTO)
        {
            if (ModelState.IsValid)
            {
                string message =  await _accSafeCashRepo.EditAccSafeCashAsync(id, accSafeCashDTO);
                return RedirectToAction("Index", "AccSafeCash", new { docType = "SFCIN", flagType = "oneANDtwo", message });
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
                //ViewBag.ErrorMessage = "Cannot delete this record because it has related data in another table.";
                return RedirectToAction("Index", "AccSafeCash", new { docType = "SFCIN", flagType = "oneANDtwo" , errorMessage });
            }

            await _accSafeCashRepo.DeleteAsync(accSafeCash);
            await _accSafeCashRepo.SaveChangesAsync();
            return RedirectToAction("Index", "AccSafeCash",new { docType = "SFCIN", flagType = "oneANDtwo" });
        }
    }
}
