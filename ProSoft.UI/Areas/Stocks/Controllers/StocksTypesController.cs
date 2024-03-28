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
    public class StocksTypesController : Controller
    {

        private readonly IStockTypeRepo _stockTypeRepo;
        private readonly IMapper _mapper;
        public StocksTypesController(IStockTypeRepo stockTypeRepo, IMapper mapper)
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

        public async Task<IActionResult> StockType()
        {
            ViewBag.stockTypeId = "Ibrahim Ahmed";
            return View();
        }
        
        public async Task<IActionResult> Add_StockType()
        {
            ViewBag.stockTypeId = await _stockTypeRepo.GetNewIdAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add_StockType(KindStoreDTO stockTypeDTO)
        {
            if (ModelState.IsValid)
            {
                //await _stockTypeRepo.EditPatientAsync(id, patientDTO);
                //return RedirectToAction("Patients", "Patient");
            }
            return View(stockTypeDTO);
        }

    }
}
