using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProSoft.EF.DTOs.Auth;
using ProSoft.EF.IRepositories;
using ProSoft.EF.IRepositories.Medical.HospitalPatData;
using ProSoft.EF.Models;

namespace ProSoft.UI.Areas.Medical.Controllers
{
    [Authorize]
    [Area("Medical")]
    public class UsersSectionController : Controller
    {
        private readonly IUsersSectionRepo _usersSectionRepo;
        private readonly IUserRepo _userRepo;
        private readonly IMapper _mapper;
        public UsersSectionController(IUsersSectionRepo usersSectionRepo, IUserRepo userRepo, IMapper mapper)
        {
            _usersSectionRepo = usersSectionRepo;
            _userRepo = userRepo;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            List<AppUser> users = await _userRepo.GetAllUsersAsync();
            List<UserDTO> usersDTO = _mapper.Map<List<UserDTO>>(users);
            return View(usersDTO);
        }
    }
}
