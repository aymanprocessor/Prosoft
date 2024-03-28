using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProSoft.EF.DTOs.Medical.Analysis;
using ProSoft.EF.IRepositories.Medical.Analysis;
using ProSoft.EF.Models.Medical.Analysis;

namespace ProSoft.UI.Areas.Medical.Controllers
{
    [Authorize]
    [Area("Medical")]
    public class MainAnalysisController : Controller
    {
        private readonly IMainRepo _mainRepo;
        private readonly IMapper _mapper;
        public MainAnalysisController(IMapper mapper, IMainRepo mainRep)
        {
            _mainRepo = mainRep;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.MainLevel_3= await _mainRepo.GetMainsByLevelAsync(3);
            ViewBag.MainLevel_4= await _mainRepo.GetMainsByLevelAsync(4);
            ViewBag.MainLevel_5= await _mainRepo.GetMainsByLevelAsync(5);
            return View();
        }

        public async Task<IActionResult> MainLevel_3()
        {
            List<MainViewDTO> mainLevel_3 = await _mainRepo.GetMainsByLevelAsync(3);

            #region sidebar
            ViewBag.MainLevel_3 = await _mainRepo.GetMainsByLevelAsync(3);
            ViewBag.MainLevel_4 = await _mainRepo.GetMainsByLevelAsync(4);
            ViewBag.MainLevel_5 = await _mainRepo.GetMainsByLevelAsync(5); 
            #endregion

            return View(mainLevel_3);
        }

        //Get Add
        public async Task<IActionResult> AddMainLevel_3()
        {
            string newMainCode = await _mainRepo.GetNewMain_3_Async();

            #region sidebar
            ViewBag.MainLevel_3 = await _mainRepo.GetMainsByLevelAsync(3);
            ViewBag.MainLevel_4 = await _mainRepo.GetMainsByLevelAsync(4);
            ViewBag.MainLevel_5 = await _mainRepo.GetMainsByLevelAsync(5); 
            #endregion

            ViewBag.maincode = newMainCode;
            return View();
        }

        //Post Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddMainLevel_3(MainEditAddDTO mainDTO)
        {
            if (ModelState.IsValid)
            {
                await _mainRepo.AddMainLevel_3_Async(mainDTO);
                return RedirectToAction(nameof(MainLevel_3));
            }
            return View(mainDTO);
        }

        //Get Edit
        public async Task<IActionResult> EditMainLevel_3(string id)
        {
            MainViewDTO existingMainCode = await _mainRepo.GetMainByIdAsync(id);
            MainEditAddDTO mappedMainDTO = _mapper.Map<MainEditAddDTO>(existingMainCode);

            #region sidebar
            ViewBag.MainLevel_3 = await _mainRepo.GetMainsByLevelAsync(3);
            ViewBag.MainLevel_4 = await _mainRepo.GetMainsByLevelAsync(4);
            ViewBag.MainLevel_5 = await _mainRepo.GetMainsByLevelAsync(5); 
            #endregion

            return View(mappedMainDTO);
        }

        //Post Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditMainLevel_3(string id, MainEditAddDTO mainDTO)
        {
            if (ModelState.IsValid)
            {
                await _mainRepo.EditMainLevelAsync(id, mainDTO);
                return RedirectToAction(nameof(MainLevel_3));
            }
            return View(mainDTO);
        }

        //Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteMainLevel_3(string id)
        {
            await _mainRepo.DeleteMainLevelAsync(id);
            return RedirectToAction(nameof(MainLevel_3));
        }

        /////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////

        public async Task<IActionResult> MainLevel_4(string id)
        {
            List<MainViewDTO> mainLevel_4 = await _mainRepo.GetMainsByLevelAsync(4);

            #region sidebar
            ViewBag.MainLevel_3 = await _mainRepo.GetMainsByLevelAsync(3);
            ViewBag.MainLevel_4 = await _mainRepo.GetMainsByLevelAsync(4);
            ViewBag.MainLevel_5 = await _mainRepo.GetMainsByLevelAsync(5);
            #endregion

            ViewBag.main = await _mainRepo.GetMainByIdAsync(id);

            string mainString = id.Substring(0, 3); //get main code and substring first 3 digit
            List<MainViewDTO> mains = mainLevel_4
                .Where(obj => obj.MainCode.Substring(0, 3) == mainString).ToList();
            return View(mains);
        }

        //get Add
        public async Task<IActionResult> AddMainLevel_4(string id)
        {
            string newMainCode = await _mainRepo.GetNewMain_4_Async(id);

            #region sidebar 
            ViewBag.MainLevel_3 = await _mainRepo.GetMainsByLevelAsync(3);
            ViewBag.MainLevel_4 = await _mainRepo.GetMainsByLevelAsync(4);
            ViewBag.MainLevel_5 = await _mainRepo.GetMainsByLevelAsync(5);
            #endregion

            ViewBag.maincode = newMainCode; //display code
            ViewBag.CurrentName = (await _mainRepo.GetMainByIdAsync(id)).MainName;//for name

            return View();
        }

        //post Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddMainLevel_4(string id, MainEditAddDTO mainDTO)
        {
            if (ModelState.IsValid)
            {
                await _mainRepo.AddMainLevel_4_Async(id, mainDTO);
                return RedirectToAction(nameof(MainLevel_4), new { id });
            }
            return View(mainDTO);
        }

        //Get Edit
        public async Task<IActionResult> EditMainLevel_4(string id)
        {
            MainViewDTO existingMainCode = await _mainRepo.GetMainByIdAsync(id);
            MainEditAddDTO mappedMainDTO = _mapper.Map<MainEditAddDTO>(existingMainCode);

            #region sidebar 
            ViewBag.MainLevel_3 = await _mainRepo.GetMainsByLevelAsync(3);
            ViewBag.MainLevel_4 = await _mainRepo.GetMainsByLevelAsync(4);
            ViewBag.MainLevel_5 = await _mainRepo.GetMainsByLevelAsync(5);
            #endregion

            return View(mappedMainDTO);
        }

        //Post Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditMainLevel_4(string id, MainEditAddDTO mainDTO)
        {
            if (ModelState.IsValid)
            {
                string parentCode = await _mainRepo.GetParentCodeAsync(id);
                await _mainRepo.EditMainLevelAsync(id, mainDTO);
                return RedirectToAction(nameof(MainLevel_4), new { id = parentCode });
            }
            return View(mainDTO);
        }

        //Post Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteMainLevel_4(string id)
        {
            string parentCode = await _mainRepo.GetParentCodeAsync(id);
            await _mainRepo.DeleteMainLevelAsync(id);
            return RedirectToAction(nameof(MainLevel_4), new { id = parentCode });
        }

        /////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////

        public async Task<IActionResult> MainLevel_5(string id)
        {
            List<MainViewDTO> mainLevel_5 = await _mainRepo.GetMainsByLevelAsync(5);

            #region sidebar
            ViewBag.MainLevel_3 = await _mainRepo.GetMainsByLevelAsync(3);
            ViewBag.MainLevel_4 = await _mainRepo.GetMainsByLevelAsync(4);
            ViewBag.MainLevel_5 = await _mainRepo.GetMainsByLevelAsync(5);
            #endregion

            ViewBag.main = await _mainRepo.GetMainByIdAsync(id);

            string mainString = id.Substring(0, 5); //get main code and substring first 3 digit
            List<MainViewDTO> mains = mainLevel_5
                .Where(obj => obj.MainCode.Substring(0, 5) == mainString).ToList();
            return View(mains);
        }

        //get Add
        public async Task<IActionResult> AddMainLevel_5(string id)
        {
            string newMainCode = await _mainRepo.GetNewMain_5_Async(id);

            #region sidebar 
            ViewBag.MainLevel_3 = await _mainRepo.GetMainsByLevelAsync(3);
            ViewBag.MainLevel_4 = await _mainRepo.GetMainsByLevelAsync(4);
            ViewBag.MainLevel_5 = await _mainRepo.GetMainsByLevelAsync(5);
            #endregion

            ViewBag.maincode = newMainCode; //display code
            ViewBag.CurrentName = (await _mainRepo.GetMainByIdAsync(id)).MainName;//for name

            return View();
        }

        //post Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddMainLevel_5(string id, MainEditAddDTO mainDTO)
        {
            if (ModelState.IsValid)
            {
                await _mainRepo.AddMainLevel_5_Async(id, mainDTO);
                return RedirectToAction(nameof(MainLevel_5), new { id });
            }
            return View(mainDTO);
        }

        //Get Edit
        public async Task<IActionResult> EditMainLevel_5(string id)
        {
            MainViewDTO existingMainCode = await _mainRepo.GetMainByIdAsync(id);
            MainEditAddDTO mappedMainDTO = _mapper.Map<MainEditAddDTO>(existingMainCode);

            #region sidebar 
            ViewBag.MainLevel_3 = await _mainRepo.GetMainsByLevelAsync(3);
            ViewBag.MainLevel_4 = await _mainRepo.GetMainsByLevelAsync(4);
            ViewBag.MainLevel_5 = await _mainRepo.GetMainsByLevelAsync(5);
            #endregion

            return View(mappedMainDTO);
        }

        //Post Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditMainLevel_5(string id, MainEditAddDTO mainDTO)
        {
            if (ModelState.IsValid)
            {
                string parentCode = await _mainRepo.GetParentCodeAsync(id);
                await _mainRepo.EditMainLevelAsync(id, mainDTO);
                return RedirectToAction(nameof(MainLevel_5), new { id = parentCode });
            }
            return View(mainDTO);
        }

        //Post Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteMainLevel_5(string id)
        {
            string parentCode = await _mainRepo.GetParentCodeAsync(id);
            await _mainRepo.DeleteMainLevelAsync(id);
            return RedirectToAction(nameof(MainLevel_5), new { id = parentCode });
        }

        /////////////////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////////////////

        public async Task<IActionResult> MainLevel_6(string id)
        {
            List<MainViewDTO> mainLevel_6 = await _mainRepo.GetMainsByLevelAsync(6);

            #region sidebar
            ViewBag.MainLevel_3 = await _mainRepo.GetMainsByLevelAsync(3);
            ViewBag.MainLevel_4 = await _mainRepo.GetMainsByLevelAsync(4);
            ViewBag.MainLevel_5 = await _mainRepo.GetMainsByLevelAsync(5);
            #endregion

            ViewBag.main = await _mainRepo.GetMainByIdAsync(id);

            string mainString = id.Substring(0,7); //get main code and substring first 3 digit
            List<MainViewDTO> mains = mainLevel_6
                .Where(obj => obj.MainCode.Substring(0, 7) == mainString).ToList();
            return View(mains);
        }

        //get Add
        public async Task<IActionResult> AddMainLevel_6(string id)
        {
            string newMainCode = await _mainRepo.GetNewMain_6_Async(id);

            #region sidebar 
            ViewBag.MainLevel_3 = await _mainRepo.GetMainsByLevelAsync(3);
            ViewBag.MainLevel_4 = await _mainRepo.GetMainsByLevelAsync(4);
            ViewBag.MainLevel_5 = await _mainRepo.GetMainsByLevelAsync(5);
            #endregion

            ViewBag.maincode = newMainCode; //display code
            ViewBag.CurrentName = (await _mainRepo.GetMainByIdAsync(id)).MainName;//for name

            return View();
        }

        //post Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddMainLevel_6(string id, MainEditAddDTO mainDTO)
        {
            if (ModelState.IsValid)
            {
                await _mainRepo.AddMainLevel_6_Async(id, mainDTO);
                return RedirectToAction(nameof(MainLevel_6), new { id });
            }
            return View(mainDTO);
        }

        //Get Edit
        public async Task<IActionResult> EditMainLevel_6(string id)
        {
            MainViewDTO existingMainCode = await _mainRepo.GetMainByIdAsync(id);
            MainEditAddDTO mappedMainDTO = _mapper.Map<MainEditAddDTO>(existingMainCode);

            #region sidebar 
            ViewBag.MainLevel_3 = await _mainRepo.GetMainsByLevelAsync(3);
            ViewBag.MainLevel_4 = await _mainRepo.GetMainsByLevelAsync(4);
            ViewBag.MainLevel_5 = await _mainRepo.GetMainsByLevelAsync(5);
            #endregion

            return View(mappedMainDTO);
        }

        //Post Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditMainLevel_6(string id, MainEditAddDTO mainDTO)
        {
            if (ModelState.IsValid)
            {
                string parentCode = await _mainRepo.GetParentCodeAsync(id);
                await _mainRepo.EditMainLevelAsync(id, mainDTO);
                return RedirectToAction(nameof(MainLevel_6), new { id = parentCode });
            }
            return View(mainDTO);
        }

        //Post Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteMainLevel_6(string id)
        {
            string parentCode = await _mainRepo.GetParentCodeAsync(id);
            await _mainRepo.DeleteMainLevelAsync(id);
            return RedirectToAction(nameof(MainLevel_6), new { id = parentCode });
        }
    }
}
