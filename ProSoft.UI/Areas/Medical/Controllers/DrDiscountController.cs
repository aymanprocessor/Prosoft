using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NetTopologySuite.Utilities;
using ProSoft.Core.Repositories.Medical.HospitalPatData;
using ProSoft.EF.DTOs.Medical.HospitalPatData;
using ProSoft.EF.IRepositories.Medical.HospitalPatData;
using ProSoft.EF.Models.Medical.HospitalPatData;
using ProSoft.UI.Global;
using System.Collections.Generic;

namespace ProSoft.UI.Areas.Medical.Controllers
{
    [Authorize]
    [Area(nameof(Medical))]
    public class DrDiscountController : Controller
    {
        private readonly IDrDiscountRepo _drDiscountRepo;
        private readonly IDoctorRepo _doctorRepo;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;



        public DrDiscountController(IDrDiscountRepo drDiscountRepo, ICurrentUserService currentUserService, IMapper mapper, IDoctorRepo doctorRepo)
        {
            _drDiscountRepo = drDiscountRepo;
            _currentUserService = currentUserService;
            _mapper = mapper;
            _doctorRepo = doctorRepo;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            ViewBag.BranchId = _currentUserService.BranchId;
            ViewBag.Doctors = await GetActiveDoctors();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(DrDiscountRequestDTO model)
        {
            ViewBag.Doctors = await GetActiveDoctors();
            if (!ModelState.IsValid)
            {

            return View(model);
            }

            DrDiscount drDiscount = _mapper.Map<DrDiscount>(model);
            await _drDiscountRepo.AddAsync(drDiscount);
            await _drDiscountRepo.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.Doctors = await GetActiveDoctors();

            var drDiscount = await _drDiscountRepo.GetByIdAsync(id);
            if (drDiscount == null) return NotFound("Dr Discount ID Not Found");

            DrDiscountRequestDTO drDiscountRequestDTO = _mapper.Map<DrDiscountRequestDTO>(drDiscount);
            return View(drDiscountRequestDTO);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id,DrDiscountRequestDTO model)
        {
            ViewBag.Doctors = await GetActiveDoctors();
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var drDiscount = await _drDiscountRepo.GetByIdAsync(id);
            _mapper.Map(model, drDiscount);

            await _drDiscountRepo.UpdateAsync(drDiscount);
            await _drDiscountRepo.SaveChangesAsync();


            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var drDiscount = await _drDiscountRepo.GetByIdAsync(id);
            if (drDiscount == null) return NotFound("Dr Discount ID Not Found");
            await _drDiscountRepo.DeleteAsync(drDiscount);
            await _drDiscountRepo.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> GetDoctorCommissions() {
            var doctorCommissions = await _drDiscountRepo.GetDoctorCommissions(); // Replace with your service/repository call

            var result = new
            {
                draw = 1,
                recordsTotal = doctorCommissions.Count(),       // Total number of records
                recordsFiltered = doctorCommissions.Count(),    // Total filtered records (for paging)
                data = doctorCommissions                        // Actual data to display
            };
            return Json(result);
        }

        private async Task<IEnumerable<SelectListItem>> GetActiveDoctors()
        {
            var doctorCommissions = await _drDiscountRepo.GetDoctorCommissions();
            return (await _doctorRepo.GetAllAsync()).Where(d => d.DrOnOff == 1 && !doctorCommissions.Any(dc => dc.DrId == d.DrId)).ToList().ToSelectListItem(d => d.DrDesc!, d => d.DrId.ToString());
        }
    }
}
