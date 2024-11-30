using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProSoft.Core.Repositories;
using ProSoft.EF.DTOs.Stocks.TransferSuppliesToStocks;
using ProSoft.EF.IRepositories.Medical.HospitalPatData;
using ProSoft.EF.IRepositories.Shared;
using ProSoft.EF.IRepositories.Stocks;

namespace ProSoft.UI.Areas.Stocks.Controllers
{
    [Area(nameof(Stocks))]
    [Authorize]
    public class CancelTransferSuppliesToStockController : Controller
    {

        private readonly IBranchRepo _branchRepo;
        private readonly IRegionRepo _regionRepo;
        private readonly IPostSuppliesToStocksRepo _postSuppliesToStocksRepo;
        private readonly ICurrentUserService _currentUserService;

        public CancelTransferSuppliesToStockController(IBranchRepo branchRepo, IRegionRepo regionRepo, IPostSuppliesToStocksRepo postSuppliesToStocksRepo, ICurrentUserService currentUserService)
        {
            _branchRepo = branchRepo;
            _regionRepo = regionRepo;
            _postSuppliesToStocksRepo = postSuppliesToStocksRepo;
            _currentUserService = currentUserService;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Branchs = new SelectList(await _branchRepo.GetAllAsync(), "BranchId", "BranchDesc");
            ViewBag.Regions = new SelectList((await _regionRepo.GetAllAsync()).Where(r => r.OnOff == 1 && r.Flag == 0).ToList(), "RegionCode", "RegionDesc");
            TransferSuppliesToStocksRequestDTO model = new();
            model.BranchId = _currentUserService.BranchId;
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetPatAdmissions(int branchId, int regionId, DateTime fromDate, DateTime toDate)
        {
            // PostId = 1 => Transferred
            // PostId = 0 => not Transferred
            var PatAdmissions = await _postSuppliesToStocksRepo.GetPatAdmissions(branchId, regionId, fromDate, toDate, 1);
            var result = new
            {
                draw = 1,
                recordsTotal = PatAdmissions.Count(),       // Total number of records
                recordsFiltered = PatAdmissions.Count(),    // Total filtered records (for paging)
                data = PatAdmissions                        // Actual data to display
            };

            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetClinicTnxs(int BranchId, int MasterId, int PatId)
        {
            var ClinicTnxs = await _postSuppliesToStocksRepo.GetClinicTnxs(BranchId, MasterId, PatId, _currentUserService.Year);
            var result = new
            {

                draw = 1,
                recordsTotal = ClinicTnxs.Count(),       // Total number of records
                recordsFiltered = ClinicTnxs.Count(),    // Total filtered records (for paging)
                data = ClinicTnxs                        // Actual data to display
            };

            return Json(result);

        }

        [HttpPost]
        public async Task<IActionResult> CancelPostSuppliesToStocks([FromBody] List<TransferSuppliesToStocksAJAXReqDTO> selectedRows)
        {
            if (selectedRows != null && selectedRows.Count > 0)
            {
                foreach (var row in selectedRows)
                {
                    await _postSuppliesToStocksRepo.CancelTransferSuppliesToStocks(selectedRows, _currentUserService.BranchId, _currentUserService.Year, _currentUserService.UserId);
                }
            }
            return Ok();

        }
    }
}
