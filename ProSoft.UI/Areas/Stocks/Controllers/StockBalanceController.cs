using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IStockEmpRepo _stockEmpRepo;
        private readonly IStockBalanceRepo _stockBalanceRepo;
        private readonly ISubItemRepo _subItemRepo;
        private readonly ICurrentUserService _currentUserService;

        public StockBalanceController(IStockRepo stockRepo, IStockEmpRepo stockEmpRepo, ICurrentUserService currentUserService, IMapper mapper, IStockBalanceRepo stockBalanceRepo, ISubItemRepo subItemRepo)
        {
            _mapper = mapper;
            _stockRepo = stockRepo;
            _stockEmpRepo = stockEmpRepo;
            _currentUserService = currentUserService;
            _stockBalanceRepo = stockBalanceRepo;
            _subItemRepo = subItemRepo;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int id)
        {
            if(id == 0)
            {
                return NotFound("Stock Id Is Require");
            }

            var stkkkkk = await _stockBalanceRepo.GetAllAsync();
            stkkkkk = stkkkkk.Where(x => x.Stkcod == id).Where(x => x.FYear == _currentUserService.Year).ToList();
            var stock = await _stockRepo.GetByIdAsync(id);
            var subItems = _subItemRepo.GetAllSubItemByStockId(id);

            var res = from si in subItems
                      join sbb in stkkkkk
                      on new { si.ItemCode,si.Flag1 } equals new { sbb.ItemCode,sbb.Flag1 } into tableJoin
                      from tj in tableJoin.DefaultIfEmpty()
                      where tj == null
                      select si;

            List<Stkbalance> stkbalances = [];
            foreach(var item in res)
            {
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
                    QtyStartDt = new DateTime(_currentUserService.Year, 1, 1)

                });
            }
            if(stkbalances.Count > 0)
            {
                _stockBalanceRepo.BulkInsert(stkbalances);
                await _stockBalanceRepo.SaveChangesAsync();
            }

            List<Stkbalance> StockBalances1 = await _stockBalanceRepo.GetAllAsync();
            List<StockBalanceViewDTO> stockBalanceViewDTOs = _mapper.Map< List<StockBalanceViewDTO>>(StockBalances1);
            
            return View(StockBalances1);
        }

        [HttpGet]
        public async Task<IActionResult> ChooseStockType()
        {
            int userId = _currentUserService.UserId;
            List<StockEmp> stockEmpTypes = await _stockEmpRepo.GetStockEmpForUserAsync(userId);
            List<Stock> stocks = stockEmpTypes.Select(x => x.Stock).ToList();

            return View(stocks);
        }
    }
}
