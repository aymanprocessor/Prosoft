using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public HospitalPatDataController(IPatientRepo patRepo)
        {
            _patRepo = patRepo;
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
