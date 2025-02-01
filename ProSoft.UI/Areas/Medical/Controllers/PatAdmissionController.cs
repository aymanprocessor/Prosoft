using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;
using ProSoft.Core.Repositories;
using ProSoft.Core.Repositories.Medical.HospitalPatData;
using ProSoft.EF.DTOs.Medical.HospitalPatData;
using ProSoft.EF.IRepositories.Medical.HospitalPatData;
using ProSoft.EF.Models.Medical.HospitalPatData;

namespace ProSoft.UI.Areas.Medical.Controllers
{
    [Authorize]
    [Area("Medical")]
    public class PatAdmissionController : Controller
    {
        private readonly IPatAdmissionRepo _patAdmissionRepo;
        private readonly ICurrentUserService _currentUserService;

        public PatAdmissionController(IPatAdmissionRepo patAdmissionRepo, ICurrentUserService currentUserService)
        {
            _patAdmissionRepo = patAdmissionRepo;
            _currentUserService = currentUserService;

        }

        public async Task<IActionResult> GetAdmissions(int id)
        {
            List<PatAdmissionViewDTO> patAdmissions = await _patAdmissionRepo
                .GetAdmissionsByPatAsync(id);
            return Json(patAdmissions);
        }
        /////////////////////////////////////////////////////////
        
        /// Get GetCompany
        public async Task<IActionResult> GetCompanys(int id)
        {
            List<CompanyViewDTO> companies = await _patAdmissionRepo.GetCompany(id);
            return Json(companies);
        }

        /// Get GetSubComp
        public async Task<IActionResult> GetSubComp(int id)
        {
            List<CompanyDtlViewDTO> companyDtls = await _patAdmissionRepo.GetSubCompany(id);
            return Json(companyDtls);
        }

        /// Get GetSection
        public async Task<IActionResult> GetSection(int id)
        {
            List<RegionViewDTO> sections = await _patAdmissionRepo.GetSection(id);
            return Json(sections);
        }
      
        ///////////////////////////////////////////////////////////////
        
        //Get Add PatAdmisson
        public async Task<IActionResult> Add_PatientAdmission(int id, string redirect)
        {
            PatAdmissionEditAddDTO patAdmissionEditAddDTO = await _patAdmissionRepo.GetEmptyPatAdmissionAsync(id);
            patAdmissionEditAddDTO.PatAdDate = DateTime.Now;
            //for redirction
            ViewBag.redirect = redirect;
            return View(patAdmissionEditAddDTO);
        }

        //Post Add PatAdmisson
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add_PatientAdmission(int id, string redirect, PatAdmissionEditAddDTO patAdmissionDTO)
        {
            if (ModelState.IsValid)
            {
                patAdmissionDTO.ExYear = _currentUserService.Year;
                patAdmissionDTO.BranchId = _currentUserService.BranchId;
                await _patAdmissionRepo.AddPatAdmissionAsync(id, patAdmissionDTO);
                return RedirectToAction(redirect, "HospitalPatData");
            }
            return View(patAdmissionDTO);
        }

        //Get Edit PatAdmisson
        public async Task<IActionResult> Edit_PatientAdmission(int id ,string redirect)
        {
            PatAdmissionEditAddDTO patAdmissionEditAddDTO = await _patAdmissionRepo.GetPatAdmissionByIdAsync(id);

            //for redirction
            ViewBag.redirect = redirect;
            return View(patAdmissionEditAddDTO);
        }
        //Post Edit PatAdmisson
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_PatientAdmission(int id, string redirect, PatAdmissionEditAddDTO patAdmissionDTO)
        {
            if (ModelState.IsValid)
            {
                patAdmissionDTO.ExYear = _currentUserService.Year;
                patAdmissionDTO.BranchId = _currentUserService.BranchId;
                await _patAdmissionRepo.EditPatAdmissionAsync(id, patAdmissionDTO);
                return RedirectToAction(redirect, "HospitalPatData");
            }
            return View(patAdmissionDTO);
        }

        //Delete PatAdmisson
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete_PatientAdmission(int id,string redirect)
        {
            await _patAdmissionRepo.DeletePatAdmissionAsync(id);
            return RedirectToAction(redirect, "HospitalPatData");
        }
    }
}
