using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProSoft.Core.Repositories.Medical.HospitalPatData;
using ProSoft.EF.DTOs.Medical.HospitalPatData;
using ProSoft.EF.IRepositories.Medical.HospitalPatData;
using ProSoft.EF.Models.Medical.HospitalPatData;

namespace ProSoft.UI.Areas.Medical.Controllers
{
    [Authorize]
    [Area("Medical")]
    public class MainClinicController : Controller
    {
        private readonly IMainClinicRepo _mainClinicRepo;
        private readonly IMapper _mapper;
        public MainClinicController(IMapper mapper, IMainClinicRepo mainClinicRep)
        {
            _mainClinicRepo = mainClinicRep;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            List<MainClinicViewDTO> mainClinicDTOs =await _mainClinicRepo.GetAllClinics();
            return View(mainClinicDTOs);
        }

        //Get Add
        public async Task<IActionResult> Add_MainClinic()
        {
            ViewBag.ClinicID= await _mainClinicRepo.GetNewIdAsync();
            MainClinicEditAddDTO mainClinicDTO= await _mainClinicRepo.GetEmptyClinicAsync();
            return View(mainClinicDTO);
        }

        //Post Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add_MainClinic(MainClinicEditAddDTO mainClinicDTO)
        {
            if (ModelState.IsValid)
            {

                await _mainClinicRepo.AddMainClinicAsync(mainClinicDTO);

                return RedirectToAction(nameof(Index));
            }
            return View(mainClinicDTO);
        }
        //Get Edit
        public async Task<IActionResult> Edit_MainClinic(int id)
        {
            MainClinicEditAddDTO mainClinicDTO =await _mainClinicRepo.GetMainClinicByIdAsync(id);
            return View(mainClinicDTO);
        }

        //Post Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_MainClinic(int id,MainClinicEditAddDTO mainClinicDTO)
        {
            if (ModelState.IsValid)
            {
                await _mainClinicRepo.EditMainClinicAsync(id,mainClinicDTO);
                return RedirectToAction(nameof(Index));
            }
            return View(mainClinicDTO);
        }

        // Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete_MainClinic(int id)
        {
            MainClinic mainClinic = await _mainClinicRepo.GetByIdAsync(id);

            await _mainClinicRepo.DeleteAsync(mainClinic);
            await _mainClinicRepo.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<JsonResult> GetMainClinics()
        {
            var mainClinics = await _mainClinicRepo.GetAllClinics();
            return Json(mainClinics);
        }


    }
}
