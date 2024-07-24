using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProSoft.EF.DTOs.Stocks;
using ProSoft.EF.IRepositories.Stocks;
using ProSoft.EF.Models.Stocks;

namespace ProSoft.UI.Areas.Stocks.Controllers
{
    [Authorize]
    [Area(nameof(Stocks))]
    public class SubItemDtlController : Controller
    {
        private readonly ISubItemDtlRepo _subItemDtlRepo;
        private readonly IMainItemRepo _mainItemRepo;
        private readonly IMapper _mapper;
        public SubItemDtlController(ISubItemDtlRepo subItemDtlRepo, IMainItemRepo mainItemRepo, IMapper mapper)
        {
            _subItemDtlRepo = subItemDtlRepo;
            _mainItemRepo = mainItemRepo;
            _mapper = mapper;
        }

        public async Task<IActionResult> GetSubItemDetails(int id)
        {
            List<SubItemDtlDTO> subItemDtlsDTO = await _subItemDtlRepo.GetSubItemDetailsAsync(id);
            return Json(subItemDtlsDTO);
        }

        //Get Add
        public async Task<IActionResult> Add_SubItemDtl(int id, int flag1)
        {
            SubItemDtlDTO subItemDtlDTO = await _subItemDtlRepo.GetNewSubItemDtlAsync(id);
            #region sidebar
            ViewBag.MainLevel_2 = await _mainItemRepo.GetMainItemsByLevelAsync(2, flag1);
            ViewBag.MainLevel_3 = await _mainItemRepo.GetMainItemsByLevelAsync(3, flag1);
            ViewBag.MainLevel_4 = await _mainItemRepo.GetMainItemsByLevelAsync(4, flag1);
            ViewBag.MainLevel_5 = await _mainItemRepo.GetMainItemsByLevelAsync(5, flag1);
            ViewBag.MainLevel_6 = await _mainItemRepo.GetMainItemsByLevelAsync(6, flag1);
            #endregion
            return View(subItemDtlDTO);
        }

        //Post Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add_SubItemDtl(int id, SubItemDtlDTO subItemDtlDTO)
        {
            if (ModelState.IsValid)
            {
                subItemDtlDTO.BranchId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "U_Branch_Id").Value);

                await _subItemDtlRepo.AddSubItemDtlAsync(id, subItemDtlDTO);
                return RedirectToAction(subItemDtlDTO.MainLevel, "MainItem",
                    new { id = subItemDtlDTO.ParentCode, flag1 = subItemDtlDTO.Flag1 });
            }
            #region sidebar
            ViewBag.MainLevel_2 = await _mainItemRepo.GetMainItemsByLevelAsync(2, (int)subItemDtlDTO.Flag1);
            ViewBag.MainLevel_3 = await _mainItemRepo.GetMainItemsByLevelAsync(3, (int)subItemDtlDTO.Flag1);
            ViewBag.MainLevel_4 = await _mainItemRepo.GetMainItemsByLevelAsync(4, (int)subItemDtlDTO.Flag1);
            ViewBag.MainLevel_5 = await _mainItemRepo.GetMainItemsByLevelAsync(5, (int)subItemDtlDTO.Flag1);
            ViewBag.MainLevel_6 = await _mainItemRepo.GetMainItemsByLevelAsync(6, (int)subItemDtlDTO.Flag1);
            #endregion
            SubItemDtlDTO _subItemDtlDTO = await _subItemDtlRepo.GetNewSubItemDtlAsync(id);
            _mapper.Map(_subItemDtlDTO, subItemDtlDTO);
            return View(subItemDtlDTO);
        }

        //Get Edit
        public async Task<IActionResult> Edit_SubItemDtl(int id)
        {
            SubItemDtlDTO subItemDtlDTO = await _subItemDtlRepo.GetSubItemDtlByIdAsync(id);
            #region sidebar
            ViewBag.MainLevel_2 = await _mainItemRepo.GetMainItemsByLevelAsync(2, (int)subItemDtlDTO.Flag1);
            ViewBag.MainLevel_3 = await _mainItemRepo.GetMainItemsByLevelAsync(3, (int)subItemDtlDTO.Flag1);
            ViewBag.MainLevel_4 = await _mainItemRepo.GetMainItemsByLevelAsync(4, (int)subItemDtlDTO.Flag1);
            ViewBag.MainLevel_5 = await _mainItemRepo.GetMainItemsByLevelAsync(5, (int)subItemDtlDTO.Flag1);
            ViewBag.MainLevel_6 = await _mainItemRepo.GetMainItemsByLevelAsync(6, (int)subItemDtlDTO.Flag1);
            #endregion
            return View(subItemDtlDTO);
        }

        //Post Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_SubItemDtl(int id, SubItemDtlDTO subItemDtlDTO)
        {
            if (ModelState.IsValid)
            {
                subItemDtlDTO.BranchId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "U_Branch_Id").Value);

                await _subItemDtlRepo.EditSubItemDtlAsync(id, subItemDtlDTO);
                return RedirectToAction(subItemDtlDTO.MainLevel, "MainItem", new { id = subItemDtlDTO.ParentCode,
                    flag1 = subItemDtlDTO.Flag1 });
            }
            #region sidebar
            ViewBag.MainLevel_2 = await _mainItemRepo.GetMainItemsByLevelAsync(2, (int)subItemDtlDTO.Flag1);
            ViewBag.MainLevel_3 = await _mainItemRepo.GetMainItemsByLevelAsync(3, (int)subItemDtlDTO.Flag1);
            ViewBag.MainLevel_4 = await _mainItemRepo.GetMainItemsByLevelAsync(4, (int)subItemDtlDTO.Flag1);
            ViewBag.MainLevel_5 = await _mainItemRepo.GetMainItemsByLevelAsync(5, (int)subItemDtlDTO.Flag1);
            ViewBag.MainLevel_6 = await _mainItemRepo.GetMainItemsByLevelAsync(6, (int)subItemDtlDTO.Flag1);
            #endregion
            SubItemDtlDTO _subItemDtlDTO = await _subItemDtlRepo.GetSubItemDtlByIdAsync(id);
            _mapper.Map(_subItemDtlDTO, subItemDtlDTO);
            return View(subItemDtlDTO);
        }

        //Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete_SubItemDtl(int id)
        {
            SubItemDtlDTO subItemDtlDTO = await _subItemDtlRepo.GetSubItemDtlByIdAsync(id);
            SubItemDtl subItemDtl = await _subItemDtlRepo.GetByIdAsync(id);

            await _subItemDtlRepo.DeleteAsync(subItemDtl);
            await _subItemDtlRepo.SaveChangesAsync();
            return RedirectToAction(subItemDtlDTO.MainLevel, "MainItem", new { id = subItemDtlDTO.ParentCode, flag1 = subItemDtlDTO.Flag1 });
        }
    }
}
