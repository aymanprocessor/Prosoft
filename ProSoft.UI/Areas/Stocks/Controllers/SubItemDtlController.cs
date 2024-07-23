using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ProSoft.UI.Areas.Stocks.Controllers
{
    [Authorize]
    [Area(nameof(Stocks))]
    public class SubItemDtlController : Controller
    {
        //private readonly IMainItemRepo _mainItemRepo;
        //private readonly ICostCenterRepo _costCenterRepo;
        //private readonly IStockTypeRepo _stockTypeRepo;
        //private readonly IMapper _mapper;
        //private readonly IStockRepo _stockRepo;
        public SubItemDtlController(/*IMainItemRepo mainItemRepo, ICostCenterRepo costCenterRepo,*/
            /*IStockTypeRepo stockTypeRepo, IMapper mapper, IStockRepo stockRepo*/)
        {
            //_mainItemRepo = mainItemRepo;
            //_costCenterRepo = costCenterRepo;
            //_stockTypeRepo = stockTypeRepo;
            //_mapper = mapper;
            //_stockRepo = stockRepo;
        }
        public async Task<IActionResult> GetSubItemDetails(int id)
        {
            return View();
        }
    }
}
