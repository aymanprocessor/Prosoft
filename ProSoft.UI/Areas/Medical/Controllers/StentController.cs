using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProSoft.Core.Repositories.Treasury;
using ProSoft.EF.DTOs.Stocks;
using ProSoft.EF.DTOs.Treasury;
using ProSoft.EF.IRepositories.Medical.HospitalPatData;
using ProSoft.EF.IRepositories.Treasury;
using ProSoft.EF.Models.Stocks;
using ProSoft.EF.Models.Treasury;

namespace ProSoft.UI.Areas.Medical.Controllers
{
    [Authorize]
    [Area(nameof(Medical))]
    public class StentController : Controller
    {
        private readonly IStentsRepo _stentsRepo;
        private readonly IMapper _mapper;

        public StentController(IStentsRepo stentsRepo, IMapper mapper)
        {
            _stentsRepo = stentsRepo;
            _mapper = mapper;
        }


        public async Task<IActionResult> Index()
        {
            List<StentDes> stentDes = await _stentsRepo.GetAllAsync();
            List<StentDesDTO> stentDesDTOs = _mapper.Map<List<StentDesDTO>>(stentDes);
            return View(stentDesDTOs);
        
        }

        public async Task<IActionResult> Add()
        {
            ViewBag.NewCode = await _stentsRepo.GetNewIdAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Add(StentDesDTO model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            StentDes stent = _mapper.Map<StentDes>(model);
            await _stentsRepo.AddAsync(stent);
            await _stentsRepo.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            StentDes stent = await _stentsRepo.GetByIdAsync(id);

            if (stent == null) return NotFound();

            StentDesDTO stentDesDTO = _mapper.Map<StentDesDTO>(stent);
            return View(stentDesDTO);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(int id, StentDesDTO model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            StentDes stent = await _stentsRepo.GetByIdAsync(id);
            _mapper.Map(model, stent);

            await _stentsRepo.UpdateAsync(stent);
            await _stentsRepo.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            StentDes stent = await _stentsRepo.GetByIdAsync(id);

            await _stentsRepo.DeleteAsync(stent);
            await _stentsRepo.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
