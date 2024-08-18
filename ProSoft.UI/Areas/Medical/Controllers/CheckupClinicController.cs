using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProSoft.EF.DTOs.Medical.HospitalPatData;
using ProSoft.EF.IRepositories.Medical.HospitalPatData;
using ProSoft.EF.Models.Medical.HospitalPatData;

namespace ProSoft.UI.Areas.Medical.Controllers
{
    [Area("Medical")]
    public class CheckupClinicController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ICheckupClinicRepo _checkupClinicRepo;

        public CheckupClinicController(IMapper mapper, ICheckupClinicRepo checkupClinicRepo)
        {
            _mapper = mapper;
            _checkupClinicRepo = checkupClinicRepo;
        }
        // Get Display
        public async Task<IActionResult> Index()
        {
            List<CheckupClinicDTO> checkupClinicDTOs = await _checkupClinicRepo.All();
            return View(checkupClinicDTOs);
        }
        // Get 
        [HttpGet]
        public async Task<IActionResult> Add_checkupClinic()
        {
            ViewBag._max = await _checkupClinicRepo.MaxCode();
            return View();
        }
        //////bost
        [HttpPost]
        public async Task<IActionResult> Add_checkupClinic(CheckupClinicDTO checkupClinicDTO)
        {
            ////==Mapper> IRepository
            if (ModelState.IsValid)
            {

                CheckupClinic checkupClinic = _mapper.Map<CheckupClinic>(checkupClinicDTO);
                await _checkupClinicRepo.AddAsync(checkupClinic);
                await _checkupClinicRepo.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(checkupClinicDTO);
        }

        [HttpGet]

        public async Task<IActionResult> Edit_checkupClinic(int id)
        {

            CheckupClinicDTO checkupClinicDTO = await _checkupClinicRepo.GetEditAsync(id);
            return View(checkupClinicDTO);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_checkupClinic(int id, CheckupClinicDTO checkupClinicDTO)
        {
            if (ModelState.IsValid)
            {
                CheckupClinic checkupClinic = await _checkupClinicRepo.GetByIdAsync(id);
                _mapper.Map(checkupClinicDTO, checkupClinic);

                await _checkupClinicRepo.UpdateAsync(checkupClinic);
                await _checkupClinicRepo.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(checkupClinicDTO);
        }

        // Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete_checkupClinic(int id)
        {
            CheckupClinic checkupClinic = await _checkupClinicRepo.GetByIdAsync(id);

            await _checkupClinicRepo.DeleteAsync(checkupClinic);
            await _checkupClinicRepo.SaveChangesAsync();
            return RedirectToAction("Index", "CheckupClinic");
        }
    }
}
