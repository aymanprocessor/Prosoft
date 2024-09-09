using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProSoft.Core.Repositories.Stocks;
using ProSoft.EF.DTOs.Stocks;
using ProSoft.EF.IRepositories;
using ProSoft.EF.IRepositories.Medical.HospitalPatData;
using ProSoft.EF.IRepositories.Stocks;
using ProSoft.EF.Models.Stocks;

namespace ProSoft.UI.Areas.Stocks.Controllers
{
    [Authorize]
    [Area(nameof(Stocks))]
    public class UserSideController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUserSideRepo _userSideRepo;
        private readonly ISideRepo _sideRepo;
        private readonly IUserRepo _userRepo;
        private readonly IRegionRepo _regionRepo;
        private readonly IEisSectionTypeRepo _eisSectionTypeRepo;
        private readonly IUsersGroupRepo _usersGroupRepo;

        private int currentBranchId;

        public UserSideController(IMapper mapper, IUserSideRepo userSideRepo, IUsersGroupRepo usersGroupRepo,ISideRepo sideRepo,IUserRepo userRepo,IRegionRepo regionRepo,IEisSectionTypeRepo eisSectionTypeRepo)
        {
            _mapper =mapper;
            _userSideRepo = userSideRepo;
            _usersGroupRepo = usersGroupRepo;
            _sideRepo = sideRepo;
            _userRepo = userRepo;
            _regionRepo = regionRepo;
            _eisSectionTypeRepo = eisSectionTypeRepo;
           // currentBranchId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "U_Branch_Id").Value);
        }

        public async Task<IActionResult> Index(int? sideId)
        {
            List<Side> sides = await _sideRepo.GetAllAsync();
            UserSideViewDTO userSideDTO = new() { Sides = sides.Select(s => new SelectListItem { Value = s.SideId.ToString(), Text = s.SideDesc }) };
            if (sideId.HasValue)
            {
                //var userSides = await _userSideRepo.GetAllUserSidesAsync();
                //return View(userSides.Where(us => us.SIDE_ID == sideId.Value));
            }

            return View(userSideDTO);
        }

        public async Task<IActionResult> Add_UserSide(int id)
        {
            //ViewBag.BranchId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "U_Branch_Id").Value);

            var existingSide = await _sideRepo.GetByIdAsync(id);
            if (existingSide == null) return NotFound();

            ViewBag.SideName = existingSide.SideDesc;

            UserSideEditAddDTO userSideDTO = new ()
            {
                SideId = id,
                BranchId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "U_Branch_Id").Value),
                Users = _userRepo.GetAllUsersAsEnumerable(),
                UserGroups = _usersGroupRepo.GetAllAsEnumerable(),
                EisSectionTypes = _eisSectionTypeRepo.GetAllAsEnumerable(),
                Regions = _regionRepo.GetAllAsEnumerable(),
            };

   

            return View(userSideDTO);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Add_UserSide(int id,UserSideEditAddDTO model)
        {
            if (!ModelState.IsValid)
            {
                model.SideId = id;
                model.BranchId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "U_Branch_Id").Value);
                model.Users = _userRepo.GetAllUsersAsEnumerable();
                model.UserGroups = _usersGroupRepo.GetAllAsEnumerable();
                model.EisSectionTypes = _eisSectionTypeRepo.GetAllAsEnumerable();
                model.Regions = _regionRepo.GetAllAsEnumerable();

                return View(model);
            }

            UserSide userSide = new()
            {
                SideId = model.SideId,
                UserId = model.UserId,
                BranchId = model.BranchId,
                RegionId = model.RegionId,
                UserGroupId = model.UserGroupId,
                EisSectionTypeId = model.EisSectionTypeId,
                Flag = model.Flag

            };
            await _userSideRepo.AddAsync(userSide);
            await _userSideRepo.SaveChangesAsync();

            return RedirectToAction("Index");
        
        }

        public async Task<IActionResult> Edit_UserSide(int orgUserId, int sideId, int orgRegionId,int branchId)
        {
            var userSide = await _userSideRepo.GetUserSideByIdAsync(orgUserId, sideId, orgRegionId, branchId);
            if (userSide == null)
            {
                return NotFound();
            }

            var side = await _sideRepo.GetByIdAsync(sideId);
            ViewBag.SideName = side.SideDesc;

            UserSideEditAddDTO userSideEditAddDTO = new()
            {
                SideId = userSide.SideId,
                BranchId = userSide.BranchId,
                RegionId = userSide.RegionId,
                UserGroupId = userSide.UserGroupId,
                UserId = userSide.UserId,
                EisSectionTypeId = userSide.EisSectionTypeId,
                Flag = userSide.Flag,
                Users = _userRepo.GetAllUsersAsEnumerable(),
                UserGroups = _usersGroupRepo.GetAllAsEnumerable(),
                EisSectionTypes = _eisSectionTypeRepo.GetAllAsEnumerable(),
                Regions = _regionRepo.GetAllAsEnumerable(),
            };

            return View(userSideEditAddDTO);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_UserSide(UserSideEditAddDTO model)
        {
            if (!ModelState.IsValid)
            {
                //model.SideId = userId;
                model.BranchId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "U_Branch_Id").Value);
                model.Users = _userRepo.GetAllUsersAsEnumerable();
                model.UserGroups = _usersGroupRepo.GetAllAsEnumerable();
                model.EisSectionTypes = _eisSectionTypeRepo.GetAllAsEnumerable();
                model.Regions = _regionRepo.GetAllAsEnumerable();
                return View(model);
            }
            var userSide = await _userSideRepo.GetUserSideByIdAsync(model.OrgUserId, model.SideId, model.OrgRegionId, model.BranchId);
            if (userSide == null)
            {
                return NotFound();
            }

            userSide.BranchId = model.BranchId;
            userSide.SideId = model.SideId;
            userSide.UserId = model.UserId;
            userSide.RegionId = model.RegionId;
            userSide.UserGroupId = model.UserGroupId;
            userSide.Flag = model.Flag;
            userSide.EisSectionTypeId = model.EisSectionTypeId;

            await _userSideRepo.UpdateAsync(userSide);
            await _userSideRepo.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete_UserSide(int userId, int sideId, int regionId, int branchId)
        {
            var userSide = await _userSideRepo.GetUserSideByIdAsync(userId, sideId, regionId, branchId);
            if (userSide == null)
            {
                return NotFound();
            }

            await _userSideRepo.DeleteAsync(userSide);
            await _userSideRepo.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        // Helper methods to fetch lists for dropdowns
        public async Task<IActionResult> GetUserSides(string sideId)
        {
            IEnumerable<UserSide> allData = await _userSideRepo.GetAllUserSidesAsync();

            var data = allData
                .Where(x => x.SideId == int.Parse(sideId))
                .ToList();

            return Json(data);
        }
    }
}
