using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProSoft.Core.Repositories.Medical.HospitalPatData;
using ProSoft.EF.DTOs.Medical.HospitalPatData;
using ProSoft.EF.IRepositories.Medical.Analysis;
using ProSoft.EF.IRepositories.Medical.HospitalPatData;
using ProSoft.EF.Models.Medical.HospitalPatData;

namespace ProSoft.UI.Areas.Medical.Controllers
{
    [Authorize]
    [Area("Medical")]
    public class HospitalPatDataController : Controller
    {
        private readonly IPatientRepo _patRepo;
        private readonly IClinicTransRepo _clinicTransRepo;

        public HospitalPatDataController(IPatientRepo patRepo, IClinicTransRepo clinicTransRepo)
        {
            _patRepo = patRepo;
            _clinicTransRepo = clinicTransRepo;
        }
        /// Analysis Transactionsv ///
        public async Task<IActionResult> AnalysisTransactions()
        {
            List<PatViewDTO> patients = await _patRepo.GetAllPatsAsync();
            return View(patients);
        }
        /// Invoices ///
        public async Task<IActionResult> Invoices()
        {
            ClinicTransEditAddDTO clinicTransEditAddDTO = await _clinicTransRepo.GetEmptyClinicTransAsync();

            ViewBag.MainClinics = clinicTransEditAddDTO.MainClinics;

            List<PatViewDTO> patients = await _patRepo.GetAllPatsAsync();
            return View(patients);
        }
        /// Invoices ///
        public async Task<IActionResult> Requirements()
        {
            List<PatViewDTO> patients = await _patRepo.GetAllPatsAsync();
            return View(patients);
        }
    }
}
