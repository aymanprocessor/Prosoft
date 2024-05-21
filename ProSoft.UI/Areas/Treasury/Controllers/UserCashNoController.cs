using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProSoft.Core.Repositories;
using ProSoft.Core.Repositories.Treasury;
using ProSoft.EF.DTOs.Auth;
using ProSoft.EF.DTOs.Stocks;
using ProSoft.EF.DTOs.Treasury;
using ProSoft.EF.IRepositories;
using ProSoft.EF.IRepositories.Treasury;
using ProSoft.EF.Models;
using ProSoft.EF.Models.Stocks;
using ProSoft.EF.Models.Treasury;

namespace ProSoft.UI.Areas.Treasury.Controllers
{
    [Authorize]
    [Area(nameof(Treasury))]
    public class UserCashNoController : Controller
    {
        private readonly IUserCashNoRepo _userCashNoRepo;
        private readonly IUserRepo _userRepo;
        private readonly IMapper _mapper;
        public UserCashNoController(IUserCashNoRepo userCashNoRepo, IUserRepo userRepo, IMapper mapper)
        {
            _userCashNoRepo = userCashNoRepo;
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
        public async Task<IActionResult> GetSafeTransForUser(int id)
        {
            List<UserCashNoViewDTO> userCashNoDTOs = await _userCashNoRepo
                .GetCashForUserAsync(id);
            return Json(userCashNoDTOs);
        }

        // Get Add
        public async Task<IActionResult> Add_UserCashNo(int id)
        {
            ViewBag.userName = (await _userRepo.GetUserByIdAsync(id)).UserName;
            UserCashNoEditAddDTO userCashNoDTO = await _userCashNoRepo.GetEmptySafeTransAsync(id);
            return View(userCashNoDTO);
        }

        //// Post Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add_UserCashNo(int id, UserCashNoEditAddDTO userCashNoDTO)
        {
            if (ModelState.IsValid)
            {
                userCashNoDTO.BranchId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "U_Branch_Id").Value);
                userCashNoDTO.UserCode = id;

                await _userCashNoRepo.AddSafeTransAsync(userCashNoDTO);
                return RedirectToAction(nameof(Index));
            }
            return View(userCashNoDTO);
        }

        // Get Edit
        public async Task<IActionResult> Edit_UserCashNo(int id)
        {
            UserCashNoEditAddDTO userCashNoDTO = await _userCashNoRepo.GetSafeTransByIdAsync(id);
            ViewBag.subAccCodesSub = await _userCashNoRepo.GetSubCodesFromAccAsync(userCashNoDTO.MainCode);
            ViewBag.subAccCodesMain = await _userCashNoRepo.GetSubCodesFromAccAsync(userCashNoDTO.MainCode2);
            ViewBag.userName = (await _userRepo.GetUserByIdAsync(userCashNoDTO.UserCode)).UserName;

            return View(userCashNoDTO);
        }

        // Post Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_UserCashNo(int id, UserCashNoEditAddDTO userCashNoDTO)
        {
            if (ModelState.IsValid)
            {
                userCashNoDTO.BranchId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "U_Branch_Id").Value);

                await _userCashNoRepo.EditSafeTransAsync(id, userCashNoDTO);
                return RedirectToAction(nameof(Index));
            }
            return View(userCashNoDTO);
        }

        // Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete_UserCashNo(int id)
        {
            UserCashNo userCashNo = await _userCashNoRepo.GetByIdAsync(id);

            await _userCashNoRepo.DeleteAsync(userCashNo);
            await _userCashNoRepo.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
