using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProSoft.EF.DTOs.Stocks;
using ProSoft.EF.IRepositories.Stocks;
using ProSoft.EF.Models.Stocks;

namespace ProSoft.UI.Areas.Stocks.Controllers
{
    [Authorize]
    [Area(nameof(Stocks))]
    public class PermissionFormDetailsController : Controller
    {
        private readonly ITransDtlRepo _transDtlRepo;
        public PermissionFormDetailsController(ITransDtlRepo transDtlRepo)
        {
            _transDtlRepo = transDtlRepo;
        }

        public async Task<IActionResult> GetPermissionDetails(int id)
        {
            List<TransDtlWithPriceDTO> transDtlListDTO = await _transDtlRepo.GetPermissionDetailsAsync(id);
            return Json(transDtlListDTO);
        }

        public async Task<IActionResult> GetBarCode(int transMAsterID, int serial, int itemMaster)
        {
            var barCode = await _transDtlRepo.GetBarCodeAsync(transMAsterID, serial, itemMaster);
            return Json(barCode);
        }

        // For Showing Trans Price
        // Get Add
        public async Task<IActionResult> Add_TransDetailWithPrice(int id)
        {
            TransDtlWithPriceDTO transDtlDTO = await _transDtlRepo.GetNewTransDtlWithPriceAsync(id);
            return View(transDtlDTO);
        }

        // Post Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add_TransDetailWithPrice(int id, TransDtlWithPriceDTO transDtlDTO)
        {
            if (ModelState.IsValid)
            {
                transDtlDTO.BranchId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "U_Branch_Id").Value);
                transDtlDTO.UserCode = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "User_Code").Value);
                transDtlDTO.TransMasterID = id;

                await _transDtlRepo.AddTransDtlWithPriceAsync(transDtlDTO);
                return RedirectToAction("Index", "PermissionForm");
            }
            return View(transDtlDTO);
        }

        // Get Edit
        public async Task<IActionResult> Edit_TransDetailWithPrice(int id)
        {
            TransDtlWithPriceDTO transDtlDTO = await _transDtlRepo.GetTransDtlWithPriceByIdAsync(id);
            return View(transDtlDTO);
        }

        // Post Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_TransDetailWithPrice(int id, TransDtlWithPriceDTO transDtlDTO)
        {
            if (ModelState.IsValid)
            {
                transDtlDTO.BranchId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "U_Branch_Id").Value);
                transDtlDTO.UserCode = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "User_Code").Value);

                await _transDtlRepo.UpdateTransDtlWithPriceAsync(id, transDtlDTO);
                return RedirectToAction("Index", "PermissionForm");
            }
            return View(transDtlDTO);
        }

        ////////////////////////////////////////////////////////////////////////////

        // For Not Showing Trans Price
        // Get Add
        public async Task<IActionResult> Add_TransDetail(int id)
        {
            TransDtlDTO transDtlDTO = await _transDtlRepo.GetNewTransDtlAsync(id);
            return View(transDtlDTO);
        }

        // Post Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add_TransDetail(int id, TransDtlDTO transDtlDTO)
        {
            if (ModelState.IsValid)
            {
                transDtlDTO.BranchId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "U_Branch_Id").Value);
                transDtlDTO.UserCode = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "User_Code").Value);
                transDtlDTO.TransMasterID = id;

                await _transDtlRepo.AddTransDtlAsync(transDtlDTO);
                return RedirectToAction("Index", "PermissionForm");
            }
            return View(transDtlDTO);
        }

        // Get Edit
        public async Task<IActionResult> Edit_TransDetail(int id)
        {
            TransDtlDTO transDtlDTO = await _transDtlRepo.GetTransDtlByIdAsync(id);
            return View(transDtlDTO);
        }

        // Post Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_TransDetail(int id, TransDtlDTO transDtlDTO)
        {
            if (ModelState.IsValid)
            {
                transDtlDTO.BranchId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "U_Branch_Id").Value);
                transDtlDTO.UserCode = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "User_Code").Value);
                
                await _transDtlRepo.UpdateTransDtlAsync(id, transDtlDTO);
                return RedirectToAction("Index", "PermissionForm");
            }
            return View(transDtlDTO);
        }

        // Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete_TransDetail(int id)
        {
            TransDtl transDetail = await _transDtlRepo.GetByIdAsync(id);

            await _transDtlRepo.DeleteAsync(transDetail);
            await _transDtlRepo.SaveChangesAsync();
            return RedirectToAction("Index", "PermissionForm");
        }
    }
}
