using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProSoft.EF.DTOs.Stocks.ItemUnits;
using ProSoft.EF.IRepositories.Stocks;
using ProSoft.EF.Models.Stocks;

namespace ProSoft.UI.Areas.Stocks.Controllers
{

    [Area("Stocks")]
    [Authorize]
    public class ItemUnitsController : Controller
    {
        private readonly IStockTypeRepo _stockTypeRepo;
        private readonly ISubItemRepo _subItemRepo;
        private readonly IUnitCodeRepo _unitCodeRepo;
        private readonly IItemUnitsRepo _itemUnitsRepo;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public ItemUnitsController(IStockTypeRepo stockTypeRepo, ISubItemRepo subItemRepo, IUnitCodeRepo unitCodeRepo, IItemUnitsRepo itemUnitsRepo, IMapper mapper, ICurrentUserService currentUserService)
        {
            _stockTypeRepo = stockTypeRepo;
            _subItemRepo = subItemRepo;
            _unitCodeRepo = unitCodeRepo;
            _itemUnitsRepo = itemUnitsRepo;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<IActionResult> Index()
        {
            var flags = (await _stockTypeRepo.GetAllAsync()).Select(f => new SelectListItem { Text = f.KName,Value = f.KId.ToString()}).ToList();
            ItemUnitsViewDTO itemUnitsViewDTO = new() { Flag1 = flags };
            return View(itemUnitsViewDTO);
        }

        [HttpGet]
        public async Task<IActionResult> Add(int stockType,string itemCode)
        {
            ItemUnitsAddEditDTO itemUnitsAddEditDTO = new ItemUnitsAddEditDTO()
            {
                Flag1 = stockType,
                ItemCode = itemCode,
                Units = (await _unitCodeRepo.GetAllAsync()).Select(u => new SelectListItem { Text = u.Names, Value = u.Code.ToString() }).ToList()
            };
            return View(itemUnitsAddEditDTO);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(ItemUnitsAddEditDTO itemUnitsAddEditDTO)
        {
            if (!ModelState.IsValid)
            {

                itemUnitsAddEditDTO.Units = (await _unitCodeRepo.GetAllAsync()).Select(u => new SelectListItem { Text = u.Names, Value = u.Code.ToString() }).ToList();
              
                return View(itemUnitsAddEditDTO);
            }
            ItemUnit itemUnit = new()
            {
                UnitCode = itemUnitsAddEditDTO.UnitCode,
                ItemCode = itemUnitsAddEditDTO.ItemCode,
                Flag1 = itemUnitsAddEditDTO.Flag1,
                DefaultUnit = itemUnitsAddEditDTO.DefaultUnit,
                ItemQty = itemUnitsAddEditDTO.ItemQty,
                BranchId = _currentUserService.BranchId

            };
            await _itemUnitsRepo.AddAsync(itemUnit);
            await _itemUnitsRepo.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> GetSubItemsByStockType(int id)
        {
            //var flags = (await _stockTypeRepo.GetAllAsync()).Select(f => new SelectListItem { Text = f.KName, Value = f.KId.ToString() }).ToList();
            var subitems = ( await _subItemRepo.GetAllSubItemByStockTypeAsync(id)).Select(s => new  { text = s.SubName,id = s.ItemCode}).ToList();
            Console.WriteLine("Count : " +subitems.Count());
            //ItemUnitsViewDTO itemUnitsViewDTO = new() { Flag1 = flags , SubItems = subitems};
            var result = new
            {
                draw = 1,
                recordsTotal = subitems.Count(),       // Total number of records
                recordsFiltered = subitems.Count(),    // Total filtered records (for paging)
                data = subitems                        // Actual data to display
            };

            return Json(result);

          
        }

        [HttpGet]
        public async Task<IActionResult> GetItemsUnitByItemCodeAndStockType(int stockType,string itemCode)
        {
            //var flags = (await _stockTypeRepo.GetAllAsync()).Select(f => new SelectListItem { Text = f.KName, Value = f.KId.ToString() }).ToList();
            var itemsUnit =  _itemUnitsRepo.GetItemsByStockTypeAndItemCode(stockType,itemCode);
            //ItemUnitsViewDTO itemUnitsViewDTO = new() { Flag1 = flags , SubItems = subitems};
            var result = new
            {
                draw = 1,
                recordsTotal = itemsUnit.Count(),       // Total number of records
                recordsFiltered = itemsUnit.Count(),    // Total filtered records (for paging)
                data = itemsUnit                        // Actual data to display
            };

            return Json(result);


        }
    }
}
