using Azure.Core;
using FastReport.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProSoft.Core.Repositories.Stocks.Reports;
using ProSoft.EF.DTOs.Stocks.Report.Total_Customer_Transaction;
using ProSoft.EF.IRepositories.Stocks;
using ProSoft.EF.IRepositories.Stocks.Reports;
using Shared;

namespace ProSoft.UI.Areas.Stocks.Controllers
{
    [Authorize]
    [Area(nameof(Stocks))]
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
            TotalCustomerTransactionReportRequestDTO request = new()
            {
                FromDate = new DateTime(DateTime.Now.Year, 1, 1),
                ToDate = DateTime.Now
            };

            if (!ModelState.IsValid)
            {
                return View(request);
            }

            return View(request);
        }

        [HttpPost]
        public async Task<IActionResult> Index( TotalCustomerTransactionReportRequestDTO request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            Filter filter = new() { CustomerId = request.CustomerId, FromDate = request.FromDate, ToDate = request.ToDate };
            WebReport webReport = new();

            var table = await _totalCustomerTransactionReportRepo.GetTotalCustomerTransactionReport(request, filter: filter);
            webReport.Report.Load(Path.Combine(Environment.CurrentDirectory, "Reports\\Stock\\Total Customer Transaction.frx"));
            webReport.Report.RegisterData(table, "Table");
            webReport.Report.SetParameterValue("FromDate", request.FromDate.ToString("dd/MM/yyyy"));
            webReport.Report.SetParameterValue("ToDate", request.ToDate.ToString("dd/MM/yyyy"));

            webReport.Report.Prepare();

            ViewBag.WebReport = webReport;

            return View(request);

        }
    }
}
