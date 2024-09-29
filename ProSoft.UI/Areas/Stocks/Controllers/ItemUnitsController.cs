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

        public ItemUnitsController(IStockTypeRepo stockTypeRepo, ISubItemRepo subItemRepo)
        {
            _stockTypeRepo = stockTypeRepo;
            _subItemRepo = subItemRepo;
        }

        public async Task<IActionResult> Index()
        {
            var flags = (await _stockTypeRepo.GetAllAsync()).Select(f => new SelectListItem { Text = f.KName,Value = f.KId.ToString()}).ToList();
            ItemUnitsViewDTO itemUnitsViewDTO = new() { Flag1 = flags };
            return View(itemUnitsViewDTO);
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
    }
}
