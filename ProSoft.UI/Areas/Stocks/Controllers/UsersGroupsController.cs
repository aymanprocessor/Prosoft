using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProSoft.EF.DTOs.Stocks;
using ProSoft.EF.IRepositories.Stocks;
using ProSoft.EF.Models.Stocks;

namespace ProSoft.UI.Areas.Stocks.Controllers
{
    [Authorize]
    [Area(nameof(Stocks))]
    public class UsersGroupsController : Controller
    {
        private readonly IUsersGroupRepo _usersGroupRepo;

        public UsersGroupsController(IUsersGroupRepo usersGroupRepo)
        {
            _usersGroupRepo = usersGroupRepo;
        }

        public async Task<IActionResult> Index()
        {
            var usersGroups = await _usersGroupRepo.GetAllUsersGroupsAsync();
          
            return View(usersGroups);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(UsersGroupDTO usersGroup)
        {
            if (ModelState.IsValid)
            {
                await _usersGroupRepo.AddUsersGroupAsync(usersGroup);
                return RedirectToAction("Index");
            }
            return View(usersGroup);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var usersGroup = await _usersGroupRepo.GetUsersGroupByIdAsync(id);
            if (usersGroup == null)
            {
                return NotFound();
            }
            return View(usersGroup);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UsersGroupDTO usersGroup)
        {
            if (ModelState.IsValid)
            {
                await _usersGroupRepo.UpdateUsersGroupAsync(usersGroup);
                return RedirectToAction("Index");
            }
            return View(usersGroup);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var usersGroup = await _usersGroupRepo.GetUsersGroupByIdAsync(id);
            if (usersGroup == null)
            {
                return NotFound();
            }

            await _usersGroupRepo.DeleteUsersGroupAsync(id);
            return RedirectToAction("Index");
        }
    }
}
