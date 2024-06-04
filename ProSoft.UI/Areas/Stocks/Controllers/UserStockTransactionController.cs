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
        private readonly IStockEmpRepo _stockEmpRepo;
        private readonly IUserRepo _userRepo;
        private readonly IMapper _mapper; 

        public UserStockTransactionController(IStockEmpRepo stockEmpRepo
            , IUserRepo userRepo, IMapper mapper)
        {
            _stockEmpRepo = stockEmpRepo;
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
            List<StockEmpViewDTO> stockTransDTO = await _stockEmpRepo
                .GetStockTransForUserAsync(id);
            return Json(stockTransDTO);
        }

        //Ajax In Add_StockTrans
        public async Task<IActionResult> GetUserStockPermissions(int id, int stockCode)
        {
            List<PermissionDefViewDTO> permissionsDTO = await _stockEmpRepo
                .GetUserStockPermissionsAsync(id, stockCode);
            return Json(permissionsDTO);
        }

        //Ajax In Add_StockTrans
        public async Task<IActionResult> GetSubCodesFromAcc(string id)
        {
            List<AccSubCodeDTO> subAccCodesDTO = await _stockEmpRepo
                .GetSubCodesFromAccAsync(id);
            return Json(subAccCodesDTO);
        }

        // Get Add
        public async Task<IActionResult> Add_StockTrans(int id)
        {
            ViewBag.userName = (await _userRepo.GetUserByIdAsync(id)).UserName;
            StockEmpEditAddDTO stockTransDTO = await _stockEmpRepo.GetEmptyStockTransAsync(id);
            return View(stockTransDTO);
        }

        // Post Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add_StockTrans(int id, StockEmpEditAddDTO stockTransDTO)
        {
            if (ModelState.IsValid)
            {
                stockTransDTO.BranchId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "U_Branch_Id").Value);
                stockTransDTO.UserId = id;

                await _stockEmpRepo.AddStockTransAsync(stockTransDTO);
                return RedirectToAction(nameof(Index));
            }
            return View(stockTransDTO);
        }

        //Ajax In Edit_StockTrans
        public async Task<IActionResult> GetUserStockPermissionsForEdit(int id, int stockCode, int transType)
        {
            List<PermissionDefViewDTO> permissionsDTO = await _stockEmpRepo
                .GetUserStockPermissionsForEditAsync(id, stockCode, transType);
            return Json(permissionsDTO);
        }

        // Get Edit
        public async Task<IActionResult> Edit_StockTrans(int id)
        {
            StockEmpEditAddDTO stockTransDTO = await _stockEmpRepo.GetStockTransByIdAsync(id);
            ViewBag.subAccCodesStk = await _stockEmpRepo.GetSubCodesFromAccAsync(stockTransDTO.MainCodeStk);
            ViewBag.subAccCodesAcc = await _stockEmpRepo.GetSubCodesFromAccAsync(stockTransDTO.MainCodeAcc);
            ViewBag.userName = (await _userRepo.GetUserByIdAsync(stockTransDTO.UserId)).UserName;
            return View(stockTransDTO);
        }

        // Post Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_StockTrans(int id, StockEmpEditAddDTO stockTransDTO)
        {
            if (ModelState.IsValid)
            {
                await _stockEmpRepo.UpdateStockTransAsync(id, stockTransDTO);
                return RedirectToAction(nameof(Index));
            }
            return View(stockTransDTO);
        }

        // Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete_StockTrans(int id)
        {
            await _stockEmpRepo.DeleteStockTransAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
