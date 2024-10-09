using FastReport;
using FastReport.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using ProSoft.EF.DTOs.Stocks.Report.TransferAndReceiptTransactionReport;
using ProSoft.EF.IRepositories.Shared;
using ProSoft.EF.IRepositories.Stocks;
using ProSoft.EF.IRepositories.Stocks.Reports;
using ProSoft.EF.Models.Shared;
using ProSoft.EF.Models.Stocks;


namespace ProSoft.UI.Areas.Stocks.Controllers
{

    [Area("Stocks")]
    [Authorize]
    public class TransferAndReceiptTransactionReportController : Controller
    {
        private readonly IBranchRepo _branchRepo;
        private readonly IGeneralTableRepo _generalTableRepo;
        private readonly IStockRepo _stockRepo;
        private readonly ISubItemRepo _subItemRepo;
        private readonly ITransMasterRepo _transMasterRepo;
        private readonly ITransDtlRepo _transDtlRepo;
        private readonly IUnitCodeRepo _unitCodeRepo;
        private readonly ISubItemDtlRepo _subItemDtlRepo;
        private readonly IReportTransferAndReceiptTransactionRepo _reportTransferAndReceiptTransactionRepo;

        public TransferAndReceiptTransactionReportController(IBranchRepo branchRepo, IGeneralTableRepo generalTableRepo, IStockRepo stockRepo, ISubItemRepo subItemRepo, ITransMasterRepo transMasterRepo, ITransDtlRepo transDtlRepo, IUnitCodeRepo unitCodeRepo, ISubItemDtlRepo subItemDtlRepo, IReportTransferAndReceiptTransactionRepo reportTransferAndReceiptTransactionRepo)
        {
            _branchRepo = branchRepo;
            _generalTableRepo = generalTableRepo;
            _stockRepo = stockRepo;
            _subItemRepo = subItemRepo;
            _transMasterRepo = transMasterRepo;
            _transDtlRepo = transDtlRepo;
            _unitCodeRepo = unitCodeRepo;
            _subItemDtlRepo = subItemDtlRepo;
            _reportTransferAndReceiptTransactionRepo = reportTransferAndReceiptTransactionRepo;
        }
        public IActionResult Index()
        {
            ViewBag.Branchs = _branchRepo.GetAllBranchesAsEnumerable();
            ViewBag.GeneralCodes = _generalTableRepo.GetAllAsSelectListItem();
            ViewBag.FromStocks = _stockRepo.GetAllStockAsEnumerable();
            ViewBag.ToStocks = _stockRepo.GetAllStockAsEnumerable();

            return View();
        }

        [HttpPost]
        public IActionResult Index(TransferAndReceiptTransactionReportRequestDTO model)
        {

            ViewBag.Branchs = _branchRepo.GetAllBranchesAsEnumerable();
            ViewBag.GeneralCodes = _generalTableRepo.GetAllAsSelectListItem();
            ViewBag.FromStocks = _stockRepo.GetAllStockAsEnumerable();
            ViewBag.ToStocks = _stockRepo.GetAllStockAsEnumerable();

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var table = _reportTransferAndReceiptTransactionRepo.GetReport(model).ToList();
            Console.WriteLine(Path.Combine(Environment.CurrentDirectory, "Reports\\Stock\\TransactionsOfTransferAndReceiptAuthorizationsDuringThePeriod.frx"));
            WebReport webReport = new();
            webReport.Report.Load(Path.Combine(Environment.CurrentDirectory, "Reports\\Stock\\TransactionsOfTransferAndReceiptAuthorizationsDuringThePeriod.frx"));
            webReport.Report.SetParameterValue("FromStock", model.FromStock);
            webReport.Report.SetParameterValue("ToStock", model.ToStock);
            webReport.Report.SetParameterValue("FromDate", model.FromDate);
            webReport.Report.SetParameterValue("ToDate", model.ToDate);
            webReport.Report.SetParameterValue("ReportType", "بيان تحيلي");
            webReport.Zoom = 1.5f;
            webReport.Report.RegisterData(table, "Table");
            webReport.Report.Prepare();

            ViewBag.WebReport = webReport;
            return View();
        }
    }
}
