using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProSoft.EF.DTOs.Stocks;
using ProSoft.EF.IRepositories.Stocks;
using ProSoft.EF.Models.Stocks;

namespace ProSoft.UI.Areas.Stocks.Controllers
{
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
            List<Stock> stocks = await _stockRepo.GetAllAsync();
            List<StockViewDTO> stocksDTO = _mapper.Map<List<StockViewDTO>>(stocks);
            return View(stocksDTO);
        }
    }
}
