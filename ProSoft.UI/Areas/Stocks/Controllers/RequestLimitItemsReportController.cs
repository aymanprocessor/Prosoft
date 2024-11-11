using FastReport.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProSoft.Core.Repositories;
using ProSoft.EF.DTOs.Stocks;
using ProSoft.EF.DTOs.Stocks.Report.Request_Limit_Items_Report;
using ProSoft.EF.IRepositories.Stocks;
using ProSoft.EF.IRepositories.Stocks.Reports;
using ProSoft.UI.Global;
using Shared;

namespace ProSoft.UI.Areas.Stocks.Controllers
{
    [Area(nameof(Stocks))]
    [Authorize]
    public class RequestLimitItemsReportController : Controller
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IStockRepo _stockRepo;
        private readonly IRequestLimitItemsReportRepo _requestLimitItemsReportRepo;

        public RequestLimitItemsReportController(ICurrentUserService currentUserService, IStockRepo stockRepo, IRequestLimitItemsReportRepo requestLimitItemsReportRepo)
        {
            _currentUserService = currentUserService;
            _stockRepo = stockRepo;
            _requestLimitItemsReportRepo = requestLimitItemsReportRepo;
        }

        public async Task<IActionResult> Index()
        {
            List<StockViewDTO> stockViewDTOs = await _stockRepo.GetActiveStocksForUserAsync(_currentUserService.UserId);
            ViewBag.Stocks = stockViewDTOs;
            ViewBag.BranchId = _currentUserService.BranchId;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(RequestLimitItemsReportRequestDTO model)
        {
            List<StockViewDTO> stockViewDTOs = await _stockRepo.GetActiveStocksForUserAsync(_currentUserService.UserId);
            ViewBag.Stocks = stockViewDTOs;
            ViewBag.BranchId = _currentUserService.BranchId;
            model.FromDate = new DateTime(_currentUserService.Year, 1, 1);
            if (!ModelState.IsValid)
            {
                return View(model);
            }


            Filter filter = new();


            filter.FromCode = model.FromCode;
            filter.ToCode = model.ToCode;
            filter.FromDate = model.FromDate;
            filter.ToDate = model.ToDate;

            WebReport webReport = new();

            var table = await _requestLimitItemsReportRepo.GetRequestLimitItemsReport(model.StockId, model.BranchId, filter: filter);
            webReport.Report.Load(Path.Combine(Environment.CurrentDirectory, "Reports\\Stock\\Request Limit Items Report.frx"));
            webReport.Report.RegisterData(table, "Table");
            var stock = await _stockRepo.GetStockByIdAsync(model.StockId);
            webReport.Report.SetParameterValue("FromDate", model.FromDate.ToString("dd/MM/yyyy"));
            webReport.Report.SetParameterValue("ToDate", model.ToDate.ToString("dd/MM/yyyy"));
            webReport.Report.SetParameterValue("StockName", stock.Stknam);


            webReport.Report.Prepare();

            ViewBag.WebReport = webReport;
            return View(model);
        }
    }
}
