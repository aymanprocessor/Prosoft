using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProSoft.EF.DTOs.Accounts;
using ProSoft.EF.DTOs.Stocks;
using ProSoft.EF.IRepositories.Accounts;
using ProSoft.EF.IRepositories.Stocks;
using ProSoft.EF.Models.Accounts;
using ProSoft.EF.Models.Stocks;

namespace ProSoft.UI.Areas.Stocks.Controllers
{
    [Authorize]
    [Area(nameof(Stocks))]
    public class MainItemController : Controller
    {
        private readonly IMainItemRepo _mainItemRepo;
        private readonly ICostCenterRepo _costCenterRepo;
        private readonly IStockTypeRepo _stockTypeRepo;
        private readonly IMapper _mapper;
        private readonly IStockRepo _stockRepo;
        public MainItemController(IMainItemRepo mainItemRepo, ICostCenterRepo costCenterRepo,
            IStockTypeRepo stockTypeRepo, IMapper mapper, IStockRepo stockRepo)
        {
            _mainItemRepo = mainItemRepo;
            _costCenterRepo = costCenterRepo;
            _stockTypeRepo = stockTypeRepo;
            _mapper = mapper;
            _stockRepo = stockRepo;
        }

        public async Task<IActionResult> ChooseStockType()
        {
            List<KindStore> stockTypes = await _stockTypeRepo.GetAllAsync();
            var stockTypesDTO = _mapper.Map<List<KindStoreDTO>>(stockTypes);
            return View(stockTypesDTO);
        }

        //Get AddStock
        public async Task<IActionResult> AddStocksToGroup(int id, int flag1)
        {
            MainItemStockDTO mainItemStockDTO = await _mainItemRepo.GetNewMainItemStockAsync(id);
            #region sidebar
            ViewBag.MainLevel_2 = await _mainItemRepo.GetMainItemsByLevelAsync(2, flag1);
            ViewBag.MainLevel_3 = await _mainItemRepo.GetMainItemsByLevelAsync(3, flag1);
            ViewBag.MainLevel_4 = await _mainItemRepo.GetMainItemsByLevelAsync(4, flag1);
            ViewBag.MainLevel_5 = await _mainItemRepo.GetMainItemsByLevelAsync(5, flag1);
            ViewBag.MainLevel_6 = await _mainItemRepo.GetMainItemsByLevelAsync(6, flag1);
            #endregion
            return View(mainItemStockDTO);
        }

        //Post AddStock
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddStocksToGroup(int id, int[] stocks, MainItemStockDTO mainItemStockDTO)
        {
            List<string> messages = await _mainItemRepo.CheckIfExistsAsync(id, stocks);
            if (messages.ElementAt(0) != "1")
            {
                foreach (var error in messages)
                {
                    ModelState.AddModelError("", error);
                }
            }
            else if (ModelState.IsValid)
            {
                await _mainItemRepo.AddStocksToGroupAsync(id, stocks);
                return RedirectToAction(mainItemStockDTO.MainLevel, new { id = mainItemStockDTO.ParentCode, flag1 = mainItemStockDTO.Flag1 });
            }
            #region sidebar
            ViewBag.MainLevel_2 = await _mainItemRepo.GetMainItemsByLevelAsync(2, (int)mainItemStockDTO.Flag1);
            ViewBag.MainLevel_3 = await _mainItemRepo.GetMainItemsByLevelAsync(3, (int)mainItemStockDTO.Flag1);
            ViewBag.MainLevel_4 = await _mainItemRepo.GetMainItemsByLevelAsync(4, (int)mainItemStockDTO.Flag1);
            ViewBag.MainLevel_5 = await _mainItemRepo.GetMainItemsByLevelAsync(5, (int)mainItemStockDTO.Flag1);
            ViewBag.MainLevel_6 = await _mainItemRepo.GetMainItemsByLevelAsync(6, (int)mainItemStockDTO.Flag1);
            #endregion
            MainItemStockDTO newMainItemStockDTO = await _mainItemRepo.GetNewMainItemStockAsync(id);
            _mapper.Map(newMainItemStockDTO, mainItemStockDTO);
            return View(mainItemStockDTO);
        }

        ///////////////////////////////////////////////////////
        // Main Level 2

        public async Task<IActionResult> MainLevel_2(int flag1)
        {
            if (flag1 == 0)
                return RedirectToAction(nameof(ChooseStockType));
            List<MainItemViewDTO> mainLevel_2 = await _mainItemRepo.GetMainItemsByLevelAsync(2, flag1);
            KindStore stockType = await _stockTypeRepo.GetByIdAsync(flag1);
            ViewBag.stockType = stockType.KName;
            #region sidebar
            ViewBag.MainLevel_2 = await _mainItemRepo.GetMainItemsByLevelAsync(2, flag1);
            ViewBag.MainLevel_3 = await _mainItemRepo.GetMainItemsByLevelAsync(3, flag1);
            ViewBag.MainLevel_4 = await _mainItemRepo.GetMainItemsByLevelAsync(4, flag1);
            ViewBag.MainLevel_5 = await _mainItemRepo.GetMainItemsByLevelAsync(5, flag1);
            ViewBag.MainLevel_6 = await _mainItemRepo.GetMainItemsByLevelAsync(6, flag1);
            #endregion
            return View(mainLevel_2);
        }

        //Get Add
        public async Task<IActionResult> Add_MainLevel_2(int flag1)
        {
            MainItemEditAddDTO newMainLevel_2 = await _mainItemRepo.GetNewMainLevel_2_Async(flag1);
            KindStore stockType = await _stockTypeRepo.GetByIdAsync(flag1);
            ViewBag.stockType = stockType.KName;
            #region sidebar
            ViewBag.MainLevel_2 = await _mainItemRepo.GetMainItemsByLevelAsync(2, flag1);
            ViewBag.MainLevel_3 = await _mainItemRepo.GetMainItemsByLevelAsync(3, flag1);
            ViewBag.MainLevel_4 = await _mainItemRepo.GetMainItemsByLevelAsync(4, flag1);
            ViewBag.MainLevel_5 = await _mainItemRepo.GetMainItemsByLevelAsync(5, flag1);
            ViewBag.MainLevel_6 = await _mainItemRepo.GetMainItemsByLevelAsync(6, flag1);
            #endregion
            return View(newMainLevel_2);
        }

        //Post Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add_MainLevel_2(MainItemEditAddDTO mainItemDTO)
        {
            if (ModelState.IsValid)
            {
                mainItemDTO.BranchId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "U_Branch_Id").Value);
                mainItemDTO.UserCode = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "User_Code").Value);

                await _mainItemRepo.AddMainLevelAsync(mainItemDTO);
                return RedirectToAction(nameof(MainLevel_2), new { flag1 = mainItemDTO.Flag1 } );
            }
            KindStore stockType = await _stockTypeRepo.GetByIdAsync((int)mainItemDTO.Flag1);
            ViewBag.stockType = stockType.KName;
            #region sidebar
            ViewBag.MainLevel_2 = await _mainItemRepo.GetMainItemsByLevelAsync(2, (int)mainItemDTO.Flag1);
            ViewBag.MainLevel_3 = await _mainItemRepo.GetMainItemsByLevelAsync(3, (int)mainItemDTO.Flag1);
            ViewBag.MainLevel_4 = await _mainItemRepo.GetMainItemsByLevelAsync(4, (int)mainItemDTO.Flag1);
            ViewBag.MainLevel_5 = await _mainItemRepo.GetMainItemsByLevelAsync(5, (int)mainItemDTO.Flag1);
            ViewBag.MainLevel_6 = await _mainItemRepo.GetMainItemsByLevelAsync(6, (int)mainItemDTO.Flag1);
            #endregion
            MainItemEditAddDTO mainLevel_2 = await _mainItemRepo.GetNewMainLevel_2_Async((int)mainItemDTO.Flag1);
            _mapper.Map(mainLevel_2, mainItemDTO);
            return View(mainItemDTO);
        }

        //Get Edit
        public async Task<IActionResult> Edit_MainLevel_2(int id)
        {
            MainItemEditAddDTO mainLevel_2 = await _mainItemRepo.GetMainLevelByIdAsync(id);
            KindStore stockType = await _stockTypeRepo.GetByIdAsync((int)mainLevel_2.Flag1);
            ViewBag.stockType = stockType.KName;
            #region sidebar
            ViewBag.MainLevel_2 = await _mainItemRepo.GetMainItemsByLevelAsync(2, (int)mainLevel_2.Flag1);
            ViewBag.MainLevel_3 = await _mainItemRepo.GetMainItemsByLevelAsync(3, (int)mainLevel_2.Flag1);
            ViewBag.MainLevel_4 = await _mainItemRepo.GetMainItemsByLevelAsync(4, (int)mainLevel_2.Flag1);
            ViewBag.MainLevel_5 = await _mainItemRepo.GetMainItemsByLevelAsync(5, (int)mainLevel_2.Flag1);
            ViewBag.MainLevel_6 = await _mainItemRepo.GetMainItemsByLevelAsync(6, (int)mainLevel_2.Flag1);
            #endregion
            return View(mainLevel_2);
        }

        //Post Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_MainLevel_2(int id, MainItemEditAddDTO mainItemDTO)
        {
            if (ModelState.IsValid)
            {
                mainItemDTO.BranchId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "U_Branch_Id").Value);
                mainItemDTO.UserCode = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "User_Code").Value);

                await _mainItemRepo.EditMainLevelAsync(id, mainItemDTO);
                return RedirectToAction(nameof(MainLevel_2), new { flag1 = mainItemDTO.Flag1 });
            }
            KindStore stockType = await _stockTypeRepo.GetByIdAsync((int)mainItemDTO.Flag1);
            ViewBag.stockType = stockType.KName;
            #region sidebar
            ViewBag.MainLevel_2 = await _mainItemRepo.GetMainItemsByLevelAsync(2, (int)mainItemDTO.Flag1);
            ViewBag.MainLevel_3 = await _mainItemRepo.GetMainItemsByLevelAsync(3, (int)mainItemDTO.Flag1);
            ViewBag.MainLevel_4 = await _mainItemRepo.GetMainItemsByLevelAsync(4, (int)mainItemDTO.Flag1);
            ViewBag.MainLevel_5 = await _mainItemRepo.GetMainItemsByLevelAsync(5, (int)mainItemDTO.Flag1);
            ViewBag.MainLevel_6 = await _mainItemRepo.GetMainItemsByLevelAsync(6, (int)mainItemDTO.Flag1);
            #endregion
            MainItemEditAddDTO mainLevel_2 = await _mainItemRepo.GetMainLevelByIdAsync(id);
            _mapper.Map(mainLevel_2, mainItemDTO);
            return View(mainItemDTO);
        }

        //Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete_MainLevel_2(int id)
        {
            MainItem mainItem = await _mainItemRepo.GetByIdAsync(id);

            await _mainItemRepo.DeleteAsync(mainItem);
            await _mainItemRepo.SaveChangesAsync();
            return RedirectToAction(nameof(MainLevel_2), new { flag1 = mainItem.Flag1 });
        }

        ///////////////////////////////////////////////////////
        // Main Level 3

        public async Task<IActionResult> MainLevel_3(string id, int flag1)
        {
            if (flag1 == 0)
                return RedirectToAction(nameof(ChooseStockType));

            MainItemViewDTO mainItemDTO = await _mainItemRepo.GetParentItemAsync(id, flag1);
            ViewBag.mainName = mainItemDTO.MainName;
            ViewBag.mainCode = mainItemDTO.MainCode;
            ViewBag.lastSub = mainItemDTO.LastSub;

            KindStore stockType = await _stockTypeRepo.GetByIdAsync(flag1);
            ViewBag.stockType = stockType.KName;

            List<MainItemViewDTO> mainLevel_3 = await _mainItemRepo.GetMainItemsByLevelAsync(3, flag1, id);
            #region sidebar
            ViewBag.MainLevel_2 = await _mainItemRepo.GetMainItemsByLevelAsync(2, flag1);
            ViewBag.MainLevel_3 = await _mainItemRepo.GetMainItemsByLevelAsync(3, flag1);
            ViewBag.MainLevel_4 = await _mainItemRepo.GetMainItemsByLevelAsync(4, flag1);
            ViewBag.MainLevel_5 = await _mainItemRepo.GetMainItemsByLevelAsync(5, flag1);
            ViewBag.MainLevel_6 = await _mainItemRepo.GetMainItemsByLevelAsync(6, flag1);
            #endregion
            return View(mainLevel_3);
        }

        //Get Add
        public async Task<IActionResult> Add_MainLevel_3(string id, int flag1)
        {
            MainItemEditAddDTO newMainLevel_3 = await _mainItemRepo.GetNewMainLevel_3_Async(id, flag1);
            KindStore stockType = await _stockTypeRepo.GetByIdAsync(flag1);
            ViewBag.stockType = stockType.KName;
            #region sidebar
            ViewBag.MainLevel_2 = await _mainItemRepo.GetMainItemsByLevelAsync(2, flag1);
            ViewBag.MainLevel_3 = await _mainItemRepo.GetMainItemsByLevelAsync(3, flag1);
            ViewBag.MainLevel_4 = await _mainItemRepo.GetMainItemsByLevelAsync(4, flag1);
            ViewBag.MainLevel_5 = await _mainItemRepo.GetMainItemsByLevelAsync(5, flag1);
            ViewBag.MainLevel_6 = await _mainItemRepo.GetMainItemsByLevelAsync(6, flag1);
            #endregion
            return View(newMainLevel_3);
        }

        //Post Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add_MainLevel_3(MainItemEditAddDTO mainItemDTO)
        {
            if (ModelState.IsValid)
            {
                mainItemDTO.BranchId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "U_Branch_Id").Value);
                mainItemDTO.UserCode = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "User_Code").Value);

                await _mainItemRepo.AddMainLevelAsync(mainItemDTO);
                return RedirectToAction(nameof(MainLevel_3), new { id = mainItemDTO.ParentCode, flag1 = mainItemDTO.Flag1 });
            }
            KindStore stockType = await _stockTypeRepo.GetByIdAsync((int)mainItemDTO.Flag1);
            ViewBag.stockType = stockType.KName;
            #region sidebar
            ViewBag.MainLevel_2 = await _mainItemRepo.GetMainItemsByLevelAsync(2, (int)mainItemDTO.Flag1);
            ViewBag.MainLevel_3 = await _mainItemRepo.GetMainItemsByLevelAsync(3, (int)mainItemDTO.Flag1);
            ViewBag.MainLevel_4 = await _mainItemRepo.GetMainItemsByLevelAsync(4, (int)mainItemDTO.Flag1);
            ViewBag.MainLevel_5 = await _mainItemRepo.GetMainItemsByLevelAsync(5, (int)mainItemDTO.Flag1);
            ViewBag.MainLevel_6 = await _mainItemRepo.GetMainItemsByLevelAsync(6, (int)mainItemDTO.Flag1);
            #endregion
            MainItemEditAddDTO mainLevel_3 = await _mainItemRepo
                .GetNewMainLevel_3_Async(mainItemDTO.ParentCode, (int)mainItemDTO.Flag1);
            _mapper.Map(mainLevel_3, mainItemDTO);
            return View(mainItemDTO);
        }

        //Get Edit
        public async Task<IActionResult> Edit_MainLevel_3(int id)
        {
            MainItemEditAddDTO mainLevel_3 = await _mainItemRepo.GetMainLevelByIdAsync(id);
            KindStore stockType = await _stockTypeRepo.GetByIdAsync((int)mainLevel_3.Flag1);
            ViewBag.stockType = stockType.KName;
            #region sidebar
            ViewBag.MainLevel_2 = await _mainItemRepo.GetMainItemsByLevelAsync(2, (int)mainLevel_3.Flag1);
            ViewBag.MainLevel_3 = await _mainItemRepo.GetMainItemsByLevelAsync(3, (int)mainLevel_3.Flag1);
            ViewBag.MainLevel_4 = await _mainItemRepo.GetMainItemsByLevelAsync(4, (int)mainLevel_3.Flag1);
            ViewBag.MainLevel_5 = await _mainItemRepo.GetMainItemsByLevelAsync(5, (int)mainLevel_3.Flag1);
            ViewBag.MainLevel_6 = await _mainItemRepo.GetMainItemsByLevelAsync(6, (int)mainLevel_3.Flag1);
            #endregion
            return View(mainLevel_3);
        }

        //Post Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_MainLevel_3(int id, MainItemEditAddDTO mainItemDTO)
        {
            if (ModelState.IsValid)
            {
                mainItemDTO.BranchId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "U_Branch_Id").Value);
                mainItemDTO.UserCode = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "User_Code").Value);

                await _mainItemRepo.EditMainLevelAsync(id, mainItemDTO);
                return RedirectToAction(nameof(MainLevel_3), new { id = mainItemDTO.ParentCode, flag1 = mainItemDTO.Flag1 });
            }
            KindStore stockType = await _stockTypeRepo.GetByIdAsync((int)mainItemDTO.Flag1);
            ViewBag.stockType = stockType.KName;
            #region sidebar
            ViewBag.MainLevel_2 = await _mainItemRepo.GetMainItemsByLevelAsync(2, (int)mainItemDTO.Flag1);
            ViewBag.MainLevel_3 = await _mainItemRepo.GetMainItemsByLevelAsync(3, (int)mainItemDTO.Flag1);
            ViewBag.MainLevel_4 = await _mainItemRepo.GetMainItemsByLevelAsync(4, (int)mainItemDTO.Flag1);
            ViewBag.MainLevel_5 = await _mainItemRepo.GetMainItemsByLevelAsync(5, (int)mainItemDTO.Flag1);
            ViewBag.MainLevel_6 = await _mainItemRepo.GetMainItemsByLevelAsync(6, (int)mainItemDTO.Flag1);
            #endregion
            MainItemEditAddDTO mainLevel_3 = await _mainItemRepo.GetMainLevelByIdAsync(id);
            _mapper.Map(mainLevel_3, mainItemDTO);
            return View(mainItemDTO);
        }

        //Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete_MainLevel_3(int id)
        {
            MainItem mainItem = await _mainItemRepo.GetByIdAsync(id);

            await _mainItemRepo.DeleteAsync(mainItem);
            await _mainItemRepo.SaveChangesAsync();
            return RedirectToAction(nameof(MainLevel_3), new { id = mainItem.ParentCode, flag1 = mainItem.Flag1 });
        }

        ///////////////////////////////////////////////////////
        // Main Level 4

        public async Task<IActionResult> MainLevel_4(string id, int flag1)
        {
            if (flag1 == 0)
                return RedirectToAction(nameof(ChooseStockType));

            MainItemViewDTO mainItemDTO = await _mainItemRepo.GetParentItemAsync(id, flag1);
            ViewBag.mainName = mainItemDTO.MainName;
            ViewBag.mainCode = mainItemDTO.MainCode;
            ViewBag.lastSub = mainItemDTO.LastSub;

            KindStore stockType = await _stockTypeRepo.GetByIdAsync(flag1);
            ViewBag.stockType = stockType.KName;

            List<MainItemViewDTO> mainLevel_4 = await _mainItemRepo.GetMainItemsByLevelAsync(4, flag1, id);
            #region sidebar
            ViewBag.MainLevel_2 = await _mainItemRepo.GetMainItemsByLevelAsync(2, flag1);
            ViewBag.MainLevel_3 = await _mainItemRepo.GetMainItemsByLevelAsync(3, flag1);
            ViewBag.MainLevel_4 = await _mainItemRepo.GetMainItemsByLevelAsync(4, flag1);
            ViewBag.MainLevel_5 = await _mainItemRepo.GetMainItemsByLevelAsync(5, flag1);
            ViewBag.MainLevel_6 = await _mainItemRepo.GetMainItemsByLevelAsync(6, flag1);
            #endregion
            return View(mainLevel_4);
        }

        //Get Add
        public async Task<IActionResult> Add_MainLevel_4(string id, int flag1)
        {
            MainItemEditAddDTO newMainLevel_4 = await _mainItemRepo.GetNewMainLevel_4_Async(id, flag1);
            KindStore stockType = await _stockTypeRepo.GetByIdAsync(flag1);
            ViewBag.stockType = stockType.KName;
            #region sidebar
            ViewBag.MainLevel_2 = await _mainItemRepo.GetMainItemsByLevelAsync(2, flag1);
            ViewBag.MainLevel_3 = await _mainItemRepo.GetMainItemsByLevelAsync(3, flag1);
            ViewBag.MainLevel_4 = await _mainItemRepo.GetMainItemsByLevelAsync(4, flag1);
            ViewBag.MainLevel_5 = await _mainItemRepo.GetMainItemsByLevelAsync(5, flag1);
            ViewBag.MainLevel_6 = await _mainItemRepo.GetMainItemsByLevelAsync(6, flag1);
            #endregion
            return View(newMainLevel_4);
        }

        //Post Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add_MainLevel_4(MainItemEditAddDTO mainItemDTO)
        {
            if (ModelState.IsValid)
            {
                mainItemDTO.BranchId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "U_Branch_Id").Value);
                mainItemDTO.UserCode = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "User_Code").Value);

                await _mainItemRepo.AddMainLevelAsync(mainItemDTO);
                return RedirectToAction(nameof(MainLevel_4), new { id = mainItemDTO.ParentCode, flag1 = mainItemDTO.Flag1 });
            }
            KindStore stockType = await _stockTypeRepo.GetByIdAsync((int)mainItemDTO.Flag1);
            ViewBag.stockType = stockType.KName;
            #region sidebar
            ViewBag.MainLevel_2 = await _mainItemRepo.GetMainItemsByLevelAsync(2, (int)mainItemDTO.Flag1);
            ViewBag.MainLevel_3 = await _mainItemRepo.GetMainItemsByLevelAsync(3, (int)mainItemDTO.Flag1);
            ViewBag.MainLevel_4 = await _mainItemRepo.GetMainItemsByLevelAsync(4, (int)mainItemDTO.Flag1);
            ViewBag.MainLevel_5 = await _mainItemRepo.GetMainItemsByLevelAsync(5, (int)mainItemDTO.Flag1);
            ViewBag.MainLevel_6 = await _mainItemRepo.GetMainItemsByLevelAsync(6, (int)mainItemDTO.Flag1);
            #endregion
            MainItemEditAddDTO newMainLevel_4 = await _mainItemRepo
                .GetNewMainLevel_4_Async(mainItemDTO.ParentCode, (int)mainItemDTO.Flag1);
            _mapper.Map(newMainLevel_4, mainItemDTO);
            return View(mainItemDTO);
        }

        //Get Edit
        public async Task<IActionResult> Edit_MainLevel_4(int id)
        {
            MainItemEditAddDTO mainLevel_4 = await _mainItemRepo.GetMainLevelByIdAsync(id);
            KindStore stockType = await _stockTypeRepo.GetByIdAsync((int)mainLevel_4.Flag1);
            ViewBag.stockType = stockType.KName;
            #region sidebar
            ViewBag.MainLevel_2 = await _mainItemRepo.GetMainItemsByLevelAsync(2, (int)mainLevel_4.Flag1);
            ViewBag.MainLevel_3 = await _mainItemRepo.GetMainItemsByLevelAsync(3, (int)mainLevel_4.Flag1);
            ViewBag.MainLevel_4 = await _mainItemRepo.GetMainItemsByLevelAsync(4, (int)mainLevel_4.Flag1);
            ViewBag.MainLevel_5 = await _mainItemRepo.GetMainItemsByLevelAsync(5, (int)mainLevel_4.Flag1);
            ViewBag.MainLevel_6 = await _mainItemRepo.GetMainItemsByLevelAsync(6, (int)mainLevel_4.Flag1);
            #endregion
            return View(mainLevel_4);
        }

        //Post Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_MainLevel_4(int id, MainItemEditAddDTO mainItemDTO)
        {
            if (ModelState.IsValid)
            {
                mainItemDTO.BranchId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "U_Branch_Id").Value);
                mainItemDTO.UserCode = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "User_Code").Value);

                await _mainItemRepo.EditMainLevelAsync(id, mainItemDTO);
                return RedirectToAction(nameof(MainLevel_4), new { id = mainItemDTO.ParentCode, flag1 = mainItemDTO.Flag1 });
            }
            KindStore stockType = await _stockTypeRepo.GetByIdAsync((int)mainItemDTO.Flag1);
            ViewBag.stockType = stockType.KName;
            #region sidebar
            ViewBag.MainLevel_2 = await _mainItemRepo.GetMainItemsByLevelAsync(2, (int)mainItemDTO.Flag1);
            ViewBag.MainLevel_3 = await _mainItemRepo.GetMainItemsByLevelAsync(3, (int)mainItemDTO.Flag1);
            ViewBag.MainLevel_4 = await _mainItemRepo.GetMainItemsByLevelAsync(4, (int)mainItemDTO.Flag1);
            ViewBag.MainLevel_5 = await _mainItemRepo.GetMainItemsByLevelAsync(5, (int)mainItemDTO.Flag1);
            ViewBag.MainLevel_6 = await _mainItemRepo.GetMainItemsByLevelAsync(6, (int)mainItemDTO.Flag1);
            #endregion
            MainItemEditAddDTO mainLevel_4 = await _mainItemRepo.GetMainLevelByIdAsync(id);
            _mapper.Map(mainLevel_4, mainItemDTO);
            return View(mainItemDTO);
        }

        //Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete_MainLevel_4(int id)
        {
            MainItem mainItem = await _mainItemRepo.GetByIdAsync(id);

            await _mainItemRepo.DeleteAsync(mainItem);
            await _mainItemRepo.SaveChangesAsync();
            return RedirectToAction(nameof(MainLevel_4), new { id = mainItem.ParentCode, flag1 = mainItem.Flag1 });
        }

        ///////////////////////////////////////////////////////
        // Main Level 5

        public async Task<IActionResult> MainLevel_5(string id, int flag1)
        {
            if (flag1 == 0)
                return RedirectToAction(nameof(ChooseStockType));

            MainItemViewDTO mainItemDTO = await _mainItemRepo.GetParentItemAsync(id, flag1);
            ViewBag.mainName = mainItemDTO.MainName;
            ViewBag.mainCode = mainItemDTO.MainCode;
            ViewBag.lastSub = mainItemDTO.LastSub;

            KindStore stockType = await _stockTypeRepo.GetByIdAsync(flag1);
            ViewBag.stockType = stockType.KName;

            List<MainItemViewDTO> mainLevel_5 = await _mainItemRepo.GetMainItemsByLevelAsync(5, flag1, id);
            #region sidebar
            ViewBag.MainLevel_2 = await _mainItemRepo.GetMainItemsByLevelAsync(2, flag1);
            ViewBag.MainLevel_3 = await _mainItemRepo.GetMainItemsByLevelAsync(3, flag1);
            ViewBag.MainLevel_4 = await _mainItemRepo.GetMainItemsByLevelAsync(4, flag1);
            ViewBag.MainLevel_5 = await _mainItemRepo.GetMainItemsByLevelAsync(5, flag1);
            ViewBag.MainLevel_6 = await _mainItemRepo.GetMainItemsByLevelAsync(6, flag1);
            #endregion
            return View(mainLevel_5);
        }

        //Get Add
        public async Task<IActionResult> Add_MainLevel_5(string id, int flag1)
        {
            MainItemEditAddDTO newMainLevel_5 = await _mainItemRepo.GetNewMainLevel_5_Async(id, flag1);
            KindStore stockType = await _stockTypeRepo.GetByIdAsync(flag1);
            ViewBag.stockType = stockType.KName;
            #region sidebar
            ViewBag.MainLevel_2 = await _mainItemRepo.GetMainItemsByLevelAsync(2, flag1);
            ViewBag.MainLevel_3 = await _mainItemRepo.GetMainItemsByLevelAsync(3, flag1);
            ViewBag.MainLevel_4 = await _mainItemRepo.GetMainItemsByLevelAsync(4, flag1);
            ViewBag.MainLevel_5 = await _mainItemRepo.GetMainItemsByLevelAsync(5, flag1);
            ViewBag.MainLevel_6 = await _mainItemRepo.GetMainItemsByLevelAsync(6, flag1);
            #endregion
            return View(newMainLevel_5);
        }

        //Post Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add_MainLevel_5(MainItemEditAddDTO mainItemDTO)
        {
            if (ModelState.IsValid)
            {
                mainItemDTO.BranchId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "U_Branch_Id").Value);
                mainItemDTO.UserCode = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "User_Code").Value);

                await _mainItemRepo.AddMainLevelAsync(mainItemDTO);
                return RedirectToAction(nameof(MainLevel_5), new { id = mainItemDTO.ParentCode, flag1 = mainItemDTO.Flag1 });
            }
            KindStore stockType = await _stockTypeRepo.GetByIdAsync((int)mainItemDTO.Flag1);
            ViewBag.stockType = stockType.KName;
            #region sidebar
            ViewBag.MainLevel_2 = await _mainItemRepo.GetMainItemsByLevelAsync(2, (int)mainItemDTO.Flag1);
            ViewBag.MainLevel_3 = await _mainItemRepo.GetMainItemsByLevelAsync(3, (int)mainItemDTO.Flag1);
            ViewBag.MainLevel_4 = await _mainItemRepo.GetMainItemsByLevelAsync(4, (int)mainItemDTO.Flag1);
            ViewBag.MainLevel_5 = await _mainItemRepo.GetMainItemsByLevelAsync(5, (int)mainItemDTO.Flag1);
            ViewBag.MainLevel_6 = await _mainItemRepo.GetMainItemsByLevelAsync(6, (int)mainItemDTO.Flag1);
            #endregion
            MainItemEditAddDTO newMainLevel_5 = await _mainItemRepo
                .GetNewMainLevel_5_Async(mainItemDTO.ParentCode, (int)mainItemDTO.Flag1);
            _mapper.Map(newMainLevel_5, mainItemDTO);
            return View(mainItemDTO);
        }

        //Get Edit
        public async Task<IActionResult> Edit_MainLevel_5(int id)
        {
            MainItemEditAddDTO mainLevel_5 = await _mainItemRepo.GetMainLevelByIdAsync(id);
            KindStore stockType = await _stockTypeRepo.GetByIdAsync((int)mainLevel_5.Flag1);
            ViewBag.stockType = stockType.KName;
            #region sidebar
            ViewBag.MainLevel_2 = await _mainItemRepo.GetMainItemsByLevelAsync(2, (int)mainLevel_5.Flag1);
            ViewBag.MainLevel_3 = await _mainItemRepo.GetMainItemsByLevelAsync(3, (int)mainLevel_5.Flag1);
            ViewBag.MainLevel_4 = await _mainItemRepo.GetMainItemsByLevelAsync(4, (int)mainLevel_5.Flag1);
            ViewBag.MainLevel_5 = await _mainItemRepo.GetMainItemsByLevelAsync(5, (int)mainLevel_5.Flag1);
            ViewBag.MainLevel_6 = await _mainItemRepo.GetMainItemsByLevelAsync(6, (int)mainLevel_5.Flag1);
            #endregion
            return View(mainLevel_5);
        }

        //Post Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_MainLevel_5(int id, MainItemEditAddDTO mainItemDTO)
        {
            if (ModelState.IsValid)
            {
                mainItemDTO.BranchId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "U_Branch_Id").Value);
                mainItemDTO.UserCode = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "User_Code").Value);

                await _mainItemRepo.EditMainLevelAsync(id, mainItemDTO);
                return RedirectToAction(nameof(MainLevel_5), new { id = mainItemDTO.ParentCode, flag1 = mainItemDTO.Flag1 });
            }
            KindStore stockType = await _stockTypeRepo.GetByIdAsync((int)mainItemDTO.Flag1);
            ViewBag.stockType = stockType.KName;
            #region sidebar
            ViewBag.MainLevel_2 = await _mainItemRepo.GetMainItemsByLevelAsync(2, (int)mainItemDTO.Flag1);
            ViewBag.MainLevel_3 = await _mainItemRepo.GetMainItemsByLevelAsync(3, (int)mainItemDTO.Flag1);
            ViewBag.MainLevel_4 = await _mainItemRepo.GetMainItemsByLevelAsync(4, (int)mainItemDTO.Flag1);
            ViewBag.MainLevel_5 = await _mainItemRepo.GetMainItemsByLevelAsync(5, (int)mainItemDTO.Flag1);
            ViewBag.MainLevel_6 = await _mainItemRepo.GetMainItemsByLevelAsync(6, (int)mainItemDTO.Flag1);
            #endregion
            MainItemEditAddDTO mainLevel_5 = await _mainItemRepo.GetMainLevelByIdAsync(id);
            _mapper.Map(mainLevel_5, mainItemDTO);
            return View(mainItemDTO);
        }

        //Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete_MainLevel_5(int id)
        {
            MainItem mainItem = await _mainItemRepo.GetByIdAsync(id);

            await _mainItemRepo.DeleteAsync(mainItem);
            await _mainItemRepo.SaveChangesAsync();
            return RedirectToAction(nameof(MainLevel_5), new { id = mainItem.ParentCode, flag1 = mainItem.Flag1 });
        }

        ///////////////////////////////////////////////////////
        // Main Level 6

        public async Task<IActionResult> MainLevel_6(string id, int flag1)
        {
            if (flag1 == 0)
                return RedirectToAction(nameof(ChooseStockType));

            MainItemViewDTO mainItemDTO = await _mainItemRepo.GetParentItemAsync(id, flag1);
            ViewBag.mainName = mainItemDTO.MainName;
            ViewBag.mainCode = mainItemDTO.MainCode;
            ViewBag.lastSub = mainItemDTO.LastSub;

            KindStore stockType = await _stockTypeRepo.GetByIdAsync(flag1);
            ViewBag.stockType = stockType.KName;

            List<MainItemViewDTO> mainLevel_6 = await _mainItemRepo.GetMainItemsByLevelAsync(6, flag1, id);
            #region sidebar
            ViewBag.MainLevel_2 = await _mainItemRepo.GetMainItemsByLevelAsync(2, flag1);
            ViewBag.MainLevel_3 = await _mainItemRepo.GetMainItemsByLevelAsync(3, flag1);
            ViewBag.MainLevel_4 = await _mainItemRepo.GetMainItemsByLevelAsync(4, flag1);
            ViewBag.MainLevel_5 = await _mainItemRepo.GetMainItemsByLevelAsync(5, flag1);
            ViewBag.MainLevel_6 = await _mainItemRepo.GetMainItemsByLevelAsync(6, flag1);
            #endregion
            return View(mainLevel_6);
        }

        //Get Add
        public async Task<IActionResult> Add_MainLevel_6(string id, int flag1)
        {
            MainItemEditAddDTO newMainLevel_6 = await _mainItemRepo.GetNewMainLevel_6_Async(id, flag1);
            KindStore stockType = await _stockTypeRepo.GetByIdAsync(flag1);
            ViewBag.stockType = stockType.KName;
            #region sidebar
            ViewBag.MainLevel_2 = await _mainItemRepo.GetMainItemsByLevelAsync(2, flag1);
            ViewBag.MainLevel_3 = await _mainItemRepo.GetMainItemsByLevelAsync(3, flag1);
            ViewBag.MainLevel_4 = await _mainItemRepo.GetMainItemsByLevelAsync(4, flag1);
            ViewBag.MainLevel_5 = await _mainItemRepo.GetMainItemsByLevelAsync(5, flag1);
            ViewBag.MainLevel_6 = await _mainItemRepo.GetMainItemsByLevelAsync(6, flag1);
            #endregion
            return View(newMainLevel_6);
        }

        //Post Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add_MainLevel_6(MainItemEditAddDTO mainItemDTO)
        {
            if (ModelState.IsValid)
            {
                mainItemDTO.BranchId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "U_Branch_Id").Value);
                mainItemDTO.UserCode = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "User_Code").Value);

                await _mainItemRepo.AddMainLevelAsync(mainItemDTO);
                return RedirectToAction(nameof(MainLevel_6), new { id = mainItemDTO.ParentCode, flag1 = mainItemDTO.Flag1 });
            }
            KindStore stockType = await _stockTypeRepo.GetByIdAsync((int)mainItemDTO.Flag1);
            ViewBag.stockType = stockType.KName;
            #region sidebar
            ViewBag.MainLevel_2 = await _mainItemRepo.GetMainItemsByLevelAsync(2, (int)mainItemDTO.Flag1);
            ViewBag.MainLevel_3 = await _mainItemRepo.GetMainItemsByLevelAsync(3, (int)mainItemDTO.Flag1);
            ViewBag.MainLevel_4 = await _mainItemRepo.GetMainItemsByLevelAsync(4, (int)mainItemDTO.Flag1);
            ViewBag.MainLevel_5 = await _mainItemRepo.GetMainItemsByLevelAsync(5, (int)mainItemDTO.Flag1);
            ViewBag.MainLevel_6 = await _mainItemRepo.GetMainItemsByLevelAsync(6, (int)mainItemDTO.Flag1);
            #endregion
            MainItemEditAddDTO newMainLevel_6 = await _mainItemRepo
                .GetNewMainLevel_6_Async(mainItemDTO.ParentCode, (int)mainItemDTO.Flag1);
            _mapper.Map(newMainLevel_6, mainItemDTO);
            return View(mainItemDTO);
        }

        //Get Edit
        public async Task<IActionResult> Edit_MainLevel_6(int id)
        {
            MainItemEditAddDTO mainLevel_6 = await _mainItemRepo.GetMainLevelByIdAsync(id);
            KindStore stockType = await _stockTypeRepo.GetByIdAsync((int)mainLevel_6.Flag1);
            ViewBag.stockType = stockType.KName;
            #region sidebar
            ViewBag.MainLevel_2 = await _mainItemRepo.GetMainItemsByLevelAsync(2, (int)mainLevel_6.Flag1);
            ViewBag.MainLevel_3 = await _mainItemRepo.GetMainItemsByLevelAsync(3, (int)mainLevel_6.Flag1);
            ViewBag.MainLevel_4 = await _mainItemRepo.GetMainItemsByLevelAsync(4, (int)mainLevel_6.Flag1);
            ViewBag.MainLevel_5 = await _mainItemRepo.GetMainItemsByLevelAsync(5, (int)mainLevel_6.Flag1);
            ViewBag.MainLevel_6 = await _mainItemRepo.GetMainItemsByLevelAsync(6, (int)mainLevel_6.Flag1);
            #endregion
            return View(mainLevel_6);
        }

        //Post Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_MainLevel_6(int id, MainItemEditAddDTO mainItemDTO)
        {
            if (ModelState.IsValid)
            {
                mainItemDTO.BranchId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "U_Branch_Id").Value);
                mainItemDTO.UserCode = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "User_Code").Value);

                await _mainItemRepo.EditMainLevelAsync(id, mainItemDTO);
                return RedirectToAction(nameof(MainLevel_6), new { id = mainItemDTO.ParentCode, flag1 = mainItemDTO.Flag1 });
            }
            KindStore stockType = await _stockTypeRepo.GetByIdAsync((int)mainItemDTO.Flag1);
            ViewBag.stockType = stockType.KName;
            #region sidebar
            ViewBag.MainLevel_2 = await _mainItemRepo.GetMainItemsByLevelAsync(2, (int)mainItemDTO.Flag1);
            ViewBag.MainLevel_3 = await _mainItemRepo.GetMainItemsByLevelAsync(3, (int)mainItemDTO.Flag1);
            ViewBag.MainLevel_4 = await _mainItemRepo.GetMainItemsByLevelAsync(4, (int)mainItemDTO.Flag1);
            ViewBag.MainLevel_5 = await _mainItemRepo.GetMainItemsByLevelAsync(5, (int)mainItemDTO.Flag1);
            ViewBag.MainLevel_6 = await _mainItemRepo.GetMainItemsByLevelAsync(6, (int)mainItemDTO.Flag1);
            #endregion
            MainItemEditAddDTO mainLevel_6 = await _mainItemRepo.GetMainLevelByIdAsync(id);
            _mapper.Map(mainLevel_6, mainItemDTO);
            return View(mainItemDTO);
        }

        //Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete_MainLevel_6(int id)
        {
            MainItem mainItem = await _mainItemRepo.GetByIdAsync(id);

            await _mainItemRepo.DeleteAsync(mainItem);
            await _mainItemRepo.SaveChangesAsync();
            return RedirectToAction(nameof(MainLevel_6), new { id = mainItem.ParentCode, flag1 = mainItem.Flag1 });
        }
    }
}
