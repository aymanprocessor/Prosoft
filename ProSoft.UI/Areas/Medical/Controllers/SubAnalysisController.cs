using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProSoft.Core.Repositories.Medical.Analysis;
using ProSoft.EF.DTOs.Medical.Analysis;
using ProSoft.EF.IRepositories.Medical.Analysis;
using System.Collections.Generic;

namespace ProSoft.UI.Areas.Medical.Controllers
{
    [Authorize]
    [Area("Medical")]
    public class SubAnalysisController : Controller
    {
        private readonly ISubRepo _subRepo;
        private readonly IMainRepo _mainRepo;
        private readonly IMapper _mapper;
        public SubAnalysisController(IMapper mapper, ISubRepo subRepo, IMainRepo mainRepo)
        {
            _subRepo = subRepo;
            _mapper = mapper;
            _mainRepo = mainRepo;
        }

        // To Get SubAnalysis By Ajax
        public async Task<IActionResult> GetSubsByMain(string id)
        {
            List<SubViewDTO> subAnalysisDTO = await _subRepo.GetSubsByMainAsync(id);
            return Json(subAnalysisDTO);
        }

        //Get Add
        public async Task<IActionResult> AddSubAnalysis(string id)
        {
            string newSubCode = await _subRepo.GetNewSubAsync(id);
            ViewBag.maincode = id;

            #region sidebar
            ViewBag.MainLevel_3 = await _mainRepo.GetMainsByLevelAsync(3);
            ViewBag.MainLevel_4 = await _mainRepo.GetMainsByLevelAsync(4);
            ViewBag.MainLevel_5 = await _mainRepo.GetMainsByLevelAsync(5);
            #endregion

            ViewBag.subcode = newSubCode;
            ViewBag.CurrentName = (await _mainRepo.GetMainByIdAsync(id)).MainName;
            return View();
        }

        //Post Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddSubAnalysis(string id, SubEditAddDTO subDTO)
        {
            if (ModelState.IsValid)
            {
                string grandCode = await _subRepo.GetParentCodeAsync(id);
                await _subRepo.AddSubAnalysisAsync(id, subDTO);
                return RedirectToAction("MainLevel_6", "MainAnalysis", new { id = grandCode });
            }
            return View(subDTO);
        }

        //Get Edit
        public async Task<IActionResult> EditSubAnalysis(string id, string maincode)
        {
            SubViewDTO subAnalysis = await _subRepo.GetSubByIDsAsync(id, maincode);
            SubEditAddDTO subAnalysisDTO = _mapper.Map<SubEditAddDTO>(subAnalysis);

            #region sidebar
            ViewBag.MainLevel_3 = await _mainRepo.GetMainsByLevelAsync(3);
            ViewBag.MainLevel_4 = await _mainRepo.GetMainsByLevelAsync(4);
            ViewBag.MainLevel_5 = await _mainRepo.GetMainsByLevelAsync(5);
            #endregion

            return View(subAnalysisDTO);
        }

        //Post Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditSubAnalysis(string id, SubEditAddDTO subDTO)
        {
            if (ModelState.IsValid)
            {
                string grandCode = await _subRepo.GetParentCodeAsync(subDTO.MainCode);
                await _subRepo.EditSubAnalysisAsync(id, subDTO);
                return RedirectToAction("MainLevel_6", "MainAnalysis", new { id = grandCode });
            }
            return View(subDTO);
        }

        // Post Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteSubAnalysis(string id, string maincode)
        {
            string grandCode = await _subRepo.GetParentCodeAsync(maincode);
            await _subRepo.DeleteSubAnalysisAsync(id, maincode);
            return RedirectToAction("MainLevel_6", "MainAnalysis", new { id = grandCode });
        }
    }
}
