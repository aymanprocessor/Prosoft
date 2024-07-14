using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProSoft.EF.DTOs.Accounts;
using ProSoft.EF.DTOs.Medical.Analysis;
using ProSoft.EF.IRepositories.Accounts;
using ProSoft.EF.Models.Accounts;

namespace ProSoft.UI.Areas.Accounts.Controllers
{
    [Authorize]
    [Area(nameof(Accounts))]
    public class AccSubCodeController : Controller
    {
        private readonly IAccSubCodeRepo _accSubCodeRepo;
        private readonly IAccMainCodeRepo _accMainCodeRepo;
        private readonly IMapper _mapper;
        public AccSubCodeController(IMapper mapper, IAccSubCodeRepo accSubCodeRepo, IAccMainCodeRepo accMainCodeRepo)
        {
            _accSubCodeRepo = accSubCodeRepo;
            _accMainCodeRepo= accMainCodeRepo;
            _mapper = mapper;
        }

        // To Get SubAnalysis By Ajax
        public async Task<IActionResult> GetSubsByMain(string id)
        {
            List<AccSubCodeDTO> accSubCodeDTO = await _accSubCodeRepo.GetSubsByMainAsync(id);
            return Json(accSubCodeDTO);
        }
        //Get Add
        public async Task<IActionResult> AddAccSubCode(string id,string? actionName)
        {
            string newSubCode = await _accSubCodeRepo.GetNewSubAsync(id);
            ViewBag.maincode = id;

            #region sidebar
            ViewBag.MainLevel_2 = await _accMainCodeRepo.GetMainsByLevelAsync(2);
            ViewBag.MainLevel_3 = await _accMainCodeRepo.GetMainsByLevelAsync(3);
            ViewBag.MainLevel_4 = await _accMainCodeRepo.GetMainsByLevelAsync(4);
            ViewBag.MainLevel_5 = await _accMainCodeRepo.GetMainsByLevelAsync(5);

            #endregion
            ViewBag.subcode = newSubCode;
            ViewBag.CurrentName = (await _accMainCodeRepo.GetMainByIdAsync(id)).MainName;
            ViewBag.ActionName = actionName;
            ViewBag.Grand = await _accSubCodeRepo.GetParentCodeAsync(id);
            return View();
        }

        //Post Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddAccSubCode(string id, AccSubCodeDTO subDTO)
        {
            if (ModelState.IsValid)
            {
                string grandCode = await _accSubCodeRepo.GetParentCodeAsync(id);
                await _accSubCodeRepo.AddAccSubCodeAsync(id, subDTO);
                if (subDTO.ActionName == "MainLevel_2")
                {
                 return RedirectToAction(subDTO.ActionName, "AccMainCode");   
                }
                return RedirectToAction(subDTO.ActionName, "AccMainCode", new { id = grandCode });
            }
            return View(subDTO);
        }

        //Get Edit
        public async Task<IActionResult> EditAccSubCode(string id, string maincode, string? actionName)
        {
            AccSubCodeDTO accSubCodeDTO = await _accSubCodeRepo.GetSubByIDsAsync(id, maincode);
            #region sidebar
            ViewBag.MainLevel_2 = await _accMainCodeRepo.GetMainsByLevelAsync(2);
            ViewBag.MainLevel_3 = await _accMainCodeRepo.GetMainsByLevelAsync(3);
            ViewBag.MainLevel_4 = await _accMainCodeRepo.GetMainsByLevelAsync(4);
            ViewBag.MainLevel_5 = await _accMainCodeRepo.GetMainsByLevelAsync(5);
            #endregion
            ViewBag.ActionName = actionName;
            ViewBag.Grand = await _accSubCodeRepo.GetParentCodeAsync(maincode);
            return View(accSubCodeDTO);
        }

        //Post Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAccSubCode(string id, AccSubCodeDTO subDTO)
        {
            if (ModelState.IsValid)
            {
                string grandCode = await _accSubCodeRepo.GetParentCodeAsync(subDTO.MainCode);
                 await _accSubCodeRepo.EditAccSubCodeAsync(id, subDTO);
                if (subDTO.ActionName == "MainLevel_2")
                {
                    return RedirectToAction(subDTO.ActionName, "AccMainCode");
                }
                return RedirectToAction(subDTO.ActionName, "AccMainCode", new { id = grandCode });
            }
            return View(subDTO);
        }

        // Post Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAccSubCode(string id, string maincode,string actionName)
        {
            string grandCode = await _accSubCodeRepo.GetParentCodeAsync(maincode);
            await _accSubCodeRepo.DeleteAccSubCodeAsync(id, maincode);
            if (actionName == "MainLevel_2")
            {
                return RedirectToAction(actionName, "AccMainCode");
            }
            return RedirectToAction(actionName, "AccMainCode", new { id = grandCode });
        }
    }
}
