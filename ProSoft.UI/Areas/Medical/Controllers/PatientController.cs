using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProSoft.Core.Repositories.Medical.HospitalPatData;
using ProSoft.EF.DTOs.Medical.HospitalPatData;
using ProSoft.EF.IRepositories.Medical.HospitalPatData;
using ProSoft.EF.Models.Medical.HospitalPatData;

namespace ProSoft.UI.Areas.Medical.Controllers
{
    [Authorize]
    [Area("Medical")]
    public class PatientController : Controller
    {
        private readonly IPatientRepo _patRepo;
        public PatientController(IPatientRepo patRepo)
        {
            _patRepo = patRepo;
        }
        //Get all
        public async Task<IActionResult> Patients()
        {
            List<PatViewDTO> patients = await _patRepo.GetAllPatsAsync();
            return View(patients);
        }

        // Get Add
        public async Task<IActionResult> Add_Patient(string redirect, string controll)
        {
            ViewBag.redirect = redirect;
            ViewBag.controll = controll;
            return View();
        }

        // Post Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add_Patient(string redirect,string controll, PatEditAddDTO patDTO)
        {
            if (ModelState.IsValid)
            {
                await _patRepo.AddPatientAsync(patDTO);
                return RedirectToAction(redirect, controll);
            }
            return View(patDTO);
        }

        //Get Edit
        public async Task<IActionResult> Edit_Patient(int id)
        {
            PatEditAddDTO patEditAddDTO = await _patRepo.GetPatientByIdAsync(id);
            ViewBag.Master = id;
            return View(patEditAddDTO);
        }
        //Post Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_Patient(int id, PatEditAddDTO patientDTO)
        {
            if (ModelState.IsValid)
            {
                await _patRepo.EditPatientAsync(id, patientDTO);
                return RedirectToAction("Patients", "Patient");
            }
            return View(patientDTO);
        }

        //Delete ClinicTrans 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete_Patient(int id)
        {
            await _patRepo.DeletePatientAsync(id);
            return RedirectToAction("Patients", "Patient");
        }

    }
}
