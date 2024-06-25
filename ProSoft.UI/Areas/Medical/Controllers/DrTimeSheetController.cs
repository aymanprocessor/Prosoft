using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProSoft.Core.Repositories.Medical.HospitalPatData;
using ProSoft.EF.DTOs.Medical.HospitalPatData;
using ProSoft.EF.DTOs.Treasury;
using ProSoft.EF.IRepositories.Medical.HospitalPatData;
using ProSoft.EF.Models.Medical.HospitalPatData;
using ProSoft.EF.Models.Treasury;

namespace ProSoft.UI.Areas.Medical.Controllers
{
    [Authorize]
    [Area("Medical")]
    public class DrTimeSheetController : Controller
    {
        private readonly IDrTimeSheetRepo _drTimeSheetRepo;
        private readonly IDoctorRepo _doctorRepo;
        private readonly IMapper _mapper;

        public DrTimeSheetController(IDrTimeSheetRepo drTimeSheetRepo, IDoctorRepo doctorRepo, IMapper mapper)
        {
            _drTimeSheetRepo = drTimeSheetRepo;
            _doctorRepo = doctorRepo;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            List<DoctorViewDTO> doctorDTOs = await _doctorRepo.GetAllDoctor();
            return View(doctorDTOs);
        }

        //Ajax In Index
        public async Task<IActionResult> GetAppointmentsForDr(int id)
        {
            List<DrTimeSheetDTO> drTimeSheetDTOs = await _drTimeSheetRepo
                .GetAppointmentsForDrAsync(id);
            return Json(drTimeSheetDTOs);
        }
        //Get add
        public async Task<IActionResult> Add_DrTimeSheet(int id)
        {
            var doctor= await _doctorRepo.GetDoctorByIdAsync(id);
            ViewBag.DrName = doctor.DrDesc;
            ViewBag.DrId = doctor.DrId;
            return View();
        }

        //Post add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add_DrTimeSheet(DrTimeSheetDTO drTimeSheetDTO)
        {
            if (ModelState.IsValid)
            {
                var branchId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "U_Branch_Id").Value);
                Drtimsheet drtimsheet = _mapper.Map<Drtimsheet>(drTimeSheetDTO);
                drtimsheet.BranchId = branchId;
                await _drTimeSheetRepo.AddAsync(drtimsheet);
                await _drTimeSheetRepo.SaveChangesAsync();
                return RedirectToAction("Index", "DrTimeSheet");
            }
            return View(drTimeSheetDTO);
        }
        //Get Edit
        public async Task<IActionResult> Edit_DrTimeSheet(int id, int drId)
        {
            Drtimsheet drtimsheet = await _drTimeSheetRepo.GetByIdAsync(id);
            DrTimeSheetDTO drTimeSheetDTO = _mapper.Map<DrTimeSheetDTO>(drtimsheet);
            var doctor = await _doctorRepo.GetDoctorByIdAsync(drId);
            ViewBag.DrName = doctor.DrDesc;
            return View(drTimeSheetDTO);
        }

        //Post Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit_DrTimeSheet(int id, DrTimeSheetDTO drTimeSheetDTO)
        {
            if (ModelState.IsValid)
            {
                Drtimsheet drtimsheet = await _drTimeSheetRepo.GetByIdAsync(id);
                _mapper.Map(drTimeSheetDTO, drtimsheet);

                await _drTimeSheetRepo.UpdateAsync(drtimsheet);
                await _drTimeSheetRepo.SaveChangesAsync();
                return RedirectToAction("Index", "DrTimeSheet");
            }
            return View(drTimeSheetDTO);
        }

        // Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete_DrTimeSheet(int id)
        {
            Drtimsheet drtimsheet = await _drTimeSheetRepo.GetByIdAsync(id);

            await _drTimeSheetRepo.DeleteAsync(drtimsheet);
            await _drTimeSheetRepo.SaveChangesAsync();
            return RedirectToAction("Index", "DrTimeSheet");
        }
    }
}
