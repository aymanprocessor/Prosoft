using AutoMapper;
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
                /// كنت مستخدم ال Dto بتاع ال view

                Doctor myDoctor = _mapper.Map<Doctor>(doctorDTO);
                await _doctorRepo.AddAsync(myDoctor);
                await _doctorRepo.SaveChangesAsync();
                // await _doctorRepo.AddDoctorAsync(doctorDTO);

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
    }
}
