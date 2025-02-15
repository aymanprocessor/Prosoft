using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProSoft.EF.DTOs.Auth;
using ProSoft.EF.IRepositories;
using ProSoft.EF.IRepositories.Shared;
using ProSoft.EF.Models;
using ProSoft.EF.Models.Shared;
using ProSoft.UI.Global;
using System.Security.Claims;

//declare SignInResult

namespace ProSoft.UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly IUserRepo _userRepo;
        private readonly IBranchRepo _branchRepo;

        public AccountController(UserManager<AppUser> userManager,
                    SignInManager<AppUser> signInManager,
                    IMapper mapper, IUserRepo userRepo,
                    RoleManager<IdentityRole> roleManager, IBranchRepo branchRepo)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _roleManager = roleManager;
            _userRepo = userRepo;
            _branchRepo = branchRepo;
        }

        //Get register
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Register()
        {
            var ourroles = _roleManager.Roles;
            ViewBag.Roles = ourroles;
            ViewBag.Branches = await _branchRepo.GetAllAsync();
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
                var allUsers = await _userRepo.GetAllUsersAsync();
                userDTO.UserCode = allUsers.Count() == 0 ? 1 : allUsers.Max(obj => obj.UserCode) + 1;

                // Mapping from view model
                AppUser user = _mapper.Map<AppUser>(userDTO);
                user.FYear = DateTime.UtcNow.Year;
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
                    await _userManager.AddToRoleAsync(user, userDTO.RoleName);

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
            ViewBag.users = (await _userRepo.GetAllUsersAsync()).ToSelectListItem(u => u.UserName, u => u.UserCode.ToString());
            //var userLoginDTOs = new UserLoginDTO();
            //userLoginDTOs.users = _mapper.Map<List<UserDTO>>(users);
            return View();
        }
        //Get Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserLoginDTO userDTO)
        {
            ViewBag.users = (await _userRepo.GetAllUsersAsync()).ToSelectListItem(u => u.UserName, u => u.UserCode.ToString());

            if (!ModelState.IsValid)
            {

                return View(userDTO);
            }
            AppUser user = await _userRepo.GetUserByIdAsync(userDTO.UserCode);
            bool checkkPassword = await _userManager.CheckPasswordAsync(user, userDTO.PassWord);
            if (user != null && checkkPassword)
            {
                Branch branch = await _userRepo.GetUserBranchAsync(Convert.ToInt32(user.BranchId));
                var claims = new List<Claim>
                    {
                        new ("F_Year", user.FYear.ToString()),
                        new ("U_Branch_Name", branch.BranchDesc),
                        new ("U_Branch_Id", branch.BranchId.ToString()),
                        new ("User_Code", user.UserCode.ToString()),
                    };
                await _signInManager.SignInWithClaimsAsync(user, userDTO.rememberMe, claims);

                if (User.IsInRole("Admin"))
                {
                    return RedirectToAction("Index", "Dashboard");
                }
                else
                    return RedirectToAction("Index", "Home");

            }
            else
            {

                return View();
            }
        }
        public async Task<IActionResult> Logout()
        {
            //await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

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
            if (ModelState.IsValid)
            {
                int currentYear = DateTime.UtcNow.Year;
                if (year >= 2020 && year <= currentYear)
                {
                    string UsrId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                    var user = await _userManager.FindByIdAsync(UsrId);
                    if (user != null)
                    {
                        user.FYear = year;
                        await _userManager.UpdateAsync(user);

                        await _signInManager.SignOutAsync();
                        return RedirectToAction("Login");
                    }
                }
                else
                    ModelState.AddModelError("", $"Please, enter a year between 2020 : {currentYear}");
            }
            else
                ModelState.AddModelError("", "Please, enter the financial year");
            return View(year);
        }
    }
}
