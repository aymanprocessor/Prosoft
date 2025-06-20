﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProSoft.Core.Repositories.Medical.HospitalPatData;
using ProSoft.EF.DbContext;
using ProSoft.EF.DTOs.Medical.HospitalPatData;
using ProSoft.EF.IRepositories.Medical.HospitalPatData;
using ProSoft.EF.Models.Medical.HospitalPatData;
using System.Collections.Generic;

namespace ProSoft.UI.Areas.Medical.Controllers
{
    [Authorize]
    [Area("Medical")]
    public class DoctorPercentageController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IDoctorsPersentageRepo _drPersentageRepo;
        public DoctorPercentageController(IMapper mapper, IDoctorsPersentageRepo drPersentageRepo)
        {
            _mapper = mapper;
            _drPersentageRepo = drPersentageRepo;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.doctors = await _drPersentageRepo.GetAllDoctorAsync(); 
            return View();
        }
        public async Task<IActionResult> GetDoctorPresentages(int id)
        {
           List<DoctorPrecentViewDTO> doctorPrecentDTOs =await _drPersentageRepo.GetAllDoctorPercentageAsync(id);
            return Json(doctorPrecentDTOs);
        }

        //Get Edit
        public async Task<IActionResult> Edit_DoctorPerc(int id)
        {
            DoctorPrecentEditAddDTO doctorPrecentDTO = await _drPersentageRepo.GetDoctorPrecntByIdAsync(id);
            return View(doctorPrecentDTO);
        }

        //Post Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_DoctorPerc(int id, DoctorPrecentEditAddDTO doctorPrecentDTO)
        {
            if (ModelState.IsValid)
            {
                await _drPersentageRepo.EditDoctorPercentAsync(id, doctorPrecentDTO);
                return RedirectToAction("Index", "DoctorPercentage");
            }
            return View(doctorPrecentDTO);
        }
        // Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete_DoctorPerc(int id)
        {
            DoctorsPercent doctorsPercent = await _drPersentageRepo.GetByIdAsync(id);

            await _drPersentageRepo.DeleteAsync(doctorsPercent);
            await _drPersentageRepo.SaveChangesAsync();
            return RedirectToAction("Index", "DoctorPercentage");
        }



    }
}
