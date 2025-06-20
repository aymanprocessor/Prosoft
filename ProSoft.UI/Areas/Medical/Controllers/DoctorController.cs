﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProSoft.Core.Repositories.Medical.HospitalPatData;
using ProSoft.EF.DTOs.Medical.HospitalPatData;
using ProSoft.EF.IRepositories.Medical.HospitalPatData;
using ProSoft.EF.Models.Medical.Analysis;
using ProSoft.EF.Models.Medical.HospitalPatData;

namespace ProSoft.UI.Areas.Medical.Controllers
{
    [Authorize]
    [Area("Medical")]
    public class DoctorController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IDoctorRepo _doctorRepo;
        public DoctorController(IMapper mapper ,IDoctorRepo doctorRepo)
        {
            _mapper = mapper;
            _doctorRepo = doctorRepo;
        }
        public async Task<IActionResult> Index()
        {
            List<DoctorViewDTO> doctorsDTO = await _doctorRepo.GetAllDoctor();
            return View(doctorsDTO);
        }
        // Get Add
        public async Task<IActionResult> Add_Doctor()
        {
            ViewBag.DoctorID = await _doctorRepo.GetNewIdAsync();
            DoctorEditAddDTO doctorDTO = await _doctorRepo.GetEmptyDoctorAsync();
            return View(doctorDTO);
        }

        // Post Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add_Doctor(DoctorEditAddDTO doctorDTO)
        {
            if (ModelState.IsValid)
            {
               await _doctorRepo.AddDoctorAsync(doctorDTO);

                return RedirectToAction(nameof(Index));
            }
            return View(doctorDTO);
        }

        // Get Edit
        public async Task<IActionResult> Edit_Doctor(int id)
        {
            DoctorEditAddDTO doctorDTO = await _doctorRepo.GetDoctorByIdAsync(id);
            return View(doctorDTO);
        }

        // Post Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_Doctor(int id,DoctorEditAddDTO doctorDTO)
        {
            if (ModelState.IsValid)
            {
                await _doctorRepo.EditDoctotAsync(id , doctorDTO);
                return RedirectToAction(nameof(Index));
            }
            return View(doctorDTO);
        }

        // Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete_Doctor(int id)
        {
            Doctor doctor = await _doctorRepo.GetByIdAsync(id);

            await _doctorRepo.DeleteAsync(doctor);
            await _doctorRepo.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> GetDoctors()
        {
            List<DoctorViewDTO> doctorsDTO = await _doctorRepo.GetAllDoctor();
            return Json(doctorsDTO);
        }
    }
}
