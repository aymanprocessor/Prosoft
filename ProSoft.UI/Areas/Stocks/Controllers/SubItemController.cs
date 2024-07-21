using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProSoft.Core.Repositories.Stocks;
using ProSoft.EF.DTOs.Stocks;
using ProSoft.EF.IRepositories.Stocks;
using ProSoft.EF.Models.Stocks;

namespace ProSoft.UI.Areas.Stocks.Controllers
{
    [Authorize]
    [Area(nameof(Stocks))]
    public class SubItemController : Controller
    {
        private readonly ISubItemRepo _subItemRepo;
        private readonly IMainItemRepo _mainItemRepo;
        private readonly IMapper _mapper;
        public SubItemController(ISubItemRepo subItemRepo, IMainItemRepo mainItemRepo, IMapper mapper)
        {
            _subItemRepo = subItemRepo;
            _mainItemRepo = mainItemRepo;
            _mapper = mapper;
        }

        public async Task<IActionResult> GetSubItemsByMainId(int id)
        {
            List<SubItemViewDTO> subItemsDTO = await _subItemRepo.GetSubItemsByMainIdAsync(id);
            return Json(subItemsDTO);
        }

        //Get Add
        public async Task<IActionResult> Add_SubItem(int id, int flag1)
        {
            SubItemEditAddDTO subItemDTO = await _subItemRepo.GetNewSubItemAsync(id);
            #region sidebar
            ViewBag.MainLevel_2 = await _mainItemRepo.GetMainItemsByLevelAsync(2, flag1);
            ViewBag.MainLevel_3 = await _mainItemRepo.GetMainItemsByLevelAsync(3, flag1);
            ViewBag.MainLevel_4 = await _mainItemRepo.GetMainItemsByLevelAsync(4, flag1);
            ViewBag.MainLevel_5 = await _mainItemRepo.GetMainItemsByLevelAsync(5, flag1);
            ViewBag.MainLevel_6 = await _mainItemRepo.GetMainItemsByLevelAsync(6, flag1);
            #endregion
            return View(subItemDTO);
        }

        //Post Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add_SubItem(int id, SubItemEditAddDTO subItemDTO)
        {
            if (ModelState.IsValid)
            {
                subItemDTO.BranchId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "U_Branch_Id").Value);

                await _subItemRepo.AddSubItemAsync(id, subItemDTO);
                return RedirectToAction(subItemDTO.MainLevel, "MainItem", new { id = subItemDTO.ParentCode, flag1 = subItemDTO.Flag1 });
            }
            #region sidebar
            ViewBag.MainLevel_2 = await _mainItemRepo.GetMainItemsByLevelAsync(2, (int)subItemDTO.Flag1);
            ViewBag.MainLevel_3 = await _mainItemRepo.GetMainItemsByLevelAsync(3, (int)subItemDTO.Flag1);
            ViewBag.MainLevel_4 = await _mainItemRepo.GetMainItemsByLevelAsync(4, (int)subItemDTO.Flag1);
            ViewBag.MainLevel_5 = await _mainItemRepo.GetMainItemsByLevelAsync(5, (int)subItemDTO.Flag1);
            ViewBag.MainLevel_6 = await _mainItemRepo.GetMainItemsByLevelAsync(6, (int)subItemDTO.Flag1);
            #endregion
            SubItemEditAddDTO _subItemDTO = await _subItemRepo.GetNewSubItemAsync(id);
            _mapper.Map(_subItemDTO, subItemDTO);
            return View(subItemDTO);
        }

        //Get Edit
        public async Task<IActionResult> Edit_SubItem(int id)
        {
            SubItemEditAddDTO subItemDTO = await _subItemRepo.GetSubItemByIdAsync(id);
            #region sidebar
            ViewBag.MainLevel_2 = await _mainItemRepo.GetMainItemsByLevelAsync(2, (int)subItemDTO.Flag1);
            ViewBag.MainLevel_3 = await _mainItemRepo.GetMainItemsByLevelAsync(3, (int)subItemDTO.Flag1);
            ViewBag.MainLevel_4 = await _mainItemRepo.GetMainItemsByLevelAsync(4, (int)subItemDTO.Flag1);
            ViewBag.MainLevel_5 = await _mainItemRepo.GetMainItemsByLevelAsync(5, (int)subItemDTO.Flag1);
            ViewBag.MainLevel_6 = await _mainItemRepo.GetMainItemsByLevelAsync(6, (int)subItemDTO.Flag1);
            #endregion
            return View(subItemDTO);
        }

        //Post Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_SubItem(int id, SubItemEditAddDTO subItemDTO)
        {
            if (ModelState.IsValid)
            {
                subItemDTO.BranchId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "U_Branch_Id").Value);
                
                await _subItemRepo.EditSubItemAsync(id, subItemDTO);
                return RedirectToAction(subItemDTO.MainLevel, "MainItem", new { id = subItemDTO.ParentCode, flag1 = subItemDTO.Flag1 });
            }
            #region sidebar
            ViewBag.MainLevel_2 = await _mainItemRepo.GetMainItemsByLevelAsync(2, (int)subItemDTO.Flag1);
            ViewBag.MainLevel_3 = await _mainItemRepo.GetMainItemsByLevelAsync(3, (int)subItemDTO.Flag1);
            ViewBag.MainLevel_4 = await _mainItemRepo.GetMainItemsByLevelAsync(4, (int)subItemDTO.Flag1);
            ViewBag.MainLevel_5 = await _mainItemRepo.GetMainItemsByLevelAsync(5, (int)subItemDTO.Flag1);
            ViewBag.MainLevel_6 = await _mainItemRepo.GetMainItemsByLevelAsync(6, (int)subItemDTO.Flag1);
            #endregion
            SubItemEditAddDTO _subItemDTO = await _subItemRepo.GetSubItemByIdAsync(id);
            _mapper.Map(_subItemDTO, subItemDTO);
            return View(subItemDTO);
        }

        //Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete_SubItem(int id)
        {
            SubItemEditAddDTO subItemDTO = await _subItemRepo.GetSubItemByIdAsync(id);
            SubItem subItem = await _subItemRepo.GetByIdAsync(id);

            await _subItemRepo.DeleteAsync(subItem);
            await _subItemRepo.SaveChangesAsync();
            return RedirectToAction(subItemDTO.MainLevel, "MainItem", new { id = subItemDTO.ParentCode, flag1 = subItemDTO.Flag1 });
        }
    }
}
