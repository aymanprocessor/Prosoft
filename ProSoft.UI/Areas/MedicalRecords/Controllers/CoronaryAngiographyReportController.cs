using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProSoft.EF.DTOs.MedicalRecords;
using ProSoft.EF.IRepositories.Medical.HospitalPatData;
using ProSoft.EF.IRepositories.MedicalRecords;
using ProSoft.EF.Models.Medical.HospitalPatData;
using ProSoft.EF.Models.MedicalRecords;

namespace ProSoft.UI.Areas.MedicalRecords.Controllers
{
    [Authorize]
    [Area(nameof(MedicalRecords))]
    public class CoronaryAngiographyReportController : Controller
    {
        private readonly ICoronaryRepo _coronaryRepo;
        private readonly IPatientRepo _patientRepo;
        public CoronaryAngiographyReportController(
            ICoronaryRepo coronaryRepo, IPatientRepo patientRepo)
        {
            _coronaryRepo = coronaryRepo;
            _patientRepo = patientRepo;
        }

        public async Task<IActionResult> Index(int id)
        {
            Pat pat = await _patientRepo.GetByIdAsync(id);
            ViewBag.PatName = pat.PatName;
            ViewBag.PatSSN = pat.PatIdCard;
            List<CoronaryDTO> coronaryListDTO = await _coronaryRepo.GetAllByPatIdAsync(id);

            return View(coronaryListDTO);
        }

        public async Task<IActionResult> Add_CoronaryReport(int id)
        {
            Pat pat = await _patientRepo.GetByIdAsync(id);

            ViewBag.patID = id;
            ViewBag.nationalID = pat?.PatIdCard;
            ViewBag.patName = pat?.PatName;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add_CoronaryReport(int id, CoronaryDTO coronaryDTO)
        {
            if (ModelState.IsValid)
            {
                await _coronaryRepo.AddCoronaryAsync(id, coronaryDTO);
                return RedirectToAction(nameof(Index), new { id });
            }
            return View(coronaryDTO);
        }

        public async Task<IActionResult> Edit_CoronaryReport(int id, int record)
        {
            Pat pat = await _patientRepo.GetByIdAsync(id);
            CoronaryDTO coronaryDTO = await _coronaryRepo.GetCoronaryByIdAsync(record);

            ViewBag.patName = pat?.PatName;
            return View(coronaryDTO);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_CoronaryReport(int id, int SerialHistory, CoronaryDTO coronaryDTO)
        {
            if (ModelState.IsValid)
            {
                await _coronaryRepo.EditCoronaryAsync(SerialHistory, coronaryDTO);
                return RedirectToAction(nameof(Index), new { id });
            }
            return View(coronaryDTO);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete_CoronaryReport(int id, int patId)
        {
            CoronaryAngiographyReport coronary = await _coronaryRepo.GetByIdAsync(id);

            await _coronaryRepo.DeleteAsync(coronary);
            await _coronaryRepo.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { id = patId });
        }
    }
}
