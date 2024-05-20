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
    public class EcgAndEchoController : Controller
    {
        private readonly IEcgAndEchoRepo _ecgAndEchoRepo;
        private readonly IPatientRepo _patientRepo;
        public EcgAndEchoController(IEcgAndEchoRepo ecgAndEchoRepo,
            IPatientRepo patientRepo)
        {
            _ecgAndEchoRepo = ecgAndEchoRepo;
            _patientRepo = patientRepo;
        }

        public async Task<IActionResult> Index(int id)
        {
            Pat pat = await _patientRepo.GetByIdAsync(id);
            ViewBag.PatName = pat.PatName;
            ViewBag.PatSSN = pat.PatIdCard;
            List<EcgAndEchoDTO> ecgAndEchoListDTO = await _ecgAndEchoRepo.GetAllByPatIdAsync(id);

            return View(ecgAndEchoListDTO);
        }

        public async Task<IActionResult> Add_ECGAndEcho(int id)
        {
            Pat pat = await _patientRepo.GetByIdAsync(id);

            ViewBag.patID = id;
            ViewBag.nationalID = pat?.PatIdCard;
            ViewBag.patName = pat?.PatName;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add_ECGAndEcho(int id, EcgAndEchoDTO ecgAndEchoDTO)
        {
            if (ModelState.IsValid)
            {
                await _ecgAndEchoRepo.AddEcgAndEchoAsync(id, ecgAndEchoDTO);
                return RedirectToAction(nameof(Index), new { id });
            }
            return View(ecgAndEchoDTO);
        }

        public async Task<IActionResult> Edit_ECGAndEcho(int id, int record)
        {
            Pat pat = await _patientRepo.GetByIdAsync(id);
            EcgAndEchoDTO ecgAndEchoDTO = await _ecgAndEchoRepo.GetEcgAndEchoByIdAsync(record);

            ViewBag.patName = pat?.PatName;
            return View(ecgAndEchoDTO);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_ECGAndEcho(int id, int SerialHistory, EcgAndEchoDTO ecgAndEchoDTO)
        {
            if (ModelState.IsValid)
            {
                await _ecgAndEchoRepo.EditEcgAndEchoAsync(SerialHistory, ecgAndEchoDTO);
                return RedirectToAction(nameof(Index), new { id });
            }
            return View(ecgAndEchoDTO);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete_ECGAndEcho(int id, int patId)
        {
            EcgAndEcho ecgAndEcho = await _ecgAndEchoRepo.GetByIdAsync(id);

            await _ecgAndEchoRepo.DeleteAsync(ecgAndEcho);
            await _ecgAndEchoRepo.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { id = patId });
        }
    }
}
