using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProSoft.Core.Repositories.Medical.HospitalPatData;
using ProSoft.EF.DTOs.Medical.HospitalPatData;
using ProSoft.EF.DTOs.Stocks;
using ProSoft.EF.IRepositories.Medical.HospitalPatData;
using ProSoft.EF.IRepositories.Stocks;
using ProSoft.EF.Models.Medical.HospitalPatData;

namespace ProSoft.UI.Areas.Shared.Controllers
{
    [Authorize]
    [Area("Shared")]
    public class ClassificationCustController : Controller
    {
        private readonly IClassificationCustRepo _classCustRepo;
        private readonly IMapper _mapper;
        public ClassificationCustController(IClassificationCustRepo classCustRepo, IMapper mapper)
        {
            _classCustRepo = classCustRepo;
            _mapper = mapper;
        }

        /// Get GetSubComp
        public async Task<IActionResult> GetDepartments()
        {
            List<ClassificationCust> departments = await _classCustRepo.GetAllAsync();
            return Json(departments);
        }
        public async Task<IActionResult> Index()
        {
            List<ClassificationCust> departments = await _classCustRepo.GetAllAsync();
            List<ClassificationViewDTO> departmentsDTO = _mapper.Map<List<ClassificationViewDTO>>(departments);
            return View(departmentsDTO);
        }

        // Get Add
        public async Task<IActionResult> Add_ClassCust()
        {
            ViewBag.departId = await _classCustRepo.GetNewIdAsync();
            return View();
        }

        // Post Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add_ClassCust(ClassificationViewDTO classCastDTO)
        {
            if (ModelState.IsValid)
            {
                ClassificationCust classCast = _mapper.Map<ClassificationCust>(classCastDTO);

                await _classCustRepo.AddAsync(classCast);
                await _classCustRepo.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(classCastDTO);
        }

        // Get Edit
        public async Task<IActionResult> Edit_ClassCust(int id)
        {
            ClassificationCust classCast = await _classCustRepo.GetByIdAsync(id);
            ClassificationViewDTO classCastDTO = _mapper.Map<ClassificationViewDTO>(classCast);
            return View(classCastDTO);
        }

        // Post Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_ClassCust(int id, ClassificationViewDTO classCastDTO)
        {
            if (ModelState.IsValid)
            {
                ClassificationCust classCast = await _classCustRepo.GetByIdAsync(id);
                _mapper.Map(classCastDTO, classCast);

                await _classCustRepo.UpdateAsync(classCast);
                await _classCustRepo.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(classCastDTO);
        }

        // Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete_ClassCust(int id)
        {
            ClassificationCust classCast = await _classCustRepo.GetByIdAsync(id);

            await _classCustRepo.DeleteAsync(classCast);
            await _classCustRepo.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
