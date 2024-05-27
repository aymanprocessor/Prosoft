using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProSoft.Core.Repositories.Treasury;
using ProSoft.EF.DTOs.Treasury;
using ProSoft.EF.IRepositories.Treasury;
using ProSoft.EF.Models.Shared;

namespace ProSoft.UI.Areas.Treasury.Controllers
{
    [Authorize]
    [Area(nameof(Treasury))]
    public class CurrencyController : Controller
    {
        private readonly IAccGlobalDefRepo _accGlobalDefRepo;
        private readonly IMapper _mapper;
        public CurrencyController(IAccGlobalDefRepo accGlobalDefRepo,IMapper mapper)
        {
            _accGlobalDefRepo= accGlobalDefRepo;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            List<AccGlobalDefDTO> accGlobalDefDTOs = await _accGlobalDefRepo.GetAllCurrencyAsync();
            return View(accGlobalDefDTOs);
        }
        // Get Add
        public async Task<IActionResult> Add_Currency()
        {
            ViewBag.CurrencyID = await _accGlobalDefRepo.GetNewIdAsync();
            return View();
        }

        // Post Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add_Currency(AccGlobalDefDTO accGlobalDefDTO)
        {
            if (ModelState.IsValid)
            {
                AccGlobalDef accGlobalDef = _mapper.Map<AccGlobalDef>(accGlobalDefDTO);
                accGlobalDef.TableCode = "CUR";

                await _accGlobalDefRepo.AddAsync(accGlobalDef);
                await _accGlobalDefRepo.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(accGlobalDefDTO);
        }

        // Get Edit
        public async Task<IActionResult> Edit_Currency(int id)
        {
            AccGlobalDef accGlobalDef = await _accGlobalDefRepo.GetByIdAsync(id);
            AccGlobalDefDTO accGlobalDefDTO = _mapper.Map<AccGlobalDefDTO>(accGlobalDef);
            return View(accGlobalDefDTO);
        }

        // Post Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_Currency(int id, AccGlobalDefDTO accGlobalDefDTO)
        {
            if (ModelState.IsValid)
            {
                AccGlobalDef accGlobalDef = await _accGlobalDefRepo.GetByIdAsync(id);
                _mapper.Map(accGlobalDefDTO, accGlobalDef);

                await _accGlobalDefRepo.UpdateAsync(accGlobalDef);
                await _accGlobalDefRepo.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(accGlobalDefDTO);
        }

        // Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete_Currency(int id)
        {
            AccGlobalDef accGlobalDef = await _accGlobalDefRepo.GetByIdAsync(id);

            await _accGlobalDefRepo.DeleteAsync(accGlobalDef);
            await _accGlobalDefRepo.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
