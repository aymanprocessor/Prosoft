using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProSoft.EF.IRepositories.Medical.HospitalPatData;
using ProSoft.EF.IRepositories.Shared;

namespace ProSoft.UI.Areas.Stocks.Controllers
{
    public class TransferSuppliesToStockController : Controller
    {
        private readonly IBranchRepo _branchRepo;
        private readonly IRegionRepo _regionRepo;

        public TransferSuppliesToStockController(IBranchRepo branchRepo, IRegionRepo regionRepo)
        {
            _branchRepo = branchRepo;
            _regionRepo = regionRepo;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Branchs = new SelectList(await _branchRepo.GetAllAsync(), "BranchId", "BranchDesc");
            ViewBag.Regions = new SelectList((await _regionRepo.GetAllAsync()).Where(r => r.OnOff == 1 && r.Flag ==0).ToList(), "RegionCode", "RegionDesc");
            

            return View();
        }
    }
}
