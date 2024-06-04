using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProSoft.EF.DTOs.Accounts;
using ProSoft.EF.IRepositories.Accounts;

namespace ProSoft.UI.Areas.Accounts.Controllers
{
    [Authorize]
    [Area(nameof(Accounts))]
    public class UserJournalTypeController : Controller
    {
        private readonly IUserJournalTypeRepo _userJournalTypeRepo;
        public UserJournalTypeController(IUserJournalTypeRepo userJournalTypeRepo)
        {
            _userJournalTypeRepo = userJournalTypeRepo;
        }
        public async Task<IActionResult> JournalTypeForUser()
        {
            var userCode = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "User_Code").Value);
            List<UserJournalTypeDTO> userJournalTypeDTOs =await _userJournalTypeRepo.GetUserJournalTypesForUser(userCode);
            return View(userJournalTypeDTOs);
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
