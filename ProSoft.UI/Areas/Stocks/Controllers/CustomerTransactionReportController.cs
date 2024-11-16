using FastReport.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProSoft.Core.Repositories;
using ProSoft.Core.Repositories.Stocks;
using ProSoft.Core.Repositories.Stocks.Reports;
using ProSoft.EF.DTOs.Stocks;
using ProSoft.EF.DTOs.Stocks.Report.Customer_Transaction;
using ProSoft.EF.IRepositories.Shared;
using ProSoft.EF.IRepositories.Stocks;
using ProSoft.EF.IRepositories.Stocks.Reports;
using Shared;

namespace ProSoft.UI.Areas.Stocks.Controllers
{
    [Area(nameof(Stocks))]
    [Authorize]
    public class CustomerTransactionReportController : Controller
    {
        private readonly ICustomerTransactionReportRepo _customerTransactionReportRepo;
        private readonly IStockRepo _stockRepo;
        private readonly ICustomerRepo _customerRepo;
        private readonly IGeneralTableRepo _generalTable;
        private readonly ITransMasterRepo _transMasterRepo;

        private readonly ICurrentUserService _currentUserService;



        public CustomerTransactionReportController(ICustomerTransactionReportRepo customerTransactionReportRepo, IStockRepo stockRepo, ICustomerRepo customerRepo, ICurrentUserService currentUserService, IGeneralTableRepo generalTable, ITransMasterRepo transMasterRepo)
        {
            _customerTransactionReportRepo = customerTransactionReportRepo;
            _stockRepo = stockRepo;
            _customerRepo = customerRepo;
            _currentUserService = currentUserService;
            _generalTable = generalTable;
            _transMasterRepo = transMasterRepo;
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
            if (!ModelState.IsValid)
            {
              return View(model);
            }

            await PopulateSelectLists();


            Filter filter = new();

            filter.FromDate = model.FromDate;
            filter.ToDate = model.ToDate;
            filter.CustomerId = model.CustomerId;

            
            WebReport webReport = new();

            var table = await _customerTransactionReportRepo.GetCustomerTransactionValueReport(model, filter: filter);
            var generalCode = await _generalTable.GetPermissionByUniqueTypeAsync(model.TransType);
            webReport.Report.Load(Path.Combine(Environment.CurrentDirectory, "Reports\\Stock\\Customer Transaction - Value.frx"));
            webReport.Report.RegisterData(table, "Table");
            webReport.Report.SetParameterValue("FromDate", model.FromDate.ToString("dd/MM/yyyy"));
            webReport.Report.SetParameterValue("ToDate", model.ToDate.ToString("dd/MM/yyyy"));
            webReport.Report.SetParameterValue("TransName", generalCode.GDesc);

            webReport.Report.Prepare();

            ViewBag.WebReport = webReport;
            return View(model);

        }





        //-----------------------------------------------------------------------------------------//
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
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await PopulateSelectLists();

            Filter filter = new();

            filter.FromDate = model.FromDate;
            filter.ToDate = model.ToDate;
            filter.CustomerId = model.CustomerId;

            
            WebReport webReport = new();

            var table = await _customerTransactionReportRepo.GetCustomerTransactionQuantityReport(model, filter: filter);
            var generalCode =await _generalTable.GetPermissionByUniqueTypeAsync(model.TransType);
            webReport.Report.Load(Path.Combine(Environment.CurrentDirectory, "Reports\\Stock\\Customer Transaction - Quantity.frx"));
            webReport.Report.RegisterData(table, "Table");
            webReport.Report.SetParameterValue("FromDate", model.FromDate.ToString("dd/MM/yyyy"));
            webReport.Report.SetParameterValue("ToDate", model.ToDate.ToString("dd/MM/yyyy"));
            webReport.Report.SetParameterValue("TransName", generalCode.GDesc);

            webReport.Report.Prepare();

            ViewBag.WebReport = webReport;
            return View(model);
        }
        
        
        //-----------------------------------------------------------------------------------------//

        private async Task PopulateSelectLists()
        {


            List<StockViewDTO> stockViewDTOs = await _stockRepo.GetActiveStocksForUserAsync(_currentUserService.UserId);
            ViewBag.Stocks = new SelectList(stockViewDTOs, "Stkcod", "Stknam");

            ViewBag.UserId = _currentUserService.UserId;

            var customers = await _customerRepo.GetAllAsync();
            ViewBag.Customers = new SelectList(customers, "CustCode1", "CustName");

       
        }
        
        public async Task<IActionResult> GetPermitsDependOnStock(int id, int userCode)
        {
            return Json(await _transMasterRepo.GetUserPermissionsForStockAsync(userCode, id));
        }
    }
}
