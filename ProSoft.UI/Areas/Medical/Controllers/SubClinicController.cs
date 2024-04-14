using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProSoft.Core.Repositories.Medical.HospitalPatData;
using ProSoft.EF.DTOs.Medical.HospitalPatData;
using ProSoft.EF.IRepositories.Medical.HospitalPatData;
using ProSoft.EF.Models.Medical.HospitalPatData;

namespace ProSoft.UI.Areas.Medical.Controllers
{
    [Authorize]
    [Area("Medical")]
    public class SubClinicController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ISubClinicRepo _subClinicRebo;
        public SubClinicController(IMapper mapper, ISubClinicRepo subClinicRebo)
        {   
            _mapper = mapper;
            _subClinicRebo = subClinicRebo;
        }
        public async Task<IActionResult> GetSubClinic(int id)
        {
            List<SubClinicViewDTO> subClinicDTOs = await _subClinicRebo.GetAllSubClinicsAsync(id);
            return Json(subClinicDTOs);
        }

        //Get Add
        public async Task<IActionResult> Add_SubClinic(int id)
        {
            ViewBag.SClinicID = await _subClinicRebo.GetNewIdAsync();
            SubClinicEditAddDTO subClinicDTO = await _subClinicRebo.GetEmptySubClinicAsync();
            return View(subClinicDTO);

        }

        //Post Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add_SubClinic(int id , SubClinicEditAddDTO subClinicDTO)
        {
            if (ModelState.IsValid)
            {
                await _subClinicRebo.AddSubClinicAsync(id, subClinicDTO);
                return RedirectToAction("Index", "MainClinic");
            }
            return View();

        }
        //Get Edit
        public async Task<IActionResult> Edit_SubClinic(int id)
        {
            SubClinicEditAddDTO subClinicDTO = await _subClinicRebo.GetSubClinicByIdAsync(id);
            return View(subClinicDTO);
        }
        //Post Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_SubClinic(int id , SubClinicEditAddDTO subClinicDTO)
        {
            if (ModelState.IsValid)
            {
                await _subClinicRebo.EditSubClinicAsync(id, subClinicDTO);
                return RedirectToAction("Index", "MainClinic");
            }
            return View(subClinicDTO);
        }

        // Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete_SubClinic(int id)
        {
            SubClinic subClinic = await _subClinicRebo.GetByIdAsync(id);

            await _subClinicRebo.DeleteAsync(subClinic);
            await _subClinicRebo.SaveChangesAsync();
            return RedirectToAction("Index", "MainClinic");
        }
    }
}
