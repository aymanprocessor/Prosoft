using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProSoft.EF.DTOs.Accounts;
using ProSoft.EF.IRepositories.Accounts;
using ProSoft.EF.Models.Accounts;

namespace ProSoft.UI.Areas.Accounts.Controllers
{
    [Authorize]
    [Area(nameof(Accounts))]
    public class CostCenterController : Controller
    {
        private readonly ICostCenterRepo _costCenterRepo;
        private readonly IMapper _mapper;
        public CostCenterController(ICostCenterRepo costCenterRepo, IMapper mapper)
        {
            _costCenterRepo = costCenterRepo;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            List<CostCenter> costCenters = await _costCenterRepo.GetAllAsync();
            List<CostCenterViewDTO> costCenterDTOs = _mapper.Map<List<CostCenterViewDTO>>(costCenters);
            return View(costCenterDTOs);
        }
        //Get add
        public async Task<IActionResult> Add_CostCenter()
        {
            ViewBag.CostId = await _costCenterRepo.GetNewIdAsync();
            return View();
        }

        //Post add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add_CostCenter(CostCenterEditAddDTO costCenterDTO)
        {
            if (ModelState.IsValid)
            {
                CostCenter costCenter = _mapper.Map<CostCenter>(costCenterDTO);

                await _costCenterRepo.AddAsync(costCenter);
                await _costCenterRepo.SaveChangesAsync();
                return RedirectToAction("Index", "CostCenter");
            }
            return View();
        }

        //Get Edit
        public async Task<IActionResult> Edit_CostCenter(int id)
        {
            CostCenter costCenter = await _costCenterRepo.GetByIdAsync(id);
            CostCenterEditAddDTO costCenterDTO = _mapper.Map<CostCenterEditAddDTO>(costCenter);
            return View(costCenterDTO);
        }

        //Post Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_CostCenter(int id, CostCenterEditAddDTO costCenterDTO)
        {
            if (ModelState.IsValid)
            {
                CostCenter costCenter = await _costCenterRepo.GetByIdAsync(id);
                _mapper.Map(costCenterDTO, costCenter);

                await _costCenterRepo.UpdateAsync(costCenter);
                await _costCenterRepo.SaveChangesAsync();
                return RedirectToAction("Index", "CostCenter");
            }
            return View(costCenterDTO);
        }

        // Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete_CostCenter(int id)
        {
            CostCenter costCenter = await _costCenterRepo.GetByIdAsync(id);

            await _costCenterRepo.DeleteAsync(costCenter);
            await _costCenterRepo.SaveChangesAsync();
            return RedirectToAction("Index", "CostCenter");
        }
    }
}
