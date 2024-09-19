using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProSoft.EF.DTOs.Stocks;
using ProSoft.EF.IRepositories.Stocks;
using ProSoft.EF.Models.Stocks;

namespace ProSoft.UI.Areas.Stocks.Controllers
{
    public class StockBalanceController : Controller
    {
        private readonly IStockRepo _stockRepo;
        private readonly IStockEmpRepo _stockEmpRepo;

        private readonly IMapper  _mapper;

        public StockBalanceController(IStockRepo stockRepo, IStockEmpRepo stockEmpRepo)
        {
            _stockRepo = stockRepo;
            _stockEmpRepo = stockEmpRepo;
        }

        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> ChooseStockType()
        {
            List<Stock> stockTypes = await _stockRepo.GetAllAsync();
            var stockTypesDTO = _mapper.Map<List<StockViewDTO>>(stockTypes);
            return View(stockTypesDTO);
        }
    }
}
