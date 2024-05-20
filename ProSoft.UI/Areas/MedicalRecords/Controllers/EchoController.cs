using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProSoft.EF.DTOs.MedicalRecords;
using ProSoft.EF.IRepositories.Medical.HospitalPatData;
using ProSoft.EF.IRepositories.MedicalRecords;
using ProSoft.EF.Models.Medical.HospitalPatData;
using ProSoft.EF.Models.MedicalRecords;

namespace ProSoft.UI.Areas.MedicalRecords.Controllers
{
    [Authorize]
    [Area(nameof(MedicalRecords))]
    public class EchoController : Controller
    {
        private readonly IEchoRepo _echoRepo;
        private readonly IPatientRepo _patientRepo;
        public EchoController(IEchoRepo echoRepo, IPatientRepo patientRepo)
        {
            _echoRepo = echoRepo;
            _patientRepo = patientRepo;
        }

        public async Task<IActionResult> Index(int id)
        {
            Pat pat = await _patientRepo.GetByIdAsync(id);
            ViewBag.PatName = pat.PatName;
            ViewBag.PatSSN = pat.PatIdCard;
            List<EchoDTO> ecgAndEchoListDTO = await _echoRepo.GetAllByPatIdAsync(id);

            return View(ecgAndEchoListDTO);
        }

        public async Task<IActionResult> Add_Echo(int id)
        {
            Pat pat = await _patientRepo.GetByIdAsync(id);

            ViewBag.patID = id;
            ViewBag.nationalID = pat?.PatIdCard;
            ViewBag.patName = pat?.PatName;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add_Echo(int id, EchoDTO echoDTO)
        {
            if (ModelState.IsValid)
            {
                await _echoRepo.AddEchoAsync(id, echoDTO);
                return RedirectToAction(nameof(Index), new { id });
            }
            return View(echoDTO);
        }

        public async Task<IActionResult> Edit_Echo(int id, int record)
        {
            Pat pat = await _patientRepo.GetByIdAsync(id);
            EchoDTO echoDTO = await _echoRepo.GetEchoByIdAsync(record);

            ViewBag.patName = pat?.PatName;
            return View(echoDTO);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_Echo(int id, int SerialHistory, EchoDTO echoDTO)
        {
            if (ModelState.IsValid)
            {
                await _echoRepo.EditEchoAsync(SerialHistory, echoDTO);
                return RedirectToAction(nameof(Index), new { id });
            }
            return View(echoDTO);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete_Echo(int id, int patId)
        {
            Echo echo = await _echoRepo.GetByIdAsync(id);

            await _echoRepo.DeleteAsync(echo);
            await _echoRepo.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { id = patId });
        }
    }
}
