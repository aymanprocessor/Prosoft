using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
            return Json("");
        }

        // Get Add
        public async Task<IActionResult> Add_TransDetailWithPrice(int id)
        {
            TransDtl permissionFormDTO = await _transDtlRepo.GetByIdAsync(id);
            return View(permissionFormDTO);
        }

        // Post Add
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Add_TransDetailWithPrice(int id, TransMasterEditAddDTO permissionFormDTO)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        permissionFormDTO.BranchId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "U_Branch_Id").Value);
        //        permissionFormDTO.UserCode = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "User_Code").Value);

        //        TransMaster permissionForm = await _transMasterRepo.AddPermissionFormAsync(permissionFormDTO);
        //        return RedirectToAction(nameof(Index), new { id = permissionForm.TransMAsterID });
        //    }
        //    return View(permissionFormDTO);
        //}

        // Get Edit
        //public async Task<IActionResult> Edit_PermissionForm(int id)
        //{
        //    TransMasterEditAddDTO permissionFormDTO = await _transMasterRepo.GetPermissionFormByIdAsync(id);
        //    return View(permissionFormDTO);
        //}

        // Post Edit
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit_PermissionForm(int id, TransMasterEditAddDTO permissionFormDTO)
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
