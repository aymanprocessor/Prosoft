using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProSoft.EF.DTOs.Calculus;
using ProSoft.EF.DTOs.Shared;
using ProSoft.EF.IRepositories.Shared;
using ProSoft.EF.Models.Shared;

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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add_SysAccount(EisPostingEditAddDTO eisPostingDTO)
        {
            if (ModelState.IsValid)
            {
                EisPosting eisPosting = _mapper.Map<EisPosting>(eisPostingDTO);
                eisPosting.BranchId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "U_Branch_Id").Value);

                await _eisPostingRepo.AddAsync(eisPosting);
                await _eisPostingRepo.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(eisPostingDTO);
        }

        // Get Edit
        public async Task<IActionResult> Edit_SysAccount(int id)
        {
            EisPostingEditAddDTO eisPostingDTO = await _eisPostingRepo.GetEisPostingByIdAsync(id);
            return View(eisPostingDTO);
        }

        // Post Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_SysAccount(int id, EisPostingEditAddDTO eisPostingDTO)
        {
            if (ModelState.IsValid)
            {
                EisPosting eisPosting = await _eisPostingRepo.GetByIdAsync(id);
                _mapper.Map(eisPostingDTO, eisPosting);
                eisPosting.PostId = id;

                await _eisPostingRepo.UpdateAsync(eisPosting);
                await _eisPostingRepo.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(eisPostingDTO);
        }

        // Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete_SysAccount(int id)
        {
            EisPosting eisPosting = await _eisPostingRepo.GetByIdAsync(id);

            await _eisPostingRepo.DeleteAsync(eisPosting);
            await _eisPostingRepo.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
