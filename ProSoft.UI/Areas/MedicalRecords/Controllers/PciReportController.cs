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
    public class PciReportController : Controller
    {
        private readonly IPciReportRepo _pciReportRepo;
        private readonly IPatientRepo _patientRepo;
        public PciReportController(IPciReportRepo pciReportRepo, IPatientRepo patientRepo)
        {
            _pciReportRepo = pciReportRepo;
            _patientRepo = patientRepo;
        }
        public async Task<IActionResult> Index(int id)
        {
            List<PciReportDTO> pciReportDTOs = await _pciReportRepo.GetAllByPatIdAsync(id);
            Pat pat = await _patientRepo.GetByIdAsync(id);
            ViewBag.PatName = pat.PatName;
            ViewBag.PatSSN = pat.PatIdCard;
            return View(pciReportDTOs);
        }

        public async Task<IActionResult> Add_PCIReport(int id)
        {
            Pat pat = await _patientRepo.GetByIdAsync(id);

            ViewBag.PatId = pat?.PatId;
            ViewBag.nationalID = pat?.PatIdCard;
            ViewBag.patName = pat?.PatName;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add_PCIReport(int id, PciReportDTO pciReportDTO)
        {
            if (ModelState.IsValid)
            {
                await _pciReportRepo.AddPciReportAsync(id, pciReportDTO);
                return RedirectToAction(nameof(Index), new { id });
            }
            return View(pciReportDTO);
        }

        public async Task<IActionResult> Edit_PCIReport(int id, int record)
        {
            Pat pat = await _patientRepo.GetByIdAsync(id);

            PciReportDTO pciReportDTO = await _pciReportRepo.GetPciReportByIdAsync(record);
            ViewBag.patName = pat?.PatName;
            return View(pciReportDTO);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_PCIReport(int id, int SerialHistory, PciReportDTO pciReportDTO)
        {
            if (ModelState.IsValid)
            {
                await _pciReportRepo.EditPciReportAsync(SerialHistory, pciReportDTO);
                return RedirectToAction(nameof(Index), new { id });
            }
            return View(pciReportDTO);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete_PCIReport(int id,int patId)
        {

            PciReport pciReport = await _pciReportRepo.GetByIdAsync(id);

            await _pciReportRepo.DeleteAsync(pciReport);
            await _pciReportRepo.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { id = patId });

        }
    }
}
