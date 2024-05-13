using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProSoft.Core.Repositories.Accounts;
using ProSoft.Core.Repositories.Medical.HospitalPatData;
using ProSoft.EF.DTOs.Accounts;
using ProSoft.EF.DTOs.Medical.HospitalPatData;
using ProSoft.EF.IRepositories.Accounts;
using ProSoft.EF.IRepositories.Medical.HospitalPatData;
using ProSoft.EF.Models.Accounts;
using ProSoft.EF.Models.Medical.HospitalPatData;

namespace ProSoft.UI.Areas.Accounts.Controllers
{
    [Authorize]
    [Area(nameof(Accounts))]
    public class AccSafeCashController : Controller
    {
        private readonly IAccSafeCashRepo _accSafeCashRepo;
        public AccSafeCashController(IAccSafeCashRepo accSafeCashRepo)
        {
            _accSafeCashRepo = accSafeCashRepo;
        }
        public async Task<IActionResult> Index(string docType,string? flagType)
        {

            List<AccSafeCashViewDTO> accSafeCashs = await _accSafeCashRepo
            .GetAccSafeCashAsync(docType, flagType);

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
        public async Task<IActionResult> Add_PaymentReceipt()
        {
            ViewBag.AccSafeCashID = await _accSafeCashRepo.GetNewIdAsync();
            ViewBag.SerialID = await _accSafeCashRepo.GetNewIdAsync();
            AccSafeCashEditAddDTO accSafeCashDTO = await _accSafeCashRepo.GetPaymentReceiptAsync();

            return View(accSafeCashDTO);
        }
        //Get Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add_PaymentReceipt(AccSafeCashEditAddDTO accSafeCashDTO)
        {
            if (ModelState.IsValid)
            {
                accSafeCashDTO.FYear = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "F_Year").Value);

                await _accSafeCashRepo.AddPaymentReceiptAsync(accSafeCashDTO);
                return RedirectToAction("Index", "AccSafeCash" ,new { docType = "SFCIN", flagType = "oneANDtwo" });
            }
            return View();
        }
        //Get Edit
        //public async Task<IActionResult> Edit_Deposit(int id)
        //{
        //    DepositEditAddDTO depositDTO = await _depositRepo.GetDepositByIdAsync(id);
        //    return View(depositDTO);
        //}

        ////Post Edit
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit_Deposit(int id, DepositEditAddDTO depositDTO)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        depositDTO.UserModify = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "User_Code").Value);

        //        await _depositRepo.EditDepositAsync(id, depositDTO);
        //        return RedirectToAction("Index", "DepositVisit", new { id = depositDTO.MasterId });
        //    }
        //    return View(depositDTO);
        //}

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
