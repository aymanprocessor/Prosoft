using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProSoft.EF.DTOs.Calculus;
using ProSoft.EF.DTOs.Shared;
using ProSoft.EF.IRepositories.Shared;

namespace ProSoft.UI.Areas.Shared.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Shared")]
    public class EisPostingController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IEisPostingRepo _eisPostingRepo;
        public EisPostingController(IMapper mapper, IEisPostingRepo eisPostingRepo)
        {
            _mapper = mapper;
            _eisPostingRepo = eisPostingRepo;
        }

        public async Task<IActionResult> Index()
        {
            List<EisPostingViewDTO> eisPostings = await _eisPostingRepo.GetAllCodesAsync();
            return View(eisPostings);
        }

        public async Task<IActionResult> GetSubCodesFor(string id)
        {
            List<AccSubCodeDTO> subCodesDTO = await _eisPostingRepo.GetSubCodesForAsync(id);
            return Json(subCodesDTO);
        }

        // Get Add
        public async Task<IActionResult> Add_SysAccount()
        {
            EisPostingEditAddDTO eisPostingDTO = await _eisPostingRepo.GetEmptyEisPostingAsync();
            return View(eisPostingDTO);
        }

        // Post Add
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Add_SysAccount(ClassificationViewDTO classCastDTO)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        ClassificationCust classCast = _mapper.Map<ClassificationCust>(classCastDTO);

        //        await _classCustRepo.AddAsync(classCast);
        //        await _classCustRepo.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(classCastDTO);
        //}

        // Get Edit
        //public async Task<IActionResult> Edit_SysAccount(int id)
        //{
        //    ClassificationCust classCast = await _classCustRepo.GetByIdAsync(id);
        //    ClassificationViewDTO classCastDTO = _mapper.Map<ClassificationViewDTO>(classCast);
        //    return View(classCastDTO);
        //}

        // Post Edit
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit_SysAccount(int id, ClassificationViewDTO classCastDTO)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        ClassificationCust classCast = await _classCustRepo.GetByIdAsync(id);
        //        _mapper.Map(classCastDTO, classCast);

        //        await _classCustRepo.UpdateAsync(classCast);
        //        await _classCustRepo.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(classCastDTO);
        //}

        // Delete
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Delete_SysAccount(int id)
        //{
        //    ClassificationCust classCast = await _classCustRepo.GetByIdAsync(id);

        //    await _classCustRepo.DeleteAsync(classCast);
        //    await _classCustRepo.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}
    }
}
