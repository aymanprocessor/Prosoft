using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProSoft.Core.Repositories;
using ProSoft.EF.DTOs.Stocks;
using ProSoft.EF.DTOs.Stocks.Report.StockBalance;
using ProSoft.EF.IRepositories.Stocks;
using ProSoft.EF.IRepositories.Stocks.Reports;

namespace ProSoft.UI.Areas.Stocks.Controllers
{
    [Authorize]
    [Area(nameof(Stocks))]
    public class StockBalanceReportController : Controller
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IStockRepo _stockRepo;
        private readonly IStockBalanceReportRepo _stockBalanceReportRepo;

        public StockBalanceReportController(ICurrentUserService currentUserService, IStockRepo stockRepo, IStockBalanceReportRepo stockBalanceReportRepo)
        {
            _currentUserService = currentUserService;
            _stockRepo = stockRepo;
            _stockBalanceReportRepo = stockBalanceReportRepo;
        }

        public async Task<IActionResult> Index()
        {
            List<StockViewDTO> stockViewDTOs = await _stockRepo.GetActiveStocksForUserAsync(_currentUserService.UserId);
            ViewBag.Stocks = stockViewDTOs;
            ViewBag.BranchId = _currentUserService.BranchId;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(StockBalanceReportRequestDTO model)
        {
            List<StockViewDTO> stockViewDTOs = await _stockRepo.GetActiveStocksForUserAsync(_currentUserService.UserId);
            ViewBag.Stocks = stockViewDTOs;
            ViewBag.BranchId = _currentUserService.BranchId;
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            switch (model.ReportType)
            {
                case "ColumnReport":
                    var columns = await _stockBalanceReportRepo.GetStockBalanceReportColumns(model.BranchId, model.StockIds, model.FromDate, model.ToDate);
                    break;
                case "RowsByStock":
                    break;
                case "RowsByItem":
                    break;

            }
           
            return View(model);
        }
    }
}
