using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProSoft.Core.Repositories.Stocks;
using ProSoft.EF.DTOs.Shared;
using ProSoft.EF.DTOs.Stocks;
using ProSoft.EF.IRepositories;
using ProSoft.EF.IRepositories.Stocks;
using ProSoft.EF.Models.Stocks;

namespace ProSoft.UI.Areas.Stocks.Controllers
{
    [Authorize]
    [Area(nameof(Stocks))]
    public class PermissionFormController : Controller
    {
        private readonly IUserTransRepo _userTransRepo;
        private readonly ITransMasterRepo _transMasterRepo;
        private readonly IUserRepo _userRepo;
        public PermissionFormController(IUserTransRepo userTransRepo,
            ITransMasterRepo transMasterRepo, IUserRepo userRepo)
        {
            _userTransRepo = userTransRepo;
            _transMasterRepo = transMasterRepo;
            _userRepo = userRepo;
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

        public async Task<IActionResult> GetPermissionsForms(int id, int transType)
        {
            List<TransMasterViewDTO> permissionsFormsDTO = await _transMasterRepo
                .GetPermissionsFormsAsync(id, transType);
            return Json(permissionsFormsDTO);
        }

        // Get Add
        public async Task<IActionResult> Add_PermissionForm(int id, int transType)
        {
            var userCode = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "User_Code").Value);
            TransMasterEditAddDTO permissionFormDTO = await _transMasterRepo.GetDTOWithDefaultsAsync(id, transType);
            permissionFormDTO.UserName = (await _userRepo.GetUserByIdAsync(userCode)).UserName;

            return View(permissionFormDTO);
        }

        // Post Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add_PermissionForm(int id, TransMasterEditAddDTO permissionFormDTO)
        {
            if (ModelState.IsValid)
            {
                permissionFormDTO.BranchId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "U_Branch_Id").Value);
                permissionFormDTO.UserCode = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "User_Code").Value);

                await _transMasterRepo.AddPermissionFormAsync(permissionFormDTO);
                return RedirectToAction(nameof(Index));
            }
            return View(permissionFormDTO);
        }

        // Get Edit
        //public async Task<IActionResult> Edit_StockTrans(int id, int transType)
        //{
        //    StockEmpEditAddDTO stockTransDTO = await _userStockRepo.GetStockTransByIdAsync(id, transType);
        //    ViewBag.subAccCodesStk = await _userStockRepo.GetSubCodesFromAccAsync(stockTransDTO.MainCodeStk);
        //    ViewBag.subAccCodesAcc = await _userStockRepo.GetSubCodesFromAccAsync(stockTransDTO.MainCodeAcc);

        //    return View(stockTransDTO);
        //}

        // Post Edit
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit_StockTrans(int id, int transType, StockEmpEditAddDTO stockTransDTO)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        await _userStockRepo.UpdateStockTransAsync(id, transType, stockTransDTO);
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(stockTransDTO);
        //}

        // Delete
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Delete_StockTrans(int id, int transType)
        //{
        //    await _userStockRepo.DeleteStockTransAsync(id, transType);
        //    return RedirectToAction(nameof(Index));
        //}
    }
}
