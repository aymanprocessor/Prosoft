using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProSoft.Core.Repositories;
using ProSoft.EF.DTOs.Stocks;
using ProSoft.EF.DTOs.Stocks.Report.Customer_Transaction;
using ProSoft.EF.IRepositories.Stocks;
using ProSoft.EF.IRepositories.Stocks.Reports;

namespace ProSoft.UI.Areas.Stocks.Controllers
{
    [Area(nameof(Stocks))]
    [Authorize]
    public class CustomerTransactionReportController : Controller
    {
        private readonly ICustomerTransactionReportRepo _customerTransactionReportRepo;
        private readonly IStockRepo _stockRepo;
        private readonly ICustomerRepo _customerRepo;
        private ICurrentUserService _currentUserService;



        public CustomerTransactionReportController(ICustomerTransactionReportRepo customerTransactionReportRepo, IStockRepo stockRepo, ICustomerRepo customerRepo, ICurrentUserService currentUserService)
        {
            _customerTransactionReportRepo = customerTransactionReportRepo;
            _stockRepo = stockRepo;
            _customerRepo = customerRepo;
            _currentUserService = currentUserService;
        }
        [HttpGet]
        public async Task<IActionResult> GetCustomerTransactionValueReport()
        {
            // Initialize the request DTO with default values
            var model = new CustomerTransactionReportRequestDTO
            {
                FromDate = new DateTime(DateTime.Now.Year, 1, 1),
                ToDate = DateTime.Now
            };

           await PopulateSelectLists();

            return View(model);
        }

        [HttpPost]
        
        public async Task<IActionResult> GetCustomerTransactionValueReport(CustomerTransactionReportRequestDTO model)
        {
            if (ModelState.IsValid)
            {
                var reportData = _customerTransactionReportRepo.GetCustomerTransactionValueReport(model);
            }

            await PopulateSelectLists();

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomerTransactionQuantityReport()
        {
            // Initialize the request DTO with default values
            var model = new CustomerTransactionReportRequestDTO
            {
                FromDate = new DateTime(DateTime.Now.Year, 1, 1),
                ToDate = DateTime.Now
            };

            await PopulateSelectLists();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> GetCustomerTransactionQuantityReport(CustomerTransactionReportRequestDTO model)
        {
            if (ModelState.IsValid)
            {
                var reportData = _customerTransactionReportRepo.GetCustomerTransactionQuantityReport(model);
            }

            await PopulateSelectLists();

            return View(model);
        }
        private async Task PopulateSelectLists()
        {


            List<StockViewDTO> stockViewDTOs = await _stockRepo.GetActiveStocksForUserAsync(_currentUserService.UserId);
            ViewBag.Stocks = new SelectList(stockViewDTOs, "Stkcod", "Stknam");

            var customers = await _customerRepo.GetAllAsync();
            ViewBag.Customers = new SelectList(customers, "CustCode1", "CustName");

            //var transTypes = 
            //ViewBag.Transactions = new SelectList(transTypes, "Value", "Text");
        }
    }
}
