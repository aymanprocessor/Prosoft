using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProSoft.Core.Repositories.Treasury;
using ProSoft.EF.DTOs.Treasury;
using ProSoft.EF.IRepositories.Medical.HospitalPatData;
using ProSoft.EF.IRepositories.Treasury;
using ProSoft.EF.Migrations;
using ProSoft.EF.Models.Treasury;

namespace ProSoft.UI.Areas.Medical.Controllers
{
    [Authorize]
    [Area("Medical")]
    public class kinshipController : Controller
    {
        private readonly IkinshipRepo _ikinshipRepo;
        private readonly IMapper _mapper;
        public kinshipController(IkinshipRepo ikinshipRepoo, IMapper mapper)
        {
            _ikinshipRepo = ikinshipRepoo;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            List<GTablelDTO> gTablelDTOs = await _ikinshipRepo.GetAllkinshipAsync();
            return View(gTablelDTOs);
        }
        // Get Add
        public async Task<IActionResult> Add_kinship()
        {
            return View();
        }

        // Post Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add_kinship(GTablelDTO gTablelDTO)
        {
            if (ModelState.IsValid)
            {
                GTable gTable = _mapper.Map<GTable>(gTablelDTO);
                gTable.Flag = 40;
                await _ikinshipRepo.AddAsync(gTable);
                await _ikinshipRepo.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gTablelDTO);
        }

        // Get Edit
        public async Task<IActionResult> Edit_kinship(int id)
        {
            GTable gTable = await _ikinshipRepo.GetByIdAsync(id);
            GTablelDTO gTablelDTO = _mapper.Map<GTablelDTO>(gTable);
            return View(gTablelDTO);
        }

        // Post Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_kinship(int id, GTablelDTO gTablelDTO)
        {
            if (ModelState.IsValid)
            {
                GTable gTable = await _ikinshipRepo.GetByIdAsync(id);
                _mapper.Map(gTablelDTO, gTable);

                await _ikinshipRepo.UpdateAsync(gTable);
                await _ikinshipRepo.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gTablelDTO);
        }

        // Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete_kinship(int id)
        {
            GTable gTable = await _ikinshipRepo.GetByIdAsync(id);

            await _ikinshipRepo.DeleteAsync(gTable);
            await _ikinshipRepo.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
