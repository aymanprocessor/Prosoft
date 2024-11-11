using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProSoft.EF.DTOs.Stocks;
using ProSoft.EF.IRepositories.Stocks;

namespace ProSoft.UI.Areas.Stocks.Controllers
{
    [Authorize]
    [Area(nameof(Stocks))]
    public class OrderLimitController : Controller
    {
        private readonly IStockRepo _stockRepo;
        private readonly IOrderLimitRepo _orderLimitRepo;
        public OrderLimitController(IStockRepo stockRepo, IOrderLimitRepo orderLimitRepo)
        {
            _stockRepo = stockRepo;
            _orderLimitRepo = orderLimitRepo;
        }

        public async Task<IActionResult> Index()
        {
            List<StockViewDTO> stocksDTO = await _stockRepo.GetAllStocksAsync();
            ViewBag.stocks = stocksDTO;
            return View();
        }

        public async Task<IActionResult> GetOrderLimitsByDates(DateTime date1, DateTime date2, int stockID)
        {
            var branchID = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "U_Branch_Id").Value);
            List<ItmReorderViewDTO> orderLimitDTOs = await _orderLimitRepo.GetItemsLimitsByDatesAsync(date1,
                date2, stockID, branchID);
            return Json(orderLimitDTOs);
        }

        //Get Edit
        public async Task<IActionResult> Edit_OrderLimit(int id)
        {
            var branchId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "U_Branch_Id").Value);
            ItmReorderEditDTO orderLimitDTO = await _orderLimitRepo.GetItemReorderByIdAsync(id, branchId);
            return View(orderLimitDTO);
        }

        //Post Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_OrderLimit(int id, ItmReorderEditDTO orderLimitDTO)
        {
            if (ModelState.IsValid)
            {

                await _orderLimitRepo.EditItemReorderAsync(id, orderLimitDTO);
                return RedirectToAction(nameof(Index));
            }
            
            //SubItemEditAddDTO _subItemDTO = await _subItemRepo.GetSubItemByIdAsync(id);
            //_mapper.Map(_subItemDTO, subItemDTO);
            return View(orderLimitDTO);
        }
    }
}
