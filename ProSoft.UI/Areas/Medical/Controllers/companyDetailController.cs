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
    public class companyDetailController : Controller
    {
        private readonly ICompanyDtlRepo _companyDtlRepo;
        public companyDetailController(ICompanyDtlRepo companyDtlRepo)
        {
            _companyDtlRepo = companyDtlRepo;
        }

        public async Task<IActionResult> GetCompanyDetail(int id)
        {
            List<CompanyDtlViewDTO> companyDtlDTOs = await _companyDtlRepo.GetAllCompanyDtlAsync(id);
            return Json(companyDtlDTOs);
        }

        //Get add
        public async Task<IActionResult> Add_CompanyDtl(int id)
        {
            ViewBag.CompanyID = await _companyDtlRepo.GetNewIdAsync();
            return View();
        }

        ////Post add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add_CompanyDtl(int id, CompanyDtlEditAddDTO companyDtlDTO)
        {
            if (ModelState.IsValid)
            {
                await _companyDtlRepo.AddCompanyDtllAsync(id, companyDtlDTO);
                return RedirectToAction("Index", "CompanyGroup");
            }
            return View();
        }

        //Get Edit
        public async Task<IActionResult> Edit_CompanyDtl(int id)
        {
            CompanyDtlEditAddDTO companyDtlDTO = await _companyDtlRepo.GetCompanyDtlByIdAsync(id);
            return View(companyDtlDTO);
        }

        //Post Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_CompanyDtl(int id, CompanyDtlEditAddDTO companyDtlDTO)
        {
            if (ModelState.IsValid)
            {
                await _companyDtlRepo.EditCompanyDtlAsync(id, companyDtlDTO);
                return RedirectToAction("Index", "CompanyGroup");
            }
            return View(companyDtlDTO);
        }

        // Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete_CompanyDtl(int id)
        {
            CompanyDtl companyDtl = await _companyDtlRepo.GetByIdAsync(id);

            await _companyDtlRepo.DeleteAsync(companyDtl);
            await _companyDtlRepo.SaveChangesAsync();
            return RedirectToAction("Index", "CompanyGroup");
        }
    }
}
