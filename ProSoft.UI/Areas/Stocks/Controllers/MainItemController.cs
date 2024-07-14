using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProSoft.EF.DTOs.Accounts;
using ProSoft.EF.DTOs.Stocks;
using ProSoft.EF.IRepositories.Accounts;
using ProSoft.EF.IRepositories.Stocks;
using ProSoft.EF.Models.Accounts;

namespace ProSoft.UI.Areas.Stocks.Controllers
{
    [Authorize]
    [Area(nameof(Stocks))]
    public class MainItemController : Controller
    {
        private readonly IMainItemRepo _mainItemRepo;
        private readonly ICostCenterRepo _costCenterRepo;
        private readonly IMapper _mapper;
        public MainItemController(IMainItemRepo mainItemRepo, ICostCenterRepo costCenterRepo, IMapper mapper)
        {
            _mainItemRepo = mainItemRepo;
            _costCenterRepo = costCenterRepo;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.MainLevel_2 = await _mainItemRepo.GetMainItemsByLevelAsync(2);
            ViewBag.MainLevel_3 = await _mainItemRepo.GetMainItemsByLevelAsync(3);
            ViewBag.MainLevel_4 = await _mainItemRepo.GetMainItemsByLevelAsync(4);
            return View();
        }

        public async Task<IActionResult> MainLevel_2()
        {
            List<MainItemViewDTO> mainLevel_2 = await _mainItemRepo.GetMainItemsByLevelAsync(2);

            #region sidebar
            ViewBag.MainLevel_2 = await _mainItemRepo.GetMainItemsByLevelAsync(2);
            ViewBag.MainLevel_3 = await _mainItemRepo.GetMainItemsByLevelAsync(3);
            ViewBag.MainLevel_4 = await _mainItemRepo.GetMainItemsByLevelAsync(4);
            #endregion
            
            return View(mainLevel_2);
        }

        public async Task<IActionResult> Add_MainLevel_2()
        {
            MainItemEditAddDTO newMainLevel_2 = await _mainItemRepo.GetNewMainLevel_2_Async();
            #region sidebar
            ViewBag.MainLevel_2 = await _mainItemRepo.GetMainItemsByLevelAsync(2);
            ViewBag.MainLevel_3 = await _mainItemRepo.GetMainItemsByLevelAsync(3);
            ViewBag.MainLevel_4 = await _mainItemRepo.GetMainItemsByLevelAsync(4);
            #endregion
            return View(newMainLevel_2);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add_MainLevel_2(MainItemEditAddDTO mainItemDTO)
        {
            if (ModelState.IsValid)
            {
                mainItemDTO.BranchId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "U_Branch_Id").Value);
                mainItemDTO.UserCode = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "User_Code").Value);

                await _mainItemRepo.AddMainLevel_2_Async(mainItemDTO);
                return RedirectToAction(nameof(MainLevel_2));
            }
            #region sidebar
            ViewBag.MainLevel_2 = await _mainItemRepo.GetMainItemsByLevelAsync(2);
            ViewBag.MainLevel_3 = await _mainItemRepo.GetMainItemsByLevelAsync(3);
            ViewBag.MainLevel_4 = await _mainItemRepo.GetMainItemsByLevelAsync(4);
            #endregion
            List<CostCenter> costCenters = await _costCenterRepo.GetAllAsync();
            mainItemDTO.CostCenters = _mapper.Map<List<CostCenterViewDTO>>(costCenters);
            return View(mainItemDTO);
        }
    }
}
