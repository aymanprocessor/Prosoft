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
                unitCode.Flag1 = 1;

                await _unitCodeRepo.AddAsync(unitCode);
                await _unitCodeRepo.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(unitCodeDTO);
        }

        // Get Edit
        public async Task<IActionResult> Edit_UnitCode(int id)
        {
            UnitCode unitCode = await _unitCodeRepo.GetByIdAsync(id);
            UnitCodeDTO unitCodeDTO = _mapper.Map<UnitCodeDTO>(unitCode);
            return View(unitCodeDTO);
        }

        // Post Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_UnitCode(int id, UnitCodeDTO unitCodeDTO)
        {
            if (ModelState.IsValid)
            {
                UnitCode unitCode = await _unitCodeRepo.GetByIdAsync(id);
                _mapper.Map(unitCodeDTO, unitCode);
                unitCode.Flag1 = 1;

                await _unitCodeRepo.UpdateAsync(unitCode);
                await _unitCodeRepo.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(unitCodeDTO);
        }

        // Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete_UnitCode(int id)
        {
            UnitCode unitCode = await _unitCodeRepo.GetByIdAsync(id);

            await _unitCodeRepo.DeleteAsync(unitCode);
            await _unitCodeRepo.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
