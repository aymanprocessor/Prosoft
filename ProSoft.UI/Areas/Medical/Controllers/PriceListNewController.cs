using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProSoft.EF.DTOs.Medical.HospitalPatData;
using ProSoft.EF.IRepositories.Medical.HospitalPatData;
using ProSoft.EF.Models.Medical.HospitalPatData;

namespace ProSoft.UI.Areas.Medical.Controllers
{
    [Authorize]
    [Area("Medical")]

    public class PriceListNewController : Controller
    {
        private readonly IPriceListRepo _priceListRepo;
        private readonly ITermsPriceListRepo _termsPriceListRepo;
        private readonly IMainClinicRepo _mainClinicRepo;
        private readonly ISubClinicRepo _subClinicRepo;
        private readonly IServiceClinicRepo _serviceClinicRepo;

        public PriceListNewController(IPriceListRepo priceListRepo, ITermsPriceListRepo termsPriceListRepo, IMainClinicRepo mainClinicRepo, ISubClinicRepo subClinicRepo, IServiceClinicRepo serviceClinicRepo)
        {
            _priceListRepo = priceListRepo;
            _termsPriceListRepo = termsPriceListRepo;
            _mainClinicRepo = mainClinicRepo;
            _subClinicRepo = subClinicRepo;
            _serviceClinicRepo = serviceClinicRepo;
        }

        public async Task<IActionResult> Index()
        {
            List <PriceList> priceLists = await _priceListRepo.GetAllAsync();
            List <PriceListDetail> priceListDetail = await _termsPriceListRepo.GetAllPriceListDetails(1);
            List <SelectListItem> mainClinics = (await _mainClinicRepo.GetAllAsync()).Select(x => new SelectListItem { Value = x.ClinicId.ToString(),Text = x.ClinicDesc}).ToList();
            List <SelectListItem> subClinics = (await _subClinicRepo.GetAllAsync()).Select(x => new SelectListItem { Value = x.SClinicId.ToString(), Text = x.SClinicDesc }).ToList();
            List<SelectListItem> services = (await _serviceClinicRepo.GetAllAsync()).Select(x => new SelectListItem { Value = x.ServId.ToString(), Text = x.ServDesc }).ToList();
            PriceListDTO priceListDTO = new() { PriceList = priceLists ,PriceListDetail = priceListDetail,MainClinic = mainClinics , SubClinic = subClinics,Services = services };
            return View(priceListDTO);
        }
    }
}
