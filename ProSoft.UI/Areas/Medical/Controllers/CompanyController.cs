using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProSoft.Core.Repositories.Medical.HospitalPatData;
using ProSoft.EF.DTOs.Medical.HospitalPatData;
using ProSoft.EF.IRepositories.Medical.HospitalPatData;
using ProSoft.EF.Models.Medical.HospitalPatData;

namespace ProSoft.UI.Areas.Medical.Controllers
{
    [Authorize]
    [Area("Medical")]
    public class CompanyController : Controller
    {
        private readonly ICompanyRepo _companyRepo;
        public CompanyController(ICompanyRepo companyRepo)
        {
            _companyRepo = companyRepo;
        }

        public async Task<IActionResult> GetCompany(int id)
        {
            List<CompanyViewDTO> companyDTOs = await _companyRepo.GetAllCompanyAsync(id);
            return Json(companyDTOs);
        }

      

        //Get add
        public async Task<IActionResult> Add_Company(int id)
        {
           ViewBag.CompanyID = await _companyRepo.GetNewIdAsync();
           CompanyEditAddDTO companyDTO = await _companyRepo.GetEmptyCompanyAsync(id);
           
           return View(companyDTO);
        }

        //Post add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add_Company(int id,CompanyEditAddDTO companyDTO)
        {
            if (ModelState.IsValid)
            {
                await _companyRepo.AddCompanylAsync(id, companyDTO);
                return RedirectToAction("Index", "CompanyGroup");
            }
            return View();
        }

        //Get Edit
        public async Task<IActionResult> Edit_Company(int id)
        {
            CompanyEditAddDTO companyDTO = await _companyRepo.GetCompanyByIdAsync(id);
            return View(companyDTO);
        }

        //Post Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_Company(int id, CompanyEditAddDTO companyDTO)
        {
            if (ModelState.IsValid)
            {
                await _companyRepo.EditCompanyAsync(id, companyDTO);
                return RedirectToAction("Index", "CompanyGroup");
            }
            return View(companyDTO);
        }

        // Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete_Company(int id)
        {
            Company company = await _companyRepo.GetByIdAsync(id);

            await _companyRepo.DeleteAsync(company);
            await _companyRepo.SaveChangesAsync();
            return RedirectToAction("Index", "CompanyGroup");
        }
    }
}
