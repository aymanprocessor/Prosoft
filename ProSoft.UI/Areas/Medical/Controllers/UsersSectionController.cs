using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProSoft.EF.DTOs.Auth;
using ProSoft.EF.DTOs.Medical.HospitalPatData;
using ProSoft.EF.DTOs.Treasury;
using ProSoft.EF.IRepositories;
using ProSoft.EF.IRepositories.Medical.HospitalPatData;
using ProSoft.EF.Models;
using ProSoft.EF.Models.Medical.HospitalPatData;
using ProSoft.EF.Models.Treasury;

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
        //Ajax In Index
        public async Task<IActionResult> GetMedicalServicesForUser(int id)
        {
            List<UsersSectionDTO> usersSectionDTOs = await _usersSectionRepo
                .GetMedicalServicesForUser(id);
            return Json(usersSectionDTOs);
        }
        // Get Add
        public async Task<IActionResult> Add_UsersSection(int id)
        {
            ViewBag.userName = (await _userRepo.GetUserByIdAsync(id)).UserName;
            UsersSectionDTO usersSectionDTO = await _usersSectionRepo.GetEmptyUsersSectionAsync(id);
            return View(usersSectionDTO);
        }

        //// Post Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add_UsersSection(int id, UsersSectionDTO usersSectionDTO)
        {
            if (ModelState.IsValid)
            {
                usersSectionDTO.BranchId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "U_Branch_Id").Value);
                usersSectionDTO.UserCode = id;

                UsersSection usersSection = _mapper.Map<UsersSection>(usersSectionDTO);
                await _usersSectionRepo.AddAsync(usersSection);
                await _usersSectionRepo.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(usersSectionDTO);
        }

        // Get Edit
        public async Task<IActionResult> Edit_UsersSection(int id)
        {
            UsersSectionDTO usersSectionDTO = await _usersSectionRepo.GetUsersSectionByIdAsync(id);
            ViewBag.userName = (await _userRepo.GetUserByIdAsync((int)usersSectionDTO.UserCode)).UserName;

            return View(usersSectionDTO);
        }

        // Post Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_UsersSection(int id, UsersSectionDTO usersSectionDTO)
        {
            if (ModelState.IsValid)
            {
                usersSectionDTO.BranchId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "U_Branch_Id").Value);

                UsersSection usersSection = _mapper.Map<UsersSection>(usersSectionDTO);
                await _usersSectionRepo.UpdateAsync(usersSection);
                await _usersSectionRepo.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(usersSectionDTO);
        }

        // Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete_UsersSection(int id)
        {
            UsersSection usersSection = await _usersSectionRepo.GetByIdAsync(id);

            await _usersSectionRepo.DeleteAsync(usersSection);
            await _usersSectionRepo.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
