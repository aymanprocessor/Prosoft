/*
 * Coded By Ayman Saad @ 2/9/2024
*/

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProSoft.EF.IRepositories.Stocks;
using ProSoft.EF.IRepositories;
using ProSoft.EF.Models;
using ProSoft.EF.DTOs.Auth;
using Microsoft.AspNetCore.Authorization;
using ProSoft.EF.DTOs.Stocks;
using ProSoft.EF.IRepositories.Shared;
using ProSoft.EF.Models.Stocks;
using Microsoft.EntityFrameworkCore;
using ProSoft.Core.Repositories;
using Microsoft.AspNetCore.Mvc.Localization;

namespace ProSoft.UI.Areas.Stocks.Controllers
{
    [Authorize]
    [Area(nameof(Stocks))]
    public class StockEmpFlagController : Controller
    {
        private readonly IUserRepo _userRepo;
        private readonly IBranchRepo _branchRepo;
        private readonly IStockRepo _stockRepo;
        private readonly IStockTypeRepo _stockTypeRepo;
        private readonly IStockEmpFlagRepo _stockEmpFlagRepo;
        private readonly IMapper _mapper;
        private readonly IViewLocalizer _localizer;
        public StockEmpFlagController(
            IViewLocalizer localizer, IStockEmpRepo stockEmpRepo, IUserRepo userRepo, IMapper mapper, IBranchRepo branchRepo, IStockRepo stockRepo, IStockTypeRepo stockTypeRepo, IStockEmpFlagRepo stockEmpFlagRepo)
        {

            _localizer = localizer;
            _userRepo = userRepo;
            _mapper = mapper;
            _branchRepo = branchRepo;
            _stockRepo = stockRepo;
            _stockTypeRepo = stockTypeRepo;
            _stockEmpFlagRepo = stockEmpFlagRepo;
        }

        public IActionResult Index()
        {
            IEnumerable<StockEmpFlag> allData = _stockEmpFlagRepo.GetStockEmpFlags();
            StockEmpFlagViewDTO model = new()
            {
                Users = _userRepo.GetAllUsersAsEnumerable(),
                StockEmpFlags = allData
            };
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Add_StockEmpFlag(int id)
        {
            ViewBag.userId = id;
            var user = await _userRepo.GetUserByIdAsync(id);
            if (user == null) return NotFound();
            ViewBag.userName = user.UserName;

            StockEmpFlagEditAddDTO stockEmpFlagEditAddDTO = new()
            {
                BranchId = int.Parse(User.Claims.FirstOrDefault(u => u.Type == "U_Branch_Id").Value),
                Branchs = _branchRepo.GetAllBranchesAsEnumerable(),
                Stocks = _stockRepo.GetAllStockAsEnumerable(),
                StockTypes = _stockTypeRepo.GetAllStockTypeAsEnumerable()
            };
            return View(stockEmpFlagEditAddDTO);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add_StockEmpFlag(StockEmpFlagEditAddDTO stockEmpFlagEditAddDTO)
        {
            if (!ModelState.IsValid)
            {
                stockEmpFlagEditAddDTO.Stocks = _stockRepo.GetAllStockAsEnumerable();
                stockEmpFlagEditAddDTO.Branchs = _branchRepo.GetAllBranchesAsEnumerable();
                stockEmpFlagEditAddDTO.StockTypes = _stockTypeRepo.GetAllStockTypeAsEnumerable();

                return View(stockEmpFlagEditAddDTO);
            }

            stockEmpFlagEditAddDTO.BranchId = int.Parse(User.Claims.FirstOrDefault(u => u.Type == "U_Branch_Id").Value);

            _stockEmpFlagRepo.Add(stockEmpFlagEditAddDTO);
            // TODO : Return General Response
            //Save Stock Emp Flag to database
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Edit_StockEmpFlag(int userid, int orgStkcod, int branchid, int orgKID)
        {
            ViewBag.userId = userid;
            var user = await _userRepo.GetUserByIdAsync(userid);
            if (user == null) return NotFound();
            ViewBag.userName = user.UserName;

            var updateData = _stockEmpFlagRepo.GetById(userid, orgStkcod, branchid, orgKID);
            if (updateData == null) return NotFound();

            StockEmpFlagEditAddDTO stockEmpFlagEditAddDTO = new()
            {
                UserCode = userid,
                KID = updateData.KID,
                Stkcod = updateData.Stkcod,
                BranchId = updateData.BranchId,
                Branchs = _branchRepo.GetAllBranchesAsEnumerable(),
                Stocks = _stockRepo.GetAllStockAsEnumerable(),
                StockTypes = _stockTypeRepo.GetAllStockTypeAsEnumerable()
            };
            return View(stockEmpFlagEditAddDTO);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_StockEmpFlag(int orgStkcod, int orgKID, StockEmpFlagEditAddDTO model)
        {

            model.Branchs = _branchRepo.GetAllBranchesAsEnumerable();
            model.Stocks = _stockRepo.GetAllStockAsEnumerable();
            model.StockTypes = _stockTypeRepo.GetAllStockTypeAsEnumerable();

            if (!ModelState.IsValid)
            {

                return View(model);
            }

            var res = (GeneralResponse<StockEmpFlag>)await _stockEmpFlagRepo.Update(model.UserCode, orgStkcod, model.BranchId, orgKID, model);
            if (!res.Success)
            {
                TempData["ErrorMessage"] = res.Message;
                //return View(model);
                return RedirectToAction(nameof(Edit_StockEmpFlag),new { userId=model.UserCode, orgStkcod = orgStkcod,branchId=model.BranchId, orgKID = orgKID });
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int userId, int stkcod, int branchId, int kId)
        {
            await _stockEmpFlagRepo.Delete(userId, stkcod, branchId, kId);
            // TODO : return general response
            return RedirectToAction(nameof(Index));
        }


        public IActionResult GetStockEmpFlags(string userId)
        {
            IEnumerable<StockEmpFlag> allData = _stockEmpFlagRepo.GetStockEmpFlags();

            var stockEmpFlags = allData
                .Where(x => x.UserId == int.Parse(userId))
                .ToList();

            return Json(stockEmpFlags);
        }

    }
}
