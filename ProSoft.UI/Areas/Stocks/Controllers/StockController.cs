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
    public class StockController : Controller
    {
        private readonly IStockRepo _stockRepo;
        private readonly IMapper _mapper;
        public StockController(IStockRepo stockTypeRepo, IMapper mapper)
        {
            _stockRepo = stockTypeRepo;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            List<StockViewDTO> stocksDTO = await _stockRepo.GetAllStocksAsync();
            return View(stocksDTO);
        }

        // Get Add
        public async Task<IActionResult> Add_Stock()
        {
            StockEditAddDTO stockDTO = await _stockRepo.GetEmptyStockAsync();
            ViewBag.stockId = await _stockRepo.GetNewIdAsync();
            return View(stockDTO);
        }

        // Post Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add_Stock(StockEditAddDTO stockDTO)
        {
            if (ModelState.IsValid)
            {
                Stock stock = _mapper.Map<Stock>(stockDTO);
                stock.StkDefult = 0;

                await _stockRepo.AddAsync(stock);
                await _stockRepo.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(stockDTO);
        }

        // Get Add
        public async Task<IActionResult> Edit_Stock(int id)
        {
            StockEditAddDTO stockDTO = await _stockRepo.GetStockByIdAsync(id);
            return View(stockDTO);
        }

        // Post Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_Stock(int id, StockEditAddDTO stockDTO)
        {
            if (ModelState.IsValid)
            {
                Stock stock = _mapper.Map<Stock>(stockDTO);
                stock.StkDefult = 0;

                await _stockRepo.AddAsync(stock);
                await _stockRepo.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(stockDTO);
        }
    }
}
