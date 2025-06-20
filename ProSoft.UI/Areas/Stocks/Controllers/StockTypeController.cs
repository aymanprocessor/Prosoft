﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProSoft.EF.DTOs.Stocks;
using ProSoft.EF.IRepositories.Stocks;
using ProSoft.EF.Models.Stocks;

namespace ProSoft.UI.Areas.Stocks.Controllers
{
    [Authorize]
    [Area(nameof(Stocks))]
    public class StockTypeController : Controller
    {

        private readonly IStockTypeRepo _stockTypeRepo;
        private readonly IMapper _mapper;
        public StockTypeController(IStockTypeRepo stockTypeRepo, IMapper mapper)
        {
            _stockTypeRepo = stockTypeRepo;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            List<KindStore> stocksTypes = await _stockTypeRepo.GetAllAsync();
            List<KindStoreDTO> stocksTypesDTO = _mapper.Map<List<KindStoreDTO>>(stocksTypes);
            return View(stocksTypesDTO);
        }
        
        // Get Add
        public async Task<IActionResult> Add_StockType()
        {
            ViewBag.stockTypeId = await _stockTypeRepo.GetNewIdAsync();
            return View();
        }

        // Post Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add_StockType(KindStoreDTO stockTypeDTO)
        {
            if (ModelState.IsValid)
            {
                KindStore stockType = _mapper.Map<KindStore>(stockTypeDTO);

                await _stockTypeRepo.AddAsync(stockType);
                await _stockTypeRepo.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(stockTypeDTO);
        }

        // Get Edit
        public async Task<IActionResult> Edit_StockType(int id)
        {
            KindStore stockType = await _stockTypeRepo.GetByIdAsync(id);
            KindStoreDTO stockTypeDTO = _mapper.Map<KindStoreDTO>(stockType);
            return View(stockTypeDTO);
        }

        // Post Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_StockType(int id, KindStoreDTO stockTypeDTO)
        {
            if (ModelState.IsValid)
            {
                KindStore stockType = await _stockTypeRepo.GetByIdAsync(id);
                _mapper.Map(stockTypeDTO, stockType);

                await _stockTypeRepo.UpdateAsync(stockType);
                await _stockTypeRepo.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(stockTypeDTO);
        }

        // Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete_StockType(int id)
        {
            KindStore stockType = await _stockTypeRepo.GetByIdAsync(id);

            await _stockTypeRepo.DeleteAsync(stockType);
            await _stockTypeRepo.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> VerifyStockType(string KName, int KId)
        {
            //if(KId == 0 || KId == null)
            //{
                //KindStore stockType1 = (await _stockTypeRepo.GetAllAsync()).FirstOrDefault(obj => obj.KName == KName);
            KindStore stockType1 = await _stockTypeRepo.GetByIdAsync(KId);
            if(stockType1 != null)
            {
                KindStore anotherStockType = (await _stockTypeRepo.GetAllAsync())
                    .FirstOrDefault(obj => obj.KName == KName && obj.KName != stockType1.KName);
                return Json(anotherStockType == null);
            }
            //}
            KindStore stockType2 = (await _stockTypeRepo.GetAllAsync()).FirstOrDefault(obj => obj.KName == KName);
            return Json(stockType2 == null);
        }
    }
}
