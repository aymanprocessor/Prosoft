using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProSoft.EF.DTOs.Stocks.TransferSuppliesToStocks;
using ProSoft.EF.IRepositories.Medical.HospitalPatData;
using ProSoft.EF.IRepositories.Shared;
using ProSoft.EF.IRepositories.Stocks;

namespace ProSoft.UI.Areas.Stocks.Controllers
{
    [Area(nameof(Stocks))]
    [Authorize]
    public class TransferSuppliesToStockController : Controller
    {
        private readonly IBranchRepo _branchRepo;
        private readonly IRegionRepo _regionRepo;
        private readonly IPostSuppliesToStocksRepo _postSuppliesToStocksRepo;


        public TransferSuppliesToStockController(IBranchRepo branchRepo, IRegionRepo regionRepo, IPostSuppliesToStocksRepo postSuppliesToStocksRepo)
        {
            _branchRepo = branchRepo;
            _regionRepo = regionRepo;
            _postSuppliesToStocksRepo = postSuppliesToStocksRepo;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Branchs = new SelectList(await _branchRepo.GetAllAsync(), "BranchId", "BranchDesc");
            ViewBag.Regions = new SelectList((await _regionRepo.GetAllAsync()).Where(r => r.OnOff == 1 && r.Flag ==0).ToList(), "RegionCode", "RegionDesc");

            TransferSuppliesToStocksRequestDTO model = new();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetPatAdmissions(int branchId,int regionId,DateTime fromDate, DateTime toDate)
        {
            var PatAdmissions = await _postSuppliesToStocksRepo.GetPatAdmissions(branchId, regionId, fromDate, toDate);
            var result = new
            {
                draw = 1,
                recordsTotal = PatAdmissions.Count(),       // Total number of records
                recordsFiltered = PatAdmissions.Count(),    // Total filtered records (for paging)
                data = PatAdmissions                        // Actual data to display
            };

            return Json(result);

        }


    }
}
