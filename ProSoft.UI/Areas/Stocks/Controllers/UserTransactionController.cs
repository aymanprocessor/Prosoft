using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProSoft.EF.DTOs.Shared;
using ProSoft.EF.DTOs.Stocks;
using ProSoft.EF.IRepositories;
using ProSoft.EF.IRepositories.Shared;
using ProSoft.EF.IRepositories.Stocks;
using ProSoft.EF.Models.Shared;
using ProSoft.EF.Models.Stocks;

namespace ProSoft.UI.Areas.Stocks.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Stocks")]
    public class UserTransactionController : Controller
    {
        private readonly IUserTransRepo _userTransRepo;
        private readonly IUserRepo _userRepo;
        private readonly IGeneralTableRepo _generalCodeRepo;
        private readonly ITransTypeRepo _transTypeRepo;
        private readonly IMapper _mapper;

        public UserTransactionController(IUserTransRepo userTransRepo,
            IUserRepo userRepo, IGeneralTableRepo generalCodeRepo,
            ITransTypeRepo transTypeRepo, IMapper mapper)
        {
            _userTransRepo = userTransRepo;
            _userRepo = userRepo;
            _generalCodeRepo = generalCodeRepo;
            _transTypeRepo = transTypeRepo;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            List<StoreTran> transTypes = await _transTypeRepo.GetAllAsync();
            ViewBag.transTypes = _mapper.Map<List<StoreTransDTO>>(transTypes);
            List<UserTransViewDTO> userTransDTO = await _userTransRepo.GetAllUserTransAsync();
            return View(userTransDTO);
        }

        public async Task<IActionResult> GetPermissionsForUser(int id, int transType)
        {
            List<PermissionDefViewDTO> permissionsDTO = await _userTransRepo
                .GetPermissionsForUserAsync(id, transType);
            return Json(permissionsDTO);
        }

        public async Task<IActionResult> GetPermissionsByTransType(string id, int userCode)
        {
            List<PermissionDefViewDTO> permissionsDTO = await _userTransRepo
                .GetPermissionsByTransTypeAsync(id, userCode);
            return Json(permissionsDTO);
        }

        // Get Add
        //public async Task<IActionResult> Add_Transaction(int id)
        //{
        //    ViewBag.userCode = id;
        //    ViewBag.userName = (await _userRepo.GetUserByIdAsync(id)).UserName;
        //    List<GeneralCode> generalCodes = await _generalCodeRepo.GetAllAsync();
        //    List<PermissionDefViewDTO> generalCodesDTO = _mapper.Map < List<PermissionDefViewDTO>>(generalCodes);
        //    ViewBag.transactions = generalCodesDTO;

        //    UserTransEditAddDTO userTransDTO = await _userTransRepo.GetEmptyUserTransAsync(id);
        //    return View(userTransDTO);
        //}

        // Post Add
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Add_Transaction(UserTransEditAddDTO userTransDTO)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        await _userTransRepo.AddUserTransAsync(userTransDTO);
        //        await _userTransRepo.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(userTransDTO);
        //}

        // Get Edit
        public async Task<IActionResult> Edit_Transaction(int id, int gId)
        {
            ViewBag.userName = (await _userRepo.GetUserByIdAsync(id)).UserName;
            ViewBag.transName = (await _generalCodeRepo.GetPermissionByIdAsync(id)).GDesc;
            UserTransEditAddDTO userTransDTO = await _userTransRepo.GetUserTransByIdAsync(id, gId);
            return View(userTransDTO);
        }

        // Post Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_Transaction(int id, UserTransEditAddDTO userTransDTO)
        {
            if (ModelState.IsValid)
            {
                await _userTransRepo.UpdateUserTransAsync(id, userTransDTO);
                await _userTransRepo.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userTransDTO);
        }

        // Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete_Transaction(int gId, int userCode)
        {
            await _userTransRepo.DeleteUserTransAsync(userCode, gId);
            await _userTransRepo.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
