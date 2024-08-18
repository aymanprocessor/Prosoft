using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProSoft.Core.Repositories.Accounts;
using ProSoft.EF.DTOs.Accounts;
using ProSoft.EF.DTOs.Auth;
using ProSoft.EF.IRepositories;
using ProSoft.EF.IRepositories.Accounts;
using ProSoft.EF.Models;
using ProSoft.EF.Models.Accounts;

namespace ProSoft.UI.Areas.Accounts.Controllers
{
    [Authorize]
    [Area(nameof(Accounts))]
    public class UserJournalTypeController : Controller
    {
        private readonly IUserJournalTypeRepo _userJournalTypeRepo;
        private readonly IUserRepo _userRepo;
        private readonly IJournalTypeRepo _journalTypeRepo;
        private readonly IMapper _mapper;
        public UserJournalTypeController(IUserJournalTypeRepo userJournalTypeRepo, IUserRepo userRepo, IJournalTypeRepo journalTypeRepo, IMapper mapper)
        {
            _userJournalTypeRepo = userJournalTypeRepo;
            _userRepo = userRepo;
            _journalTypeRepo = journalTypeRepo;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index(int? redirect)
        {
            List<AppUser> users = await _userRepo.GetAllUsersAsync();
            List<UserDTO> usersDTO = _mapper.Map<List<UserDTO>>(users);
            List<JournalType> journalTypes =await _journalTypeRepo.GetAllAsync();
            ViewBag.journalCount = journalTypes.Count;
            ViewBag.redirect = redirect;

            return View(usersDTO);
        }
        public async Task<IActionResult> JournalTypeForUser()
        {
            var userCode = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "User_Code").Value);
            List<UserJournalTypeDTO> userJournalTypeDTOs =await _userJournalTypeRepo.GetUserJournalTypesForUser(userCode);
            return View(userJournalTypeDTOs);
        }
        ////overloading
        public async Task<IActionResult> GetJournalTypeForUser(int id)
        {
            List<UserJournalTypeDTO> userJournalTypeDTOs = await _userJournalTypeRepo.GetUserJournalTypesForUser(id);
            return Json(userJournalTypeDTOs);
        }
        //Get add
        public async Task<IActionResult> Add_UserJournalType(int id)
        {
            UserJournalTypeDTO userJournalTypeDTO = await _userJournalTypeRepo.GetEmptyUserJournalTypeAsync(id);
            ViewBag.userCode = id;
            return View(userJournalTypeDTO);
        }

        //Post add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add_UserJournalType(int id,UserJournalTypeDTO userJournalTypeDTO)
        {
            if (ModelState.IsValid)
            {
                UserJournalType userJournalType = _mapper.Map<UserJournalType>(userJournalTypeDTO);
                userJournalType.BranchId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "U_Branch_Id").Value);
                await _userJournalTypeRepo.AddAsync(userJournalType);
                await _userJournalTypeRepo.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { redirect = id });
            }
            return View();
        }

        //Get Edit
        public async Task<IActionResult> Edit_UserJournalType(int id)
        {
            UserJournalTypeDTO userJournalTypeDTO= await _userJournalTypeRepo.GetUserJournalTypeByIdAsync(id);
            ViewBag.userCode = userJournalTypeDTO.UserCode;

            return View(userJournalTypeDTO);
        }

        //Post Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_UserJournalType(int id, UserJournalTypeDTO userJournalTypeDTO)
        {
            if (ModelState.IsValid)
            {
                userJournalTypeDTO.BranchId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "U_Branch_Id").Value);
                await _userJournalTypeRepo.EditUserJournalTypeAsync(id, userJournalTypeDTO);
                return RedirectToAction(nameof(Index), new { redirect = userJournalTypeDTO.UserCode });
            }
            return View(userJournalTypeDTO);
        }

        // Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete_UserJournalType(int id)
        {
            UserJournalType userJournalType = await _userJournalTypeRepo.GetByIdAsync(id);

            await _userJournalTypeRepo.DeleteAsync(userJournalType);
            await _userJournalTypeRepo.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new { redirect = userJournalType.UserCode });
        }
    }
}
