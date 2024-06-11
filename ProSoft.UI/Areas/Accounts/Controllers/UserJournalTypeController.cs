using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProSoft.EF.DTOs.Accounts;
using ProSoft.EF.DTOs.Auth;
using ProSoft.EF.IRepositories;
using ProSoft.EF.IRepositories.Accounts;
using ProSoft.EF.Models;

namespace ProSoft.UI.Areas.Accounts.Controllers
{
    [Authorize]
    [Area(nameof(Accounts))]
    public class UserJournalTypeController : Controller
    {
        private readonly IUserJournalTypeRepo _userJournalTypeRepo;
        private readonly IUserRepo _userRepo;

        private readonly IMapper _mapper;
        public UserJournalTypeController(IUserJournalTypeRepo userJournalTypeRepo, IUserRepo userRepo, IMapper mapper)
        {
            _userJournalTypeRepo = userJournalTypeRepo;
            _userRepo = userRepo;
            _mapper = mapper;
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
        public async Task<IActionResult> Index()
        {
            List<AppUser> users = await _userRepo.GetAllUsersAsync();
            List<UserDTO> usersDTO = _mapper.Map<List<UserDTO>>(users);
            return View(usersDTO);
        }
    }
}
