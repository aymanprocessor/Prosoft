using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProSoft.EF.DTOs.Stocks.TransferSuppliesToStocks;
using ProSoft.EF.IRepositories.Medical.HospitalPatData;
using ProSoft.EF.IRepositories.Shared;
using ProSoft.EF.IRepositories.Stocks;
using ProSoft.EF.Migrations;

namespace ProSoft.UI.Areas.Stocks.Controllers
{
    [Area(nameof(Stocks))]
    [Authorize]
    public class TransferSuppliesToStockController : Controller
    {
        private readonly IBranchRepo _branchRepo;
        private readonly IRegionRepo _regionRepo;
        private readonly IPostSuppliesToStocksRepo _postSuppliesToStocksRepo;
        private readonly ICurrentUserService _currentUserService;


        public TransferSuppliesToStockController(IBranchRepo branchRepo, IRegionRepo regionRepo, IPostSuppliesToStocksRepo postSuppliesToStocksRepo, ICurrentUserService currentUserService)
        {
            _branchRepo = branchRepo;
            _regionRepo = regionRepo;
            _postSuppliesToStocksRepo = postSuppliesToStocksRepo;
            _currentUserService = currentUserService;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Branchs = new SelectList(await _branchRepo.GetAllAsync(), "BranchId", "BranchDesc");
            ViewBag.Regions = new SelectList((await _regionRepo.GetAllAsync()).Where(r => r.OnOff == 1 && r.Flag ==0).ToList(), "RegionCode", "RegionDesc");

            TransferSuppliesToStocksRequestDTO model = new();
            model.BranchId = _currentUserService.BranchId;
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetPatAdmissions(int branchId,int regionId,DateTime fromDate, DateTime toDate)
        {
            // PostId = 1 => Transferred
            // PostId = 0 => not Transferred
            var PatAdmissions = await _postSuppliesToStocksRepo.GetPatAdmissions(branchId, regionId, fromDate, toDate,null); 
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
        public async Task<IActionResult> GetClinicTnxs(int BranchId, int MasterId,int PatId)
        {
            var ClinicTnxs = await _postSuppliesToStocksRepo.GetClinicTnxs(BranchId, MasterId,PatId, _currentUserService.Year);
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
        public async Task<IActionResult> PostSuppliesToStocks([FromBody] List<TransferSuppliesToStocksAJAXReqDTO> selectedRows)
        {
            // Check if selectedRows is not null and contains any data
            if (selectedRows != null && selectedRows.Count > 0)
            {
                // Iterate over the selected rows and process each one
                foreach (var row in selectedRows)
                {
                    // Call the repository method to transfer supplies to stocks
                    await _postSuppliesToStocksRepo.TransferSuppliesToStocks(
                        selectedRows,
                        _currentUserService.BranchId,
                        _currentUserService.Year,
                        _currentUserService.UserId
                    );
                }

                // Return a success response with the count of transferred rows
                return Ok(new { success = true, message = "Rows transferred successfully", transferredRowsCount = selectedRows.Count });
            }

            // If no rows were selected, return a failure response
            return BadRequest(new { success = false, message = "No rows selected" });

        }

    }
}
