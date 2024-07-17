using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProSoft.EF.DTOs.Stocks;
using ProSoft.EF.IRepositories.Stocks;

namespace ProSoft.UI.Areas.Stocks.Controllers
{
    [Authorize]
    [Area(nameof(Stocks))]
    public class SubItemController : Controller
    {
        private readonly ISubItemRepo _subItemRepo;
        //private readonly IMapper _mapper;
        public SubItemController(ISubItemRepo subItemRepo/*, IMapper mapper*/)
        {
            _subItemRepo = subItemRepo;
            //_mapper = mapper;
        }

        public async Task<IActionResult> GetSubItemsByMainId(int id)
        {
            List<SubItemViewDTO> subItemsDTO = await _subItemRepo.GetSubItemsByMainIdAsync(id);
            return Json(subItemsDTO);
        }

        //Get Add
        public async Task<IActionResult> Add_SubItem(int id)
        {
            /*MainItemEditAddDTO*/var subItemDTO = await _subItemRepo.GetAllAsync();
            return View(subItemDTO);
        }

        //Post Add
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Add_SubItem(MainItemEditAddDTO mainItemDTO)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        mainItemDTO.BranchId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "U_Branch_Id").Value);
        //        mainItemDTO.UserCode = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "User_Code").Value);

        //        await _mainItemRepo.AddMainLevelAsync(mainItemDTO);
        //        return RedirectToAction(nameof(MainLevel_6), new { id = mainItemDTO.ParentCode, flag1 = mainItemDTO.Flag1 });
        //    }
        //    KindStore stockType = await _stockTypeRepo.GetByIdAsync((int)mainItemDTO.Flag1);
        //    ViewBag.stockType = stockType.KName;
        //    #region sidebar
        //    ViewBag.MainLevel_2 = await _mainItemRepo.GetMainItemsByLevelAsync(2, (int)mainItemDTO.Flag1);
        //    ViewBag.MainLevel_3 = await _mainItemRepo.GetMainItemsByLevelAsync(3, (int)mainItemDTO.Flag1);
        //    ViewBag.MainLevel_4 = await _mainItemRepo.GetMainItemsByLevelAsync(4, (int)mainItemDTO.Flag1);
        //    ViewBag.MainLevel_5 = await _mainItemRepo.GetMainItemsByLevelAsync(5, (int)mainItemDTO.Flag1);
        //    ViewBag.MainLevel_6 = await _mainItemRepo.GetMainItemsByLevelAsync(6, (int)mainItemDTO.Flag1);
        //    #endregion
        //    MainItemEditAddDTO newMainLevel_6 = await _mainItemRepo
        //        .GetNewMainLevel_6_Async(mainItemDTO.ParentCode, (int)mainItemDTO.Flag1);
        //    _mapper.Map(newMainLevel_6, mainItemDTO);
        //    return View(mainItemDTO);
        //}

        //Get Edit
        //public async Task<IActionResult> Edit_MainLevel_6(int id)
        //{
        //    MainItemEditAddDTO mainLevel_6 = await _mainItemRepo.GetMainLevelByIdAsync(id);
        //    KindStore stockType = await _stockTypeRepo.GetByIdAsync((int)mainLevel_6.Flag1);
        //    ViewBag.stockType = stockType.KName;
        //    #region sidebar
        //    ViewBag.MainLevel_2 = await _mainItemRepo.GetMainItemsByLevelAsync(2, (int)mainLevel_6.Flag1);
        //    ViewBag.MainLevel_3 = await _mainItemRepo.GetMainItemsByLevelAsync(3, (int)mainLevel_6.Flag1);
        //    ViewBag.MainLevel_4 = await _mainItemRepo.GetMainItemsByLevelAsync(4, (int)mainLevel_6.Flag1);
        //    ViewBag.MainLevel_5 = await _mainItemRepo.GetMainItemsByLevelAsync(5, (int)mainLevel_6.Flag1);
        //    ViewBag.MainLevel_6 = await _mainItemRepo.GetMainItemsByLevelAsync(6, (int)mainLevel_6.Flag1);
        //    #endregion
        //    return View(mainLevel_6);
        //}

        //Post Edit
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit_MainLevel_6(int id, MainItemEditAddDTO mainItemDTO)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        mainItemDTO.BranchId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "U_Branch_Id").Value);
        //        mainItemDTO.UserCode = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "User_Code").Value);

        //        await _mainItemRepo.EditMainLevelAsync(id, mainItemDTO);
        //        return RedirectToAction(nameof(MainLevel_6), new { id = mainItemDTO.ParentCode, flag1 = mainItemDTO.Flag1 });
        //    }
        //    KindStore stockType = await _stockTypeRepo.GetByIdAsync((int)mainItemDTO.Flag1);
        //    ViewBag.stockType = stockType.KName;
        //    #region sidebar
        //    ViewBag.MainLevel_2 = await _mainItemRepo.GetMainItemsByLevelAsync(2, (int)mainItemDTO.Flag1);
        //    ViewBag.MainLevel_3 = await _mainItemRepo.GetMainItemsByLevelAsync(3, (int)mainItemDTO.Flag1);
        //    ViewBag.MainLevel_4 = await _mainItemRepo.GetMainItemsByLevelAsync(4, (int)mainItemDTO.Flag1);
        //    ViewBag.MainLevel_5 = await _mainItemRepo.GetMainItemsByLevelAsync(5, (int)mainItemDTO.Flag1);
        //    ViewBag.MainLevel_6 = await _mainItemRepo.GetMainItemsByLevelAsync(6, (int)mainItemDTO.Flag1);
        //    #endregion
        //    MainItemEditAddDTO mainLevel_6 = await _mainItemRepo.GetMainLevelByIdAsync(id);
        //    _mapper.Map(mainLevel_6, mainItemDTO);
        //    return View(mainItemDTO);
        //}

        //Delete
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Delete_MainLevel_6(int id)
        //{
        //    MainItem mainItem = await _mainItemRepo.GetByIdAsync(id);

        //    await _mainItemRepo.DeleteAsync(mainItem);
        //    await _mainItemRepo.SaveChangesAsync();
        //    return RedirectToAction(nameof(MainLevel_6), new { id = mainItem.ParentCode, flag1 = mainItem.Flag1 });
        //}
    }
}
