using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ProSoft.EF.DTOs.Accounts;
using ProSoft.EF.IRepositories.Accounts;


namespace ProSoft.UI.Areas.Accounts.Controllers
{
    [Authorize]
    [Area(nameof(Accounts))]
    public class AccStartBalController : Controller
    {
        private readonly IUserJournalTypeRepo _userJournalTypeRepo;
        private readonly IAccStartBalRepo _accStartBalRepo;
        public AccStartBalController(IUserJournalTypeRepo userJournalTypeRepo, IAccStartBalRepo accStartBalRepo)
        {
            _userJournalTypeRepo = userJournalTypeRepo;
            _accStartBalRepo = accStartBalRepo;
        }

        public async Task<IActionResult> Index()
        {
            var userCode = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "User_Code").Value);
            List<UserJournalTypeDTO> userJournalTypeDTOs = await _userJournalTypeRepo.GetUserJournalTypesForUser(userCode);
            return View(userJournalTypeDTOs);
        }
        public async Task<IActionResult> GetAccStartBal(int id)
        {
            var fYear = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "F_Year").Value);
            var branchId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "U_Branch_Id").Value);
            List<AccStartBalViewDTO> accStartBalDTOs = await _accStartBalRepo.GetAccStartBalAsync(id, fYear, branchId);
            return Json(accStartBalDTOs);
        }

    }
}
