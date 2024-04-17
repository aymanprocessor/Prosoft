using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ProSoft.Core.Repositories.Medical.HospitalPatData;
using ProSoft.EF.DTOs.Medical.HospitalPatData;
using ProSoft.EF.IRepositories.Medical.HospitalPatData;
using ProSoft.EF.Models.Medical.HospitalPatData;

namespace ProSoft.UI.Areas.Medical.Controllers
{
    [Authorize]
    [Area("Medical")]
    public class ServiceClinicController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IServiceClinicRepo _servClinicRepo;
        public ServiceClinicController(IMapper mapper, IServiceClinicRepo servClinicRepo)
        {
            _mapper = mapper;
            _servClinicRepo = servClinicRepo;    
        }
        public async Task<IActionResult> GetServClinic(int id)
        {
            List<ServiceClinicViewDTO> servClinicDTOs = await _servClinicRepo.GetAllServClinicAsync(id);
            return Json(servClinicDTOs);
        }

        //Get Add
        public async Task<IActionResult> Add_ServClinic(int id,int clinicId)
        {
            ViewBag.ServeID = await _servClinicRepo.GetNewIdAsync();
            ViewBag.clinicId=clinicId;
            
            ServClinicEditAddDTO servClinicDTO =await _servClinicRepo.GetEmptyServClinicAsync(id);
            return View(servClinicDTO);
        }

        //Get Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add_ServClinic(int id,int clinicID, ServClinicEditAddDTO servClinicDTO)
        {
            if (ModelState.IsValid)
            {
                await _servClinicRepo.AddServClinicAsync(id,clinicID, servClinicDTO);
                return RedirectToAction("Index", "MainClinic");
            }
            return View();
        }

        //Get Edit
        public async Task<IActionResult> Edit_ServClinic(int id)
        {
            ServClinicEditAddDTO servClinic = await _servClinicRepo.GetServClinicByIdAsync(id);
            return View(servClinic);
        }
        //Post Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_ServClinic(int id, ServClinicEditAddDTO servClinicDTO)
        {
            if (ModelState.IsValid)
            {
                await _servClinicRepo.EditServClinicAsync(id, servClinicDTO);
                return RedirectToAction("Index", "MainClinic");
            }
            return View(servClinicDTO);
        }

        // Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete_ServClinic(int id)
        {
            ServiceClinic servClinic = await _servClinicRepo.GetByIdAsync(id);

            await _servClinicRepo.DeleteAsync(servClinic);
            await _servClinicRepo.SaveChangesAsync();
            return RedirectToAction("Index", "MainClinic");
        }
    }
}
