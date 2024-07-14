using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProSoft.EF.DTOs.Treasury;
using ProSoft.EF.IRepositories.Treasury;
using ProSoft.EF.Models.Treasury;

namespace ProSoft.UI.Areas.Medical.Controllers
{
    [Authorize]
    [Area("Medical")]
    public class NursingDataController : Controller
    {
        private readonly IGTableRepo _gTableRepo;
        private readonly IMapper _mapper;

        public NursingDataController(IGTableRepo gTableRepo, IMapper mapper)
        {
            _gTableRepo = gTableRepo;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            List<GTablelDTO> gTablelDTOs = await _gTableRepo.GetAllCostCenterTreasuryAsync(50);
            return View(gTablelDTOs);
        }
        // Get Add
        public async Task<IActionResult> Add_NursingData()
        {
            ViewBag.gtableID = await _gTableRepo.GetNewIdAsync();
            return View();
        }

        // Post Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add_NursingData(GTablelDTO gTablelDTO)
        {
            if (ModelState.IsValid)
            {
                GTable gTable = _mapper.Map<GTable>(gTablelDTO);
                gTable.Flag = 50;
                await _gTableRepo.AddAsync(gTable);
                await _gTableRepo.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gTablelDTO);
        }

        // Get Edit
        public async Task<IActionResult> Edit_NursingData(int id)
        {
            GTable gTable = await _gTableRepo.GetByIdAsync(id);
            GTablelDTO gTablelDTO = _mapper.Map<GTablelDTO>(gTable);
            return View(gTablelDTO);
        }

        // Post Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_NursingData(int id, GTablelDTO gTablelDTO)
        {
            if (ModelState.IsValid)
            {
                GTable gTable = await _gTableRepo.GetByIdAsync(id);
                _mapper.Map(gTablelDTO, gTable);

                await _gTableRepo.UpdateAsync(gTable);
                await _gTableRepo.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gTablelDTO);
        }

        // Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete_NursingData(int id)
        {
            GTable gTable = await _gTableRepo.GetByIdAsync(id);

            await _gTableRepo.DeleteAsync(gTable);
            await _gTableRepo.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
 