using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProSoft.Core.Repositories.Medical.Analysis;
using ProSoft.EF.DTOs.Medical.Analysis;
using ProSoft.EF.IRepositories.Medical.Analysis;

namespace ProSoft.UI.Areas.Medical.Controllers
{
    [Authorize]
    [Area("Medical")]
    public class ItemAnalysisController : Controller
    {
        private readonly IItemAnalysisRepo _itemRepo;
        private readonly ISubRepo _subRepo;
        private readonly IMainRepo _mainRepo;
        private readonly IMapper _mapper;
        public ItemAnalysisController(IMapper mapper, ISubRepo subRepo,
                              IMainRepo mainRepo, IItemAnalysisRepo itemRepo)
        {
            _itemRepo = itemRepo;
            _mapper = mapper;
            _subRepo = subRepo;
            _mainRepo=mainRepo;
        }
        // To Get SubAnalysis By Ajax
        public async Task<IActionResult> GetItemAnalysis(string id, string maincode)
        {
            List<ItemViewDTO> subAnalysisDTO = await _itemRepo.GetItemsBySubAsync(id, maincode);
            return Json(subAnalysisDTO);
        }
        //Get Add
        public async Task<IActionResult> AddItemAnalysis(string id ,string maincode)
        {
            ViewBag.ItemCode = await _itemRepo.GetNewItemAsync(id,maincode);
            ViewBag.maincode = maincode;
            ViewBag.subItemcode = id;
            ViewBag.grandCode = (await _mainRepo.GetParentCodeAsync(maincode)); 
            
            #region sidebar
            ViewBag.MainLevel_3 = await _mainRepo.GetMainsByLevelAsync(3);
            ViewBag.MainLevel_4 = await _mainRepo.GetMainsByLevelAsync(4);
            ViewBag.MainLevel_5 = await _mainRepo.GetMainsByLevelAsync(5);
            #endregion

            ViewBag.CurrentName = (await _subRepo.GetSubByIDsAsync(id,maincode)).SubName;
            return View();
        }

        //Post Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddItemAnalysis(string id, ItemEditAddDTO itemDTO)
        {
            if (ModelState.IsValid)
            {
                string grandCode = await _subRepo.GetParentCodeAsync(itemDTO.Subanalcode);
                await _itemRepo.AddItemAnalysisAsync(id, itemDTO);
                return RedirectToAction("MainLevel_6", "MainAnalysis", new { id = grandCode });
            }
            return View(itemDTO);
        }

        //Get Edit
        public async Task<IActionResult> EditItemAnalysis(int id,string subcode, string maincode)
        {
            ItemViewDTO itemAnalysis = await _itemRepo.GetItemByIDsAsync(id, subcode, maincode);
            ItemEditAddDTO itemAnalysisDTO = _mapper.Map<ItemEditAddDTO>(itemAnalysis);

            #region sidebar
            ViewBag.MainLevel_3 = await _mainRepo.GetMainsByLevelAsync(3);
            ViewBag.MainLevel_4 = await _mainRepo.GetMainsByLevelAsync(4);
            ViewBag.MainLevel_5 = await _mainRepo.GetMainsByLevelAsync(5);
            #endregion

            return View(itemAnalysisDTO);
        }

        //Post Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditItemAnalysis(int id ,ItemEditAddDTO itemDTO)
        {
            if (ModelState.IsValid)
            {
                string grandCode = await _subRepo.GetParentCodeAsync(itemDTO.Subanalcode);
                await _itemRepo.EditItemAnalysisAsync(id, itemDTO);
                return RedirectToAction("MainLevel_6", "MainAnalysis", new { id = grandCode });
            }
            return View(itemDTO);
        }

        // Post Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteItemAnalysis(int id,string subcode, string maincode)
        {
            string grandCode = await _subRepo.GetParentCodeAsync(maincode);
            await _itemRepo.DeleteItemAnalysisAsync(id, subcode, maincode);
            return RedirectToAction("MainLevel_6", "MainAnalysis", new { id = grandCode });
        }
    }
}
