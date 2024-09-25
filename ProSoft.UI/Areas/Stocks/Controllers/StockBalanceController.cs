using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using ProSoft.Core.Repositories.Stocks;
using ProSoft.EF.DTOs.Stocks;
using ProSoft.EF.IRepositories.Stocks;
using ProSoft.EF.Models.Stocks;
using System.Collections.Generic;

namespace ProSoft.UI.Areas.Stocks.Controllers
{
    [Authorize]
    [Area("Stocks")]
    public class StockBalanceController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IStockRepo _stockRepo;
        private readonly IStockBalanceRepo _stockBalanceRepo;
        private readonly IMainItemRepo _mainItemRepo;
        private readonly ISubItemRepo _subItemRepo;
        private readonly ICurrentUserService _currentUserService;
        private readonly IUnitCodeRepo _unitCodeRepo;
        private readonly IStockEmpRepo _stockEmpRepo;

        public StockBalanceController(IStockRepo stockRepo, IStockEmpRepo stockEmpRepo, ICurrentUserService currentUserService, IMapper mapper, IStockBalanceRepo stockBalanceRepo, ISubItemRepo subItemRepo, IMainItemRepo mainItemRepo, IUnitCodeRepo unitCodeRepo)
        {
            _mapper = mapper;
            _stockRepo = stockRepo;
            _currentUserService = currentUserService;
            _stockBalanceRepo = stockBalanceRepo;
            _subItemRepo = subItemRepo;
            _mainItemRepo = mainItemRepo;
            _unitCodeRepo = unitCodeRepo;
            _stockEmpRepo = stockEmpRepo;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int id)
        {
            if(id == 0)
            {
                return NotFound("Stock Id Is Require");
            }

            var stock = await _stockRepo.GetByIdAsync(id);
        
            var stkkkkk = await _stockBalanceRepo.GetAllAsync();
            stkkkkk = stkkkkk.Where(x => x.Stkcod == id).Where(x => x.FYear == _currentUserService.Year).ToList();
           
            var subItems = _subItemRepo.GetAllSubItemByStockId(id);
           
            var res = from si in subItems
                      join sbb in stkkkkk
                      on new { si.ItemCode,si.Flag1 } equals new { sbb.ItemCode,sbb.Flag1 } into tableJoin
                      from tj in tableJoin.DefaultIfEmpty()
                      where tj == null
                      select si;


            var mainItems = (await _mainItemRepo.GetAllAsync()).Where(x => res.Select(r => r.MainCode ).Contains( x.MainCode)).ToList();
          

            List<Stkbalance> stkbalances = [];
            foreach(var item in res)
            {
                var mainItem = mainItems.FirstOrDefault(x => x.MainCode == item.MainCode);
                stkbalances.Add(new Stkbalance()
                {

                    Stkcod = (short)stock.Stkcod,
                    ItemCode = item.ItemCode,
                    MainCode = item.MainCode,
                    FYear = _currentUserService.Year,
                    Ser = item.Sub,
                    Flag1 = stock.Flag1,
                    BranchId = stock.BranchId,
                    DelItem = 0,
                    ItemId = 0,
                    BarCode = item.ItemCode,
                    ItemCounter = item.ItemCounter,
                    QtyStartDt = new DateTime(_currentUserService.Year, 1, 1),
                  MainItem = mainItem,
                  SubItem = item,
                    
                   
                });
            }
            if(stkbalances.Count > 0)
            {
                _stockBalanceRepo.BulkInsert(stkbalances);
                await _stockBalanceRepo.SaveChangesAsync();
            }

            List<Stkbalance> StockBalances1 = await _stockBalanceRepo.GetAllByStockId(id);
            StockBalanceViewDTO stockBalanceViewDTOs = new StockBalanceViewDTO()
            {
                StockBalances = StockBalances1,
                UnitCodesList = (await _unitCodeRepo.GetAllAsync()).Select(u => new SelectListItem { Value = u.Code.ToString(), Text = u.Names }).ToList()
            };
            return View(stockBalanceViewDTOs);
        }

        [HttpGet("ChooseStockType")]
        public async Task<IActionResult> ChooseStockType()
        {
            int userId = _currentUserService.UserId;
            List<StockEmp> stockEmpTypes = await _stockEmpRepo.GetStockEmpForUserAsync(userId);
            List<Stock> stocks = stockEmpTypes.Select(x => x.Stock).ToList();

            return View(stocks);
        }

        [HttpGet]
        public async Task<IActionResult> Search(int id,string searchValue)
        {
            List<Stkbalance> StockBalances =( await _stockBalanceRepo.GetAllByStockId(id)).Where(s => s.ItemCode.Contains(searchValue) || s.MainItem.MainName.Contains(searchValue)).ToList();
           
            return Ok(StockBalances);

        }
    }
}
