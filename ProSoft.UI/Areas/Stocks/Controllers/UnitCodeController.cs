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
    [Area("Stocks")]
    public class UnitCodeController : Controller
    {
        private readonly IUnitCodeRepo _unitCodeRepo;
        private readonly IMapper _mapper;
        public UnitCodeController(IUnitCodeRepo unitCodeRepo, IMapper mapper)
        {
            _unitCodeRepo = unitCodeRepo;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            List<UnitCode> unitCodes = await _unitCodeRepo.GetAllAsync();
            List<UnitCodeDTO> unitCodesDTO = _mapper.Map<List<UnitCodeDTO>>(unitCodes);
            return View(unitCodesDTO);
        }
        // Get Add
        public async Task<IActionResult> Add_UnitCode()
        {
            ViewBag.UnitCodeId = await _unitCodeRepo.GetNewIdAsync();
            return View();
        }

        // Post Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add_UnitCode(UnitCodeDTO unitCodeDTO)
        {
            if (ModelState.IsValid)
            {
                UnitCode unitCode = _mapper.Map<UnitCode>(unitCodeDTO);

                await _unitCodeRepo.AddAsync(unitCode);
                await _unitCodeRepo.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(unitCodeDTO);
        }

        // Get Add
        //public async Task<IActionResult> Edit_UnitCode(int id)
        //{
        //    Side side = await _sideRepo.GetByIdAsync(id);
        //    SideDTO sideDTO = _mapper.Map<SideDTO>(side);
        //    return View(sideDTO);
        //}

        // Post Add
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit_UnitCode(int id, SideDTO sideDTO)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        Side side = await _sideRepo.GetByIdAsync(id);
        //        _mapper.Map(sideDTO, side);

        //        await _sideRepo.UpdateAsync(side);
        //        await _sideRepo.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(sideDTO);
        //}

        // Delete
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Delete_UnitCode(int id)
        //{
        //    Side side = await _sideRepo.GetByIdAsync(id);

        //    await _sideRepo.DeleteAsync(side);
        //    await _sideRepo.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}
    }
}
