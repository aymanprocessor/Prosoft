using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProSoft.Core.Repositories.MedicalRecords;
using ProSoft.EF.DTOs.MedicalRecords;
using ProSoft.EF.IRepositories.Medical.HospitalPatData;
using ProSoft.EF.IRepositories.MedicalRecords;
using ProSoft.EF.Models.Medical.HospitalPatData;
using ProSoft.EF.Models.MedicalRecords;

namespace ProSoft.UI.Areas.MedicalRecords.Controllers
{
    [Authorize]
    [Area(nameof(MedicalRecords))]
    public class LabReportController : Controller
    {
        private readonly ILabReportRepo _labReportRepo;
        private readonly IPatientRepo _patientRepo;
        public LabReportController(ILabReportRepo labReportRepo, IPatientRepo patientRepo)
        {
            _labReportRepo = labReportRepo;
            _patientRepo = patientRepo;
        }
        public async Task<IActionResult> Index(int id)
        {
            List<LabReportDTO> labReposrtDTOs = await _labReportRepo.GetAllByPatIdAsync(id);
            Pat pat = await _patientRepo.GetByIdAsync(id);
            ViewBag.PatName = pat.PatName;
            ViewBag.PatSSN = pat.PatIdCard;

            return View(labReposrtDTOs);
        }

        public async Task<IActionResult> Add_LabReport(int id)
        {
            Pat pat = await _patientRepo.GetByIdAsync(id);

            ViewBag.PatId = pat?.PatId;
            ViewBag.nationalID = pat?.PatIdCard;
            ViewBag.patName = pat?.PatName;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add_LabReport(int id, LabReportDTO labReportDTO)
        {
            if (ModelState.IsValid)
            {
                await _labReportRepo.AddLabReportAsync(id, labReportDTO);
                return RedirectToAction(nameof(Index), new { id });
            }
            return View(labReportDTO);
        }

        public async Task<IActionResult> Edit_LabReport(int id, int record)
        {
            Pat pat = await _patientRepo.GetByIdAsync(id);

            LabReportDTO labReportDTO = await _labReportRepo.GetLabReportByIdAsync(record);
            ViewBag.patName = pat?.PatName;
            return View(labReportDTO);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_LabReport(int id, int SerialHistory, LabReportDTO labReportDTO)
        {
            if (ModelState.IsValid)
            {
                await _labReportRepo.EditLabReportAsync(SerialHistory, labReportDTO);
                return RedirectToAction(nameof(Index), new { id });
            }
            return View(labReportDTO);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete_LabReport(int id, int patId)
        {
            LabReport labReport = await _labReportRepo.GetByIdAsync(id);

            await _labReportRepo.DeleteAsync(labReport);
            await _labReportRepo.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { id = patId });
        }
    }
}
