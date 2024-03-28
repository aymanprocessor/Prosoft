using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProSoft.EF.DTOs.Medical.Analysis;
using ProSoft.EF.IRepositories.Medical.Analysis;
using ProSoft.EF.Models.Medical.Analysis;

namespace ProSoft.UI.Areas.Medical.Controllers
{
    [Authorize]
    [Area("Medical")]
    public class LabUnitsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ILabUnitRepo _labUnitRep;
        private readonly IMainRepo _mainRepo;

        public LabUnitsController(IMapper mapper, ILabUnitRepo labUnitRep, IMainRepo mainRepo)
        {
            _mapper = mapper;
            _labUnitRep = labUnitRep;
            _mainRepo = mainRepo;
        }

        public async Task<IActionResult> Index()
        {
            List<LabUnit> labUnits = await _labUnitRep.GetAllAsync();
            List<LabUnitDTO> labUnitDTOs = _mapper.Map<List<LabUnitDTO>>(labUnits);

            #region sidebar
            ViewBag.MainLevel_3 = await _mainRepo.GetMainsByLevelAsync(3);
            ViewBag.MainLevel_4 = await _mainRepo.GetMainsByLevelAsync(4);
            ViewBag.MainLevel_5 = await _mainRepo.GetMainsByLevelAsync(5);
            #endregion

            return View(labUnitDTOs);
        }

        // Get Add
        public async Task<IActionResult> Add_LabUnit()
        {
            ViewBag.UnitCode = await _labUnitRep.GetNewIdAsync();

            #region sidebar
            ViewBag.MainLevel_3 = await _mainRepo.GetMainsByLevelAsync(3);
            ViewBag.MainLevel_4 = await _mainRepo.GetMainsByLevelAsync(4);
            ViewBag.MainLevel_5 = await _mainRepo.GetMainsByLevelAsync(5);
            #endregion

            return View();
        }

        // Post Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add_LabUnit(LabUnitDTO labUnitDTO)
        {
            if (ModelState.IsValid)
            {
                LabUnit labUnit = _mapper.Map<LabUnit>(labUnitDTO);

                await _labUnitRep.AddAsync(labUnit);
                await _labUnitRep.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(labUnitDTO);
        }

        // Get Edit
        public async Task<IActionResult> Edit_LabUnit(short id)
        {
            LabUnit labUnit  = await _labUnitRep.GetByIdAsync(id);
            LabUnitDTO labUnitDTO = _mapper.Map<LabUnitDTO>(labUnit);

            #region sidebar
            ViewBag.MainLevel_3 = await _mainRepo.GetMainsByLevelAsync(3);
            ViewBag.MainLevel_4 = await _mainRepo.GetMainsByLevelAsync(4);
            ViewBag.MainLevel_5 = await _mainRepo.GetMainsByLevelAsync(5);
            #endregion

            return View(labUnitDTO);
        }

        // Post Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_LabUnit(short id, LabUnitDTO labUnitDTO)
        {
            if (ModelState.IsValid)
            {
                LabUnit labUnit  = await _labUnitRep.GetByIdAsync(id);
                _mapper.Map(labUnitDTO, labUnit);

                await _labUnitRep.UpdateAsync(labUnit);
                await _labUnitRep.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(labUnitDTO);
        }

        // Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete_LabUnit(short id)
        {
            LabUnit labUnit = await _labUnitRep.GetByIdAsync(id);

            await _labUnitRep.DeleteAsync(labUnit);
            await _labUnitRep.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
