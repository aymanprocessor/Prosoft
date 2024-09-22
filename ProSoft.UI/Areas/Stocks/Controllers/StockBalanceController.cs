using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProSoft.EF.DTOs.Stocks;
using ProSoft.EF.IRepositories.Stocks;
using ProSoft.EF.Models.Stocks;

namespace ProSoft.UI.Areas.Stocks.Controllers
{
    [Authorize]
    [Area("Stocks")]
    public class StockBalanceController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IStockRepo _stockRepo;
        private readonly IStockEmpRepo _stockEmpRepo;
        private readonly ICurrentUserService _currentUserService;

        public StockBalanceController(IStockRepo stockRepo, IStockEmpRepo stockEmpRepo, ICurrentUserService currentUserService, IMapper mapper)
        {
            _stockRepo = stockRepo;
            _stockEmpRepo = stockEmpRepo;
            _currentUserService = currentUserService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
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
