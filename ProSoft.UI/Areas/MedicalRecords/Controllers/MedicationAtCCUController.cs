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
    public class MedicationAtCCUController : Controller
    {
        private readonly IMedicationRepo _medicationRepo;
        private readonly IPatientRepo _patientRepo;
        public MedicationAtCCUController(IMedicationRepo medicationRepo, IPatientRepo patientRepo)
        {
            _medicationRepo = medicationRepo;
            _patientRepo = patientRepo;
        }
        public async Task<IActionResult> Index(int id)
        {
            List<MedicationDTO> medicationDTOs = await _medicationRepo.GetAllByPatIdAsync(id);
            Pat pat = await _patientRepo.GetByIdAsync(id);
            ViewBag.PatName = pat.PatName;
            ViewBag.PatSSN = pat.PatIdCard;
            return View(medicationDTOs);
        }

        public async Task<IActionResult> Add_MedicationAtCCU(int id)
        {
            Pat pat = await _patientRepo.GetByIdAsync(id);

            ViewBag.PatId = pat?.PatId;
            ViewBag.nationalID = pat?.PatIdCard;
            ViewBag.patName = pat?.PatName;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add_MedicationAtCCU(int id, MedicationDTO medicationDTO)
        {
            if (ModelState.IsValid)
            {
                await _medicationRepo.AddMedicationAsync(id, medicationDTO);
                return RedirectToAction(nameof(Index), new { id });
            }
            return View(medicationDTO);
        }

        public async Task<IActionResult> Edit_MedicationAtCCU(int id, int record)
        {
            Pat pat = await _patientRepo.GetByIdAsync(id);

            MedicationDTO medicationDTO = await _medicationRepo.GetMedicationByIdAsync(record);
            ViewBag.patName = pat?.PatName;
            return View(medicationDTO);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_MedicationAtCCU(int id, int SerialHistory, MedicationDTO medicationDTO)
        {
            if (ModelState.IsValid)
            {
                await _medicationRepo.EditMedicationAsync(SerialHistory, medicationDTO);
                return RedirectToAction(nameof(Index), new { id });
            }
            return View(medicationDTO);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete_MedicationAtCCU(int id, int patId)
        {

            MedicationAtCcu medication = await _medicationRepo.GetByIdAsync(id);

            await _medicationRepo.DeleteAsync(medication);
            await _medicationRepo.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { id = patId });

        }

    }
}
