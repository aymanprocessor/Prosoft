using AutoMapper;
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
    public class HistoryExaminationController : Controller
    {
        private readonly IHistoryExaminationRepo _historyExaminationRepo;
        private readonly IPatientRepo _patientRepo;
        public HistoryExaminationController(IHistoryExaminationRepo historyExaminationRepo, IPatientRepo patientRepo)
        {
            _historyExaminationRepo = historyExaminationRepo;
            _patientRepo = patientRepo;
        }
        public async Task<IActionResult> Index(int id)
        {
            List<HistoryExaminationDTO> historyExaminationDTOs = await _historyExaminationRepo.GetAllByPatIdAsync(id);
            Pat pat = await _patientRepo.GetByIdAsync(id);
            ViewBag.PatName = pat.PatName;
            ViewBag.PatSSN = pat.PatIdCard;
            return View(historyExaminationDTOs);
        }

        public async Task<IActionResult> Add_HistoryExamination(int id)
        {
            Pat pat = await _patientRepo.GetByIdAsync(id);

            ViewBag.PatId = pat?.PatId;
            ViewBag.nationalID = pat?.PatIdCard;
            ViewBag.patName = pat?.PatName;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add_HistoryExamination(int id, HistoryExaminationDTO historyExaminationDTO)
        {
            if (ModelState.IsValid)
            {
                await _historyExaminationRepo.AddHistoryExaminationAsync(id, historyExaminationDTO);
                return RedirectToAction(nameof(Index), new { id });
            }
            return View(historyExaminationDTO);
        }

        public async Task<IActionResult> Edit_HistoryExamination(int id, int record)
        {
            Pat pat = await _patientRepo.GetByIdAsync(id);

            HistoryExaminationDTO historyExaminationDTO = await _historyExaminationRepo.GetHistoryExaminationByIdAsync(record);
            ViewBag.patName = pat?.PatName;
            return View(historyExaminationDTO);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_HistoryExamination(int id, int SerialHistory, HistoryExaminationDTO historyExaminationDTO)
        {
            if (ModelState.IsValid)
            {
                await _historyExaminationRepo.EditHistoryExaminationAsync(SerialHistory, historyExaminationDTO);
                return RedirectToAction(nameof(Index), new { id });
            }
            return View(historyExaminationDTO);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete_HistoryExamination(int id, int patId)
        {
            HistoryExamination historyExamination = await _historyExaminationRepo.GetByIdAsync(id);

            await _historyExaminationRepo.DeleteAsync(historyExamination);
            await _historyExaminationRepo.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { id = patId });
        }
    }
}
