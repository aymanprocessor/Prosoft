using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProSoft.EF.DTOs.Medical.HospitalPatData;
using ProSoft.EF.IRepositories.Medical.HospitalPatData;

namespace ProSoft.UI.Areas.Medical.Controllers
{
    [Authorize]
    [Area("Medical")]
    public class TermsPriceListController : Controller
    {
        private readonly ITermsPriceListRepo _termsPriceListRepo;
        public TermsPriceListController(ITermsPriceListRepo termsPriceListRepo)
        {
            _termsPriceListRepo = termsPriceListRepo;
        }
        public async Task<IActionResult> GetTermOfPriceLists(int id)
        {
            List<TermsPriceListViewDTO> termsPriceListDTOs = await _termsPriceListRepo.GetAllTermsPriceList(id);
            return Json(termsPriceListDTOs);
        }
    }
}
