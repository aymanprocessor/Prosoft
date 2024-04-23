using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProSoft.Core.Repositories.Medical.HospitalPatData;
using ProSoft.EF.DTOs.Medical.HospitalPatData;
using ProSoft.EF.IRepositories.Medical.HospitalPatData;

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
          //  ViewBag.DoctorID = await _docSubDtlRepo.GetNewIdAsync();

         //DocSubDtlEditAddDTO docSubDtlDTO = await _docSubDtlRepo.GetEmptyDocSubDtlAsync(id);
            return View();
        }

    }
}
