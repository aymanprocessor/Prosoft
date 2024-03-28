using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProSoft.EF.DTOs.Medical.HospitalPatData;
using ProSoft.EF.IRepositories.Medical.HospitalPatData;
using ProSoft.EF.Models.Medical.Analysis;
using ProSoft.EF.Models.Medical.HospitalPatData;
using System.IO;

namespace ProSoft.UI.Areas.Medical.Controllers
{
    [Authorize]
    [Area("Medical")]
    public class AnalysisDetailController : Controller
    {
        private readonly IAnalysisDetailRepo _analysisDtlRepo;
        public AnalysisDetailController(IAnalysisDetailRepo analysisDtlRepo)
        {
            _analysisDtlRepo = analysisDtlRepo;
        }
        /// Get Ajax For AnalysisDetails
        public async Task<IActionResult> GetAnalysisDetail(int id)
        {
            List<AnalysiDtlViewDTO> analysis =await _analysisDtlRepo.GetAnalysisDtlByClinicTransAsync(id);
            return Json(analysis);
        }

        //Get Edit AnalysisDetails
        public async Task<IActionResult> Edit_Analysis(int id)
        {
            AnalysisDtlEditDTO analdetail = await _analysisDtlRepo.GetAnalysisDtlByIdAsync(id);
            return View(analdetail);
        }

        //Post Edit AnalysisDetails
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_Analysis(int id, AnalysisDtlEditDTO analdetailDTO)
        {
            if (ModelState.IsValid)
            {
                await _analysisDtlRepo.EditAnalysisDtl(id, analdetailDTO);
                return RedirectToAction("AnalysisTransactions", "HospitalPatData");
            }
            return View(analdetailDTO);
        }

        public async Task<IActionResult> PrintAnalysis(int id)
        {
            var data = await _analysisDtlRepo.PrintAnalysisDtlAsync(id);
            ViewBag.patName = data.patName;
            ViewBag.patId = data.patId;
            ViewBag.age = data.age;
            ViewBag.date = data.admissonDate;
            ViewBag.drName = data.drName;
            ViewBag.drId = data.drId;
            ViewBag.compName = data.compName;
            ViewBag.compNameDetail = data.compNameDetail;
            ViewBag.analysisName = data.analysisName;
            List<AnalysiDtlViewDTO> analysiDtlViewDTOs = data.analysiDtlViewDTOs;

            return View(analysiDtlViewDTOs);
        }
    }
}
