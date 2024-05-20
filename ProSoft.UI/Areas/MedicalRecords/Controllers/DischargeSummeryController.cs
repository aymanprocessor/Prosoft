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
    public class DischargeSummeryController : Controller
    {
        private readonly IDischargeRepo _dischargeRepo;
        private readonly IPatientRepo _patientRepo;
        public DischargeSummeryController(IDischargeRepo dischargeRepo, IPatientRepo patientRepo)
        {
            _dischargeRepo = dischargeRepo;
            _patientRepo = patientRepo;
        }

        public async Task<IActionResult> Index(int id)
        {
            Pat pat = await _patientRepo.GetByIdAsync(id);
            ViewBag.PatName = pat.PatName;
            ViewBag.PatSSN = pat.PatIdCard;
            List<DischargeDTO> dischargeListDTO = await _dischargeRepo.GetAllByPatIdAsync(id);

            return View(dischargeListDTO);
        }

        public async Task<IActionResult> Add_DischargeSummery(int id)
        {
            Pat pat = await _patientRepo.GetByIdAsync(id);

            ViewBag.patID = id;
            ViewBag.nationalID = pat?.PatIdCard;
            ViewBag.patName = pat?.PatName;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add_DischargeSummery(int id, DischargeDTO dischargeDTO)
        {
            if (ModelState.IsValid)
            {
                await _dischargeRepo.AddDischargeAsync(id, dischargeDTO);
                return RedirectToAction(nameof(Index), new { id });
            }
            return View(dischargeDTO);
        }

        public async Task<IActionResult> Edit_DischargeSummery(int id, int record)
        {
            Pat pat = await _patientRepo.GetByIdAsync(id);
            DischargeDTO dischargeDTO = await _dischargeRepo.GetDischargeByIdAsync(record);

            ViewBag.patName = pat?.PatName;
            return View(dischargeDTO);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_DischargeSummery(int id, int SerialHistory, DischargeDTO dischargeDTO)
        {
            if (ModelState.IsValid)
            {
                await _dischargeRepo.EditDischargeAsync(SerialHistory, dischargeDTO);
                return RedirectToAction(nameof(Index), new { id });
            }
            return View(dischargeDTO);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete_DischargeSummery(int id, int patId)
        {
            DischargeSummery discharge = await _dischargeRepo.GetByIdAsync(id);

            await _dischargeRepo.DeleteAsync(discharge);
            await _dischargeRepo.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { id = patId });
        }
    }
}
