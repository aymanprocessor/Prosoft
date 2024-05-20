using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProSoft.Core.Repositories.Stocks;
using ProSoft.EF.DTOs.Stocks;
using ProSoft.EF.IRepositories.Stocks;
using ProSoft.EF.Models.Stocks;

namespace ProSoft.UI.Areas.Stocks.Controllers
{
    [Authorize]
    [Area(nameof(Stocks))]
    public class SideController : Controller
    {
        private readonly ISideRepo _sideRepo;
        private readonly IMapper _mapper;
        public SideController(ISideRepo sideRepo, IMapper mapper)
        {
            _sideRepo = sideRepo;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            List<Side> sides = await _sideRepo.GetAllAsync();
            List<SideDTO> sidesDTO = _mapper.Map<List<SideDTO>>(sides);
            return View(sidesDTO);
        }

        // Get Add
        public async Task<IActionResult> Add_Side()
        {
            ViewBag.sideId = await _sideRepo.GetNewIdAsync();
            return View();
        }

        // Post Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add_Side(SideDTO sideDTO)
        {
            if (ModelState.IsValid)
            {
                Side side = _mapper.Map<Side>(sideDTO);

                await _sideRepo.AddAsync(side);
                await _sideRepo.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sideDTO);
        }

        // Get Add
        public async Task<IActionResult> Edit_Side(int id)
        {
            Side side = await _sideRepo.GetByIdAsync(id);
            SideDTO sideDTO = _mapper.Map<SideDTO>(side);
            return View(sideDTO);
        }

        // Post Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_Side(int id, SideDTO sideDTO)
        {
            if (ModelState.IsValid)
            {
                Side side = await _sideRepo.GetByIdAsync(id);
                _mapper.Map(sideDTO, side);

                await _sideRepo.UpdateAsync(side);
                await _sideRepo.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sideDTO);
        }

        // Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete_Side(int id)
        {
            Side side = await _sideRepo.GetByIdAsync(id);

            await _sideRepo.DeleteAsync(side);
            await _sideRepo.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
