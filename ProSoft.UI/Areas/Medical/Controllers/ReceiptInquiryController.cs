using FastReport.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProSoft.EF.DTOs.Medical.HospitalPatData.Reports;
using ProSoft.EF.IRepositories.Medical.HospitalPatData;

namespace ProSoft.UI.Areas.Medical.Controllers
{
    [Authorize]
    [Area(nameof(Medical))]
    public class ReceiptInquiryController : Controller
    {
        readonly private IReceiptInquiryRepo _receiptInquiryRepo;
        readonly private ICurrentUserService _currentUserService;

        public ReceiptInquiryController(IReceiptInquiryRepo receiptInquiryRepo, ICurrentUserService currentUserService)
        {
            _receiptInquiryRepo = receiptInquiryRepo;
            _currentUserService = currentUserService;
        }

        public IActionResult Index()
        {
            ViewBag.FYear = _currentUserService.Year;

            return View();
        }

        [HttpPost]
        public IActionResult Index(ReceiptInquiryRequestDTO model)
        {
            ViewBag.FYear = _currentUserService.Year;
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var table =  _receiptInquiryRepo.GetReceiptInquiryReport(model.ExInvoiceNo, _currentUserService.Year, _currentUserService.BranchId).ToList();
            WebReport webReport = new();
            webReport.Report.Load(Path.Combine(Environment.CurrentDirectory, "Reports\\Medical\\ReceiptInquiry.frx"));


            webReport.Report.RegisterData(table, "Table");
            webReport.Report.Prepare();

            ViewBag.WebReport = webReport;
            return View();
        }
    }
}
