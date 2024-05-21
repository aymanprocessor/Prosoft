using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProSoft.Core.Repositories.Stocks;
using ProSoft.EF.DTOs.Shared;
using ProSoft.EF.DTOs.Stocks;
using ProSoft.EF.IRepositories.Stocks;

namespace ProSoft.UI.Areas.Stocks.Controllers
{
    [Authorize]
    [Area(nameof(Stocks))]
    public class PermissionFormController : Controller
    {
        private readonly IUserTransRepo _userTransRepo;
        private readonly ITransMasterRepo _transMasterRepo;
        public PermissionFormController(IUserTransRepo userTransRepo, ITransMasterRepo transMasterRepo)
        {
            _userTransRepo = userTransRepo;
            _transMasterRepo = transMasterRepo;
        }

        public async Task<IActionResult> Index()
        {
            var userCode = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "User_Code").Value);
            List<PermissionDefViewDTO> permissionsDTO = await _userTransRepo.GetPermissionsForUserAsync(userCode);
            ViewBag.Permissions = permissionsDTO;

            List<StockViewDTO> stocksDTO = await _transMasterRepo.GetActiveStocksForUserAsync(userCode);
            ViewBag.Stocks = stocksDTO;
            return View();
        }

        public async Task<IActionResult> GetUserPermissionsForStock(int id)
        {
            var userCode = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "User_Code").Value);
            List<PermissionDefViewDTO> permissionsDTO = await _transMasterRepo
                .GetUserPermissionsForStockAsync(userCode, id);
            return Json(permissionsDTO);
        }

        public async Task<IActionResult> Add_PermissionForm(int id, int transType)
        {
            var userCode = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "User_Code").Value);
            List<PermissionDefViewDTO> permissionsDTO = await _transMasterRepo
                .GetUserPermissionsForStockAsync(userCode, id);
            return Json(permissionsDTO);
        }
    }
}
