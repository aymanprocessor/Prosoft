using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProSoft.Core.Repositories.Accounts;
using ProSoft.EF.DTOs.Accounts;
using ProSoft.EF.IRepositories.Accounts;

namespace ProSoft.UI.Areas.Accounts.Controllers
{
    [Authorize]
    [Area(nameof(Accounts))]
    public class AccMainCodeDtlController : Controller
    {
        private readonly IAccMainCodeDtlRepo _accMainCodeDtlRepo;
        private readonly IAccMainCodeRepo _accMainCodeRepo;
        private readonly IAccSubCodeRepo _accSubCodeRepo;

        public AccMainCodeDtlController(IAccMainCodeDtlRepo accMainCodeDtlRepo, IAccMainCodeRepo accMainCodeRepo, IAccSubCodeRepo accSubCodeRepo)
        {
            _accMainCodeDtlRepo = accMainCodeDtlRepo;
            _accMainCodeRepo = accMainCodeRepo;
            _accSubCodeRepo = accSubCodeRepo;
        }
        public async Task<IActionResult> GetAccMainCodeDtl(string id)
        {
            List<AccMainCodeDtlDTO> accMainCodeDtlDTOs = await _accMainCodeDtlRepo.GetAccMainCodeDtl(id);
            return Json(accMainCodeDtlDTOs);
        }
        //Get Add
        public async Task<IActionResult> Add_AccMainCodeDtl(string id, string? actionName)
        {
            ViewBag.maincode = id;
            #region sidebar
            ViewBag.MainLevel_2 = await _accMainCodeRepo.GetMainsByLevelAsync(2);
            ViewBag.MainLevel_3 = await _accMainCodeRepo.GetMainsByLevelAsync(3);
            ViewBag.MainLevel_4 = await _accMainCodeRepo.GetMainsByLevelAsync(4);
            ViewBag.MainLevel_5 = await _accMainCodeRepo.GetMainsByLevelAsync(5);

            #endregion
            ViewBag.CurrentName = (await _accMainCodeRepo.GetMainByIdAsync(id)).MainName;
            ViewBag.ActionName = actionName;

            AccMainCodeDTO existingMainCode = await _accMainCodeRepo.GetMainByIdAsync(id);
            ViewBag.parent = existingMainCode.ParentCode;
            ViewBag.AccMainCode = await _accMainCodeRepo.GetAllMainsAsync();

            return View();
        }
        //Post Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add_AccMainCodeDtl(string id, AccMainCodeDtlDTO accMainCodeDtlDTO)
        {
            if (ModelState.IsValid)
            {
                var branchId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "U_Branch_Id").Value);
                accMainCodeDtlDTO.CoCode = branchId;
                string grandCode = await _accSubCodeRepo.GetParentCodeAsync(id);
                await _accMainCodeDtlRepo.AddAccMainCodeDtlAsync(accMainCodeDtlDTO);
                if (accMainCodeDtlDTO.ActionName == "MainLevel_2")
                {
                    return RedirectToAction(accMainCodeDtlDTO.ActionName, "AccMainCode", new { clickId = id });
                }
                return RedirectToAction(accMainCodeDtlDTO.ActionName, "AccMainCode", new { id = grandCode, clickId = id });
            }
            return View(accMainCodeDtlDTO);
        }

        //Post Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete_AccMainCodeDtl(string id, string maincode, string actionName)
        {
            var branchId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "U_Branch_Id").Value);
            string grandCode = await _accSubCodeRepo.GetParentCodeAsync(maincode);
            await _accMainCodeDtlRepo.DeleteAccMainCodeDtlAsync(id, maincode, branchId);
            if (actionName == "MainLevel_2")
            {
                return RedirectToAction(actionName, "AccMainCode", new { clickId = maincode});
            }
            return RedirectToAction(actionName, "AccMainCode", new { id = grandCode, clickId = maincode });
        }
    }
}
