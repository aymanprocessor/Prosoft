using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProSoft.Core.Repositories.Medical.HospitalPatData;
using ProSoft.EF.DTOs.Medical.HospitalPatData;
using ProSoft.EF.IRepositories.Medical.HospitalPatData;

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
        //public async Task<IActionResult> Add_Company(int id)
        //{
        //    ViewBag.CompanyID = await _companyDtlRepo.GetNewIdAsync();
        //    CompanyEditAddDTO companyDTO = await _companyRepo.GetEmptyCompanyAsync(id);

        //    return View(companyDTO);
        //}

        ////Post add
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Add_Company(int id, CompanyEditAddDTO companyDTO)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        await _companyRepo.AddCompanylAsync(id, companyDTO);
        //        return RedirectToAction("Index", "CompanyGroup");
        //    }
        //    return View();
        //}
    }
}
