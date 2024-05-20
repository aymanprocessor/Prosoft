using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ProSoft.UI.Areas.Stocks.Controllers
{
    [Authorize]
    [Area(nameof(Stocks))]
    public class PermissionFormController : Controller
    {
        public PermissionFormController()
        {

        }

        public async Task<IActionResult> Index()
        {
            var userCode = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "User_Code").Value);
            //List<StoreTran> transTypes = await _transTypeRepo.GetAllAsync();
            //ViewBag.transTypes = _mapper.Map<List<StoreTransDTO>>(transTypes);
            //List<AppUser> users = await _userRepo.GetAllUsersAsync();
            //List<UserDTO> usersDTO = _mapper.Map<List<UserDTO>>(users);
            return View();
        }
    }
}
