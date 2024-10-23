using FastReport.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProSoft.Core.Repositories.Stocks;
using ProSoft.EF.DTOs.Stocks;
using ProSoft.EF.DTOs.Stocks.Report.ClassCard;
using ProSoft.EF.IRepositories.Stocks;
using ProSoft.EF.Models.MedicalRecords;
using ProSoft.EF.Models.Stocks;

namespace ProSoft.UI.Areas.Stocks.Controllers
{
    [Authorize]
    [Area(nameof(Stocks))]
    public class QuantityClassCardController : Controller
    {
        private readonly IReportQuantityClassCardRepo _reportQuantityClassCardRepo;
        private readonly IStockRepo _stockRepo;
        private readonly IUnitCodeRepo _unitCodeRepo;
        private readonly ISubItemRepo _subItemRepo;
        public QuantityClassCardController(IReportQuantityClassCardRepo reportQuantityClassCardRepo, IStockRepo stockRepo, ISubItemRepo subItemRepo, IUnitCodeRepo unitCodeRepo)
        {
            _reportQuantityClassCardRepo = reportQuantityClassCardRepo;
            _stockRepo = stockRepo;
            _subItemRepo = subItemRepo;
            _unitCodeRepo = unitCodeRepo;
        }
        public async Task<IActionResult> Index()
        {
            var branch = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "U_Branch_Id").Value);

            var fYear = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "F_Year").Value);
            var userCode = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "User_Code").Value);
            ReportQuantityClassCardDTO reportQuantityClassCardDTO = await _reportQuantityClassCardRepo.GetAllDataAsync(userCode);
            reportQuantityClassCardDTO.FYear= fYear;
            reportQuantityClassCardDTO.BranchId= branch;
            return View(reportQuantityClassCardDTO);
        }

        public async Task<IActionResult> QuantityClassCardOnly(int id, string subItem, DateTime? fromPeriod, DateTime? toPeriod)
        {
            var branch = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "U_Branch_Id").Value);

            var reportData = await _reportQuantityClassCardRepo.GetQuantityCardOnly(id, subItem, fromPeriod, toPeriod, branch);
            return Json(reportData);
        }

        [HttpGet]
        public async Task<IActionResult> QuantityAndValueClassCard()
        {

            var branch = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "U_Branch_Id").Value);
            var userCode = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "User_Code").Value);
            var fYear = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "F_Year").Value);

            ReportQuantityClassCardDTO reportQuantityClassCardDTO = await _reportQuantityClassCardRepo.GetAllDataAsync(userCode);

            reportQuantityClassCardDTO.FYear = fYear;
            reportQuantityClassCardDTO.BranchId = branch;
            return View(reportQuantityClassCardDTO);


        }

        [HttpPost]
        public async Task<IActionResult> QuantityAndValueClassCard(ReportQuantityClassCardDTO model)
        {



            var branch = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "U_Branch_Id").Value);
            var userCode = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "User_Code").Value);
            var fYear = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "F_Year").Value);

            ReportQuantityClassCardDTO reportQuantityClassCardDTO = await _reportQuantityClassCardRepo.GetAllDataAsync(userCode);

            reportQuantityClassCardDTO.FYear = fYear;
            reportQuantityClassCardDTO.BranchId = branch;
            reportQuantityClassCardDTO.FromDate = model.FromDate;
            reportQuantityClassCardDTO.ToDate = model.ToDate;
            reportQuantityClassCardDTO.StockId = model.StockId;
            reportQuantityClassCardDTO.ItemMaster = model.ItemMaster;

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var table = await _reportQuantityClassCardRepo.GetQuantityAndValueCard(model.StockId, model.ItemMaster, model.FromDate, model.ToDate, model.BranchId);
            WebReport webReport = new();
            webReport.Report.Load(Path.Combine(Environment.CurrentDirectory, "Reports\\Stock\\QuantityAndValueClassCard.frx"));
            var Stock = await _stockRepo.GetStockByIdAsync(model.StockId);
            var subItem = await _subItemRepo.GetSubItemByItemCodeAsync(model.ItemMaster);
            //var toStock = await _stockRepo.GetStockByIdAsync(model.ToStock);
            //var stockType = await _generalTableRepo.GetPermissionByUniqueTypeAsync(model.UniqueType);
            webReport.Report.SetParameterValue("FromDate", model.FromDate.ToString("dd/MM/yyyy"));
            webReport.Report.SetParameterValue("ToDate", model.ToDate.ToString("dd/MM/yyyy"));
            webReport.Report.SetParameterValue("ItemName", subItem?.SubName ?? "غير معروف");
            webReport.Report.SetParameterValue("ItemCode", model.ItemMaster);
            webReport.Report.SetParameterValue("StockName", Stock.Stknam);
            webReport.Report.SetParameterValue("UnitName","باكيت");
            webReport.Report.RegisterData(table, "Table");
            webReport.Report.Prepare();

            ViewBag.WebReport = webReport;
            return View(reportQuantityClassCardDTO);

    
        }

        [HttpGet]
        public async Task<IActionResult> AtLevelTransactionCard()
        {

            var branch = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "U_Branch_Id").Value);
            var userCode = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "User_Code").Value);
            var fYear = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "F_Year").Value);


            List<StockViewDTO> stockViewDTOs = await _stockRepo.GetActiveStocksForUserAsync(userCode);
            List<SubItem> subItems = await _subItemRepo.GetAllAsync();
            List<UnitCode> unitCodes = await _unitCodeRepo.GetAllAsync();

            ViewBag.SubItems = subItems;
            ViewBag.UnitCodes = unitCodes;

            ViewBag.Stocks = stockViewDTOs;
            ViewBag.BranchId= branch;
            ViewBag.FYear = fYear;
            ViewBag.WebReport = null;


            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AtLevelTransactionCard(AtTransactionLevelRequestDTO model)
        {
            var branch = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "U_Branch_Id").Value);
            var userCode = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "User_Code").Value);
            var fYear = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "F_Year").Value);


            List<StockViewDTO> stockViewDTOs = await _stockRepo.GetActiveStocksForUserAsync(userCode);
            List<SubItem> subItems = await _subItemRepo.GetAllAsync();
            List<UnitCode> unitCodes = await _unitCodeRepo.GetAllAsync();

            ViewBag.SubItems = subItems;
            ViewBag.Stocks = stockViewDTOs;
            ViewBag.UnitCodes = unitCodes;
            ViewBag.BranchId = branch;
            ViewBag.FYear = fYear;


            if (!ModelState.IsValid)
            {
                return View(model);

            }
            var table = await _reportQuantityClassCardRepo.GetAtLevelTransactionCard(model.StockId, model.ItemMaster,(int)model.UnitCode, model.FromDate, model.ToDate, model.BranchId);
            WebReport webReport = new();
            webReport.Report.Load(Path.Combine(Environment.CurrentDirectory, "Reports\\Stock\\AtLevelTranactionCard.frx"));
            var Stock = await _stockRepo.GetStockByIdAsync(model.StockId);
            var subItem = await _subItemRepo.GetSubItemByItemCodeAsync(model.ItemMaster);
            var Unit = await _unitCodeRepo.GetByIdAsync((int)model.UnitCode);
        
            webReport.Report.SetParameterValue("FromDate", model.FromDate.ToString("dd/MM/yyyy"));
            webReport.Report.SetParameterValue("ToDate", model.ToDate.ToString("dd/MM/yyyy"));
            webReport.Report.SetParameterValue("ItemName", subItem?.SubName ?? "غير معروف");
            webReport.Report.SetParameterValue("ItemCode", model.ItemMaster);
            webReport.Report.SetParameterValue("StockName", Stock.Stknam);
            webReport.Report.SetParameterValue("UnitName", Unit.Names);
            webReport.Report.RegisterData(table, "Table");
            webReport.Report.Prepare();

            ViewBag.WebReport = webReport;
            return View(model);

        }
    }
}
