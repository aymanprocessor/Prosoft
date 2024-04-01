using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProSoft.EF.DTOs.Auth;
using ProSoft.EF.DbContext;
using ProSoft.EF.IRepositories;
using ProSoft.EF.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using static System.Runtime.InteropServices.JavaScript.JSType;
using ProSoft.EF.IRepositories.Shared;
using ProSoft.EF.Models.Shared;
using ProSoft.EF.DTOs.Shared;

namespace ProSoft.UI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IRoleRepo _roleRepo;
        private readonly IUserRepo _userRepo;
        private readonly IBranchRepo _branchRepo;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DashboardController(IMapper mapper, IRoleRepo roleRepo, IUserRepo userRepo,
                    RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager,
                    IBranchRepo branchRepo)
        {
            _mapper = mapper;
            _roleRepo = roleRepo;
            _roleManager = roleManager;
            _userRepo = userRepo;
            _userManager = userManager;
            _branchRepo = branchRepo;
        }
        public IActionResult Index()
        {
            return View();
        }

        //Display Roles
        public async Task<IActionResult> Roles()
        {
            List<IdentityRole> roles=await _roleRepo.GetAllRolesAsync();
            // take from list roles and put in rolesDtos
            List<RoleDTO> rolesDtos = _mapper.Map<List<RoleDTO>>(roles);

            //var usersRoles = await _roleRepo.GetAllUserRolesAsync();
            ViewBag.usersRoles = await _roleRepo.GetAllUserRolesAsync();

            return View(rolesDtos);
        }
        //Get Add Roles
        public async Task<IActionResult> Add_Role()
        {
            return View();
        }
        //Post Add Roles
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add_Role(RoleDTO roleDTO)
        {
            if (ModelState.IsValid)
            { 
                // Create new role
                IdentityRole role = new IdentityRole();
                role.Name = roleDTO.Name;

                IdentityResult resultRole =
                    await _roleManager.CreateAsync(role);
                if (resultRole.Succeeded)
                {
                    return RedirectToAction("Roles", "Dashboard");
                }
                else
                {
                    foreach (var error in resultRole.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(roleDTO);
        }

        //Get Edit Roles
        public async Task<IActionResult> Edit_Role(string id)
        {
            IdentityRole role = await _roleManager.FindByIdAsync(id);
            RoleDTO roleDTO = _mapper.Map<RoleDTO>(role);
            return View(roleDTO);
        }

        //Post Edit Roles
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_Role(string id, RoleDTO roleDTO)
        {
            if (ModelState.IsValid)
            {
                // Get role from DB
                IdentityRole existingrole = await _roleManager.FindByIdAsync(id);
                if (existingrole == null)
                {
                    return NotFound();
                }
                // Update role
                existingrole.Name = roleDTO.Name;
                var result = await _roleManager.UpdateAsync(existingrole);
                if (result.Succeeded)
                {
                    return RedirectToAction("Roles", "Dashboard");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(roleDTO);
        }

        //Post Delete Roles
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete_Role(string id)
        {
            IdentityRole existingrole = await _roleManager.FindByIdAsync(id);
            if (existingrole == null)
                return NotFound();

            await _roleManager.DeleteAsync(existingrole);
            return RedirectToAction("Roles", "Dashboard");
        }

        ////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////

        public async Task<IActionResult> Users()
        {
            IEnumerable<AppUser> users = await _userRepo.GetAllUsersAsync();

            // Create empty list
            List<UserDTO> usersDTO = new List<UserDTO>();
            foreach (var user in users)
            {
                List<IdentityUserRole<string>> userRoles = await _roleRepo.GetUserRolesByUserAsync(user.Id);

                // Create empty list
                List<IdentityRole> roles = new List<IdentityRole>();

                foreach (var userRole in userRoles)
                {
                    IdentityRole role = await _roleRepo.GetRoleByIdAsync(userRole.RoleId);
                    roles.Add(role);
                }
                List<RoleDTO> rolesDTO = _mapper.Map<List<RoleDTO>>(roles);

                UserDTO userDTO = _mapper.Map<UserDTO>(user);
                userDTO.Roles = rolesDTO;
                usersDTO.Add(userDTO);
            }
            // Another way for sorting
            //usersDTO.Sort((a, b) => a.UserCode - b.UserCode);
            usersDTO = usersDTO.OrderBy(obj => obj.UserCode).ToList();
            return View(usersDTO);
        }

        //Get Edit User
        public async Task<IActionResult> Edit_User(int id)
        {
            AppUser user = await _userRepo.GetUserByIdAsync(id);

            List<IdentityRole> allRoles = await _roleRepo.GetAllRolesAsync();
            // For getting roles names only
            ViewBag.Roles = _mapper.Map<List<RoleDTO>>(allRoles);

            List<IdentityUserRole<string>> userRoles = await _roleRepo.GetUserRolesByUserAsync(user.Id);
            // Create empty list
            List<IdentityRole> roles = new List<IdentityRole>();

            foreach (var userRole in userRoles)
            {
                IdentityRole role = await _roleRepo.GetRoleByIdAsync(userRole.RoleId);
                roles.Add(role);
            }
            List<RoleDTO> rolesDTO = _mapper.Map<List<RoleDTO>>(roles);

            UserEditDTO userEditDTO = _mapper.Map<UserEditDTO>(user);
            userEditDTO.Roles = rolesDTO;

            // Get Branches
            List<Branch> branches = await _branchRepo.GetAllAsync();
            userEditDTO.Branches = _mapper.Map<List<BranchDTO>>(branches);

            return View(userEditDTO);
        }
        //Post Edit User
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_User(int id, string[] roles, UserEditDTO userEditDTO)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await _userRepo.GetUserByIdAsync(id);

                var userRoles = await _userManager.GetRolesAsync(user);

                await _userManager.RemoveFromRolesAsync(user, userRoles);
                await _userManager.AddToRolesAsync(user, roles);

                _mapper.Map(userEditDTO, user);
                IdentityResult result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                    return RedirectToAction("Users", "Dashboard");
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            #region This code is working, Open it (The same in Get Edit)

            List<IdentityRole> allRoles = await _roleRepo.GetAllRolesAsync();
            // For getting roles names only
            ViewBag.Roles = _mapper.Map<List<RoleDTO>>(allRoles);

            // Set empty list so as not to be null
            userEditDTO.Roles = new List<RoleDTO>();
            #endregion

            return View(userEditDTO);
        }

        //Post Delete Users
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete_User(int id)
        {
            AppUser user = await _userRepo.GetUserByIdAsync(id);
            if (user == null)
                return NotFound();

            var roles = await _userManager.GetRolesAsync(user);

            //foreach (var role in roles)
            await _userManager.RemoveFromRolesAsync(user, roles);

            await _userManager.DeleteAsync(user);
            return RedirectToAction("Users");
        }
    }
}
