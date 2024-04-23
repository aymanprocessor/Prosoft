using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProSoft.EF.DTOs.Shared;
using ProSoft.EF.DTOs.Stocks;
using ProSoft.EF.IRepositories;
using ProSoft.EF.IRepositories.Shared;
using ProSoft.EF.IRepositories.Stocks;
using ProSoft.EF.Models.Stocks;

namespace ProSoft.UI.Areas.Stocks.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Stocks")]
    public class UserTransactionController : Controller
    {
        private readonly IUserTransRepo _userTransRepo;
        private readonly IUserRepo _userRepo;
        private readonly IGeneralTableRepo _permissionsRepo;
        private readonly IMapper _mapper;
        public UserTransactionController(IUserTransRepo userTransRepo,
            IUserRepo userRepo, IGeneralTableRepo permissionsRepo, IMapper mapper)
        {
            _userTransRepo = userTransRepo;
            _userRepo = userRepo;
            _permissionsRepo = permissionsRepo;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            List<UserTransViewDTO> userTransDTO = await _userTransRepo.GetAllUserTransAsync();
            return View(userTransDTO);
        }

        public async Task<IActionResult> GetPermissionsForUser(int id)
        {
            List<PermissionDefViewDTO> permissionsDTO = await _userTransRepo
                .GetPermissionsForUserAsync(id);
            return Json(permissionsDTO);
        }

        public async Task<IActionResult> GetPermissionsByTransType(string id/*, int userCode*/)
        {
            List<PermissionDefViewDTO> permissionsDTO = await _userTransRepo
                .GetPermissionsByTransTypeAsync(id/*, userCode*/);
            return Json(permissionsDTO);
        }

        // Get Add
        public async Task<IActionResult> Add_Transaction(int id)
        {
            ViewBag.userCode = id;
            ViewBag.userName = (await _userRepo.GetUserByIdAsync(id)).UserName;
            ViewBag.transactions = await _permissionsRepo.GetAllPermissionsAsync();
            UserTransEditAddDTO userTransDTO = await _userTransRepo.GetEmptyUserTransAsync(id);
            return View(userTransDTO);
        }

        // Post Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add_Transaction(UserTransEditAddDTO userTransDTO)
        {
            if (ModelState.IsValid)
            {
                await _userTransRepo.AddUserTransAsync(userTransDTO);
                await _userTransRepo.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userTransDTO);
        }

        // Get Edit
        //public async Task<IActionResult> Edit_Transaction(int id)
        //{
        //    PermissionDefEditAddDTO permissionDTO = await _permissionRepo.GetPermissionByIdAsync(id);
        //    return View(permissionDTO);
        //}

        // Post Edit
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit_Transaction(int id, PermissionDefEditAddDTO permissionDTO)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        GeneralCode permission = await _permissionRepo.GetByIdAsync(id);
        //        _mapper.Map(permissionDTO, permission);
        //        //permission.GType = "4";

        //        await _permissionRepo.UpdateAsync(permission);
        //        await _permissionRepo.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(permissionDTO);
        //}

        // Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete_Transaction(int id, int userCode)
        {
            await _userTransRepo.DeleteUserTransAsync(id, userCode);
            await _userTransRepo.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
