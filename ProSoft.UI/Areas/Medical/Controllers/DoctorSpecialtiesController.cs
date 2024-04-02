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
    public class DoctorSpecialtiesController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IDocSubDtlRepo _docSubDtlRepo;
        public DoctorSpecialtiesController(IMapper mapper, IDocSubDtlRepo docSubDtlRepo)
        {
            _mapper = mapper;
            _docSubDtlRepo = docSubDtlRepo;
        }
        public async Task<IActionResult> GetDocSubDtl(int id)
        {
            List<DocSubDtlViewDTO> docSubDtlDTOs =await _docSubDtlRepo.GetDocSubDtlByDoctorAsync(id);
            return Json(docSubDtlDTOs);
        }

        //Get add
        public async Task<IActionResult> Add_DocSubDtl(int id)
        {
            ViewBag.DoctorID = await _docSubDtlRepo.GetNewIdAsync();
            
            DocSubDtlEditAddDTO docSubDtlDTO = await _docSubDtlRepo.GetEmptyDocSubDtlAsync(id);
            return View(docSubDtlDTO);
        }

        //Get Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add_DocSubDtl(int id , DocSubDtlEditAddDTO docSubDtlDTO)
        {
            if (ModelState.IsValid)
            {
                await _docSubDtlRepo.AddDocSubDtlAsync(id, docSubDtlDTO);
                return RedirectToAction("Index","Doctor");
            }
            return View();
        }

        //Get Edit
        public async Task<IActionResult> Edit_DocSubDtl(int id)
        {
            DocSubDtlEditAddDTO docSubDtlDTO =await _docSubDtlRepo.GetDocSubDtlByIdAsync(id);
            return View(docSubDtlDTO);
        }

        //Post Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_DocSubDtl(int id, DocSubDtlEditAddDTO docSubDtlDTO)
        {
            if (ModelState.IsValid)
            {
                await _docSubDtlRepo.EditDocSubDtlAsync(id, docSubDtlDTO);
                return RedirectToAction("Index", "Doctor");
            }
            return View(docSubDtlDTO);
        }
        // Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete_DocSubDtl(int id)
        {
            DocSubDtl doctorSubDtl = await _docSubDtlRepo.GetByIdAsync(id);

            await _docSubDtlRepo.DeleteAsync(doctorSubDtl);
            await _docSubDtlRepo.SaveChangesAsync();
            return RedirectToAction("Index", "Doctor");
        }
    }
}
