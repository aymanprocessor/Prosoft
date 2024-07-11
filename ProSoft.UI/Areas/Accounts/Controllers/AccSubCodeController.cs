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
        public async Task<IActionResult> Add_AccSubCode(string id)
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
            return View();
        }

        ////Post Add
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> AddSubAnalysis(string id, SubEditAddDTO subDTO)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        string grandCode = await _accSubCodeRepo.GetParentCodeAsync(id);
        //        await _accSubCodeRepo.AddSubAnalysisAsync(id, subDTO);
        //        return RedirectToAction("MainLevel_6", "MainAnalysis", new { id = grandCode });
        //    }
        //    return View(subDTO);
        //}

        ////Get Edit
        //public async Task<IActionResult> EditSubAnalysis(string id, string maincode)
        //{
        //    SubViewDTO subAnalysis = await _accSubCodeRepo.GetSubByIDsAsync(id, maincode);
        //    SubEditAddDTO subAnalysisDTO = _mapper.Map<SubEditAddDTO>(subAnalysis);

        //    #region sidebar
        //    ViewBag.MainLevel_2 = await _accMainCodeRepo.GetMainsByLevelAsync(2);
        //    ViewBag.MainLevel_3 = await _accMainCodeRepo.GetMainsByLevelAsync(3);
        //    ViewBag.MainLevel_4 = await _accMainCodeRepo.GetMainsByLevelAsync(4);
        //    ViewBag.MainLevel_5 = await _accMainCodeRepo.GetMainsByLevelAsync(5);

        //    #endregion

        //    return View(subAnalysisDTO);
        //}

        ////Post Edit
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> EditSubAnalysis(string id, SubEditAddDTO subDTO)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        string grandCode = await _accSubCodeRepo.GetParentCodeAsync(subDTO.MainCode);
        //     // await _accSubCodeRepo.EditSubAnalysisAsync(id, subDTO);
        //        return RedirectToAction("MainLevel_6", "MainAnalysis", new { id = grandCode });
        //    }
        //    return View(subDTO);
        //}

        //// Post Delete
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteSubAnalysis(string id, string maincode)
        //{
        //    string grandCode = await _accSubCodeRepo.GetParentCodeAsync(maincode);
        //   // await _accSubCodeRepo.DeleteSubAnalysisAsync(id, maincode);
        //    return RedirectToAction("MainLevel_6", "MainAnalysis", new { id = grandCode });
        //}
    }
}
