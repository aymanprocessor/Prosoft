using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProSoft.Core.Repositories.Stocks;
using ProSoft.EF.DTOs.Accounts;
using ProSoft.EF.DTOs.Auth;
using ProSoft.EF.DTOs.Shared;
using ProSoft.EF.DTOs.Stocks;
using ProSoft.EF.IRepositories;
using ProSoft.EF.IRepositories.Stocks;
using ProSoft.EF.Models;
using ProSoft.EF.Models.Stocks;
using System.Web.Mvc.Ajax;

namespace ProSoft.UI.Areas.Stocks.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area(nameof(Stocks))]
    public class UserStockTransactionController : Controller
    {
        private readonly IStockEmpRepo _userStockRepo;
        private readonly IUserRepo _userRepo;
        private readonly IMapper _mapper; 

        public UserStockTransactionController(IStockEmpRepo serStockRepo
            , IUserRepo userRepo, IMapper mapper)
        {
            _userStockRepo = serStockRepo;
            _userRepo = userRepo;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            List<AppUser> users = await _userRepo.GetAllUsersAsync();
            List<UserDTO> usersDTO = _mapper.Map<List<UserDTO>>(users);
            return View(usersDTO);
        }

        //Ajax In Index
        public async Task<IActionResult> GetStockTransForUser(int id)
        {
            List<StockEmpViewDTO> stockTransDTO = await _userStockRepo
                .GetStockTransForUserAsync(id);
            return Json(stockTransDTO);
        }

        //Ajax In Add_StockTrans
        public async Task<IActionResult> GetStockPermissionsForAdd(int id, int stockCode)
        {
            List<PermissionDefViewDTO> permissionsDTO = await _userStockRepo
                .GetStockPermissionsForAddAsync(id, stockCode);
            return Json(permissionsDTO);
        }
        //Ajax In Add_StockTrans
        public async Task<IActionResult> GetSubCodesFromAcc(string id)
        {
            List<AccSubCodeDTO> subAccCodesDTO = await _userStockRepo
                .GetSubCodesFromAccAsync(id);
            return Json(subAccCodesDTO);
        }

        // Get Add
        public async Task<IActionResult> Add_StockTrans(int id)
        {
            ViewBag.userName = (await _userRepo.GetUserByIdAsync(id)).UserName;
            StockEmpEditAddDTO stockTransDTO = await _userStockRepo.GetEmptyStockTransAsync(id);
            return View(stockTransDTO);
        }

        // Post Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add_StockTrans(int id, StockEmpEditAddDTO stockTransDTO)
        {
            if (ModelState.IsValid)
            {
                //StockEmp stockTrans = _mapper.Map<StockEmp>(stockTransDTO);
                stockTransDTO.BranchId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "U_Branch_Id").Value);
                stockTransDTO.UserId = id;

                await _userStockRepo.AddStockTransAsync(stockTransDTO);
                return RedirectToAction(nameof(Index));
            }
            return View(stockTransDTO);
        }

        //Ajax In Edit_StockTrans
        public async Task<IActionResult> GetStockPermissionsForEdit(int id, int stockCode, int transType)
        {
            List<PermissionDefViewDTO> permissionsDTO = await _userStockRepo
                .GetStockPermissionsForEditAsync(id, stockCode, transType);
            return Json(permissionsDTO);
        }

        // Get Edit
        public async Task<IActionResult> Edit_StockTrans(int id)
        {
            StockEmpEditAddDTO stockTransDTO = await _userStockRepo.GetStockTransByIdAsync(id);
            ViewBag.subAccCodesStk = await _userStockRepo.GetSubCodesFromAccAsync(stockTransDTO.MainCodeStk);
            ViewBag.subAccCodesAcc = await _userStockRepo.GetSubCodesFromAccAsync(stockTransDTO.MainCodeAcc);

            return View(stockTransDTO);
        }

        // Post Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_StockTrans(int id, StockEmpEditAddDTO stockTransDTO)
        {
            if (ModelState.IsValid)
            {
                await _userStockRepo.UpdateStockTransAsync(id, stockTransDTO);
                return RedirectToAction(nameof(Index));
            }
            return View(stockTransDTO);
        }

        // Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete_StockTrans(int id)
        {
            await _userStockRepo.DeleteStockTransAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
