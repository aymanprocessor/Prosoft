using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProSoft.EF.DTOs.Medical.HospitalPatData;
using ProSoft.EF.DTOs.Shared;
using ProSoft.EF.IRepositories.Medical.Analysis;
using ProSoft.EF.IRepositories.Medical.HospitalPatData;
using ProSoft.EF.Models.Medical.HospitalPatData;
using ProSoft.EF.Models.Shared;
using System.Collections.Generic;

namespace ProSoft.UI.Areas.Medical.Controllers
{
    [Authorize]
    [Area("Medical")]
    public class DrDegreeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IDrDegreeRepo _degreeRepo;
        public DrDegreeController(IMapper mapper, IDrDegreeRepo degreeRepo)
        {
            _mapper = mapper;
            _degreeRepo = degreeRepo;   
        }
        public async Task<IActionResult> Index()
        {
            List<DrDegree> degrees = await _degreeRepo.GetAllAsync();
            List<DrDegreeDTO> degressDTO = _mapper.Map<List<DrDegreeDTO>>(degrees);
            return View(degressDTO);
        }

        //Get Add
        public async Task<IActionResult> Add_DrDegree()
        {
            ViewBag.DegreeID =await _degreeRepo.GetNewIdAsync();
            return View();
        }
        //Post Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add_DrDegree(DrDegreeDTO degreeDTO)
        {
            if (ModelState.IsValid)
            {
                DrDegree drDegree = _mapper.Map<DrDegree>(degreeDTO);
                await _degreeRepo.AddAsync(drDegree);
                await _degreeRepo.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(degreeDTO);
        }
        //Get Add
        public async Task<IActionResult> Edit_DrDegree(int id)
        {
            DrDegree drDegree=await _degreeRepo.GetByIdAsync(id);
            DrDegreeDTO drDegreeDTO = _mapper.Map<DrDegreeDTO>(drDegree);
            return View(drDegreeDTO);
        }
        //Post Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_DrDegree(int id,DrDegreeDTO degreeDTO)
        {
            if (ModelState.IsValid)
            {
                DrDegree drDegree = await _degreeRepo.GetByIdAsync(id);
                _mapper.Map(degreeDTO, drDegree);

                await _degreeRepo.UpdateAsync(drDegree);
                await _degreeRepo.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(degreeDTO);
        }

        // Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete_DrDegree(int id)
        {
            DrDegree drDegree = await _degreeRepo.GetByIdAsync(id);

            await _degreeRepo.DeleteAsync(drDegree);
            await _degreeRepo.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
