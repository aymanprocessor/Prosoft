using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProSoft.EF.DTOs.Stocks.ItemsCustPrice;
using ProSoft.EF.IRepositories.Stocks;
using ProSoft.EF.Models.Stocks;
using ProSoft.UI.Global;

namespace ProSoft.UI.Areas.Stocks.Controllers
{
    [Area("Stocks")]
    [Authorize]
    public class ItemsCustPriceController : Controller
    {
        private readonly IAdjectiveCustRepo _adjectiveCustRepo;

        public ItemsCustPriceController(IAdjectiveCustRepo adjectiveCustRepo)
        {
            _adjectiveCustRepo = adjectiveCustRepo;
        }

        public async  Task<IActionResult> Index()
        {
            ItemsCustPriceViewDTO itemsCustPriceViewDTO = new ItemsCustPriceViewDTO();
            ViewBag.AdjectiveCust = (await _adjectiveCustRepo.GetAllAsync()).ToSelectListItem<AdjectiveCust>(a => a.AdjectiveDesc,a=>a.AdjectiveCode.ToString());

            return View(itemsCustPriceViewDTO);
        }
    }
}
