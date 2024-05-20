using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProSoft.EF.DTOs.Shared;
using ProSoft.EF.DTOs.Treasury;
using ProSoft.EF.IRepositories.Treasury;

namespace ProSoft.UI.Areas.Treasury.Controllers
{
    [Authorize]
    [Area(nameof(Treasury))]
    public class CustCollectionsDiscountController : Controller
    {
        private readonly ICustCollectionsDiscountRepo _custCollectionsDiscountRepo;
        public CustCollectionsDiscountController(ICustCollectionsDiscountRepo custCollectionsDiscountRepo)
        {
            _custCollectionsDiscountRepo = custCollectionsDiscountRepo;
        }
        public async Task<IActionResult> Index()
        {
            List<CustCollectionsDiscountViewDTO> custCollectionsDiscounts = await _custCollectionsDiscountRepo.GetAllCustCollectionsDiscountAsync();
            return View(custCollectionsDiscounts);
        }
    }
}
