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
    public class PastHistoryController : Controller
    {
        private readonly IPastHistoryRepo _pastHistoryRepo;
        private readonly IPatientRepo _patientRepo;

        public PastHistoryController(
            IPatientRepo patientRepo, IPastHistoryRepo pastHistoryRepo)
        {
            _pastHistoryRepo = pastHistoryRepo;
            _patientRepo = patientRepo;
        }

        public async Task<IActionResult> Index(int id)
        {
            Pat pat = await _patientRepo.GetByIdAsync(id);
            ViewBag.PatName = pat.PatName;
            ViewBag.PatSSN = pat.PatIdCard;
            List<PastHistoryDTO> pastHistoryListDTO = await _pastHistoryRepo.GetAllByPatIdAsync(id);

            return View(pastHistoryListDTO);
        }

        public async Task<IActionResult> Add_PastHistory(int id)
        {
            Pat pat = await _patientRepo.GetByIdAsync(id);

            ViewBag.patID = id;
            ViewBag.nationalID = pat?.PatIdCard;
            ViewBag.patName = pat?.PatName;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add_PastHistory(int id, PastHistoryDTO pastHistoryDTO)
        {
            if (ModelState.IsValid)
            {
                await _pastHistoryRepo.AddPastHistoryAsync(id, pastHistoryDTO);
                return RedirectToAction(nameof(Index), new { id });
            }
            return View(pastHistoryDTO);
        }

        public async Task<IActionResult> Edit_PastHistory(int id, int record)
        {
            Pat pat = await _patientRepo.GetByIdAsync(id);
            PastHistoryDTO pastHistoryDTO = await _pastHistoryRepo.GetPastHistoryByIdAsync(record);

            ViewBag.patName = pat?.PatName;
            return View(pastHistoryDTO);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_PastHistory(int id, int SerialHistory, PastHistoryDTO pastHistoryDTO)
        {
            if (ModelState.IsValid)
            {
                await _pastHistoryRepo.EditPastHistoryAsync(SerialHistory, pastHistoryDTO);
                return RedirectToAction(nameof(Index), new { id });
            }
            return View(pastHistoryDTO);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete_PastHistory(int id, int patId)
        {
            PastHistory pastHistory = await _pastHistoryRepo.GetByIdAsync(id);

            await _pastHistoryRepo.DeleteAsync(pastHistory);
            await _pastHistoryRepo.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { id = patId });
        }
    }
}
