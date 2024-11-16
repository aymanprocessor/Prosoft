using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProSoft.EF.DTOs.Stocks.Report.Total_Customer_Transaction;
using ProSoft.EF.IRepositories.Stocks;
using ProSoft.EF.IRepositories.Stocks.Reports;

namespace ProSoft.UI.Areas.Stocks.Controllers
{
    public class TotalCustomerTransactionReportController : Controller
    {
        private readonly ITotalCustomerTransactionReportRepo _totalCustomerTransactionReportRepo;
        private readonly ICustomerRepo _customerRepo;
        public TotalCustomerTransactionReportController(ITotalCustomerTransactionReportRepo reportRepo, ITotalCustomerTransactionReportRepo totalCustomerTransactionReportRepo, ICustomerRepo customerRepo)
        {
            _totalCustomerTransactionReportRepo = totalCustomerTransactionReportRepo;
            _customerRepo = customerRepo;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {

            var customers = await _customerRepo.GetAllAsync();
            ViewBag.Customers = new SelectList(customers, "CustCode1", "CustName");

            if (!ModelState.IsValid)
            {
                return View();
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index( TotalCustomerTransactionReportRequestDTO request)
        {
            var customers = await _customerRepo.GetAllAsync();
            ViewBag.Customers = new SelectList(customers, "CustCode1", "CustName");
            if (!ModelState.IsValid)
            {
                return View(request);
            }
            
            return View(request);

        }
    }
}
