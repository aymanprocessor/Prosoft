using FastReport.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProSoft.Core.Repositories.Stocks;
using ProSoft.EF.DbContext;
using ProSoft.EF.DTOs.Stocks;
using ProSoft.EF.DTOs.Stocks.Report.TotalItemCards;
using ProSoft.EF.IRepositories.Stocks;
using ProSoft.EF.IRepositories.Stocks.Reports;
using ProSoft.EF.Models.Stocks;

namespace ProSoft.UI.Areas.Stocks.Controllers
{
    [Area("Stocks")]
    [Authorize]
    public class TotalItemCardsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IMainItemRepo _mainItemRepo;
        private readonly ISupplierRepo _supplierRepo;
        private readonly IStockRepo _stockRepo;
        private readonly ICurrentUserService _currentUserService;
        private readonly ITotalItemCardsRepo _totalItemCardsRepo;
        public TotalItemCardsController(IMainItemRepo mainItemRepo, ISupplierRepo supplierRepo, AppDbContext context, ICurrentUserService currentUserService, IStockRepo stockRepo, ITotalItemCardsRepo totalItemCardsRepo)
        {
            this._context = context;
            _mainItemRepo = mainItemRepo;
            _supplierRepo = supplierRepo;
            _currentUserService = currentUserService;
            _stockRepo = stockRepo;
            _totalItemCardsRepo = totalItemCardsRepo;
        }
        public async Task<IActionResult> TotalItemCardsQuantity()
        {
            List<StockViewDTO> stockViewDTOs = await _stockRepo.GetActiveStocksForUserAsync(_currentUserService.UserId);
            ViewBag.MainItems = await GetMainItem();
            ViewBag.Suppliers = await _context.SupCodes.ToListAsync();
            ViewBag.BranchId = _currentUserService.BranchId;
            ViewBag.Stocks = stockViewDTOs;
            return View();
        }

       

        [HttpPost]
        public async Task<IActionResult> TotalItemCardsQuantity(TotalItemCardsRequestDTO model)
        {

            List<StockViewDTO> stockViewDTOs = await _stockRepo.GetActiveStocksForUserAsync(_currentUserService.UserId);
            ViewBag.MainItems = await GetMainItem();
            ViewBag.Suppliers = await _context.SupCodes.ToListAsync();
            ViewBag.BranchId = model.BranchId;
            ViewBag.Stocks = stockViewDTOs;

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var table = await _totalItemCardsRepo.GetTotalItemCardQuantity( model.FromDate, model.ToDate);
            WebReport webReport = new();
            webReport.Report.Load(Path.Combine(Environment.CurrentDirectory, "Reports\\Stock\\TheTotalItemCards-Quantity.frx"));
            var Stock = await _stockRepo.GetStockByIdAsync(model.StockId);

            webReport.Report.SetParameterValue("FromDate", model.FromDate.ToString("dd/MM/yyyy"));
            webReport.Report.SetParameterValue("ToDate", model.ToDate.ToString("dd/MM/yyyy"));
            webReport.Report.SetParameterValue("StockName", Stock.Stknam);

            webReport.Report.RegisterData(table, "Table");
            webReport.Report.Prepare();

            ViewBag.WebReport = webReport;
            return View(model);


           
        }

        private async Task<List<MainItemDTO>> GetMainItem()
        {
            return await _context.MainItems.Join(_context.SubItems, m => new { m.MainCode, m.Flag1 }
            , s => new { s.MainCode, s.Flag1 },
            (m, s) => new { m, s }
            ).Where(joined =>
            string.Compare(joined.m.MainCode, "1") > 0 &&
            joined.m.LastSub == 1 &&
            joined.s.RowOnOff == 1
            ).Select(joined => new MainItemDTO
            {
                Name = joined.m.MainName,
                MainCode = joined.m.MainCode,
                AllName = joined.m.MainNameAll,
                Flag1 = joined.m.Flag1,
            })
            .Distinct()
            .ToListAsync();
        }
    }
}
