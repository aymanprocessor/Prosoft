using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProSoft.Core.Repositories.Medical.Analysis;
using ProSoft.EF.DTOs.Accounts;
using ProSoft.EF.DTOs.Medical.Analysis;
using ProSoft.EF.IRepositories.Accounts;
using ProSoft.EF.IRepositories.Medical.Analysis;

namespace ProSoft.UI.Areas.Accounts.Controllers
{
    [Authorize]
    [Area(nameof(Accounts))]
    public class AccMainCodeController : Controller
    {
        private readonly IAccMainCodeRepo _accMainCodeRepo;
        private readonly IMapper _mapper;
        public AccMainCodeController(IMapper mapper, IAccMainCodeRepo accMainCodeRepo)
        {
            _accMainCodeRepo = accMainCodeRepo;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.MainLevel_3 = await _accMainCodeRepo.GetMainsByLevelAsync(3);
            ViewBag.MainLevel_4 = await _accMainCodeRepo.GetMainsByLevelAsync(4);
            ViewBag.MainLevel_5 = await _accMainCodeRepo.GetMainsByLevelAsync(5);
            return View();
        }
        public async Task<IActionResult> MainLevel_3()
        {
            List<AccMainCodeDTO> mainLevel_3 = await _accMainCodeRepo.GetMainsByLevelAsync(3);

            #region sidebar
            ViewBag.MainLevel_3 = await _accMainCodeRepo.GetMainsByLevelAsync(3);
            ViewBag.MainLevel_4 = await _accMainCodeRepo.GetMainsByLevelAsync(4);
            ViewBag.MainLevel_5 = await _accMainCodeRepo.GetMainsByLevelAsync(5);
            #endregion

            return View(mainLevel_3);
        }
        public async Task<IActionResult> MainLevel_4(string id)
        {
            List<AccMainCodeDTO> mainLevel_4 = await _accMainCodeRepo.GetMainsByLevelAsync(4);

            #region sidebar
            ViewBag.MainLevel_3 = await _accMainCodeRepo.GetMainsByLevelAsync(3);
            ViewBag.MainLevel_4 = await _accMainCodeRepo.GetMainsByLevelAsync(4);
            ViewBag.MainLevel_5 = await _accMainCodeRepo.GetMainsByLevelAsync(5);
            #endregion

            ViewBag.main = await _accMainCodeRepo.GetMainByIdAsync(id);

            string mainString = id.Substring(0, 3); //get main code and substring first 3 digit
            List<AccMainCodeDTO> mains = mainLevel_4
                .Where(obj => obj.MainCode.Substring(0, 3) == mainString).ToList();
            return View(mains);
        }
        public async Task<IActionResult> MainLevel_5(string id)
        {
            List<AccMainCodeDTO> mainLevel_5 = await _accMainCodeRepo.GetMainsByLevelAsync(5);

            #region sidebar
            ViewBag.MainLevel_3 = await _accMainCodeRepo.GetMainsByLevelAsync(3);
            ViewBag.MainLevel_4 = await _accMainCodeRepo.GetMainsByLevelAsync(4);
            ViewBag.MainLevel_5 = await _accMainCodeRepo.GetMainsByLevelAsync(5);
            #endregion

            ViewBag.main = await _accMainCodeRepo.GetMainByIdAsync(id);

            string mainString = id.Substring(0, 5); //get main code and substring first 3 digit
            List<AccMainCodeDTO> mains = mainLevel_5
                .Where(obj => obj.MainCode.Substring(0, 5) == mainString).ToList();
            return View(mains);
        }
        public async Task<IActionResult> MainLevel_6(string id)
        {
            List<AccMainCodeDTO> mainLevel_6 = await _accMainCodeRepo.GetMainsByLevelAsync(4);

            #region sidebar
            ViewBag.MainLevel_3 = await _accMainCodeRepo.GetMainsByLevelAsync(3);
            ViewBag.MainLevel_4 = await _accMainCodeRepo.GetMainsByLevelAsync(4);
            ViewBag.MainLevel_5 = await _accMainCodeRepo.GetMainsByLevelAsync(5);
            #endregion

            ViewBag.main = await _accMainCodeRepo.GetMainByIdAsync(id);

            string mainString = id.Substring(0, 7); //get main code and substring first 3 digit
            List<AccMainCodeDTO> mains = mainLevel_6
                .Where(obj => obj.MainCode.Substring(0, 7) == mainString).ToList();
            return View(mains);
        }
    }
}
