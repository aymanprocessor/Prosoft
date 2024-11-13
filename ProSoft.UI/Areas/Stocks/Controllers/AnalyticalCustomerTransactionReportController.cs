using FastReport.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProSoft.Core.Repositories.Stocks;
using ProSoft.Core.Repositories.Stocks.Reports;
using ProSoft.EF.DTOs.Stocks.Report.Analytical_Customer_Transaction_Report;
using ProSoft.EF.IRepositories.Stocks;
using ProSoft.EF.IRepositories.Stocks.Reports;
using Shared;

namespace ProSoft.UI.Areas.Stocks.Controllers
{
    [Area(nameof(Stocks))]
    [Authorize]
    public class AnalyticalCustomerTransactionReportController : Controller
    {
        private readonly ICustomerRepo _customerRepo;
        private readonly IAnalyticalCustomerTransactionReportRepo _analyticalCustomerTransactionReportRepo;

        public AnalyticalCustomerTransactionReportController(ICustomerRepo customerRepo, IAnalyticalCustomerTransactionReportRepo analyticalCustomerTransactionReportRepo)
        {
            _customerRepo = customerRepo;
            _analyticalCustomerTransactionReportRepo = analyticalCustomerTransactionReportRepo;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Customers = new SelectList(await _customerRepo.GetAllAsync(), "CustCode1", "CustName");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(AnalyticalCustomerTransactionReportRequestDTO model)
        {
            ViewBag.Customers = new SelectList(await _customerRepo.GetAllAsync(), "CustCode1", "CustName");

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            Filter filter = new();
        
            filter.FromDate = model.FromDate;
            filter.ToDate = model.ToDate;
            filter.CustomerId = model.CustomerId;
            WebReport webReport = new();

            var table = await _analyticalCustomerTransactionReportRepo.GetAnalyticalCustomerTransactionReport(model.FromDate,model.ToDate, filter: filter);
            webReport.Report.Load(Path.Combine(Environment.CurrentDirectory, "Reports\\Stock\\Analytical Customer Transaction Report.frx"));
            webReport.Report.RegisterData(table, "Table");
            webReport.Report.SetParameterValue("FromDate", model.FromDate.ToString("dd/MM/yyyy"));
            webReport.Report.SetParameterValue("ToDate", model.ToDate.ToString("dd/MM/yyyy"));

            webReport.Report.Prepare();

            ViewBag.WebReport = webReport;

            return View(model);
        }
    }
}
