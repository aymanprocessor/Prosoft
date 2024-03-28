using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using ProSoft.EF.DTOs.Auth;
using ProSoft.EF.IRepositories;
using ProSoft.EF.Models;
using System.Data;
using System.Security.Claims;
using static System.Runtime.InteropServices.JavaScript.JSType;

//declare SignInResult
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace ProSoft.UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly IUserRepo _userRepo;

        public AccountController(UserManager<AppUser> userManager,
                    SignInManager<AppUser> signInManager,
                    IMapper mapper, IUserRepo userRepo,
                    RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _roleManager = roleManager;
            _userRepo= userRepo;
        }

        //Get register
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Register()
        {
            var ourroles = _roleManager.Roles;
            ViewBag.Roles = ourroles;
            return View();
        }

        //Post register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserRegisterDTO userDTO)
        {
            if (ModelState.IsValid)
            {
                // Set Id
                var allUsers= await _userRepo.GetAllUsersAsync();
                userDTO.UserCode = allUsers.Count() == 0 ? 1 : allUsers.Max(obj => obj.UserCode) + 1;

                // Mapping from view model
                AppUser user = _mapper.Map<AppUser>(userDTO);
                #region mapper
                //AppUser user = new AppUser();
                //_mapper.Map(userDTO, user);
                #endregion

                #region  Create Role
                // Create new role
                //IdentityRole role = new IdentityRole();
                //role.Name = "Admin";

                //IdentityResult resultRole = 
                //    await _roleManager.CreateAsync(role);
                #endregion

                // Create user result
                IdentityResult result =
                    await _userManager.CreateAsync(user, user.PassWord);


                if (result.Succeeded)// && resultRole.Succeeded)
                {
                    // Add role to user
                    await _userManager.AddToRoleAsync(user, userDTO.Name);

                    // Add cookies for login
                    //await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Users", "Dashboard");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(userDTO);
        }
        //Get Login
        public async Task<IActionResult> Login()
        {
            return View();
        }
        //Get Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserLoginDTO userDTO)
        {
           if (ModelState.IsValid) 
            {
                AppUser user = await _userRepo.GetUserByIdAsync(userDTO.UserCode);
                if (user != null) 
                {
                    //create Cookie
                    SignInResult result = await _signInManager
                        .PasswordSignInAsync(user, userDTO.PassWord, userDTO.rememberMe, false);
                    //Check Cookie
                    if (result.Succeeded) 
                    {
                        if (User.IsInRole("Admin"))
                        {
                            return RedirectToAction("Index", "Dashboard");
                        }else
                            return RedirectToAction("Index", "Home");
                    }
                    else
                        ModelState.AddModelError("", "Invalid User Id or password");
                }
                else
                    ModelState.AddModelError("", "Invalid User Id or password");
            }
            return View(userDTO);
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

        //////////////////////////////////////////////////////
        // Remote Validation
        public async Task<IActionResult> VerifyUser(string PassWord, int UserCode)
        {
            AppUser user = await _userRepo.GetUserByIdAsync(UserCode);
            var checkpassword = false;
            if (user != null)
                checkpassword = await _userManager.CheckPasswordAsync(user, PassWord);

            return Json(checkpassword);
        }

        //////////////////////////////////////////////////////

        //  Get ChangePassword
        [Authorize]
        public async Task<IActionResult> ChangePassword()
        {
            return View();
        }

        //  Post ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordDTO changePassword)
        {
            if (ModelState.IsValid)
            {
                string UsrId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                var user = await _userManager.FindByIdAsync(UsrId);
                if (user != null)
                {
                    var checkpassword = await _userManager.CheckPasswordAsync(user, changePassword.CurrentPassword);
                    if (checkpassword)
                    {
                        var ChangeResult = await _userManager.ChangePasswordAsync(user, changePassword.CurrentPassword, changePassword.NewPassword);
                        if (ChangeResult.Succeeded)
                        {
                            user.PassWord = changePassword.NewPassword;
                            user.PassConfirm = changePassword.ConfirmPassword;
                            await _userManager.UpdateAsync(user);

                            await _signInManager.SignOutAsync();
                            return RedirectToAction("Login");
                        }
                        else
                            return View(changePassword);
                    }
                    else
                        ModelState.AddModelError("CurrentPassword", "Error the correct password");
                }
                else return NotFound();
            }
            return View(changePassword);
        }


        // Get Change Financial Year
        public async Task<IActionResult> Edit_FinancialYear()
        {
            return View();
        }
        
        // Post Change Financial Year
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_FinancialYear(int year)
        {
            return View();
        }
    }
}
