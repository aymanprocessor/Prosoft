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
            ViewBag.MainLevel_2 = await _accMainCodeRepo.GetMainsByLevelAsync(2);
            ViewBag.MainLevel_3 = await _accMainCodeRepo.GetMainsByLevelAsync(3);
            ViewBag.MainLevel_4 = await _accMainCodeRepo.GetMainsByLevelAsync(4);
            ViewBag.MainLevel_5 = await _accMainCodeRepo.GetMainsByLevelAsync(5);
            return View();
        }
        public async Task<IActionResult> MainLevel_2(string? clickId = "")
        {
            List<AccMainCodeDTO> mainLevel_2 = await _accMainCodeRepo.GetMainsByLevelAsync(2);

            #region sidebar
            ViewBag.MainLevel_2 = await _accMainCodeRepo.GetMainsByLevelAsync(2);
            ViewBag.MainLevel_3 = await _accMainCodeRepo.GetMainsByLevelAsync(3);
            ViewBag.MainLevel_4 = await _accMainCodeRepo.GetMainsByLevelAsync(4);
            ViewBag.MainLevel_5 = await _accMainCodeRepo.GetMainsByLevelAsync(5);
            #endregion
            ViewBag.clickId = clickId;
            return View(mainLevel_2);
        }

        //Get Add
        public async Task<IActionResult> AddMainLevel_2()
        {
            string newMainCode = await _accMainCodeRepo.GetNewMain_2_Async();

            #region sidebar
            ViewBag.MainLevel_2 = await _accMainCodeRepo.GetMainsByLevelAsync(2);
            ViewBag.MainLevel_3 = await _accMainCodeRepo.GetMainsByLevelAsync(3);
            ViewBag.MainLevel_4 = await _accMainCodeRepo.GetMainsByLevelAsync(4);
            ViewBag.MainLevel_5 = await _accMainCodeRepo.GetMainsByLevelAsync(5);
            #endregion

            ViewBag.maincode = newMainCode;
            return View();
        }

        //Post Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddMainLevel_2(AccMainCodeEditAddDTO mainDTO)
        {
            if (ModelState.IsValid)
            {
                await _accMainCodeRepo.AddMainLevel_2_Async(mainDTO);
                return RedirectToAction(nameof(MainLevel_2));
            }
            return View(mainDTO);
        }
        //Get Edit
        public async Task<IActionResult> EditMainLevel_2(string id)
        {
            AccMainCodeDTO existingMainCode = await _accMainCodeRepo.GetMainByIdAsync(id);
            AccMainCodeEditAddDTO mappedMainDTO = _mapper.Map<AccMainCodeEditAddDTO>(existingMainCode);

            #region sidebar
            ViewBag.MainLevel_2 = await _accMainCodeRepo.GetMainsByLevelAsync(2);
            ViewBag.MainLevel_3 = await _accMainCodeRepo.GetMainsByLevelAsync(3);
            ViewBag.MainLevel_4 = await _accMainCodeRepo.GetMainsByLevelAsync(4);
            ViewBag.MainLevel_5 = await _accMainCodeRepo.GetMainsByLevelAsync(5);
            #endregion

            return View(mappedMainDTO);
        }

        //Post Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditMainLevel_2(string id, AccMainCodeEditAddDTO mainDTO)
        {
            if (ModelState.IsValid)
            {
                await _accMainCodeRepo.EditMainLevelAsync(id, mainDTO);
                return RedirectToAction(nameof(MainLevel_2));
            }
            return View(mainDTO);
        }

        //Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteMainLevel_2(string id)
        {
            await _accMainCodeRepo.DeleteMainLevelAsync(id);
            return RedirectToAction(nameof(MainLevel_2));
        }

        //////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////
        public async Task<IActionResult> MainLevel_3(string? id ="", string? clickId = "")
        {
            List<AccMainCodeDTO> mainLevel_3 = await _accMainCodeRepo.GetMainsByLevelAsync(3,id);

            #region sidebar
            ViewBag.MainLevel_2 = await _accMainCodeRepo.GetMainsByLevelAsync(2);
            ViewBag.MainLevel_3 = await _accMainCodeRepo.GetMainsByLevelAsync(3, id);
            ViewBag.MainLevel_4 = await _accMainCodeRepo.GetMainsByLevelAsync(4);
            ViewBag.MainLevel_5 = await _accMainCodeRepo.GetMainsByLevelAsync(5);
            #endregion
            ViewBag.main = await _accMainCodeRepo.GetMainByIdAsync(id);
            ViewBag.parent = id;
            ViewBag.clickId = clickId;

            return View(mainLevel_3);
        }

        //Get Add
        public async Task<IActionResult> AddMainLevel_3(string parent)
        {
            string newMainCode = await _accMainCodeRepo.GetNewMain_3_Async(parent);

            #region sidebar
            ViewBag.MainLevel_2 = await _accMainCodeRepo.GetMainsByLevelAsync(2);
            ViewBag.MainLevel_3 = await _accMainCodeRepo.GetMainsByLevelAsync(3);
            ViewBag.MainLevel_4 = await _accMainCodeRepo.GetMainsByLevelAsync(4);
            ViewBag.MainLevel_5 = await _accMainCodeRepo.GetMainsByLevelAsync(5);
            #endregion

            ViewBag.maincode = newMainCode;
            ViewBag.CurrentName = (await _accMainCodeRepo.GetMainByIdAsync(parent)).MainName;//for name

            return View();
        }

        //Post Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddMainLevel_3(AccMainCodeEditAddDTO mainDTO)
        {
            if (ModelState.IsValid)
            {
                await _accMainCodeRepo.AddMainLevel_3_Async(mainDTO);
                return RedirectToAction(nameof(MainLevel_3), new { id = mainDTO.MainCode});
            }
            return View(mainDTO);
        }
        //Get Edit
        public async Task<IActionResult> EditMainLevel_3(string id)
        {
            AccMainCodeDTO existingMainCode = await _accMainCodeRepo.GetMainByIdAsync(id);
            AccMainCodeEditAddDTO mappedMainDTO = _mapper.Map<AccMainCodeEditAddDTO>(existingMainCode);

            #region sidebar
            ViewBag.MainLevel_2 = await _accMainCodeRepo.GetMainsByLevelAsync(2);
            ViewBag.MainLevel_3 = await _accMainCodeRepo.GetMainsByLevelAsync(3);
            ViewBag.MainLevel_4 = await _accMainCodeRepo.GetMainsByLevelAsync(4);
            ViewBag.MainLevel_5 = await _accMainCodeRepo.GetMainsByLevelAsync(5);
            #endregion

            return View(mappedMainDTO);
        }

        //Post Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditMainLevel_3(string id, AccMainCodeEditAddDTO mainDTO)
        {
            if (ModelState.IsValid)
            {
                await _accMainCodeRepo.EditMainLevelAsync(id, mainDTO);
                return RedirectToAction(nameof(MainLevel_3), new { id = id });
            }
            return View(mainDTO);
        }

        //Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteMainLevel_3(string id)
        {
            await _accMainCodeRepo.DeleteMainLevelAsync(id);
            return RedirectToAction(nameof(MainLevel_2));
        }

        //////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////

        public async Task<IActionResult> MainLevel_4(string id, string? clickId = "")
        {
            List<AccMainCodeDTO> mainLevel_4 = await _accMainCodeRepo.GetMainsByLevelAsync(4);

            #region sidebar
            ViewBag.MainLevel_2 = await _accMainCodeRepo.GetMainsByLevelAsync(2);
            ViewBag.MainLevel_3 = await _accMainCodeRepo.GetMainsByLevelAsync(3);
            ViewBag.MainLevel_4 = await _accMainCodeRepo.GetMainsByLevelAsync(4);
            ViewBag.MainLevel_5 = await _accMainCodeRepo.GetMainsByLevelAsync(5);
            #endregion

            ViewBag.main = await _accMainCodeRepo.GetMainByIdAsync(id);

            string mainString = id.Substring(0, 3); //get main code and substring first 3 digit
            List<AccMainCodeDTO> mains = mainLevel_4
                .Where(obj => obj.MainCode.Substring(0, 3) == mainString).ToList();
            ViewBag.clickId = clickId;
            return View(mains);
        }

        //get Add
        public async Task<IActionResult> AddMainLevel_4(string id)
        {
            string newMainCode = await _accMainCodeRepo.GetNewMain_4_Async(id);

            #region sidebar 
            ViewBag.MainLevel_2 = await _accMainCodeRepo.GetMainsByLevelAsync(2);
            ViewBag.MainLevel_3 = await _accMainCodeRepo.GetMainsByLevelAsync(3);
            ViewBag.MainLevel_4 = await _accMainCodeRepo.GetMainsByLevelAsync(4);
            ViewBag.MainLevel_5 = await _accMainCodeRepo.GetMainsByLevelAsync(5);
            #endregion
            ViewBag.parent = id;
            ViewBag.maincode = newMainCode; //display code
            ViewBag.CurrentName = (await _accMainCodeRepo.GetMainByIdAsync(id)).MainName;//for name

            return View();
        }

        //post Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddMainLevel_4(string id, AccMainCodeEditAddDTO mainDTO)
        {
            if (ModelState.IsValid)
            {
                await _accMainCodeRepo.AddMainLevel_4_Async(id, mainDTO);
                return RedirectToAction(nameof(MainLevel_4), new { id });
            }
            return View(mainDTO);
        }


        //Get Edit
        public async Task<IActionResult> EditMainLevel_4(string id)
        {
            AccMainCodeDTO existingMainCode = await _accMainCodeRepo.GetMainByIdAsync(id);
            AccMainCodeEditAddDTO mappedMainDTO = _mapper.Map<AccMainCodeEditAddDTO>(existingMainCode);

            #region sidebar 
            ViewBag.MainLevel_2 = await _accMainCodeRepo.GetMainsByLevelAsync(2);
            ViewBag.MainLevel_3 = await _accMainCodeRepo.GetMainsByLevelAsync(3);
            ViewBag.MainLevel_4 = await _accMainCodeRepo.GetMainsByLevelAsync(4);
            ViewBag.MainLevel_5 = await _accMainCodeRepo.GetMainsByLevelAsync(5);
            #endregion
            ViewBag.parent = existingMainCode.ParentCode;

            return View(mappedMainDTO);
        }

        //Post Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditMainLevel_4(string id, AccMainCodeEditAddDTO mainDTO)
        {
            if (ModelState.IsValid)
            {
                string parentCode = await _accMainCodeRepo.GetParentCodeAsync(id);
                await _accMainCodeRepo.EditMainLevelAsync(id, mainDTO);
                return RedirectToAction(nameof(MainLevel_4), new { id = parentCode });
            }
            return View(mainDTO);
        }

        //Post Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteMainLevel_4(string id)
        {
            string parentCode = await _accMainCodeRepo.GetParentCodeAsync(id);
            await _accMainCodeRepo.DeleteMainLevelAsync(id);
            return RedirectToAction(nameof(MainLevel_4), new { id = parentCode });
        }

        //////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////

        public async Task<IActionResult> MainLevel_5(string id,string? clickId = "")
        {
            List<AccMainCodeDTO> mainLevel_5 = await _accMainCodeRepo.GetMainsByLevelAsync(5);

            #region sidebar
            ViewBag.MainLevel_2 = await _accMainCodeRepo.GetMainsByLevelAsync(2);
            ViewBag.MainLevel_3 = await _accMainCodeRepo.GetMainsByLevelAsync(3);
            ViewBag.MainLevel_4 = await _accMainCodeRepo.GetMainsByLevelAsync(4);
            ViewBag.MainLevel_5 = await _accMainCodeRepo.GetMainsByLevelAsync(5);
            #endregion

            ViewBag.main = await _accMainCodeRepo.GetMainByIdAsync(id);

            string mainString = id.Substring(0, 5); //get main code and substring first 3 digit
            List<AccMainCodeDTO> mains = mainLevel_5
                .Where(obj => obj.MainCode.Substring(0, 5) == mainString).ToList();
            ViewBag.clickId = clickId;

            return View(mains);
        }
        //get Add
        public async Task<IActionResult> AddMainLevel_5(string id)
        {
            string newMainCode = await _accMainCodeRepo.GetNewMain_5_Async(id);

            #region sidebar 
            ViewBag.MainLevel_2 = await _accMainCodeRepo.GetMainsByLevelAsync(2);
            ViewBag.MainLevel_3 = await _accMainCodeRepo.GetMainsByLevelAsync(3);
            ViewBag.MainLevel_4 = await _accMainCodeRepo.GetMainsByLevelAsync(4);
            ViewBag.MainLevel_5 = await _accMainCodeRepo.GetMainsByLevelAsync(5);
            #endregion
            ViewBag.parent = id;
            ViewBag.maincode = newMainCode; //display code
            ViewBag.CurrentName = (await _accMainCodeRepo.GetMainByIdAsync(id)).MainName;//for name

            return View();
        }

        //post Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddMainLevel_5(string id, AccMainCodeEditAddDTO mainDTO)
        {
            if (ModelState.IsValid)
            {
                await _accMainCodeRepo.AddMainLevel_5_Async(id, mainDTO);
                return RedirectToAction(nameof(MainLevel_5), new { id });
            }
            return View(mainDTO);
        }
        //Get Edit
        public async Task<IActionResult> EditMainLevel_5(string id)
        {
            AccMainCodeDTO existingMainCode = await _accMainCodeRepo.GetMainByIdAsync(id);
            AccMainCodeEditAddDTO mappedMainDTO = _mapper.Map<AccMainCodeEditAddDTO>(existingMainCode);

            #region sidebar 
            ViewBag.MainLevel_2 = await _accMainCodeRepo.GetMainsByLevelAsync(2);
            ViewBag.MainLevel_3 = await _accMainCodeRepo.GetMainsByLevelAsync(3);
            ViewBag.MainLevel_4 = await _accMainCodeRepo.GetMainsByLevelAsync(4);
            ViewBag.MainLevel_5 = await _accMainCodeRepo.GetMainsByLevelAsync(5);
            #endregion
            ViewBag.parent = existingMainCode.ParentCode;

            return View(mappedMainDTO);
        }

        //Post Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditMainLevel_5(string id, AccMainCodeEditAddDTO mainDTO)
        {
            if (ModelState.IsValid)
            {
                string parentCode = await _accMainCodeRepo.GetParentCodeAsync(id);
                await _accMainCodeRepo.EditMainLevelAsync(id, mainDTO);
                return RedirectToAction(nameof(MainLevel_5), new { id = parentCode });
            }
            return View(mainDTO);
        }

        //Post Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteMainLevel_5(string id)
        {
            string parentCode = await _accMainCodeRepo.GetParentCodeAsync(id);
            await _accMainCodeRepo.DeleteMainLevelAsync(id);
            return RedirectToAction(nameof(MainLevel_5), new { id = parentCode });
        }

        //////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////

        public async Task<IActionResult> MainLevel_6(string id ,string? clickId = "")
        {
            List<AccMainCodeDTO> mainLevel_6 = await _accMainCodeRepo.GetMainsByLevelAsync(6);

            #region sidebar
            ViewBag.MainLevel_2 = await _accMainCodeRepo.GetMainsByLevelAsync(2);
            ViewBag.MainLevel_3 = await _accMainCodeRepo.GetMainsByLevelAsync(3);
            ViewBag.MainLevel_4 = await _accMainCodeRepo.GetMainsByLevelAsync(4);
            ViewBag.MainLevel_5 = await _accMainCodeRepo.GetMainsByLevelAsync(5);
            #endregion
            
            ViewBag.main = await _accMainCodeRepo.GetMainByIdAsync(id);

            string mainString = id.Substring(0, 7); //get main code and substring first 3 digit
            List<AccMainCodeDTO> mains = mainLevel_6
                .Where(obj => obj.MainCode.Substring(0, 7) == mainString).ToList();
            ViewBag.clickId = clickId;
            return View(mains);
        }
        //get Add
        public async Task<IActionResult> AddMainLevel_6(string id)
        {
            string newMainCode = await _accMainCodeRepo.GetNewMain_6_Async(id);

            #region sidebar 
            ViewBag.MainLevel_2 = await _accMainCodeRepo.GetMainsByLevelAsync(2);
            ViewBag.MainLevel_3 = await _accMainCodeRepo.GetMainsByLevelAsync(3);
            ViewBag.MainLevel_4 = await _accMainCodeRepo.GetMainsByLevelAsync(4);
            ViewBag.MainLevel_5 = await _accMainCodeRepo.GetMainsByLevelAsync(5);
            #endregion
            ViewBag.parent = id;
            ViewBag.maincode = newMainCode; //display code
            ViewBag.CurrentName = (await _accMainCodeRepo.GetMainByIdAsync(id)).MainName;//for name

            return View();
        }

        //post Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddMainLevel_6(string id, AccMainCodeEditAddDTO mainDTO)
        {
            if (ModelState.IsValid)
            {
                await _accMainCodeRepo.AddMainLevel_6_Async(id, mainDTO);
                return RedirectToAction(nameof(MainLevel_6), new { id });
            }
            return View(mainDTO);
        }

        //Get Edit
        public async Task<IActionResult> EditMainLevel_6(string id)
        {
            AccMainCodeDTO existingMainCode = await _accMainCodeRepo.GetMainByIdAsync(id);
            AccMainCodeEditAddDTO mappedMainDTO = _mapper.Map<AccMainCodeEditAddDTO>(existingMainCode);

            #region sidebar 
            ViewBag.MainLevel_2 = await _accMainCodeRepo.GetMainsByLevelAsync(2);
            ViewBag.MainLevel_3 = await _accMainCodeRepo.GetMainsByLevelAsync(3);
            ViewBag.MainLevel_4 = await _accMainCodeRepo.GetMainsByLevelAsync(4);
            ViewBag.MainLevel_5 = await _accMainCodeRepo.GetMainsByLevelAsync(5);
            #endregion
            ViewBag.parent = existingMainCode.ParentCode;

            return View(mappedMainDTO);
        }

        //Post Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditMainLevel_6(string id, AccMainCodeEditAddDTO mainDTO)
        {
            if (ModelState.IsValid)
            {
                string parentCode = await _accMainCodeRepo.GetParentCodeAsync(id);
                await _accMainCodeRepo.EditMainLevelAsync(id, mainDTO);
                return RedirectToAction(nameof(MainLevel_6), new { id = parentCode });
            }
            return View(mainDTO);
        }

        //Post Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteMainLevel_6(string id)
        {
            string parentCode = await _accMainCodeRepo.GetParentCodeAsync(id);
            await _accMainCodeRepo.DeleteMainLevelAsync(id);
            return RedirectToAction(nameof(MainLevel_6), new { id = parentCode });
        }
        //////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////
    }
}
