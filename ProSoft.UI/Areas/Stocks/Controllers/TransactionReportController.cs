using FastReport.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProSoft.Core.Repositories;
using ProSoft.Core.Repositories.Stocks;
using ProSoft.EF.DTOs.Stocks;
using ProSoft.EF.DTOs.Stocks.Report.Transactions;
using ProSoft.EF.IRepositories.Shared;
using ProSoft.EF.IRepositories.Stocks;
using ProSoft.EF.IRepositories.Stocks.Reports;

namespace ProSoft.UI.Areas.Stocks.Controllers
{
    [Area("Stocks")]
    [Authorize]
    public class TransactionReportController : Controller
    {
        private readonly IMainItemRepo _mainItemRepo;
        private readonly ICurrentUserService _currentUserService;
        private readonly IStockRepo _stockRepo;
        private readonly ITransactionReportRepo _transactionReportRepo;
        private readonly ITransMasterRepo _transMasterRepo;

        public TransactionReportController(ICurrentUserService currentUserService, IMainItemRepo mainItemRepo, IStockRepo stockRepo, ITransactionReportRepo transactionReportRepo, ITransMasterRepo transMasterRepo)
        {
            _currentUserService = currentUserService;
            _mainItemRepo = mainItemRepo;
            _stockRepo = stockRepo;
            _transactionReportRepo = transactionReportRepo;
            _transMasterRepo = transMasterRepo;
        }

        public async Task<IActionResult> FastTransactionReport()
        {
            List<StockViewDTO> stockViewDTOs = await _stockRepo.GetActiveStocksForUserAsync(_currentUserService.UserId);
            ViewBag.MainItems = await _mainItemRepo.GetDistinctMainItemsWithSubConditions();
            ViewBag.Stocks = stockViewDTOs;
            ViewBag.BranchId = _currentUserService.BranchId;

            return View();
        }
        [HttpPost]
        public async Task< IActionResult> FastTransactionReport(TransactionRequestDTO model)
        {
            List<StockViewDTO> stockViewDTOs = await _stockRepo.GetActiveStocksForUserAsync(_currentUserService.UserId);
            ViewBag.MainItems = await _mainItemRepo.GetDistinctMainItemsWithSubConditions();
            ViewBag.Stocks = stockViewDTOs;
            ViewBag.BranchId = _currentUserService.BranchId;

            if (!ModelState.IsValid)
            {
                return View(model);
            }


            var table = await _transactionReportRepo.GetTransactionReport(model.FromDate, model.ToDate, model.StockId, FirstRows: model.FirstRows ?? null);
            WebReport webReport = new();
            webReport.Report.Load(Path.Combine(Environment.CurrentDirectory, "Reports\\Stock\\Fast Transaction Items.frx"));
            var Stock = await _stockRepo.GetStockByIdAsync(model.StockId);

            webReport.Report.SetParameterValue("FromDate", model.FromDate.ToString("dd/MM/yyyy"));
            webReport.Report.SetParameterValue("ToDate", model.ToDate.ToString("dd/MM/yyyy"));
            webReport.Report.SetParameterValue("StockName", Stock.Stknam);

            webReport.Report.RegisterData(table, "Table");
            webReport.Report.Prepare();

            ViewBag.WebReport = webReport;
            return View(model);
        }

        public async Task<IActionResult> SlowTransactionReport()
        {
            List<StockViewDTO> stockViewDTOs = await _stockRepo.GetActiveStocksForUserAsync(_currentUserService.UserId);
            ViewBag.MainItems = await _mainItemRepo.GetDistinctMainItemsWithSubConditions();
            ViewBag.Stocks = stockViewDTOs;
            ViewBag.BranchId = _currentUserService.BranchId;

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SlowTransactionReport(TransactionRequestDTO model)
        {
            List<StockViewDTO> stockViewDTOs = await _stockRepo.GetActiveStocksForUserAsync(_currentUserService.UserId);
            ViewBag.MainItems = await _mainItemRepo.GetDistinctMainItemsWithSubConditions();
            ViewBag.Stocks = stockViewDTOs;
            ViewBag.BranchId = _currentUserService.BranchId;

            if (!ModelState.IsValid)
            {
                return View(model);
            }


            var table = await _transactionReportRepo.GetTransactionReport(model.FromDate, model.ToDate, model.StockId,OrderType:"DSC", FirstRows: model.FirstRows ?? null);
            WebReport webReport = new();
            webReport.Report.Load(Path.Combine(Environment.CurrentDirectory, "Reports\\Stock\\Slow Transaction Items.frx"));
            var Stock = await _stockRepo.GetStockByIdAsync(model.StockId);

            webReport.Report.SetParameterValue("FromDate", model.FromDate.ToString("dd/MM/yyyy"));
            webReport.Report.SetParameterValue("ToDate", model.ToDate.ToString("dd/MM/yyyy"));
            webReport.Report.SetParameterValue("StockName", Stock.Stknam);

            webReport.Report.RegisterData(table, "Table");
            webReport.Report.Prepare();

            ViewBag.WebReport = webReport;
            return View(model);
        }

        public async Task<IActionResult> ZeroTransactionReport()
        {
            List<StockViewDTO> stockViewDTOs = await _stockRepo.GetActiveStocksForUserAsync(_currentUserService.UserId);
            ViewBag.MainItems = await _mainItemRepo.GetDistinctMainItemsWithSubConditions();
            ViewBag.Stocks = stockViewDTOs;
            ViewBag.BranchId = _currentUserService.BranchId;

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> ZeroTransactionReport(TransactionRequestDTO model)
        {
            List<StockViewDTO> stockViewDTOs = await _stockRepo.GetActiveStocksForUserAsync(_currentUserService.UserId);
            ViewBag.Stocks = stockViewDTOs;
            ViewBag.BranchId = _currentUserService.BranchId;
            if (!ModelState.IsValid)
            {
                return View(model);
            }


            var table = await _transactionReportRepo.GetZeroTransactionReport(model.FromDate, model.ToDate, model.StockId,_currentUserService.Year,FirstRows:model.FirstRows??null);
            WebReport webReport = new();
            webReport.Report.Load(Path.Combine(Environment.CurrentDirectory, "Reports\\Stock\\Zero Transaction Items.frx"));
            var Stock = await _stockRepo.GetStockByIdAsync(model.StockId);

            webReport.Report.SetParameterValue("FromDate", model.FromDate.ToString("dd/MM/yyyy"));
            webReport.Report.SetParameterValue("ToDate", model.ToDate.ToString("dd/MM/yyyy"));
            webReport.Report.SetParameterValue("StockName", Stock.Stknam);

            webReport.Report.RegisterData(table, "Table");
            webReport.Report.Prepare();

            ViewBag.WebReport = webReport;
            return View(model);
        }

        public async Task<IActionResult> TotalPermitsTransactionReport()
        {
            List<StockViewDTO> stockViewDTOs = await _stockRepo.GetActiveStocksForUserAsync(_currentUserService.UserId);
            ViewBag.Stocks = stockViewDTOs;
            //ViewBag.PermitTypes = _transMasterRepo.GetUserPermissionsForStockAsync()
            ViewBag.BranchId = _currentUserService.BranchId;

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> TotalPermitsTransactionReport(TransactionRequestDTO model)
        {
            List<StockViewDTO> stockViewDTOs = await _stockRepo.GetActiveStocksForUserAsync(_currentUserService.UserId);
            ViewBag.Stocks = stockViewDTOs;
            ViewBag.BranchId = _currentUserService.BranchId;
            if (!ModelState.IsValid)
            {
                return View(model);
            }


            var table = await _transactionReportRepo.GetZeroTransactionReport(model.FromDate, model.ToDate, model.StockId, _currentUserService.Year, FirstRows: model.FirstRows ?? null);
            WebReport webReport = new();
            webReport.Report.Load(Path.Combine(Environment.CurrentDirectory, "Reports\\Stock\\Zero Transaction Items.frx"));
            var Stock = await _stockRepo.GetStockByIdAsync(model.StockId);

            webReport.Report.SetParameterValue("FromDate", model.FromDate.ToString("dd/MM/yyyy"));
            webReport.Report.SetParameterValue("ToDate", model.ToDate.ToString("dd/MM/yyyy"));
            webReport.Report.SetParameterValue("StockName", Stock.Stknam);

            webReport.Report.RegisterData(table, "Table");
            webReport.Report.Prepare();

            ViewBag.WebReport = webReport;
            return View(model);
        }
    }
}
