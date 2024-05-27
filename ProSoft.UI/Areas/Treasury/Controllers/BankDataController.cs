using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProSoft.Core.Repositories.Treasury;
using ProSoft.EF.DTOs.Stocks;
using ProSoft.EF.DTOs.Treasury;
using ProSoft.EF.IRepositories.Treasury;
using ProSoft.EF.Migrations;
using ProSoft.EF.Models.Stocks;
using ProSoft.EF.Models.Treasury;

namespace ProSoft.UI.Areas.Treasury.Controllers
{
    [Authorize]
    [Area(nameof(Treasury))]
    public class BankDataController : Controller
    {
        private readonly IBankDataRepo _bankDataRepo;
        private readonly IMapper _mapper;

        public BankDataController(IBankDataRepo bankDataRepo, IMapper mapper)
        {
            _bankDataRepo = bankDataRepo;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            List<BankData> bankDatas = await _bankDataRepo.GetAllAsync();
            List<BankDataDTO> bankDataDTOs = _mapper.Map<List<BankDataDTO>>(bankDatas);
            return View(bankDataDTOs);
        }
        // Get Add
        public async Task<IActionResult> Add_Bank()
        {
            ViewBag.BankID = await _bankDataRepo.GetNewIdAsync();
            return View();
        }

        // Post Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add_Bank(BankDataDTO bankDataDTO)
        {
            if (ModelState.IsValid)
            {
                BankData bankData = _mapper.Map<BankData>(bankDataDTO);

                await _bankDataRepo.AddAsync(bankData);
                await _bankDataRepo.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bankDataDTO);
        }

        // Get Edit
        public async Task<IActionResult> Edit_Bank(int id)
        {
            BankData bankData = await _bankDataRepo.GetByIdAsync(id);
            BankDataDTO bankDataDTO = _mapper.Map<BankDataDTO>(bankData);
            return View(bankDataDTO);
        }

        // Post Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_Bank(int id, BankDataDTO bankDataDTO)
        {
            if (ModelState.IsValid)
            {
                BankData bankData = await _bankDataRepo.GetByIdAsync(id);
                _mapper.Map(bankDataDTO, bankData);

                await _bankDataRepo.UpdateAsync(bankData);
                await _bankDataRepo.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bankDataDTO);
        }

        // Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete_Bank(int id)
        {
            BankData bankData = await _bankDataRepo.GetByIdAsync(id);

            await _bankDataRepo.DeleteAsync(bankData);
            await _bankDataRepo.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
