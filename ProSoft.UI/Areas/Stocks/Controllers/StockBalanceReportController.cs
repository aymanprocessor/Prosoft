using FastReport.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ProSoft.Core.Repositories;
using ProSoft.Core.Repositories.Stocks.Reports;
using ProSoft.EF.DTOs.Stocks;
using ProSoft.EF.DTOs.Stocks.Report.StockBalance;
using ProSoft.EF.IRepositories.Stocks;
using ProSoft.EF.IRepositories.Stocks.Reports;
using Shared;
using System.Collections;
using System.Data;

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

            Filter filter = new();
            filter.NagativeQty = model.NagativeQty;
            filter.ZeroQty = model.ZeroQty;
            filter.PositiveQty = model.PositiveQty;

            filter.FromCode = model.FromCode;
            filter.ToCode = model.ToCode;

            filter.SearchByItemName = model.SearchByItemName;
            WebReport webReport = new();
            IEnumerable table ;
            switch (model.ReportType)
            {
                case "ColumnReport":
                     table = await _stockBalanceReportRepo.GetStockBalanceReportColumns(model.BranchId, model.StockIds, model.FromDate, model.ToDate,_currentUserService.Year, filter: filter);
                    webReport.Report.Load(Path.Combine(Environment.CurrentDirectory, "Reports\\Stock\\Stock Balance Report Column.frx"));
                    webReport.Report.RegisterData(table, "Table");
                    break;
                case "RowsByStock":
                     table = await _stockBalanceReportRepo.GetStockBalanceReportRowByStocks(model.BranchId, model.StockIds, model.FromDate, model.ToDate, _currentUserService.Year, filter: filter);
                    webReport.Report.Load(Path.Combine(Environment.CurrentDirectory, "Reports\\Stock\\Stock Balance Report Row By Stock.frx"));
                    webReport.Report.RegisterData(table, "Table");
                    break;
                case "RowsByItem":
                     table = await _stockBalanceReportRepo.GetStockBalanceReportRowByItems(model.BranchId, model.StockIds, model.FromDate, model.ToDate, _currentUserService.Year, filter: filter);
                    webReport.Report.Load(Path.Combine(Environment.CurrentDirectory, "Reports\\Stock\\Stock Balance Report Row By Item.frx"));
                    webReport.Report.RegisterData(table, "Table");
                    break;
            }

            
           
            

            webReport.Report.SetParameterValue("FromDate", model.FromDate.ToString("dd/MM/yyyy"));
            webReport.Report.SetParameterValue("ToDate", model.ToDate.ToString("dd/MM/yyyy"));

           
            webReport.Report.Prepare();

            ViewBag.WebReport = webReport;

            return View(model);
        }
    }
}
