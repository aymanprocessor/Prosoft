using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProSoft.Core.Repositories.Medical.HospitalPatData;
using ProSoft.EF.DTOs.Treasury;
using ProSoft.EF.IRepositories.Treasury;
using ProSoft.EF.Models.Medical.HospitalPatData;
using ProSoft.EF.Models.Treasury;

namespace ProSoft.UI.Areas.Medical.Controllers
{
    [Authorize]
    [Area(nameof(Medical))]

    public class SourceOfCatheterController : Controller
    {
        private readonly IGTableRepo _gTableRepo;
        private readonly IMapper _mapper;

        public SourceOfCatheterController(IGTableRepo gTableRepo, IMapper mapper)
        {
            _gTableRepo = gTableRepo;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            List<GTablelDTO> gTablelDTOs = await _gTableRepo.GetAllCostCenterTreasuryAsync(81);
            return View(gTablelDTOs);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            ViewBag.NewCode = await _gTableRepo.GetNewIdAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Add(GTablelDTO model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            GTable gTable = _mapper.Map<GTable>(model);
            gTable.Flag = 81;
            await _gTableRepo.AddAsync(gTable);
            await _gTableRepo.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            GTable gTable = await _gTableRepo.GetByIdAsync(id);
           
            if (gTable == null) return NotFound();
           
            GTablelDTO gTablelDTO = _mapper.Map<GTablelDTO>(gTable);
            return View(gTablelDTO);

        }

         [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(int id,GTablelDTO model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            GTable gTable = await _gTableRepo.GetByIdAsync(id);
            _mapper.Map(model, gTable);
            
            await _gTableRepo.UpdateAsync(gTable);
            await _gTableRepo.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            GTable gTable = await _gTableRepo.GetByIdAsync(id);

            await _gTableRepo.DeleteAsync(gTable);
            await _gTableRepo.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
