using AutoMapper;
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
    public class DailyFollowUpCCUChantController : Controller
    {
        private readonly IDailyFollowUpRepo _dailyFollowUpRepo;
        private readonly IPatientRepo _patientRepo;
        public DailyFollowUpCCUChantController( IDailyFollowUpRepo dailyFollowUpRepo, IPatientRepo patientRepo)
        {
            _dailyFollowUpRepo = dailyFollowUpRepo;
            _patientRepo = patientRepo;
        }
        public async Task<IActionResult> Index(int id)
        {
            List<DailyFollowUpDTO> dailyFollowUpDTOs = await _dailyFollowUpRepo.GetAllByPatIdAsync(id);
            Pat pat = await _patientRepo.GetByIdAsync(id);
            ViewBag.PatName = pat.PatName;
            ViewBag.PatSSN = pat.PatIdCard;

            return View(dailyFollowUpDTOs);
        }

        public async Task<IActionResult> Add_DailyFollowUp(int id)
        {
            Pat pat = await _patientRepo.GetByIdAsync(id);

            ViewBag.PatId = pat?.PatId;
            ViewBag.nationalID = pat?.PatIdCard;
            ViewBag.patName = pat?.PatName;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add_DailyFollowUp(int id, DailyFollowUpDTO dailyFollowDTO)
        {
            if (ModelState.IsValid)
            {
                await _dailyFollowUpRepo.AddDailyFollowUpAsync(id, dailyFollowDTO);
                return RedirectToAction(nameof(Index), new { id });
            }
            return View(dailyFollowDTO);
        }

        public async Task<IActionResult> Edit_DailyFollowUp(int id, int record)
        {
            Pat pat = await _patientRepo.GetByIdAsync(id);

            DailyFollowUpDTO dailyFollowUpDTO = await _dailyFollowUpRepo.GetDailyFollowUpByIdAsync(record);
            ViewBag.patName = pat?.PatName;
            return View(dailyFollowUpDTO);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_DailyFollowUp(int id, int SerialHistory, DailyFollowUpDTO dailyFollowDTO)
        {
            if (ModelState.IsValid)
            {
                await _dailyFollowUpRepo.EditDailyFollowUpAsync(SerialHistory, dailyFollowDTO);
                return RedirectToAction(nameof(Index), new { id });
            }
            return View(dailyFollowDTO);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete_DailyFollowUp(int id,int patId)
        {

            DailyFollowUpCcuChant dailyFollowUp = await _dailyFollowUpRepo.GetByIdAsync(id);

            await _dailyFollowUpRepo.DeleteAsync(dailyFollowUp);
            await _dailyFollowUpRepo.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { id = patId });

        }
    }
}
