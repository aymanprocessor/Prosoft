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

        // Get Add
        public async Task<IActionResult> Add_TransDetailWithPrice(int id)
        {
            TransDtlWithPriceDTO transDtlDTO = await _transDtlRepo.GetNewTransDtlAsync();
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

                await _transDtlRepo.addTransDtlWithPriceAsync(transDtlDTO);
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
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit_TransDetailWithPrice(int id, TransMasterEditAddDTO permissionFormDTO)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        await _transMasterRepo.UpdateTransMasterAsync(id, permissionFormDTO);
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(permissionFormDTO);
        //}

        // Delete
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Delete_PermissionForm(int id)
        //{
        //    TransMaster permissionForm = await _transMasterRepo.GetByIdAsync(id);

        //    await _transMasterRepo.DeleteAsync(permissionForm);
        //    await _transMasterRepo.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}
    }
}
